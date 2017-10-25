using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;
using System.Collections;

namespace Demo
{
	public class TileMap
	{
		public string mapPath;
		public XDocument doc;
		public XElement root;

		public TileMap(string mapPath)
		{
			this.mapPath = mapPath;
			doc = XDocument.Load(mapPath);
			root = doc.Root;
		}

		public int[,] MapArray()
		{
			int mapWidth = int.Parse(root.Attribute("width").Value);
			int mapheight = int.Parse(root.Attribute("height").Value);
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

	}
}
