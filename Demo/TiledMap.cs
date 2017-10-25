using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;
using System.Collections;

namespace Demo
{
	public class TiledMap
	{
		private string mapPath;
		private XDocument _document;
		private XElement root;

		public TiledMap(string mapPath)
		{
			this.mapPath = mapPath;
			_document = XDocument.Load(mapPath);
			root = _document.Root;
			
		}

		public int[,] MapArray()
		{
			var mapWidth = int.Parse(root.Attribute("width").Value);
			var mapheight = int.Parse(root.Attribute("height").Value);
			string[] mapData = root.Element("layer").Element("data").Value.Split(',');
			int [,] mapArray = new int[mapWidth,mapheight];
			for (int i = 0; i < mapWidth; i++)
			{
				for (int j = 0; j < mapheight; j++)
				{
					mapArray[i, j] = int.Parse(mapData[i + j * mapWidth]);
				}
			}

			return mapArray;
		}
		
		public Vector2[] SourcePos(int tilecount, int columns)
		{
			int key = 0;
			Vector2[] sourcePos = new Vector2[tilecount];
			for(int x = 0; x<tilecount / columns; x++)
			{
				for(int y = 0; y < columns; y++)
				{
					sourcePos[key] = new Vector2(y * 8, x * 8);
					key++;
				}
			}
			return sourcePos;
		}
		
		
		
		
		
	}
}
