﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LNUbiz.BLL.DTO.Notification;
using LNUbiz.BLL.Interfaces.Notifications;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.DAL.Entities;
using LNUbiz.DAL.Repositories;

namespace LNUbiz.BLL.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly IUserManagerService _userManagerService;
        public NotificationService(IMapper mapper, IRepositoryWrapper repoWrapper, IUserManagerService userManagerService)
        {
            _mapper = mapper;
            _repoWrapper = repoWrapper;
            _userManagerService = userManagerService;
        }

        public async Task<IEnumerable<NotificationTypeDTO>> GetAllNotificationTypesAsync()
        {
            var notificationTypes = await _repoWrapper.NotificationTypes.GetAllAsync();

            return _mapper.Map<IEnumerable<NotificationTypeDTO>>(notificationTypes);
        }

        public async Task<IEnumerable<UserNotificationDTO>> GetAllUserNotificationsAsync(string userId)
        {
            var userNotitfications = await _repoWrapper.UserNotifications.GetAllAsync(un => un.OwnerUserId == userId);

            return _mapper.Map<IEnumerable<UserNotificationDTO>>(userNotitfications.OrderByDescending(d => d.CreatedAt));
        }

        public async Task<IEnumerable<UserNotificationDTO>> AddListUserNotificationAsync(IEnumerable<UserNotificationDTO> userNotificationsDTO)
        {
            bool addedSuccessfully = true;
            var resultUserNotifications = new List<UserNotification>(); 
            foreach (var userNotificationDTO in userNotificationsDTO)
            {
                if ((await _userManagerService.FindByIdAsync(userNotificationDTO.OwnerUserId)) != null)
                {
                    UserNotification userNotification = _mapper.Map<UserNotification>(userNotificationDTO);
                    NotificationType notificationType = await _repoWrapper.NotificationTypes.GetFirstOrDefaultAsync(nt => nt.Id == userNotificationDTO.NotificationTypeId);
                    if (notificationType != null)
                    {
                        userNotification.Checked = false;
                        userNotification.CreatedAt = DateTime.Now;
                        resultUserNotifications.Add(userNotification);
                        await _repoWrapper.UserNotifications.CreateAsync(userNotification);
                    }
                    else
                    {
                        addedSuccessfully = false;
                    }
                }
                else
                {
                    addedSuccessfully = false;
                }
            }
            if (addedSuccessfully)
            {
                await _repoWrapper.SaveAsync();
                return _mapper.Map<IEnumerable<UserNotificationDTO>>(resultUserNotifications);
            }
            throw new InvalidOperationException();
        }

        public async Task<bool> SetCheckForListNotificationAsync(string userId)
        {
            bool ChangedSuccessfully = false;
            if (!string.IsNullOrEmpty(userId) && await _userManagerService.FindByIdAsync(userId) != null)
            {
                var userNotifications = await _repoWrapper.UserNotifications.GetAllAsync(nt => nt.OwnerUserId == userId);
                if (userNotifications.ToList().Count != 0)
                {
                    foreach (var userNotification in userNotifications)
                    {
                        if (!userNotification.Checked)
                        {
                            userNotification.Checked = true;
                            userNotification.CheckedAt = DateTime.Now;
                            _repoWrapper.UserNotifications.Update(userNotification);
                        }
                    }
                    await _repoWrapper.SaveAsync();
                    ChangedSuccessfully = true;
                }
            }

            return ChangedSuccessfully;
        }

        public async Task<bool> RemoveUserNotificationAsync(int notificationId)
        {
            bool removedSuccessfully = false;
            var userNotification = await _repoWrapper.UserNotifications.GetFirstOrDefaultAsync(nt => nt.Id == notificationId);
            if (userNotification != null)
            {
                _repoWrapper.UserNotifications.Delete(userNotification);
                await _repoWrapper.SaveAsync();
                removedSuccessfully = true;
            }
            return removedSuccessfully;
        }

        public async Task<bool> RemoveAllUserNotificationAsync(string userId)
        {
            bool removedSuccessfully = false;
            var userNotifications = await _repoWrapper.UserNotifications.GetAllAsync(nt => nt.OwnerUserId == userId);
            if (userNotifications.ToList().Count != 0)
            {
                foreach (var userNotification in userNotifications)
                {
                    _repoWrapper.UserNotifications.Delete(userNotification);
                }
                await _repoWrapper.SaveAsync();
                removedSuccessfully = true;
            }
            return removedSuccessfully;
        }
    }
}
