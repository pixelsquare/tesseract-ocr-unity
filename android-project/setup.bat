@echo off
setlocal

SET SW_PROJ_ID=128446
SET TESSERACT_PROJ_PATH=%~dp0..\tesseract

SET TESSERACT_SRC_PATH=%TESSERACT_PROJ_PATH%\src
SET TESSERACT_INCLUDE_PATH=%TESSERACT_PROJ_PATH%\include

XCOPY /S /E /I /Y %TESSERACT_SRC_PATH% %~dp0jni\libtesseract\src
XCOPY /S /E /I /Y %TESSERACT_INCLUDE_PATH% %~dp0jni\libtesseract\include

:: This will produce the wrong version. The project has to be built first via cmake to get the proper version.h
REM REN %~dp0jni\libtesseract\includes\tesseract\version.h.in "version.h"

SET LEPTONICA_PROJ_PATH=%USERPROFILE%\.sw\storage\pkg\73\b8\9775\src\sdir
SET LEPTONICA_SRC_PATH=%LEPTONICA_PROJ_PATH%\src

XCOPY /S /E /I /Y %LEPTONICA_SRC_PATH% %~dp0jni\libleptonica\src
XCOPY /S /E /I /Y %LEPTONICA_PROJ_PATH%\..\..\obj\bld\%SW_PROJ_ID%\bd %~dp0jni\libleptonica\src

SET PANGO_PROJ_PATH=%USERPROFILE%\.sw\storage\pkg\27\5d\65e6\src\sdir
SET PANGO_SRC_PATH=%PANGO_PROJ_PATH%\pango

XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-layout.c %~dp0jni\libtesseract\src\training\pango
XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-layout.h %~dp0jni\libtesseract\src\training\pango

XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-attributes.c %~dp0jni\libtesseract\src\training\pango
XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-attributes.h %~dp0jni\libtesseract\src\training\pango

XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-font.c %~dp0jni\libtesseract\src\training\pango
XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-font.h %~dp0jni\libtesseract\src\training\pango

XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-coverage.c %~dp0jni\libtesseract\src\training\pango
XCOPY /S /E /I /Y %PANGO_SRC_PATH%\pango-coverage.h %~dp0jni\libtesseract\src\training\pango

SET GLIB_PROJ_PATH=%USERPROFILE%\.sw\storage\pkg\1b\de\9e91\src\sdir
SET GLIB_GOBBJECT_PROJ_PATH=%USERPROFILE%\.sw\storage\pkg\8e\32\0cfb\src\sdir

XCOPY /S /E /I /Y %GLIB_PROJ_PATH%\glib %~dp0jni\libglib\src\glib
XCOPY /S /E /I /Y %GLIB_PROJ_PATH%\..\..\obj\bld\%SW_PROJ_ID%\bd %~dp0jni\libglib\src\glib
XCOPY /S /E /I /Y %GLIB_PROJ_PATH%\..\..\obj\bld\%SW_PROJ_ID%\bdp %~dp0jni\libglib\src\glib

XCOPY /S /E /I /Y %GLIB_GOBBJECT_PROJ_PATH%\glib %~dp0jni\libglib\src\glib
XCOPY /S /E /I /Y %GLIB_GOBBJECT_PROJ_PATH%\gobject %~dp0jni\libglib\src\gobject

XCOPY /S /E /I /Y %GLIB_GOBBJECT_PROJ_PATH%\..\..\obj\bld\%SW_PROJ_ID%\bd\gobject %~dp0jni\libglib\src\gobject
XCOPY /S /E /I /Y %GLIB_GOBBJECT_PROJ_PATH%\..\..\obj\bld\%SW_PROJ_ID%\bdp %~dp0jni\libglib\src\gobject

pause