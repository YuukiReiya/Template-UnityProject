@echo off

REM ���[�g�t�H���_
set root=%~dp0..\
explorer %root%


REM Unity�N��
:: �Ώۂ�Unity�ƃv���W�F�N�g�̎w��
set hubPath="C:\Program Files\Unity\Hub\Editor\"
set version=2019.4.1f1
set projectName=UnityProject

:: �p�X�ڍ�
set unityPath=%hubPath%%version%"\Editor\Unity.exe"
set projectPath=%~dp0..\%projectName%

start "" %unityPath% -projectPath "%projectPath%"

REM Visual Studio
:: �Ώۂ�Visual Studio�ƃv���W�F�N�g�̎w��
set vsPath="C:\Program Files (x86)\Microsoft Visual Studio\"
set version=2019
set vsPath=%vsPath%%version%\Community\Common7\IDE\devenv.exe
set slnPath=%projectPath%\%projectName%.sln
if exist %slnPath% (
    ::start %vsPath% %slnPath%
    echo %slnPath% 
) else (
    echo Unity�v���W�F�N�g�̃\�����[�V�������J���܂���ł����B
)


REM Visual Studio Code
::  VDCODE�̃C���X�g�[����
set vscPath="C:\Users\%USERNAME%\Documents\Microsoft VS Code\Code.exe" 
if exist %vscPath% (
    call %~dp0..\BuildEvent\Command.bat code %root%
)

REM SourceTree



REM Assets�t�H���_
set expPath=%projectPath%\Assets
if exist %expPath% (
    explorer %expPath% 
) else (
    echo Assets���J���܂���ł����B
)


REM �}�l�[�W�h�v���O�C��
set projectName=UnityDLL
set slnPath=%root%\%projectName%\%projectName%.sln
@echo %slnPath%
if exist %slnPath% (
    echo ManagedPluginProject:projectName
    start %vsPath% %slnPath%
) else (
    echo �}�l�[�W�h�v���O�C���̃\�����[�V�������J���܂���ł����B
)


REM �l�C�e�B�u�v���O�C��
set projectName=UnityDLLforCPP
set slnPath=%root%\%projectName%\%projectName%.sln
@echo %slnPath%
if exist %slnPath% (
    echo NativePluginProject:projectName 
    start %vsPath% %slnPath% 
) else (
    echo �l�C�e�B�u�v���O�C���̃\�����[�V�������J���܂���ł����B
)


REM Git-bash
set gitPath="C:\Program Files\Git\bin\bash.exe"

if exist %gitPath% (
    start cmd /c %~dp0..\BuildEvent\Command.bat %gitPath%
)

read
exit /b