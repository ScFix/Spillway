﻿using Spillway.Interfaces;
using Spillway.Services;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Windows;

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

		#endregion ProjectName

		#region Tabs

		private IList<ISection> _Tabs = new List<ISection>();

		public IList<ISection> Tabs
		{
			get { return _Tabs; }
			set { _Tabs = value; }
		}

		#endregion Tabs

		#region SelectedTab

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

		#endregion SelectedTab

		#region TimingService

		public DataTimingService TimingService { get; set; }

		#endregion TimingService

		public MainViewModel()
		{

		}

		/// <summary>
		/// This method will wrap up allocated resources that the application uses
		///  i.e. Closing all background threads
		/// </summary>
		internal void Close()
		{
			TimingService?.CloseTimer();
		}

		internal async void CheckForUpdate()
		{
			try
			{
				using (var mgr = await UpdateManager.GitHubUpdateManager("https://github.com/ScFix/Spillway"))
				{
					var release = await mgr.UpdateApp();
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message + Environment.NewLine;
				if (ex.InnerException != null)
					message += ex.InnerException.Message;
				MessageBox.Show(message);
			}
		}
	}
}