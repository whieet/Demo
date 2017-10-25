//using System;
//using System.Xml;
//using System.Xml.Linq;
//
//namespace test
//{
//	class MainClass
//	{
//		public static void Main(string[] args)
//		{
//			TileMap tileMap = new TileMap("/Users/whieet/Projects/Demo/Demo/Content/demo.tmx");
//			tileMap.LoadXml();
//		}
//	}
//	
//	
//
//	class TileMap
//	{
//		public string xmlPath;
//
//		public TileMap(string xmlPath)
//		{
//			this.xmlPath = xmlPath;
//		}
//
//		public void LoadXml()
//		{
//			XDocument doc = XDocument.Load(xmlPath);
//			XElement root = doc.Root;
//			var tileset = root.Elements("tileset");
//
//			foreach (var i in tileset)
//			{
//				Console.WriteLine(i.Attribute("firstgid").Value);
//				Console.WriteLine(i.Attribute("tilecount").Value);
//				Console.WriteLine(i.Attribute("columns").Value);
//				Console.WriteLine(i.Attribute("name").Value);
//			}
//
//			foreach (var VARIABLE in root.Elements("layer"))
//			{
//				Console.WriteLine(VARIABLE.Element("data").Value);
//			}
//	
//		}
//	}
//}
