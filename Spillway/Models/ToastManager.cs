﻿using System;
using System.Diagnostics;
using System.IO;
using Spillway.Utilities;
using MS.WindowsAPICodePack.Internal;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace Spillway.Models
{
	public class ToastManager
	{
		private const String APP_ID = "Spillway";

		public ToastManager()
		{
			TryCreateShortcut();
		}


		private bool TryCreateShortcut()
		{
			String shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Spillway.lnk";
			if (!File.Exists(shortcutPath))
			{
				InstallShortcut(shortcutPath);
				return true;
			}
			return false;
		}

		private void InstallShortcut(String shortcutPath)
		{
			// Find the path to the current executable
			String exePath = Process.GetCurrentProcess().MainModule.FileName;
			IShellLinkW newShortcut = (IShellLinkW)new CShellLink();

			// Create a shortcut to the exe
			ErrorHelper.VerifySucceeded(newShortcut.SetPath(exePath));
			ErrorHelper.VerifySucceeded(newShortcut.SetArguments(""));

			// Open the shortcut property store, set the AppUserModelId property
			IPropertyStore newShortcutProperties = (IPropertyStore)newShortcut;

			using (PropVariant appId = new PropVariant(APP_ID))
			{
				ErrorHelper.VerifySucceeded(newShortcutProperties.SetValue(SystemProperties.System.AppUserModel.ID, appId));
				ErrorHelper.VerifySucceeded(newShortcutProperties.Commit());
			}

			// Commit the shortcut to disk
			IPersistFile newShortcutSave = (IPersistFile)newShortcut;

			ErrorHelper.VerifySucceeded(newShortcutSave.Save(shortcutPath, true));
		}

		public void ShowToast()
		{
			XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

			// Fill in the text elements
			XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
			for (int i = 0; i < stringElements.Length; i++)
			{
				stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
			}

			// Specify the absolute path to an image
			String imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");
			XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
			imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

			// Create the toast and attach event listeners
			ToastNotification toast = new ToastNotification(toastXml);
			toast.Activated += ToastActivated;
			toast.Dismissed += ToastDismissed;
			toast.Failed += ToastFailed;

			// Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
			ToastNotificationManager.CreateToastNotifier(APP_ID).Show(toast);
		}

		private void ToastFailed(ToastNotification sender, ToastFailedEventArgs args)
		{
			Console.WriteLine("Toast failed");
		}

		private void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs args)
		{
			Console.WriteLine("Toast Dismissed");
		}

		private void ToastActivated(ToastNotification sender, object args)
		{
			Console.WriteLine("Toast Activated");
		}
	}
}
