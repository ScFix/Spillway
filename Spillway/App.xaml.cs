using Spillway.Models;
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
			var dataManager = new StackOverflowDataManager();



		

			var profileViewModel = new ProfileViewModel(dataManager);
			var optionsViewModel = new OptionsViewModel();

			var token = Spillway.Properties.Settings.Default.Access_Token;
			if (!String.IsNullOrEmpty(token))
				dataManager.SetToken(token);

			mainViewModel.Settings.Add(profileViewModel);
			mainViewModel.Settings.Add(new OptionsViewModel());

			mainViewModel.SelectedTab = profileViewModel;

			MainWindow mw = new MainWindow();
			mw.DataContext = mainViewModel;
			mw.Show();
		}
	}
}
