using Headstarter.Interfaces;
using Headstarter.Protos;

namespace UnitTests.Mocks
{
    public class NotificationService : INotificationService
    {
        public Task<bool> MarkAsRead(int Id)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<Notification>> INotificationService.GetUnseenMessagesAfter(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
