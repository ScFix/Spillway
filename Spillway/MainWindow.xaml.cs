﻿using Spillway.ViewModels;
using System;
using System.Drawing;
using System.Windows;

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
            System.Windows.Forms.MenuItem closingMenuItem = new System.Windows.Forms.MenuItem("Close");
            closingMenuItem.Click += delegate (object sender, EventArgs args)
            {
                this.canClose = true;
                this.Close();
            };
            menu.MenuItems.Add(closingMenuItem);

            return menu;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //#if DEBUG
            //			Trace.WriteLine("This should exit to the system tray");
            //#else
            if (!canClose)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                // This should call up to the the datacontext and cancel all of the threads
                var viewModel = this.DataContext as MainViewModel;
                viewModel.Close();
            }
            //#endif
        }
    }
}