@echo off
@chcp 932
@setlocal enabledelayedexpansion
@set readFile=%~dp0Pluginsのパス.txt
if not exist %readFile% (
    @echo パスの読み込み先ファイル"Pluginsのパス.txt"が見つかりませんでした。
    @echo ビルドを再試行してください。
    exit /b
)

REM "Pluginsのパス.txt"が存在するが中身が空
for %%f in (%readFile%) do set fs=%%~zf
if %fs%==0 (
    @echo "Pluginsのパス.txt"の中身が空でした。
    @echo UnityプロジェクトのPluguinsフォルダまでのパスを入力し、再試行してください。
    exit /b
)

REM ファイル内の初めの有効なパスが見つかったらシンボリックリンクを作成する
for /f %%a in (%readFile%) do (
    if exist %%a (
        powershell start-process "%~dp0SymbolicLink.bat" "Directory,%~1,%%a" -verb runas -wait
        @echo "%%a"のシンボリックリンクを"%~1"に作成しました。
        exit /b        
    )
)
exit /b