using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data
{
	public class LabelData
	{
		//PUT /1/cards/[card id or shortlink]/labels
		public enum ColorTypes
		{
			blue,
			green,
			orange,
			purple,
			red,
			yellow,
		}
		public ColorTypes Value { get; set; }

	}
}
