using ITI_Project.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Interface
{
    public interface INotificationRepo
    {
        public void AddNotification(Notification notification);
        public IEnumerable<Notification> GetNotificationsForCustomer(int customerId);
        public void MarkAsRead(int notificationId);
    }
}
