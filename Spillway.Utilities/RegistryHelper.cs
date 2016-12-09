using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Utilities
{
	public static class RegistryHelper
	{
		//registryKey.SetValue("ApplicationName", Application.ExecutablePath);
		public static void RegisterAppToStartup(bool setAppToStartUp, string appName)
		{
			RegistryKey registyKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN", true);
			if (setAppToStartUp)
			{
				registyKey.SetValue(appName, System.Reflection.Assembly.GetExecutingAssembly().Location);
			}
			else
			{
				registyKey.DeleteValue(appName);
			}
		}
		public static bool IsAppRegisteredToStartUp(string appName)
		{
			RegistryKey registyKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MICROSOFT\WINDOWS\CURRENTVERSION\RUN", true);
			return registyKey.GetValue(appName) != null;
		}
	}
}
