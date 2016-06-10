using System.Threading;

namespace OutlookMatters.Core.Notification
{
    public interface INotification
    {
        void NotifyMattermostRestSuccess();
    }

    public class NotficationService : INotification
    {
        private NotificationWindow _notificationWindow;

        public void NotifyMattermostRestSuccess()
        {

            var thread = new Thread(ShowNotificationWindow);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }
        
        private void ShowNotificationWindow()
        {
            _notificationWindow = new NotificationWindow()
            {
                AllowsTransparency = true,
                ShowInTaskbar = false,
                ShowActivated = false
        };
            _notificationWindow.ShowDialog();
        }
    }
}
