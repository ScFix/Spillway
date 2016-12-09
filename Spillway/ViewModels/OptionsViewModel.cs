using Spillway.Interfaces;
using Spillway.Utilities;
using System;

namespace Spillway.ViewModels
{
	public class OptionsViewModel : ViewModelBase, ISection
	{

		public OptionsViewModel()
		{
			// TODO(Matthew): Get rid of the static name of spillway or reduce the occurance of them but they do not need to be hard coded in
			_IsAutoStart = RegistryHelper.IsAppRegisteredToStartUp("Spillway");
		}

		#region IsAutoStart
		protected bool _IsAutoStart;
		public bool IsAutoStart
		{
			get
			{
				return _IsAutoStart;
			}
			set
			{
				if (value != _IsAutoStart)
				{
					_IsAutoStart = value;
					SetAutoStart(_IsAutoStart);
					OnPropertyChanged("IsAutoStart");
				}
			}
		}

		#endregion //IsAutoStart

		public override string ToString()
		{
			return "Options";
		}

		public String SectionName
		{
			get { return ToString(); }
		}

		private void SetAutoStart(bool isAutoStart)
		{
			RegistryHelper.RegisterAppToStartup(isAutoStart, "Spillway");
		}

	}
}