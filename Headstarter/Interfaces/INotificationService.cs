using Headstarter.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Headstarter.Interfaces
{
    public interface INotificationService
    {
        Task<IList<Notification>> GetUnseenMessagesAfter(int Id);
        bool MarkAsRead(int Id);
    }
}
