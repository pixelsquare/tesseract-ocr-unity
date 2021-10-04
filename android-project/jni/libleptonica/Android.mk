LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

LOCAL_MODULE := libleptonica

BLACKLIST_SRC_FILES := \

LEPTONICA_SRC_FILES := \
	$(wildcard $(LEPTONICA_PATH)/src/*.c) \
	
LOCAL_SRC_FILES := $(filter-out $(BLACKLIST_SRC_FILES), $(LEPTONICA_SRC_FILES))

LOCAL_C_INCLUDES := \
	$(LEPTONICA_PATH)/src \

LOCAL_CFLAGS := \
	-DHAVE_CONFIG_H \
	
LOCAL_LDLIBS += \
	-lz \
	-ljnigraphics \
	-llog
	
LOCAL_PRELINK_MODULE := false
# LOCAL_SHARED_LIBRARIES := libleptonica
LOCAL_DISABLE_FORMAT_STRING_CHECKS := true

include $(BUILD_SHARED_LIBRARY)