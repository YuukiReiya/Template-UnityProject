# Gitに関して
PRを作る際は頭に以下の文字を付け、タグの設定を行うこと。
>&emsp;バグ修正・リファクタリング<br>
【Bugfix】<br>
&emsp;機能追加や開発全般<br>
【Develop】<br>
&emsp;使用Assetの追加やProjectSetting、VSの設定変更など開発環境全般  <br>
【Environment】<br>
&emsp;ブランチマージ<br>
【Merge】<br>

コミットメッセージ
>上記のものに対して**小文字**で表記<br>
>【bugfix】<br>
>【develop】 <br>
>【environment】<br>
>【merge】

# 機能追加に関して
新機能やツール開発は専用のブランチを切って行うが、その際に管理しやすいように開発用のフォルダとして<br>
`Experimental(試験的/実験的)`
フォルダを用いる。

**フォルダ構成**<br>
Assets&nbsp;/<br>
┗Experimental&nbsp;/<br>
&emsp;┣ Prefabs&nbsp;/<br>
&emsp;┣ Resources&nbsp;/<br>
&emsp;┗ Scripts&nbsp;/<br>
機能の実装(マージ)時点では`Experimental`フォルダから移しておく←あくまで作業用とするため。

___
> 参考リンク<br>
>[Markdownを使う際に知っておくと便利なhtml](https://hacknote.jp/archives/45735/)<br>
>[ディレクトリ構成図を書くときに便利な記号](https://qiita.com/paty-fakename/items/c82ed27b4070feeceff6)<br>