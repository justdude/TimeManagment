using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data
{
	public interface ILogTime
	{
		float SpentValue
		{ get; set; }

		float EstimateValue
		{ get; set; }

		float Remaining
		{ get; }

		//virtual void ProcessValues();

	}

	public class LogTimeBase : ILogTime
	{

		public LogTimeBase()
		{
			SpentValue = 0;
			EstimateValue = 0;
		}

		public LogTimeBase(float spent, float estimate)
		{
			SpentValue = spent;
			EstimateValue = estimate;
		}

		#region ILogTime Members

		public float SpentValue
		{
			get;
			set;
		}

		public float EstimateValue
		{
			get;
			set;
		}

		public float Remaining
		{
			get
			{
				return EstimateValue - SpentValue;
			}
		}

		public void FromString(string str)
		{
			float r1 = 0, r2 = 0;
			
			Handlers.WordsParser.ToFloat(str, out r1, out r2);

			SpentValue += r1;
			EstimateValue += r2;
		}

		public override string ToString()
		{
			return Handlers.WordsParser.ToString(SpentValue, EstimateValue);
		}

		#endregion

		#region ILogTime Members


		public virtual void ProcessValues()
		{
			
		}

		#endregion
	}

}
