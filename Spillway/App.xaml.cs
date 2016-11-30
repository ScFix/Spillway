using Spillway.Services;
using Spillway.ViewModels;
using System;
using System.Windows;
using Squirrel;

namespace Spillway
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            using (var mgr = new UpdateManager(@"W:\Public Repositories\Spillway\Releases"))
            {
               mgr.UpdateApp();
            }
            //Interaction logic for all known connections as well as all other started features
            var mainViewModel = new MainViewModel();

            //declare services
            var dataService = new StackOverflowDataService();
            var toastService = new ToastService();

            var profileViewModel = new ProfileViewModel(dataService);
            var messagesviewModel = new MessagesViewModel();
            var optionsViewModel = new OptionsViewModel();

            // Note(Matthew): Hooking into everything that needs to be notified that new messages are coming in from stack overflow
            dataService.IncomingNotificationsEvent += toastService.PostNotifications;
            dataService.IncomingNotificationsEvent += messagesviewModel.ProcessNotifications;

            // Note(Matthew): this will start the call for seeing if the token is actually valid.
            var token = Spillway.Properties.Settings.Default.Access_Token;
            if (!String.IsNullOrEmpty(token))
            {
                dataService.SetToken(token);
            }

            // Note(Matthew): Adding in the views
            mainViewModel.Tabs.Add(profileViewModel);
            mainViewModel.Tabs.Add(messagesviewModel);
            mainViewModel.Tabs.Add(optionsViewModel);

#if DEBUG
            // Note(Matthew): Added in a debug panel to test ui functionality so to easially debug in future cases. I might want to remove this later
            var debugPanel = new DebugViewModel();
            debugPanel.Toasts = toastService;
            debugPanel.DataManager = dataService;
            debugPanel.Messages = messagesviewModel;
            mainViewModel.Tabs.Add(debugPanel);
#endif

            var dataTimingService = new DataTimingService(dataService, null, 0);
            mainViewModel.TimingService = dataTimingService;

            mainViewModel.SelectedTab = profileViewModel;
            MainWindow mw = new MainWindow();
            mw.DataContext = mainViewModel;
            mw.Show();
        }
    }
}