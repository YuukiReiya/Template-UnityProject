@echo off
@chcp 932
@setlocal enabledelayedexpansion
@set readFile=%~dp0Plugins�̃p�X.txt
if not exist %readFile% (
    @echo �p�X�̓ǂݍ��ݐ�t�@�C��"Plugins�̃p�X.txt"��������܂���ł����B
    @echo �r���h���Ď��s���Ă��������B
    exit /b
)

REM "Plugins�̃p�X.txt"�����݂��邪���g����
for %%f in (%readFile%) do set fs=%%~zf
if %fs%==0 (
    @echo "Plugins�̃p�X.txt"�̒��g����ł����B
    @echo Unity�v���W�F�N�g��Pluguins�t�H���_�܂ł̃p�X����͂��A�Ď��s���Ă��������B
    exit /b
)

REM �t�@�C�����̏��߂̗L���ȃp�X������������V���{���b�N�����N���쐬����
for /f %%a in (%readFile%) do (
    if exist %%a (
        powershell start-process "%~dp0SymbolicLink.bat" "Directory,%~1,%%a" -verb runas -wait
        @echo "%%a"�̃V���{���b�N�����N��"%~1"�ɍ쐬���܂����B
        exit /b        
    )
)
exit /b