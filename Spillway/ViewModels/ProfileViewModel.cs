using Spillway.Interfaces;
using Spillway.Models;
using Spillway.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Spillway.ViewModels
{
	public enum ProfileViewState
	{
		Authorize,
		RequestToken,
		CurrentProfile
	}
	public class ProfileViewModel : ViewModelBase, ISection, IProfile
	{
		#region Members

		private IDataManager dataManager;
		private string TokenTag = "access_token=";

		#endregion //Members

#if Debug
		public ProfileViewModel()
		{
			
		}
#endif

		public ProfileViewModel(IDataManager dataManager)
		{
			this.dataManager = dataManager;
			this.dataManager.UserChangedEvent += DataManager_UserChangedEvent;
			ImageCache.ImageLoaded += ImageLoachedEvent;
		}

		private void ImageLoachedEvent(object sender, ImageLoadedEventArgs e)
		{
			if (e.LoadedUrl == CurrentUser.ImageUrl)
			{
				OnPropertyChanged("CurrentUser");
			}
		}

		#region Properites

		#region ViewState
		protected ProfileViewState _ViewState = ProfileViewState.Authorize;
		public ProfileViewState ViewState
		{
			get
			{
				return _ViewState;
			}
			set
			{
				if (value != _ViewState)
				{
					_ViewState = value;
					OnPropertyChanged("ViewState");
				}
			}
		}
		#endregion //ViewState

		#region TokenUrl
		protected String _TokenUrl;
		public String TokenUrl
		{
			get
			{
				return _TokenUrl;
			}
			set
			{
				if (value != _TokenUrl)
				{
					_TokenUrl = value;
					OnPropertyChanged("TokenUrl");
				}
			}
		}
		#endregion //Token



		public User CurrentUser
		{
			get
			{
				return dataManager.CurrentUser;
			}
		}

		#endregion //Propeties

		#region Commands

		#region SubmitTokenUrl
		protected ICommand _SubmitTokenUrl = null;
		public ICommand SubmitTokenUrl
		{
			get
			{
				if (_SubmitTokenUrl == null)
				{
					_SubmitTokenUrl = new RelayCommand(SubmitTokenUrlExecute, CanSubmitTokenUrl);
				}
				return _SubmitTokenUrl;
			}
		}

		private bool CanSubmitTokenUrl(object obj)
		{

			return TokenUrl != null ? TokenUrl.Contains(TokenTag) : false;
		}

		private void SubmitTokenUrlExecute(object obj)
		{
			string accessToken = ParseTokenOutOfUrl();
			dataManager.SetToken(accessToken);

			ViewState = ProfileViewState.CurrentProfile;
		}

		private string ParseTokenOutOfUrl()
		{
			return TokenUrl.Substring(TokenUrl.IndexOf(TokenTag) + TokenTag.Length);
		}
		#endregion //SubmitTokenUrl


		#region AuthorizeApplication
		protected ICommand _AuthorizeApplication = null;
		public ICommand AuthorizeApplication
		{
			get
			{
				if (_AuthorizeApplication == null)
				{
					_AuthorizeApplication = new RelayCommand(AuthorizeApplicationExecute, CanAuthorizeApplication);
				}
				return _AuthorizeApplication;
			}
		}

		private bool CanAuthorizeApplication(object obj)
		{
			return true;
		}

		private void AuthorizeApplicationExecute(object obj)
		{
			if (!dataManager.HasCurrentSessionOpen())
			{
				RequestPermissionFromStackOverflow();
			}
			else
			{
				throw new Exception("You are currently logged in, and the views are incorrectly displaying");
			}
		}
		#endregion //AuthorizeApplication

		#endregion //Commands

		#region Event Handlers
		private void DataManager_UserChangedEvent(object sender, EventArgs e)
		{
			ViewState = ProfileViewState.CurrentProfile;
			//tunnel the changed event to the view
			OnPropertyChanged("CurrentUser");
		}
		#endregion // Event Handlers

		#region ISection Properties
		public override string ToString()
		{
			return "Profile";
		}


		public String SectionName
		{
			get { return ToString(); }
		}
		#endregion // ISection Properties

		#region IProfile Implementation

		public void RequestPermissionFromStackOverflow()
		{
			// call the data function in spillway.data
			// Note(Matthew): This is going to be a bit messy to begin with. I am going to include the Data interface to be referenced in this project. It will need to be accessed througha  wrapper this way I will try
			// To make thias generic as possibe. 
			dataManager.RequestUserVerification();
			// Note(Matthew): we will need to tansition the state of the ProfileViewModel into accepting Tokens
			ViewState = ProfileViewState.RequestToken;
		}

		public void StartNotificationUpdate()
		{
			throw new NotImplementedException();
		}

		#endregion //IProfile Implementation
	}
}
