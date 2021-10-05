@ECHO OFF
SETLOCAL

SET APK_NAME="tesseract.apk"
SET PACKAGE_NAME="com.PixelSquare.tesseractocr"

REM SET /p INSTALLED_PACKAGE_NAME=adb shell pm list packages %PACKAGE_NAME%
REM ECHO %INSTALLED_PACKAGE_NAME%
REM IF adb shell pm list packages %PACKAGE_NAME% == "" GOTO InstallPackage

:UninstallPackage
ECHO "Uninstalling existing application ..."
adb uninstall %PACKAGE_NAME%
ECHO.

:InstallPackage
ECHO "Installing apk ..."
adb install %APK_NAME%
ECHO.

ECHO.
ECHO DONE!

PAUSE