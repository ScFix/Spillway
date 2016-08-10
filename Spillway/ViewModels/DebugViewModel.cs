using Spillway.Interfaces;
using Spillway.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spillway.ViewModels
{
	public class DebugViewModel : ViewModelBase, ISection
	{

		#region SendSampleToast
		protected ICommand _SendSampleToast = null;
		public ICommand SendSampleToast
		{
			get
			{
				if (_SendSampleToast == null)
				{
					_SendSampleToast = new RelayCommand(SendSampleToastExecute, CanSendSampleToast);
				}
				return _SendSampleToast;
			}
		}

		private bool CanSendSampleToast(object obj)
		{
			return true;
		}

		private void SendSampleToastExecute(object obj)
		{
			//TODO Implement the SendSampleToast Method
			
		}
		#endregion //SendSampleToast

		public string SectionName
		{
			get
			{
				return "Debug Panel";
			}
		}

	}
}
