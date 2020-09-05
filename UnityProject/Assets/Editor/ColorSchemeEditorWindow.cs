//デフォルトリソースの位置と読み込み
//https://kan-kikuchi.hatenablog.com/entry/GetBuiltinResource
using UnityEngine;
using UnityEditor;
using DLL;
using System.Linq;
using System;

namespace Exception.Window
{
    //  配色情報を表示する拡張ウィンドウ
    public class ColorSchemeEditorWindow : EditorWindow
    {
        private Vector2 scrollPos = Vector2.zero;
        private Color basicColor = Color.red;
        private float offset = 1.0f;
        private int formatIndex = 0;

        #region GUI_Style
        private GUIStyle style;
        private Color ContentColor
        {
            get
            {
                return Color.white;
            }
        }
        private Color BackColor
        {
            get
            {
                return Color.grey;
            }
        }
        private Color HelpBoxBackgroundColor
        {
            get
            {
                return Color.white;
            }
        }
        #endregion

        #region 定数/読み取り専用
        private readonly (int index, int bitFormat, string disStr)[] FormatList =
        {
            (0,UnityAPIExpantions.Format24,"フルカラー" ),
            (1,UnityAPIExpantions.Format16,"ハイカラー" ),
        };

        // TODO:汚いからどうにかしたい
        // とりまEditorだけだし全部直書きする。
        private const string WndTitle = "配色情報";
        private const string OffsetHelpMessage =
            "ダイアード配色等で色の近づき具合を指定するバイアス値。" +
            "\n0に近いほど基調色に近い色になり、" +
            "\n0に遠いほど基調色から離れた色になる。";
        private const string ComplementaryColorHelpMessage =
            "色相環上で基調色の反対に位置するもの。" +
            "\nお互いの色を引き立て、メインカラーを引き立てるのに効果的。";
        private const string AnalogousColorsHelpMessage =
            "色相環上で隣り合う色。" +
            "\n色合いが近いためグラデーションが作りやすく調和がとれた配色。" +
            "\n視覚的に柔らかい雰囲気を作るのに向いている。";
        private const 
            string TriadColorsHelpMessage =
            "色相環上で正三角形で3等分した位置にあたる色。" +
            "\nバランスが良く明快で安定感のある配色になる。" +
            "\n高彩度の3色で賑やかな印象を与えやすい。";
        private const string SplitComplementaryColorsHelpMessage = 
            "補色の両隣に位置する2色。" +
            "\n補色の関係ほど奇抜にならず、調和がとれた印象になり、" +
            "\nコントラストを少し柔らかく見せることができる。";
        private const string SquareColorsHelpMessage =
            "色相環上で正方形に4等分した位置にあたる色。" +
            "\n2組の補色の組み合わせなので、カラフルで変化に富む。";
        private const string TetradColorsHelpMessage = 
            "色相環上で長方形に4分した位置にある4色を組み合わせる方法で「レクタンギュラー」という。" +
            "\n2組の補色の組み合わせなので、カラフルで賑やかな印象になります。" +
            "\nスクエアカラーと違い閾値(バイアス)を調整することで細かく確認できる。";
        private const string PentardColorHelpMessages =
            "色相環上で正五角形で5等分した位置にあたる色。" +
            "\n色数が多いので、賑やかな印象になる。";
        private const string HexardColorsHelpMessage =
            "色相環上で正六角形で6等分した位置にあたる色。" +
            "\n赤系、橙系、黄系、緑系、青系、紫系から六つの色をまんべんなく含むため、変化にとんだ印象になる。";
        #endregion

        [MenuItem("拡張ウィンドウ/配色")]
        private static void Open()
        {
            var wnd = GetWindow<ColorSchemeEditorWindow>(WndTitle);
        }

        private void OnEnable()
        {
            //  初期化
            SetupStyles();
            var min = minSize;
            min.x = 480;
            min.y = 360;
            minSize = min;
        }
        
        private void OnGUI()
        {
            DrawSchemeArea("設定", style, DrawBaseConfig, ContentColor, BackColor);
            using (var view = new GUILayout.ScrollViewScope(scrollPos))
            {
                scrollPos = view.scrollPosition;
                using (new GUILayout.VerticalScope(GUI.skin.box))
                {
                    DrawSchemeArea("補色", style, DrawComplementaryColor, ContentColor, BackColor);
                    DrawSchemeArea("類似色(アナロガス配色)", style, DrawAnalogousColors, ContentColor, BackColor);
                }
                using (new GUILayout.HorizontalScope(GUI.skin.box))
                {
                    DrawSchemeArea("トライアド配色", style, DrawTriadColors, ContentColor, BackColor);
                    DrawSchemeArea("スプリット・コンプリメンタリー配色", style, DrawSplitComplementaryColors, ContentColor, BackColor);
                }
                using (new GUILayout.HorizontalScope(GUI.skin.box))
                {
                    DrawSchemeArea("スクエア配色", style, DrawSquareColors, ContentColor, BackColor);
                    DrawSchemeArea("テトラード配色", style, DrawTetradColors, ContentColor, BackColor);
                }
                using (new GUILayout.HorizontalScope(GUI.skin.box))
                {
                    DrawSchemeArea("ペンタード配色", style, DrawPentardColors, ContentColor, BackColor);
                    DrawSchemeArea("ヘクサード配色", style, DrawHexardColors, ContentColor, BackColor);
                }
            }
        }

