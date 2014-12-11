
using System.Collections.Generic;
namespace BugTracker.Data
{
    public class TaskBoard 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

				public List<BoardList> Lists { get; set; }
    }
}