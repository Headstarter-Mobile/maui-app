using Headstarter.Protos;

namespace Headstarter.Services;

public interface INotificationService
{
    public static readonly string TitleKey = "title";
    public static readonly string MessageKey = "message";

    Task<ICollection<Notification>> GetUnseenMessagesAfter(int Id);
    Task<bool> MarkAsRead(int Id);
}
