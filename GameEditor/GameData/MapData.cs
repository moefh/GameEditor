using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GameEditor.GameData
{
    public class MapData : IDataAsset
    {
        private Tileset tileset;
        private readonly MapTiles tiles;

        public MapData(string name, int width, int height, Tileset ts) {
            Name = name;
            tiles = new MapTiles(width, height);
            BgWidth = width;
            BgHeight = height;
            tileset = ts;
        }

        public MapData(string name, int width, int height, int bgWidth, int bgHeight, Tileset ts, List<byte> tileData) {
            Name = name;
            tiles = new MapTiles(width, height, tileData);
            BgWidth = bgWidth;
            BgHeight = bgHeight;
            tileset = ts;
        }

        public string Name { get; set; }
        public DataAssetType AssetType { get { return DataAssetType.Map; } }
        public int Width { get { return Tiles.Width; } }
        public int Height { get { return Tiles.Height; } }
        public int BgWidth { get; set; }
        public int BgHeight { get; set; }

        public MapTiles Tiles {
            get { return tiles; }
        }

        public Tileset Tileset {
            get { return tileset; }
            set { tileset = value; }
        }

        public int GameDataSize {
            get {
                // (fg(1) + bg(1) + collision(1)) * width * height
                int tileSize = 3 * Tiles.Width * Tiles.Height;
                // width(2) + height(2) + bgWidth(2) + bgHeight(2) + tilesetPtr(4) + tilesPtr(4)
                return tileSize + 2+2+2+2 + 4+4;
            }
        }

        public void Dispose() {
        }

        public void Resize(int width, int height) {
            tiles.Resize(width, height);
        }

        public void InsertedTile(int index, int count) {
            tiles.InsertedTiles(index, count);
        }

        public void RemovedTiles(int index, int count) {
            tiles.RemovedTiles(index, count);
        }
    }
}
