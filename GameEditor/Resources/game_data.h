#ifndef ${PREFIX}_DATA_H_FILE
#define ${PREFIX}_DATA_H_FILE

#include <stddef.h>
#include <stdint.h>

#ifndef ${PREFIX}_SKIP_STRUCTS_MOD

struct ${PREFIX}_MOD_SAMPLE {
    uint32_t len;
    uint32_t loop_start;
    uint32_t loop_len;
    uint8_t  finetune;
    uint8_t  volume;
    uint16_t bits_per_sample;
    union {
        const void *data;
        const int8_t *data8;
        const int16_t *data16;
    };
};

struct ${PREFIX}_MOD_CELL {
    uint8_t  sample;
    uint8_t  note_index;
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

#endif /* ${PREFIX}_SKIP_STRUCTS_MOD */

#ifndef ${PREFIX}_SKIP_STRUCTS_SFX

struct ${PREFIX}_SFX {
    int32_t len;
    int32_t loop_start;
    int32_t loop_len;
    int32_t bits_per_sample;
    union {
        const void *samples;
        const int8_t *spl8;
        const int16_t *spl16;
    };
};

#endif /* ${PREFIX}_SKIP_STRUCTS_SFX */

#ifndef ${PREFIX}_SKIP_STRUCTS_IMAGE

struct ${PREFIX}_IMAGE {
    int32_t width;
    int32_t height;
    int32_t stride;
    int32_t num_frames;
    const uint32_t *data;
};

#endif /* ${PREFIX}_SKIP_STRUCTS_IMAGE */

#ifndef ${PREFIX}_SKIP_STRUCTS_MAP

struct ${PREFIX}_MAP {
    int16_t w;
    int16_t h;
    int16_t bg_w;
    int16_t bg_h;
    const struct ${PREFIX}_IMAGE *tileset;
    const uint8_t *tiles;
};

#endif /* #ifndef ${PREFIX}_SKIP_STRUCTS_MAP */

#ifndef ${PREFIX}_SKIP_STRUCT_SPRITE_ANIMATION

struct ${PREFIX}_SPRITE_ANIMATION_LOOP {
    uint16_t offset;   // offset into animation frame_indices
    uint16_t length;   // loop data length
};

struct ${PREFIX}_SPRITE_ANIMATION_COLLISION {
   uint16_t x;
   uint16_t y;
   uint16_t w;
   uint16_t h;
};

struct ${PREFIX}_SPRITE_ANIMATION {
    const uint8_t *frame_indices;
    const struct ${PREFIX}_IMAGE *sprite;
    struct ${PREFIX}_SPRITE_ANIMATION_COLLISION collision;
    int8_t foot_overlap;
    struct ${PREFIX}_SPRITE_ANIMATION_LOOP loops[20];
};

#endif /* #ifndef ${PREFIX}_SKIP_STRUCTS_SPRITE_ANIMATION */

#ifndef ${PREFIX}_SKIP_STRUCTS_FONT

struct ${PREFIX}_FONT {
    uint8_t width;
    uint8_t height;
    const uint8_t *data;
};

#endif /* #ifndef ${PREFIX}_SKIP_STRUCTS_FONT */

#ifndef ${PREFIX}_SKIP_STRUCTS_PROP_FONT

struct ${PREFIX}_PROP_FONT {
    uint8_t height;
    const uint8_t *data;
    uint8_t char_width[96];
    uint16_t char_offset[96];
};

#endif /* #ifndef ${PREFIX}_SKIP_STRUCTS_PROP_FONT */

#ifndef ${PREFIX}_SKIP_STRUCTS_ROOM

struct ${PREFIX}_ROOM_MAP_INFO {
    uint16_t x;
    uint16_t y;
    const struct ${PREFIX}_MAP *map;
};

struct ${PREFIX}_ROOM {
    uint16_t num_maps;
    const struct ${PREFIX}_ROOM_MAP_INFO *maps;
};

#endif /* #ifndef ${PREFIX}_SKIP_STRUCTS_ROOM */

extern const struct ${PREFIX}_FONT ${prefix}_fonts[];
extern const struct ${PREFIX}_PROP_FONT ${prefix}_prop_fonts[];
extern const struct ${PREFIX}_MOD_DATA ${prefix}_mods[];
extern const struct ${PREFIX}_SFX ${prefix}_sfxs[];
extern const struct ${PREFIX}_IMAGE ${prefix}_tilesets[];
extern const struct ${PREFIX}_IMAGE ${prefix}_sprites[];
extern const struct ${PREFIX}_MAP ${prefix}_maps[];
extern const struct ${PREFIX}_SPRITE_ANIMATION ${prefix}_sprite_animations[];
extern const struct ${PREFIX}_ROOM ${prefix}_rooms[];

#endif /* ${PREFIX}_DATA_H_FILE */
