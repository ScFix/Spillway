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
        private IOptions options = null;
        #endregion Members

        #region Constructors

        public DataTimingService(IDataService dataService, IOptions options = null, int refreshPeriod = 0)
        {
            this.dataService = dataService;
            this.options = options;
            this.refreshPeriod = refreshPeriod < lowestRefreshInterval ? lowestRefreshInterval : refreshPeriod;
            if (dataService != null)
            {
                refreshTimer = new Timer(this.callData, null, 0, this.refreshPeriod);
            }
        }

        #endregion Constructors

        #region Methods

        // ticks are in milli seconds
        public void SetRefreshRate(int ticks)
        {
            // this causes the refresh to immediately ping the servers
            refreshPeriod = ticks < lowestRefreshInterval ? lowestRefreshInterval : ticks;
            refreshTimer.Change(0, refreshPeriod);
        }

        public void CancelTimer()
        {
            refreshTimer.Dispose();
        }

        private void callData(object state)
        {
            dataService.RequestUnreadNotifications(options);
        }

        #endregion Methods
    }
}