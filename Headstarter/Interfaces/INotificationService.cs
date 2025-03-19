using Headstarter.Protos;

namespace Headstarter.Interfaces
{
    public interface INotificationService
    {
        Task<ICollection<Notification>> GetUnseenMessagesAfter(int Id);
        Task<bool> MarkAsRead(int Id);
    }
}
