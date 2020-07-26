## Project
* UnityDLLforCPP
> C++のDLL(ネイティブプラグイン)
> ネームマングリング回避のために**C**で定義しているのでよしなに。
> Unityプロジェクト側でホットリロード対応のUtilクラスを用意(WindowsAPI使用)。
* ExecutionTestProject
> 上記DLL関数のテストプロジェクト。
> 簡単なエントリーポイントしか用意してないので適宜変えてください。

※ 一応DEBUGビルドでも問題なく書き出しと起動確認できるようにしているがUnity側では基本的にReleaseビルド推奨なのでデフォルトはReleaseにしといたほうがいい(DEBUGはあくまでトレース用にしてください。) ＋ <u>x64推奨</u>

## ビルドイベント
DEBUG/RELEASEどちらのビルドでもdllをシンボリックリンク(Plugins)先にコピーするようにしています。
プロジェクトの名前は変えてもいいけど、相対パス指定してるBuildEvent変えるとヤバそう…
言うまでもなくプロパティ書き換えてください。

## 注意
  `crtdbg.h No such file or directory error`が出る場合[こちら](https://social.msdn.microsoft.com/Forums/vstudio/ja-JP/0dcc6e61-5ee3-4b25-941d-9ded1cdd4387/quotcrtdbghquot-no-such-file-or-directory-error?forum=netfxbcl)を参考にして修正してください。  
  ※上記で直らない場合WindowsSDKが漏れていることが考えられるため、`VisualStudioInstaller`でコンポーネントを追加してください。
