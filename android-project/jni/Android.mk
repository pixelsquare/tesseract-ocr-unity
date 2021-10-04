LOCAL_PATH := $(call my-dir)
TESSERACT_PATH := $(LOCAL_PATH)/libtesseract
LEPTONICA_PATH := $(LOCAL_PATH)/libleptonica
GLIB_PATH := $(LOCAL_PATH)/libglib

include $(call all-subdir-makefiles)