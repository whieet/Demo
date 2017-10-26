using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Demo1
{
    public class MapXml
    {
        private string mapPath;
        private XDocument _xDocument;
        private XElement xrootElement;

        public struct Tileset
        {
            public string name;
            public int firstgid;
            public int tilecount;
            public int columns;
            public int tilewidth;
            public int tileheight;
        };

        public struct Layer
        {
            public string name;
            public int width;
            public int height;
            public string[] data;
        };
        
        public struct ObjectElement
        {
            public int id;
            public int xpos;
            public int ypos;
            public int width;
            public int height;

        }
        
        public MapXml(string mapPath)
        {
            this.mapPath = mapPath;
            _xDocument = XDocument.Load(this.mapPath);
            xrootElement = _xDocument.Root;
        }

        public Tileset[] GetTileset()
        {
            
            var xmlTilesets = xrootElement.Elements("tileset");
            Tileset[] tileset = new Tileset[xmlTilesets.Count()];
            int key = 0;
            foreach (var VARIABLE in xmlTilesets)
            {
                tileset[key].name = VARIABLE.Attribute("name").Value;
                tileset[key].firstgid = int.Parse(VARIABLE.Attribute("firstgid").Value);
                tileset[key].tilecount = int.Parse(VARIABLE.Attribute("tilecount").Value);
                tileset[key].columns = int.Parse(VARIABLE.Attribute("columns").Value);
                tileset[key].tilewidth = int.Parse(VARIABLE.Attribute("tilewidth").Value);
                tileset[key].tileheight = int.Parse(VARIABLE.Attribute("tileheight").Value);
                key++;

            }

            return tileset;
        }

        public Layer[] GetMapLayers()
        {
            var mapLayer = xrootElement.Elements("layer");
            Layer[] layer = new Layer[mapLayer.Count()];
            int key = 0;
            foreach (var VARIABLE in mapLayer)
            {
                layer[key].name = VARIABLE.Attribute("name").Value;
                layer[key].width = int.Parse(VARIABLE.Attribute("width").Value);
                layer[key].height = int.Parse(VARIABLE.Attribute("height").Value);
                layer[key].data = VARIABLE.Element("data").Value.Split(',');
                key++;
            }

            return layer;

        }

        public int[,] GetDataArrays(string[] data, int width, int height)
        {
            int[,] dataArrays = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    dataArrays[i, j] = int.Parse(data[i + j * width]);
                }
            }

            return dataArrays;
        }

        public List<ObjectElement> GetObjects()
        {
            var xmlObjectGroup = xrootElement.Elements("objectgroup");
            List<ObjectElement> objects = new List<ObjectElement>();
            foreach (var objectGroup in xmlObjectGroup)
            {
                foreach (var objectElement in objectGroup.Elements("object"))
                {
                    ObjectElement objectsElement = new ObjectElement();
                    objectsElement.id = (int)objectElement.Attribute("id");
                    objectsElement.xpos = (int)objectElement.Attribute("x");
                    objectsElement.ypos = (int)objectElement.Attribute("y");
                    objectsElement.width = (int)objectElement.Attribute("width");
                    objectsElement.height = (int)objectElement.Attribute("height");
                    objects.Add(objectsElement);
                }
            }

            return objects;
        }
        
    }
}






























