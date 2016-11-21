using Spillway.Interfaces;
using Spillway.Models;
using Spillway.Services;
using Spillway.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Spillway.ViewModels
{
    public class DebugViewModel : ViewModelBase, ISection
    {
        public string SectionName
        {
            get
            {
                return "Debug Panel";
            }
        }

        #region DataManager

        protected IDataService _DataManager;

        public IDataService DataManager
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

        #endregion DataManager

        #region Toasts

        protected ToastService _Toasts;

        public ToastService Toasts
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

        #endregion Toasts

        #region Messages
        public MessagesViewModel Messages { get; set; }

        #endregion //Messages

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
            Notification n = new Notification();
            n.Type = "EXAMPLE";
            n.Date = 0;
            n.Link = "http:\\google.com";

            Toasts.ShowToast(n);
        }

        #endregion // SendSampleToast

        #region DataFlowSample

        protected ICommand _DataFlowSample = null;
        public ICommand DataFlowSample
        {
            get
            {
                if (_DataFlowSample == null)
                {
                    _DataFlowSample = new RelayCommand(DataFlowSampleExectue, CanDataFlowSample);
                }
                return _DataFlowSample;
            }
        }

        private bool CanDataFlowSample(object obj)
        {
            return true;
        }
        private void DataFlowSampleExectue(object obj) {
            StackArgs sa = new StackArgs();
            sa.Notifications = new List<Notification>();
            // Note(Matthew): Stack overflow sends everything in Epoch time
            // In order to get all the time to mesh up correctly you need to run everything throuhg the converter
            sa.Notifications.Add(new Notification()
            {
                Date = DateUtil.ToUnixTime(DateTime.Now),
                IsUnread = 1,
                Link = "http://www.Google.com",
                Type = "something"
            });

            Messages?.ProcessNotifications(this, sa);
        }
        #endregion // Data  Flow Sample

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

        #endregion RequestSampleData
    }
}