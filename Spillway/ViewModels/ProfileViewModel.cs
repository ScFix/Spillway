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
	public class ProfileViewModel : ViewModelBase, ISection
	{

		#region SignIn
		protected ICommand _SignIn = null;
		public ICommand SignIn
		{
			get
			{
				if (_SignIn == null)
				{
					_SignIn = new RelayCommand(SignInExecute, CanSignIn);
				}
				return _SignIn;
			}
		}

		private bool CanSignIn(object obj)
		{
			return true;
		}

		private void SignInExecute(object obj)
		{
			//TODO Implement the SignIn Method
			Debug.WriteLine("SignIn Executed");
		}
		#endregion //SignIn

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
	}
}
