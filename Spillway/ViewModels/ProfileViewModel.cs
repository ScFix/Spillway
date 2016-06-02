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

		private IDataManager dataManager;

		public ProfileViewModel()
		{
			dataManager = new StackOverflowDataManager();
		}


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

		#region Token
		protected String _Token;
		public String Token
		{
			get
			{
				return _Token;
			}
			set
			{
				if (value != _Token)
				{
					_Token = value;
					OnPropertyChanged("Token");
				}
			}
		}
		#endregion //Token


		#region SubmitToken
		protected ICommand _SubmitToken = null;
		public ICommand SubmitToken
		{
			get
			{
				if (_SubmitToken == null)
				{
					_SubmitToken = new RelayCommand(SubmitTokenExecute, CanSubmitToken);
				}
				return _SubmitToken;
			}
		}

		private bool CanSubmitToken(object obj)
		{
			return true;
		}

		private void SubmitTokenExecute(object obj)
		{
			//TODO Implement the SubmitToken Method
			ViewState = ProfileViewState.CurrentProfile;
		}
		#endregion //SubmitToken


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

		#endregion //IProfile Implementation
	}
}
