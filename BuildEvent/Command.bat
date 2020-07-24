@echo off
@setlocal enabledelayedexpansion
REM 引数で受け取ったコマンドをそのまま実行するバッチ(ex vscode,gitbashなど別バッチから別ウィンドウで呼び出したい時など)

::  引数が一つも設定されていない
if "%~1"=="" (
    exit /b 
)

set buff=
for %%i in (%*) do (
    set buff=!buff!%%i 
)

REM コマンド実行
!buff!
exit /b