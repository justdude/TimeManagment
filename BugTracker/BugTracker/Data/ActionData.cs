using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data
{

	public class CommentData
	{
		public BoardData Board { get; set; }
		public CardData Card { get; set; }
		public string Text { get; set; }
	}

	public class ActionData
	{
		/*
{"id":"548d93fbf88f6c3cd476370e","idMemberCreator":"52bb1ad017035d0520035852",
		 * "data":{"list":{"name":"To Do","id":"5489a3de25ca9b35ed76f522"},
		 * "board":"shortLink":"E3yM6Q9A","name":"TestForDevBoard","id":"5489a3de25ca9b35ed76f521"},
				"card":{"shortLink":"GohkR3y1","idShort":22,"name":"14.12.2014 11:24:53","id":"548d5765d091f6b5184d80c4"},
		 * "text":"plus! 4/5 dfsdfsd"},
"type":"commentCard",
"date":"2014-12-14T13:43:23.799Z",
"memberCreator":{"id":"52bb1ad017035d0520035852","avatarHash":"07e6ed521d179e40f46948a10b8fe2f2","fullName":"dude just dude","initials":"DD","username":"dudejustdude"}}
		 
		 */
		public string Id { get; set; }
		public CommentData Data { get; set; }
	}
}
