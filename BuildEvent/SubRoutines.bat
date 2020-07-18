@echo off
goto %~1 %~2 %~3 %~4 %~5
exit /b

REM 引数1:削除するフォルダ/ファイル
:Clean
if %~f2 == "" (
    goto ErrorLog %~0
)
if not exist %~f2 exit /b
::rd /s /q directory %~f2
del /s /q %~f2
exit /b

REM 引数1:コピー元ファイル　引数2:コピー先フォルダ
:Copy
if "%~f2"=="" goto ErrorLog %1 %~f2
if "%~f3"=="" goto ErrorLog %1 %~f2 %~f3
xcopy %~2 %~3 /y
exit /b

REM 引数1:コピー元ファイル　引数2:コピー先フォルダ
:eCopy
if "%~f2"=="" goto ErrorLog %1 %~f2
if "%~f3"=="" goto ErrorLog %1 %~f2 %~f3
xcopy %~f2 %~f3 /y /e
exit /b

:ErrorLog
if "%1"=="" (
    @echo エラー：引数が指定されていません。
    exit /b
)
if "%2"=="" (
    @echo エラー：%~1の第1引数が指定されていません。
    exit /b
)
if "%3"=="" (
    @echo エラー：%~1の第2引数が指定されていません。
    exit /b
)
if "%4"=="" (
    @echo エラー：%~1の第3引数が指定されていません。
    exit /b
)
if "%5"=="" (
    @echo エラー：%~1の第4引数が指定されていません。
    exit /b
)
 exit /b