using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

using BugTracker.Data;
using TrelloNet;
using BugTracker.Services;

namespace BugTracker.ViewModel
{
	public class BoardViewModel :ViewModelBase
	{
		public TaskBoard Board { get; private set; }


		private List<TaskCard> mvCards;
		public List<TaskCard> Cards
		{
			get
			{
				if (mvCards == null || mvCards.Count <= 0)
				{
					mvCards = GetCards().ToList();
				}
				return GetCards().ToList();
			}
		}

		public string Name
		{
			get
			{
				return Board.Name;
			}
			set
			{
				if (Board.Name == value)
					return;

				Board.Name = value;

				base.RaisePropertyChanged("Name");
			}
		}

		public string Desc
		{
			get
			{
				return Board.Desc;
			}
			set
			{
				if (Board.Desc == value)
					return;

				Board.Desc = value;

				base.RaisePropertyChanged("Desc");
			}
		}

		public string Id
		{
			get
			{
				return Board.Id;
			}
		}

		public BoardViewModel(TaskBoard board)
		{
			Board = board;
		}

		public IEnumerable<TaskCard> GetCards()
		{
			return Engine.Instance.Board.GetCards(Id).ToList();
		}

	}
}
