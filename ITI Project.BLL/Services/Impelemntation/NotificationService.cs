using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using ITI_Project.DAL.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI_Project.BLL.Services.Impelemntation
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepo _notificationRepository;
        private readonly IFollowRepo followRepo;
        private readonly IMapper _mapper; 

        private readonly IVendorRepo _vendorRepository;

        public IVendorRepo VendorRepository => _vendorRepository;

        public NotificationService(IVendorRepo vendorRepository, INotificationRepo notificationRepository , IFollowRepo followRepo)
        {
            _vendorRepository = vendorRepository;
            _notificationRepository = notificationRepository;
            this.followRepo = followRepo;
        }

        public void SendNotificationToFollowers(int vendorId, string message)
        {
            try
            {

                var followers = followRepo.GetAllFollowers(vendorId);

                foreach (var follower in followers)
                {
                    var notification = new Notification
                    {
                        CustomerId = follower.CustomerId,  
                        Message = message,
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    };

                    _notificationRepository.AddNotification(notification);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending notifications to followers", ex);
            }
        }



        public IEnumerable<AddNotificationVM> GetNotificationsForCustomer(int customerId)
        {
            try
            {
                var notifications = _notificationRepository.GetNotificationsForCustomer(customerId);
                return _mapper.Map<IEnumerable<AddNotificationVM>>(notifications);
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
                _notificationRepository.MarkAsRead(notificationId);
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while marking the notification as read", ex);
            }
        }
    }

}
