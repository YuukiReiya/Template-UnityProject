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
REM <�t�@�C��>�V���{���b�N�����N�쐬
:: ����1:�쐬���郊���N�p�X�@����2:�����N�̎Q�Ɛ�
:File
mklink "%linkPath%" "%targetPath%"
pause
exit /b
endlocal

setlocal
REM <�f�B���N�g��>�V���{���b�N�����N�쐬
:: ����1:�쐬���郊���N�p�X�@����2:�����N�̎Q�Ɛ�
:Directory
mklink /D "%linkPath%" "%targetPath%"
exit /b
endlocal