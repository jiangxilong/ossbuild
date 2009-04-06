
#ifdef HAVE_CONFIG_H
#include "config.h"
#endif

#include <schroedinger/schro.h>
#include <schroedinger/schromotion.h>
#include <schroedinger/schrodebug.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define OIL_ENABLE_UNSTABLE_API
#include <liboil/liboilprofile.h>

void
schro_frame_data_clear (SchroFrameData *fd)
{
  int i;
  for(i=0;i<fd->height;i++){
    memset (SCHRO_FRAME_DATA_GET_LINE (fd, i), 0, fd->width);
  }

}

void
schro_frame_clear (SchroFrame *frame)
{
  schro_frame_data_clear (frame->components + 0);
  schro_frame_data_clear (frame->components + 1);
  schro_frame_data_clear (frame->components + 2);
}

int
main (int argc, char *argv[])
{
  SchroFrame *dest;
  SchroFrame *ref;
  SchroUpsampledFrame *uref;
  SchroParams params;
  SchroVideoFormat video_format;
  SchroMotionVector *motion_vectors;
  int i;
  int j;
  OilProfile prof;
  double ave, std;

  schro_init();

  memset (&video_format, 0, sizeof(video_format));
  memset (&params, 0, sizeof(params));

  schro_video_format_set_std_video_format (&video_format,
      SCHRO_VIDEO_FORMAT_CUSTOM);
  video_format.width = 720;
  video_format.height = 480;
  video_format.chroma_format = SCHRO_CHROMA_420;
  schro_video_format_validate (&video_format);

  params.video_format = &video_format;
  params.xbsep_luma = 8;
  params.ybsep_luma = 8;
  params.xblen_luma = 12;
  params.yblen_luma = 12;

  schro_params_calculate_mc_sizes(&params);

  dest = schro_frame_new_and_alloc (NULL, SCHRO_FRAME_FORMAT_S16_420,
      video_format.width, video_format.height);
  schro_frame_clear(dest);

  ref = schro_frame_new_and_alloc_extended (NULL, SCHRO_FRAME_FORMAT_U8_420,
      video_format.width, video_format.height, 32);
  schro_frame_clear(ref);

  uref = schro_upsampled_frame_new (ref);

  schro_upsampled_frame_upsample (uref);

  motion_vectors = malloc(sizeof(SchroMotionVector) *
      params.x_num_blocks * params.y_num_blocks);
  memset (motion_vectors, 0, sizeof(SchroMotionVector) *
      params.x_num_blocks * params.y_num_blocks);
  
  printf("sizeof(SchroMotionVector) = %lu\n",(unsigned long) sizeof(SchroMotionVector));
  printf("num blocks %d x %d\n", params.x_num_blocks, params.y_num_blocks);
  for(i=0;i<params.x_num_blocks*params.y_num_blocks;i++){
    motion_vectors[i].dx[0] = 0;
    motion_vectors[i].dy[0] = 0;
    motion_vectors[i].pred_mode = 1;
    motion_vectors[i].split = 2;
  }

  for(i=0;i<10;i++){
    oil_profile_init (&prof);
    for(j=0;j<10;j++){
      SchroMotion *motion;
      void *mv_save;

      motion = schro_motion_new (&params, uref, NULL);
      mv_save = motion->motion_vectors;
      motion->motion_vectors = motion_vectors;
      oil_profile_start(&prof);
      schro_motion_render (motion, dest);
      oil_profile_stop(&prof);
      motion->motion_vectors = mv_save;
      schro_motion_free (motion);
    }
    oil_profile_get_ave_std (&prof, &ave, &std);
    printf("cycles %g %g\n", ave, std);
  }



  schro_upsampled_frame_free (uref);
  schro_frame_unref (dest);
  free (motion_vectors);

  return 0;
}

