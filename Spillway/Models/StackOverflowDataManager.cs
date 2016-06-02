using Spillway.Interfaces;
using Spillway.Utilities;
//using Spillway.Data.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.Models
{

	public class StackOverflowDataManager : IDataManager
	{
		// This Should be passed into via client_id 
		private const string _clientId = "7246";
		private const string _scope = "read_inbox,noexpiry,write_access,private_info";
		private const string _redirectUrl = "www.stackexchange.com/oauth/login_success";
		private const string _accessUrl = "https://stackexchange.com/oauth/dialog";
		private const string _requestKey = "TKxyoBaKAvQJy*kNpSXOiA((";
		private string AccessToken;



		public bool HasCurrentSessionOpen()
		{
			//the most basic of validation, see if there is a stored token
			return !String.IsNullOrEmpty(AccessToken);
		}

		public bool RequestUserVerification()
		{
			try
			{
				System.Diagnostics.Process.Start(_accessUrl + "?client_id=" + _clientId + "&scope=" + _scope + "&redirect_uri" + _redirectUrl);
				return true;
			}
			catch (Exception e)
			{
				Tracing.Trace(e.Message);
				return false;
			}
		}


	}
}
