@echo off
REM SIFT-JIS
chcp 932
REM utf-8
::chcp 65001

set linkPath=%~2
set targetPath=%~3

goto %~1
exit /b

setlocal
REM <ファイル>シンボリックリンク作成
:: 引数1:作成するリンクパス　引数2:リンクの参照先
:File
mklink "%linkPath%" "%targetPath%"
pause
exit /b
endlocal

setlocal
REM <ディレクトリ>シンボリックリンク作成
:: 引数1:作成するリンクパス　引数2:リンクの参照先
:Directory
mklink /D "%linkPath%" "%targetPath%"
exit /b
endlocal