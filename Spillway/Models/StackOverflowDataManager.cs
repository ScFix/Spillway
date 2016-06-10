using RestSharp;
using Spillway.Interfaces;
using Spillway.Utilities;
//using Spillway.Data.OAuth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spillway.Contracts;

namespace Spillway.Models
{

	public class StackOverflowDataManager : IDataManager
	{
		#region Members
		// This Should be passed into via client_id 
		private const string _clientId = "7246";
		private const string _scope = "read_inbox,no_expiry,write_access,private_info";
		private const string _redirectUrl = "https://stackexchange.com/oauth/login_success ";
		private const string _accessUrl = "https://stackexchange.com/oauth/dialog";
		private const string _requestKey = "TKxyoBaKAvQJy*kNpSXOiA((";
		private const string _baseUrl = "https://api.stackexchange.com";
		private string AccessToken;
		#endregion //Members

		#region Properties
		public User CurrentUser { get; set; }

		private IList<INotification> _UnreadNotifications = new List<INotification>();
		public IList<INotification> UnreadNotifications { get; set; }
		#endregion //Properties

		#region Events

		//public delegate void UserChangedHandler(object sender, EventArgs e);
		public event EventHandler UserChangedEvent;

		#endregion //Events

		#region Methods
		public bool HasCurrentSessionOpen()
		{
			//the most basic of validation, see if there is a stored token
			return !String.IsNullOrEmpty(AccessToken);
		}

		public bool RequestUserVerification()
		{
			try
			{
				System.Diagnostics.Process.Start(_accessUrl + "?client_id=" + _clientId + "&scope=" + _scope + "&redirect_uri=" + _redirectUrl);
				return true;
			}
			catch (Exception e)
			{
				Tracing.Trace(e.Message);
				return false;
			}
		}

		//This will need to be tested before setting it. However this will get me up and running.
		public void SetToken(string accessToken)
		{
			AccessToken = accessToken;
			GetUserInfo();
		}

		public void GetUserInfo()
		{
			var client = new RestClient(_baseUrl);


			var request = new RestRequest("/2.2/me");
			request.AddParameter("order", "desc");
			request.AddParameter("sort", "reputation");
			request.AddParameter("site", "stackoverflow");
			request.AddParameter("filter", "!0Z-Lvgpjwf2JywjvDYD7ipJc3");
			request.AddParameter("access_token", AccessToken);
			request.AddParameter("key", _requestKey);

			var asyncHandle = client.ExecuteAsync<Users>(request, response =>
			{
				Trace.WriteLine(response.Data.Items[0]);

				CurrentUser = response.Data.Items[0];
				UserChangedEvent(this, EventArgs.Empty);

				Spillway.Properties.Settings.Default.Access_Token = this.AccessToken;
				Spillway.Properties.Settings.Default.Save();
			});
		}

		public IList<INotification> RequestUnreadNotifications(IOptions options)
		{
			var client = new RestClient(_baseUrl);


			var request = new RestRequest("/2.2/users/" + CurrentUser.Id + "/notifications/unread");
			//this will eventually have to be expanded to encompass other sites
			request.AddParameter("site", "stackoverflow");
			request.AddParameter("access_token", AccessToken);
			request.AddParameter("key", _requestKey);

			var asyncHandle = client.ExecuteAsync<Users>(request, response =>
			{
				Trace.WriteLine(response.Data.Items[0]);

				CurrentUser = response.Data.Items[0];
				UserChangedEvent(this, EventArgs.Empty);

				Spillway.Properties.Settings.Default.Access_Token = this.AccessToken;
				Spillway.Properties.Settings.Default.Save();
			});
			return null;
		}

		public void StartNotificationFetching()
		{
			//Start the thread that will fetch all of the unread messages
		}
		#endregion //Methods
	}
}