        private void SetupStyles()
        {
            style = new GUIStyle();
            style.normal.textColor = Color.white;
        }

        private void DrawOffset()
        {
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                var limit = FormatList[formatIndex].bitFormat / 2;
                offset = EditorGUILayout.FloatField("閾値",
                    //  MEMO:オフセットの範囲制限
                    //  -1 = 1で要素①と②に表示される色が逆転するだけ。
                    //  バイアス値がカラービット数/2を超えたときに表現域が被る。
                    //  例)24bit　
                    //      バイアス値:11 要素① = バイアス値:13 要素② 
                    //      (同様に要素② = 要素①も満たし完全に逆転しているだけ。)
                    //  TODO:
                    //  とりまこのまま実装するけど気になったら編集する。
                    Mathf.Clamp(offset, -limit, limit)
                    ); ;
            }
            DrawInfoBox(OffsetHelpMessage);
        }
        private void DrawBaseConfig()
        {
            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {

                using (new GUILayout.VerticalScope())
                {
                    //  カラーフォーマット
                    DrawSchemeArea("カラーフォーマット", style, () =>
                    {
                        formatIndex = EditorGUILayout.Popup(formatIndex, FormatList
                            .Select(i => i.disStr).ToArray());
                    }
                    , ContentColor, BackColor);
                    //  オフセット値
                    DrawSchemeArea("オフセット", style, DrawOffset, ContentColor, BackColor);
                }

                //  基調色
                DrawSchemeArea("基調色", style, DrawBasicColor, ContentColor, BackColor);
            }
        }

        private void DrawBasicColor()
        {
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                basicColor = EditorGUILayout.ColorField(basicColor);
            }
        }

        private void DrawComplementaryColor()
        {
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.ColorField(basicColor.GetComplementaryColor());
            }
            DrawInfoBox(ComplementaryColorHelpMessage);
        }

        private void DrawAnalogousColors()
        {
            DrawColorsElement(basicColor.GetAnalogousColors(offset, FormatList[formatIndex].bitFormat));
            DrawInfoBox(AnalogousColorsHelpMessage);
        }
        
        private void DrawTriadColors()
        {
            DrawColorsElement(basicColor.GetTriadColors());
            DrawInfoBox(TriadColorsHelpMessage);
        }

        private void DrawSplitComplementaryColors()
        {
            DrawColorsElement(basicColor.GetSplitComplementaryColors(offset, FormatList[formatIndex].bitFormat));
            DrawInfoBox(SplitComplementaryColorsHelpMessage);
        }

        private void DrawSquareColors()
        {
            DrawColorsElement(basicColor.GetSquareColors());
            DrawInfoBox(SquareColorsHelpMessage);
        }

        private void DrawTetradColors()
        {
            DrawColorsElement(basicColor.GetTetradColors(offset, FormatList[formatIndex].bitFormat));
            DrawInfoBox(TetradColorsHelpMessage);
        }

        private void DrawPentardColors()
        {
            DrawColorsElement(basicColor.GetPentardColors());
            DrawInfoBox(PentardColorHelpMessages);
        }

        private void DrawHexardColors()
        {
            DrawColorsElement(basicColor.GetHexardColors());
            DrawInfoBox(HexardColorsHelpMessage);
        }

        private void DrawSchemeArea(string schemeLabel, GUIStyle style, Action drawEvent, Color contentColor, Color backColor)
        {
            var defaultBackColor = GUI.backgroundColor;
            var defaultContentColor = GUI.contentColor;
            GUI.contentColor = contentColor;
            GUI.backgroundColor = backColor;
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                //  ヘッダー
                using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
                {
                    EditorGUILayout.LabelField(schemeLabel, style);
                }
                GUI.backgroundColor = defaultBackColor;
                GUI.contentColor = defaultContentColor;

                //  登録された描画イベント
                drawEvent?.Invoke();
            }
        }

        private void DrawColorsElement(Color[]colors)
        {
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                for (int i = 0; i < colors.Length; ++i)
                {
                    EditorGUILayout.ColorField("要素" + (i + 1), colors[i]);
                }
            }
        }

        //  HelpBoxのラッパー、前後で表示する際に色を保持する
        private void HelpBox(string message, MessageType type, Color backgroundColor, Color contentColor)
        {
            var defaultBackColor = GUI.backgroundColor;
            var defaultContentColor = GUI.contentColor;
            GUI.contentColor = contentColor;
            GUI.backgroundColor = backgroundColor;
            EditorGUILayout.HelpBox(message, type);
            GUI.contentColor = defaultContentColor;
            GUI.backgroundColor = defaultBackColor;
        }

        private void HelpBox(string message, MessageType type, Color backgroundColor)
        {
            HelpBox(message, type, backgroundColor, GUI.contentColor);
        }

        private void DrawInfoBox(string message)
        {
            HelpBox(message, MessageType.Info, HelpBoxBackgroundColor);
        }
    }
}