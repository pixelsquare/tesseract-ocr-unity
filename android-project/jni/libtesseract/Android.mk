LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

LOCAL_MODULE := libtesseract50

BLACKLIST_SRC_FILES := \

TESSERACT_SRC_FILES := \
	$(wildcard $(TESSERACT_PATH)/src/api/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/arch/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/ccmain/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/ccstruct/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/ccutil/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/classify/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/cutil/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/dict/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/lstm/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/opencl/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/textord/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/viewer/*.cpp) \
	$(wildcard $(TESSERACT_PATH)/src/wordrec/*.cpp) \
	
LOCAL_SRC_FILES := $(filter-out $(BLACKLIST_SRC_FILES), $(TESSERACT_SRC_FILES))

LOCAL_C_INCLUDES := \
	$(TESSERACT_PATH)/include \
	$(TESSERACT_PATH)/src/api \
	$(TESSERACT_PATH)/src/arch \
	$(TESSERACT_PATH)/src/ccmain \
	$(TESSERACT_PATH)/src/ccstruct \
	$(TESSERACT_PATH)/src/ccutil \
	$(TESSERACT_PATH)/src/classify \
	$(TESSERACT_PATH)/src/cutil \
	$(TESSERACT_PATH)/src/dict \
	$(TESSERACT_PATH)/src/lstm \
	$(TESSERACT_PATH)/src/opencl \
	$(TESSERACT_PATH)/src/textord \
	$(TESSERACT_PATH)/src/viewer \
	$(TESSERACT_PATH)/src/wordrec \
	$(LEPTONICA_PATH)/src \
	$(GLIB_PATH)/src \
	$(GLIB_PATH)/src/glib \

LOCAL_CFLAGS := \
	-DGRAPHICS_DISABLED \
	--std=c++17 \
	-DUSE_STD_NAMESPACE \
	-D'VERSION="Android"'\
	-include ctype.h \
	-include unistd.h \
	-fpermissive \
	-Wno-deprecated \
	-Wno-shift-negative-value \
	-D_GLIBCXX_PERMIT_BACKWARD_HASH \
	
LOCAL_LDLIBS += \
	-ljnigraphics \
	-llog \
	# -latomic \
	
LOCAL_SHORT_COMMANDS := true
LOCAL_PRELINK_MODULE := false
LOCAL_SHARED_LIBRARIES := libleptonica
LOCAL_DISABLE_FORMAT_STRING_CHECKS := true

include $(BUILD_SHARED_LIBRARY)