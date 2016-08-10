using Spillway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		#region ProjectName
		private string _ProjectName = "Spillway";

		public string ProjectName
		{
			get
			{
				return _ProjectName;

			}
			set
			{
				_ProjectName = value;
				OnPropertyChanged("ProjectName");
			}
		}
		#endregion


		//This will need to be renamed for in hte end
		private IList<ISection> _Settings = new List<ISection>();

		public IList<ISection> Settings
		{
			get { return _Settings; }
			set { _Settings = value; }
		}


		private ISection _SelectedTab;
		public ISection SelectedTab
		{
			get
			{
				return _SelectedTab;
			}
			set
			{
				if (_SelectedTab != value)
				{
					_SelectedTab = value;
					OnPropertyChanged("SelectedTab");
				}
			}
		}

		public MainViewModel()
		{
		}

	}
}
