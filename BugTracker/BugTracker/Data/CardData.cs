

using System.Collections.Generic;
namespace BugTracker.Data
{
	public class CardData : LogTimeBase
	{
		public string Id { get; set; }
		public string IdBoard { get; set; }
		public string IdList { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }

		public List<ActionData> Actions { get; set; }

		public CardData():base()
		{ }

		public CardData(float spent, float estimate): base(spent, estimate)
		{ }

		public override void ProcessValues()
		{
			foreach(var item in Actions)
			{
				base.FromString(item.Data.Text);
			}
		}

	}
}