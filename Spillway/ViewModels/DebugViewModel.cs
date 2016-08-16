using Spillway.Interfaces;
using Spillway.Models;
using Spillway.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Spillway.ViewModels
{
	public class DebugViewModel : ViewModelBase, ISection
	{

		#region DataManager
		protected IDataManager _DataManager;
		public IDataManager DataManager
		{
			get
			{
				return _DataManager;
			}
			set
			{
				if (value != _DataManager)
				{
					_DataManager = value;
					OnPropertyChanged("DataManager");
				}
			}
		}
		#endregion //DataManager

		#region Toasts
		protected ToastManager _Toasts;
		public ToastManager Toasts
		{
			get
			{
				return _Toasts;
			}
			set
			{
				if (value != _Toasts)
				{
					_Toasts = value;
					OnPropertyChanged("Toasts");
				}
			}
		}
		#endregion //Toasts

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
			Toasts.ShowToast(null);
		}
		#endregion //SendSampleToast


		#region RequestSampleData
		protected ICommand _RequestSampleData = null;
		public ICommand RequestSampleData
		{
			get
			{
				if (_RequestSampleData == null)
				{
					_RequestSampleData = new RelayCommand(RequestSampleDataExecute, CanRequestSampleData);
				}
				return _RequestSampleData;
			}
		}

		private bool CanRequestSampleData(object obj)
		{
			return true;
		}

		private void RequestSampleDataExecute(object obj)
		{
			DataManager.RequestUnreadNotifications(null);
		}
		#endregion //RequestSampleData

		public string SectionName
		{
			get
			{
				return "Debug Panel";
			}
		}


	}
}
