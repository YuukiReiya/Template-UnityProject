@echo off

REM ルートフォルダ
set root=%~dp0..\
explorer %root%


REM Unity起動
:: 対象のUnityとプロジェクトの指定
set hubPath="C:\Program Files\Unity\Hub\Editor\"
set version=2019.4.1f1
set projectName=UnityProject

:: パス接合
set unityPath=%hubPath%%version%"\Editor\Unity.exe"
set projectPath=%~dp0..\%projectName%

start "" %unityPath% -projectPath "%projectPath%"

REM Visual Studio
:: 対象のVisual Studioとプロジェクトの指定
set vsPath="C:\Program Files (x86)\Microsoft Visual Studio\"
set version=2019
set vsPath=%vsPath%%version%\Community\Common7\IDE\devenv.exe
set slnPath=%projectPath%\%projectName%.sln
if exist %slnPath% (
    ::start %vsPath% %slnPath%
    echo %slnPath% 
) else (
    echo Unityプロジェクトのソリューションを開けませんでした。
)


REM Visual Studio Code
::  VDCODEのインストール先
set vscPath="C:\Users\%USERNAME%\Documents\Microsoft VS Code\Code.exe" 
if exist %vscPath% (
    call %~dp0..\BuildEvent\Command.bat code %root%
)

REM SourceTree



REM Assetsフォルダ
set expPath=%projectPath%\Assets
if exist %expPath% (
    explorer %expPath% 
) else (
    echo Assetsを開けませんでした。
)


REM マネージドプラグイン
set projectName=UnityDLL
set slnPath=%root%\%projectName%\%projectName%.sln
@echo %slnPath%
if exist %slnPath% (
    echo ManagedPluginProject:projectName
    start %vsPath% %slnPath%
) else (
    echo マネージドプラグインのソリューションを開けませんでした。
)


REM ネイティブプラグイン
set projectName=UnityDLLforCPP
set slnPath=%root%\%projectName%\%projectName%.sln
@echo %slnPath%
if exist %slnPath% (
    echo NativePluginProject:projectName 
    start %vsPath% %slnPath% 
) else (
    echo ネイティブプラグインのソリューションを開けませんでした。
)


REM Git-bash
set gitPath="C:\Program Files\Git\bin\bash.exe"

if exist %gitPath% (
    start cmd /c %~dp0..\BuildEvent\Command.bat %gitPath%
)

read
exit /b