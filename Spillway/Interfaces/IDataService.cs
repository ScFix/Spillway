using Spillway.Models;
using System;
using System.Collections.Generic;

namespace Spillway.Interfaces
{
	/// <summary>
	///  This class wraps the data access from the other library and will also store all of the models for other classes to access from
	/// </summary>
	public interface IDataService
	{
		User CurrentUser { get; set; }

		bool RequestUserVerification();

		bool HasCurrentSessionOpen();

		void SetToken(string accessToken);

		IList<INotification> RequestUnreadNotifications(IOptions options);

		event EventHandler UserChangedEvent;
	}
}