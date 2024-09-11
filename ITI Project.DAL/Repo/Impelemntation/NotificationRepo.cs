using ITI_Project.DAL.DB.ApplicationDB;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.DAL.Repo.Impelemntation
{
    public class NotificationRepo:INotificationRepo
    {
        private readonly ApplicationDbContext db;

        public NotificationRepo(ApplicationDbContext context)
        {
            db = context;
        }

        public void AddNotification(Notification notification)
        {
            try
            {
                db.Notifications.Add(notification);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle or log exception
                throw new Exception("An error occurred while adding the notification", ex);
            }
        }

        public IEnumerable<Notification> GetNotificationsForCustomer(int customerId)
        {
            try
            {
                return db.Notifications
                         .Where(n =>
                            (n.CustomerId == customerId) && (n.IsRead==false) 
                         )
                         .ToList();
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while retrieving notifications", ex);
            }
        }


        public void MarkAsRead(int notificationId)
        {
            try
            {
                var notification = db.Notifications.Find(notificationId);
                if (notification != null)
                {
                    notification.IsRead = true;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while marking the notification as read", ex);
            }
        }
    }

}

