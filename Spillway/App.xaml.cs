using Spillway.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Spillway
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			//Interaction logic for all known connections as well as all other started features

			MainViewModel mainViewModel = new MainViewModel();
			var profileViewModel = new ProfileViewModel();
			var optionsViewModel = new OptionsViewModel();

			mainViewModel.Settings.Add(profileViewModel);
			mainViewModel.Settings.Add(new OptionsViewModel());

			mainViewModel.SelectedTab = profileViewModel;

			MainWindow mw = new MainWindow();
			mw.DataContext = mainViewModel;
			mw.Show();
		}
	}
}
