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
    public class MapData
    {
        private string name;
        private Tileset tileset;
        private readonly MapTiles tiles;

        public MapData(int width, int height, Tileset ts) {
            name = "new_map";
            tiles = new MapTiles(width, height);
            tileset = ts;
        }

        public MapData(string name, int width, int height, Tileset ts, List<byte> tileData) {
            this.name = name;
            tiles = new MapTiles(width, height, tileData);
            tileset = ts;
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public MapTiles Tiles {
            get { return tiles; }
        }

        public Tileset Tileset {
            get { return tileset; }
            set { tileset = value; }
        }

        public void Resize(int width, int height) {
            tiles.Resize(width, height);
        }
    }
}
