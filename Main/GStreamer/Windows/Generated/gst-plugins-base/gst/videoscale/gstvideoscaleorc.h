
/* autogenerated from E:\Development\OSSBuild\Main\GStreamer\Windows\.\..\Source\gst-plugins-base\gst\videoscale\gstvideoscaleorc.orc */

#ifndef _E__DEVELOPMENT_OSSBUILD_MAIN_GSTREAMER_WINDOWS______WINDOWS_GENERATED_GST_PLUGINS_BASE_GST_VIDEOSCALE_GSTVIDEOSCALEORC_H_
#define _E__DEVELOPMENT_OSSBUILD_MAIN_GSTREAMER_WINDOWS______WINDOWS_GENERATED_GST_PLUGINS_BASE_GST_VIDEOSCALE_GSTVIDEOSCALEORC_H_

#include <glib.h>

#ifdef __cplusplus
extern "C" {
#endif

void gst_videoscale_orc_init (void);



#ifndef _ORC_INTEGER_TYPEDEFS_
#define _ORC_INTEGER_TYPEDEFS_
#if defined(__STDC_VERSION__) && __STDC_VERSION__ >= 199901L
#include <stdint.h>
typedef int8_t orc_int8;
typedef int16_t orc_int16;
typedef int32_t orc_int32;
typedef int64_t orc_int64;
typedef uint8_t orc_uint8;
typedef uint16_t orc_uint16;
typedef uint32_t orc_uint32;
typedef uint64_t orc_uint64;
#define ORC_UINT64_C(x) UINT64_C(x)
#elif defined(_MSC_VER)
typedef signed __int8 orc_int8;
typedef signed __int16 orc_int16;
typedef signed __int32 orc_int32;
typedef signed __int64 orc_int64;
typedef unsigned __int8 orc_uint8;
typedef unsigned __int16 orc_uint16;
typedef unsigned __int32 orc_uint32;
typedef unsigned __int64 orc_uint64;
#define ORC_UINT64_C(x) (x##Ui64)
#else
#include <limits.h>
typedef signed char orc_int8;
typedef short orc_int16;
typedef int orc_int32;
typedef unsigned char orc_uint8;
typedef unsigned short orc_uint16;
typedef unsigned int orc_uint32;
#if INT_MAX == LONG_MAX
typedef long long orc_int64;
typedef unsigned long long orc_uint64;
#define ORC_UINT64_C(x) (x##ULL)
#else
typedef long orc_int64;
typedef unsigned long orc_uint64;
#define ORC_UINT64_C(x) (x##UL)
#endif
#endif
typedef union { orc_int16 i; orc_int8 x2[2]; } orc_union16;
typedef union { orc_int32 i; float f; orc_int16 x2[2]; orc_int8 x4[4]; } orc_union32;
typedef union { orc_int64 i; double f; orc_int32 x2[2]; float x2f[2]; orc_int16 x4[4]; } orc_union64;
#endif
void orc_merge_linear_u8 (orc_uint8 * d1, const orc_uint8 * s1, const orc_uint8 * s2, int p1, int n);
void orc_merge_linear_u16 (orc_uint16 * d1, const orc_uint16 * s1, const orc_uint16 * s2, int p1, int p2, int n);
void orc_splat_u16 (orc_uint16 * d1, int p1, int n);
void orc_splat_u32 (orc_uint32 * d1, int p1, int n);
void orc_downsample_u8 (guint8 * d1, const guint8 * s1, int n);
void orc_downsample_u16 (guint16 * d1, const guint16 * s1, int n);
void gst_videoscale_orc_downsample_u32 (guint8 * d1, const guint8 * s1, int n);
void gst_videoscale_orc_downsample_yuyv (guint8 * d1, const guint8 * s1, int n);
void gst_videoscale_orc_resample_nearest_u8 (guint8 * d1, const guint8 * s1, int p1, int p2, int n);
void gst_videoscale_orc_resample_bilinear_u8 (guint8 * d1, const guint8 * s1, int p1, int p2, int n);
void gst_videoscale_orc_resample_nearest_u32 (guint8 * d1, const guint8 * s1, int p1, int p2, int n);
void gst_videoscale_orc_resample_bilinear_u32 (guint8 * d1, const guint8 * s1, int p1, int p2, int n);
void gst_videoscale_orc_resample_merge_bilinear_u32 (guint8 * d1, guint8 * d2, const guint8 * s1, const guint8 * s2, int p1, int p2, int p3, int n);
void gst_videoscale_orc_merge_bicubic_u8 (guint8 * d1, const guint8 * s1, const guint8 * s2, const guint8 * s3, const guint8 * s4, int p1, int p2, int p3, int p4, int n);

#ifdef __cplusplus
}
#endif

#endif

