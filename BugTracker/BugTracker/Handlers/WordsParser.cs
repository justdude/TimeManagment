using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Handlers
{
	public class WordsParser
	{
		

		//private string modResult = "";

		//WordsParser()
		//{ }

		//public WordsParser GetParser(string target)
		//{
		//	return new WordsParser() { modResult = target };
		//}

		//public void Handle()
		//{
		//	var res = modResult.Split(' ');

		//	foreach (var item in res)
		//	{
		//		if (item.Contains(delimer))
		//			continue;
				
		//		item.Split(delimer.ToCharArray());
		//	}
		//}


		//Plus S/E 0.01/0 end timer.
		public static void ToFloat(string query, out float r1, out float r2)
		{
			//var res = query.Contains(Constants.Global.Plus);
			//res &= query.Contains(Constants.Global.SpentEstimate);

			//if (res == false)
			//	return "";

			var splitted = query.Split(' ');
			var skipped = splitted.TakeWhile(p => p == Constants.Global.SpentEstimate).ToString();
			var splitted1 = skipped.Split('/');
			//float r1, r2;

			float.TryParse(splitted1[0], out r1);
			float.TryParse(splitted1[1], out r2);
			//return new float[]{r1, r2};
		}

		public static string ToString(float spent, float estimate)
		{
			string ff = string.Format("{0} {1} {2}/{3}", 
				Constants.Global.Plus,
				Constants.Global.SpentEstimate, 
				spent, 
				estimate);

			return ff;
		}


	}
}
