using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spillway
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool canClose = false;

		public MainWindow()
		{
			InitializeComponent();
			SetupSytemTray();
		}

		private void SetupSytemTray()
		{
			//inserts this icon into the system tray of the OS. This will allow for the application to close from there
			System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();

			Bitmap bmp = Spillway.Properties.Resources.spillway;

			ni.Icon = System.Drawing.Icon.FromHandle(bmp.GetHicon());
			ni.Visible = true;
			ni.DoubleClick +=
				delegate (object sender, EventArgs args)
				{
					this.Show();
					this.WindowState = System.Windows.WindowState.Normal;
				};

			var menu = GetContextMenuForSystemTray();
			ni.ContextMenu = menu;
		}

		private System.Windows.Forms.ContextMenu GetContextMenuForSystemTray()
		{
			System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
			System.Windows.Forms.MenuItem closingMeneItem = new System.Windows.Forms.MenuItem("Close");
			closingMeneItem.Click += delegate (object sender, EventArgs args)
			{
				this.canClose = true;
				this.Close();
			}; ;
			return menu;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
#if DEBUG
			Trace.WriteLine("This should exit to the system tray");
#else
			if (!canClose)
			{
				e.Cancel = true;
				this.Hide();
			}
#endif
		}

		private void Window_StateChanged(object sender, EventArgs e)
		{
			if (WindowState == WindowState.Minimized) this.Hide();
			base.OnStateChanged(e);
		}
	}
}
