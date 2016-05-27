using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.ViewModels
{
	public class ProfileViewModel : ViewModelBase
	{
		private String _LoginName;

		public String LoginName
		{
			get { return _LoginName; }
			set { _LoginName = value; }
		}



		public override string ToString()
		{
			return "Profile";
		}

		public String SecitonName
		{
			get { return ToString(); }
		}
	}
}
