@echo off
@setlocal enabledelayedexpansion
REM �����Ŏ󂯎�����R�}���h�����̂܂܎��s����o�b�`(ex vscode,gitbash�ȂǕʃo�b�`����ʃE�B���h�E�ŌĂяo���������Ȃ�)

::  ����������ݒ肳��Ă��Ȃ�
if "%~1"=="" (
    exit /b 
)

set buff=
for %%i in (%*) do (
    set buff=!buff!%%i 
)

REM �R�}���h���s
!buff!
exit /b