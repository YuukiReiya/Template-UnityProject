using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DLL
{
    // 配色パターンを求める。
    //  対応フォーマット:Colorクラス24ビットフルカラー[RGB=0～255≒0.0～1.0=2^8 + A=0～255≒0.0～1.0=2^8 = 各8bit * 3色 ＋ Aの透過域]
    public static class UnityAPIExpantions
    {
        #region private
        private const float ColorMaxRange = 1.0f;
        private const int ComplementaryColorCount = 2;
        private const int TriadColorCount = 3;
        private const int AnalogousColorCount = 3;
        private const int SquareColorCount = 4;
        private const int PentardColorCount = 5;
        private const int HexardColorCount = 6;
        #endregion
        #region public
        //  TODO:
        //  カラー表現が増えるようなら列挙体も考慮する
        //  今はスマホ用のハイカラーとPC用にフルカラーで十分
        public const int Format24 = 24;//24bitカラー表現(フルカラー)
        public const int Format16 = 16;//16bitカラー表現(ハイカラー)
        #endregion

        //  補色(ダイアード配色)を求める
        public static Color GetComplementaryColor(this Color color)
        {
            return GetColorScheme(color, ColorMaxRange / ComplementaryColorCount);
        }

        //  トライアド配色を求める
        public static Color[] GetTriadColors(this Color color)
        {
            //  ベースカラーを除いた2色を求める
            Color[] ret = new Color[TriadColorCount - 1];
            ret[0] = GetColorScheme(color, ColorMaxRange / TriadColorCount);
            ret[1] = GetColorScheme(ret[0], ColorMaxRange / TriadColorCount);
            return ret;
        }

        //  類似(アナロガス)配色を求める
        //  16bitカラー(ハイカラー)を求めたい場合は明示的に引数を変える
        //  offset == bitFormat の時 → offset * 1.0f/offset = 1.0f を満たし変化しない。
        public static Color[] GetAnalogousColors(this Color color, float offset, int bitFormat = Format24)
        {
            //  ベースカラーを除いた2色を求める
            Color[] ret = new Color[AnalogousColorCount - 1];

            //色相環(0.0～1.0) ÷ カラーフォーマット 
            //例)24bit:1.0f ÷ RGB3種類 × (各8ビット)
            //例)16bit:1.0f ÷ RGBA4種類 × (各4ビット)
            float bias = 1.0f / bitFormat;//   フォーマットに応じたバイアスを求める
            ret[0] = GetColorScheme(color, offset * bias);
            ret[1] = GetColorScheme(color, -offset * bias);
            return ret;
        }

        //  スプリット・コンプリメンタリー配色
        //  補色の類似配色を求める
        //  要素数はアナロガス配色と同様
        public static Color[] GetSplitComplementaryColors(this Color color, float offset, int bitFormat = Format24)
        {
            return GetAnalogousColors(GetComplementaryColor(color), offset, bitFormat);
        }

        //  テトラード配色(正方形)
        public static Color[] GetSquareColors(this Color color)
        {
            Color[] ret = new Color[SquareColorCount - 1];
            ret[0] = GetColorScheme(color, ColorMaxRange / SquareColorCount);
            for (int i = 1; i < ret.Length; ++i)
            {
                ret[i] = GetColorScheme(ret[i - 1], ColorMaxRange / SquareColorCount);

            }
            return ret;
        }

        //  テトラード配色(長方形)
        public static Color[] GetTetradColors(this Color color, float offset, int bitFormat = Format24)
        {
            Color[] ret = new Color[SquareColorCount - 1];
            //  バイアスの算出
            float bias = 1.0f / bitFormat;
            ret[0] = GetColorScheme(color, offset * bias);
            ret[1] = GetComplementaryColor(color);
            ret[2] = GetColorScheme(ret[1], offset * bias);
            return ret;
        }

        //  ペンタード配色(正五角形)
        //  トライアド + 白黒 で表した5色も存在するが別途トライアド配色を求めて手動で求められるので関数は用意しない
        public static Color[] GetPentardColors(this Color color)
        {
            Color[] ret = new Color[PentardColorCount - 1];
            ret[0] = GetColorScheme(color, ColorMaxRange / PentardColorCount);
            for (int i = 1; i < ret.Length; ++i)
            {
                ret[i] = GetColorScheme(ret[i - 1], ColorMaxRange / PentardColorCount);
            }
            return ret;
        }

        //  ヘクサード配色(正六角形)
        //  テトラード + 白黒 で表した5色も存在するが別途テトラード配色を求めて手動で求められるので関数は用意しない
        public static Color[] GetHexardColors(this Color color)
        {
            Color[] ret = new Color[HexardColorCount - 1];
            ret[0] = GetColorScheme(color, ColorMaxRange / HexardColorCount);
            for (int i = 1; i < ret.Length; ++i)
            {
                ret[i] = GetColorScheme(ret[i - 1], ColorMaxRange / HexardColorCount);
            }
            return ret;
        }

        //  オフセット分ずらした配色を求める
        private static Color GetColorScheme(Color basic,float offset)
        {
            float h, s, v;
            Color.RGBToHSV(basic, out h, out s, out v);
            h += offset;
            //  色相環で1周していたら補正する(範囲:0.0～1.0)
            if (h > ColorMaxRange)
            {
                h -= ColorMaxRange;
            }

            return Color.HSVToRGB(h, s, v);
        }

    }
}
