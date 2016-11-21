using RestSharp;
using Spillway.Interfaces;
using Spillway.Models;
using Spillway.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Spillway.Services
{
    public class StackOverflowDataService : IDataService
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
        private RestClient client = new RestClient(_baseUrl);

        #endregion Members

        #region Properties

        public User CurrentUser { get; set; }

        private IList<INotification> _UnreadNotifications = new List<INotification>();
        public IList<INotification> UnreadNotifications { get; set; }

        #endregion Properties

        #region Events

        // public delegate void UserChangedHandler(object sender, EventArgs e);
        public event EventHandler UserChangedEvent;

        public event StackNotifyHandler IncomingNotificationsEvent;

        #endregion Events

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

        // This will need to be tested before setting it. However this will get me up and running.
        public void SetToken(string accessToken)
        {
            AccessToken = accessToken;
            GetUserInfo();
        }

        private void GetUserInfo()
        {
            var request = new RestRequest("/2.2/me");
            request.AddParameter("order", "desc");
            request.AddParameter("sort", "reputation");
            request.AddParameter("site", "stackoverflow");
            request.AddParameter("filter", "!40DJoyoFxZjgBkP1(");
            request.AddParameter("access_token", AccessToken);
            request.AddParameter("key", _requestKey);

            var asyncHandle = client.ExecuteAsync<Users>(request, response =>
            {
                Trace.WriteLine(response.Data.Items[0]);

                CurrentUser = response.Data.Items[0];
                // check to make sure someone has subscribed to this.
                UserChangedEvent?.Invoke(this, EventArgs.Empty);

                Spillway.Properties.Settings.Default.Access_Token = this.AccessToken;
                Spillway.Properties.Settings.Default.Save();
                //start fetching the notifications in a background thread!
            });
        }

        public IList<INotification> RequestUnreadNotifications(IOptions options)
        {
            var request = new RestRequest("/2.2/inbox/unread");
            //var request = new RestRequest("/2.2/inbox/");

            // request.AddParameter("site", "stackoverflow");
            request.AddParameter("filter", "!*L6Kz8C2J(yJQxNo");
            request.AddParameter("access_token", AccessToken);
            request.AddParameter("key", _requestKey);
            // Todo(Matthew): finish this response
            var asyncHandle = client.ExecuteAsync<Mail>(request, response =>
            {
                //Trace.WriteLine(response.Data.Items[0]);

                var mail = response.Data;

#if DEBUG
                mail.Items.ForEach((notification) =>
                {
                    Debug.WriteLine(notification.Link);
                });
#endif
                // check to make sure someone has subscribed to this.
                //UserChangedEvent?.Invoke(this, EventArgs.Empty);
                IncomingNotificationsEvent?.Invoke(this, new StackArgs() { Notifications = (List<Notification>)mail.Items });
            });

            return null;
        }

        #endregion Methods
    }
}