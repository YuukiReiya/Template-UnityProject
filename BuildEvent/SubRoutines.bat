@echo off
goto %~1 %~2 %~3 %~4 %~5
exit /b

REM ����1:�폜����t�H���_/�t�@�C��
:Clean
if %~f2 == "" (
    goto ErrorLog %~0
)
if not exist %~f2 exit /b
::rd /s /q directory %~f2
del /s /q %~f2
exit /b

REM ����1:�R�s�[���t�@�C���@����2:�R�s�[��t�H���_
:Copy
if "%~f2"=="" goto ErrorLog %1 %~f2
if "%~f3"=="" goto ErrorLog %1 %~f2 %~f3
xcopy %~2 %~3 /y
exit /b

REM ����1:�R�s�[���t�@�C���@����2:�R�s�[��t�H���_
:eCopy
if "%~f2"=="" goto ErrorLog %1 %~f2
if "%~f3"=="" goto ErrorLog %1 %~f2 %~f3
xcopy %~f2 %~f3 /y /e
exit /b

:ErrorLog
if "%1"=="" (
    @echo �G���[�F�������w�肳��Ă��܂���B
    exit /b
)
if "%2"=="" (
    @echo �G���[�F%~1�̑�1�������w�肳��Ă��܂���B
    exit /b
)
if "%3"=="" (
    @echo �G���[�F%~1�̑�2�������w�肳��Ă��܂���B
    exit /b
)
if "%4"=="" (
    @echo �G���[�F%~1�̑�3�������w�肳��Ă��܂���B
    exit /b
)
if "%5"=="" (
    @echo �G���[�F%~1�̑�4�������w�肳��Ă��܂���B
    exit /b
)
 exit /b