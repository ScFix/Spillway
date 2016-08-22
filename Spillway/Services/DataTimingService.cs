using Spillway.Interfaces;
using System.Threading;

namespace Spillway.Services
{
    public class DataTimingService
    {
        #region Members

        private Timer refreshTimer = null;
        public const int lowestRefreshInterval = 300000; // lowest you can refresh is set to 5 minutes
        private int refreshPeriod = 0;
        private IDataService dataService = null;

        #endregion Members

        #region Constructors

        public DataTimingService(IDataService dataService, IOptions options, int period = 0)
        {
            this.dataService = dataService;
            refreshPeriod = period;
            if (dataService != null)
            {
                refreshTimer = new Timer(calldata, null, 0, refreshPeriod);
            }
        }

        #endregion Constructors

        #region Methods

        // ticks are in milli seconds
        public void SetRefreshRate(int ticks)
        {
            refreshTimer.Change(0, ticks);
        }

        public void CancelTimer()
        {
            refreshTimer.Dispose();
        }

        private void calldata(object state)
        {
            dataService.RequestUnreadNotifications(null);
        }

        #endregion Methods
    }
}