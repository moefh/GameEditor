using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GameEditor.GameData
{
    public class MapData : IDataAsset
    {
        private Tileset tileset;
        private readonly MapFgTiles fg;
        private readonly MapBgTiles bg;

        public MapData(string name, int width, int height, Tileset ts) {
            Name = name;
            fg = new MapFgTiles(width, height);
            bg = new MapBgTiles(width, height);
            tileset = ts;
        }

        public MapData(string name, int fgWidth, int fgHeight, int bgWidth, int bgHeight, Tileset ts, List<byte> tileData) {
            Name = name;
            fg = new MapFgTiles(fgWidth, fgHeight, tileData, 0);
            bg = new MapBgTiles(bgWidth, bgHeight, tileData, 3*fgWidth*fgHeight);
            tileset = ts;
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Map; } }
        public int FgWidth { get { return fg.Width; } }
        public int FgHeight { get { return fg.Height; } }
        public int BgWidth { get { return bg.Width; } }
        public int BgHeight { get { return bg.Height; } }

        public MapFgTiles FgTiles {
            get { return fg; }
        }

        public MapBgTiles BgTiles {
            get { return bg; }
        }

        public Tileset Tileset {
            get { return tileset; }
            set { tileset = value; }
        }

        public int DataSize {
            get {
                return fg.DataSize + bg.DataSize + 4+4;
            }
        }

        public void Dispose() {
        }

        public static int ByteToTile(byte b) {
            if (b == 0xff) return -1;
            return b;
        }

        public void InsertedTiles(int index, int count) {
            fg.InsertedTiles(index, count);
            bg.InsertedTiles(index, count);
        }

        public void RemovedTiles(int index, int count) {
            fg.RemovedTiles(index, count);
            bg.RemovedTiles(index, count);
        }
    }
}
