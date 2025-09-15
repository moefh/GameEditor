#ifndef ${PREFIX}_DATA_H_FILE
#define ${PREFIX}_DATA_H_FILE

#include <stddef.h>
#include <stdint.h>

struct ${PREFIX}_MOD_SAMPLE {
    uint32_t       len;
    uint32_t       loop_start;
    uint32_t       loop_len;
    uint8_t        finetune;
    uint8_t        volume;
    const int8_t  *data;
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

struct ${PREFIX}_SFX {
    int32_t len;
    int32_t loop_start;
    int32_t loop_len;
    const int8_t *samples;
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
    int16_t bg_w;
    int16_t bg_h;
    const struct ${PREFIX}_IMAGE *tileset;
    const uint8_t *tiles;
};

struct ${PREFIX}_SPRITE_ANIMATION_LOOP {
    uint16_t offset;   // offset into animation frame_indices
    uint16_t length;   // loop data length
};

struct ${PREFIX}_SPRITE_ANIMATION {
    const uint8_t *frame_indices;
    const struct ${PREFIX}_IMAGE *sprite;
    int8_t foot_overlap;
    struct ${PREFIX}_SPRITE_ANIMATION_LOOP loops[20];
};

struct ${PREFIX}_FONT {
    uint8_t width;
    uint8_t height;
    const uint8_t *data;
};

struct ${PREFIX}_PROP_FONT {
    uint8_t height;
    const uint8_t *data;
    uint8_t char_width[96];
    uint16_t char_offset[96];
};

struct ${PREFIX}_ROOM_MAP_INFO {
    uint16_t x;
    uint16_t y;
    struct ${PREFIX}_MAP *map;
};

struct ${PREFIX}_ROOM {
    uint16_t num_maps;
    struct ${PREFIX}_ROOM_MAP_INFO *maps[8];
};

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
