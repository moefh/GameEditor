#ifndef ${PREFIX}_DATA_H_FILE
#define ${PREFIX}_DATA_H_FILE

#include <stddef.h>
#include <stdint.h>

struct ${PREFIX}_MOD_SAMPLE {
  uint32_t       len;
  uint32_t       loop_start;
  uint32_t       loop_len;
  int8_t         finetune;
  uint8_t        volume;
  const int8_t  *data;
};

struct ${PREFIX}_MOD_CELL {
  uint8_t  sample;
  uint16_t period;
  uint16_t effect;
};

struct ${PREFIX}_MOD_DATA {
  struct ${PREFIX}_MOD_SAMPLE samples[31];
  uint8_t num_channels;

  uint8_t num_song_positions;
  uint8_t song_positions[128];
  
  uint8_t num_patterns;
  const struct ${PREFIX}_MOD_CELL *pattern;
};

struct ${PREFIX}_SFX {
   int32_t num_samples;
   const uint8_t *samples;
};

struct ${PREFIX}_IMAGE {
   int32_t width;
   int32_t height;
   int32_t stride;
   int32_t num_frames;
   const uint32_t *data;
};
   
struct ${PREFIX}_MAP {
   int16_t w;
   int16_t h;
   const struct ${PREFIX}_IMAGE *tileset;
   const uint8_t *tiles;
};

struct ${PREFIX}_SPRITE_ANIMATION_LOOP {
   uint8_t num_frames;
   uint8_t frames[16];
};

struct ${PREFIX}_SPRITE_ANIMATION {
  const struct ${PREFIX}_IMAGE *sprite;
  uint8_t num_loops;
  struct ${PREFIX}_SPRITE_ANIMATION_LOOP loops[4];
};

struct ${PREFIX}_FONT {
  uint8_t width;
  uint8_t height;
  const uint8_t *data;
};

extern const struct ${PREFIX}_FONT ${prefix}_fonts[];
extern const struct ${PREFIX}_MOD_DATA ${prefix}_mods[];
extern const struct ${PREFIX}_SFX ${prefix}_sfxs[];
extern const struct ${PREFIX}_IMAGE ${prefix}_tilesets[];
extern const struct ${PREFIX}_IMAGE ${prefix}_sprites[];
extern const struct ${PREFIX}_MAP ${prefix}_maps[];
extern const struct ${PREFIX}_SPRITE_ANIMATION ${prefix}_sprite_animations[];

#endif /* ${PREFIX}_DATA_H_FILE */
