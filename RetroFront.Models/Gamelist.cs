using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RetroFront.Models
{
	[XmlRoot(ElementName = "gameList")]
	public class GameList
	{

		[XmlElement(ElementName = "provider")]
		public Provider Provider { get; set; }

		[XmlElement(ElementName = "game")]
		public List<Game> Game { get; set; }
	}

	[XmlRoot(ElementName = "provider")]
	public class Provider
	{

		[XmlElement(ElementName = "System")]
		public string System { get; set; }

		[XmlElement(ElementName = "software")]
		public string Software { get; set; }

		[XmlElement(ElementName = "database")]
		public string Database { get; set; }

		[XmlElement(ElementName = "web")]
		public string Web { get; set; }
	}
	[XmlRoot(ElementName = "game")]
	public class Game
	{

		[XmlElement(ElementName = "path")]
		public string Path { get; set; }

		[XmlElement(ElementName = "name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "desc")]
		public string Desc { get; set; }

		[XmlElement(ElementName = "rating")]
		public double Rating { get; set; }

		[XmlElement(ElementName = "releasedate")]
		public string Releasedate { get; set; }

		[XmlElement(ElementName = "developer")]
		public string Developer { get; set; }

		[XmlElement(ElementName = "publisher")]
		public string Publisher { get; set; }

		[XmlElement(ElementName = "genre")]
		public string Genre { get; set; }

		[XmlElement(ElementName = "players")]
		public string Players { get; set; }

		[XmlElement(ElementName = "hash")]
		public string Hash { get; set; }

		[XmlElement(ElementName = "image")]
		public string Image { get; set; }

		[XmlElement(ElementName = "thumbnail")]
		public string Thumbnail { get; set; }

		[XmlElement(ElementName = "genreid")]
		public int Genreid { get; set; }

		[XmlAttribute(AttributeName = "id")]
		public int Id { get; set; }

		[XmlAttribute(AttributeName = "source")]
		public string Source { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
