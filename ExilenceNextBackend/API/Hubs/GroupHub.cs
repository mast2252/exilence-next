﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace API.Hubs
{
    public partial class BaseHub : Hub
    {
        [Authorize]
        public async Task<GroupModel> JoinGroup(string groupName)
        {
            var groupModel = await _groupService.JoinGroup(ConnectionId, groupName);
            await Groups.AddToGroupAsync(ConnectionId, groupName);
            await Log($"{ConnectionId}´joined group {groupName}");
            return groupModel;
        }

        public async Task<string> LeaveGroup(string groupName)
        {
            await _groupService.LeaveGroup(ConnectionId, groupName);
            await Groups.RemoveFromGroupAsync(ConnectionId, groupName);
            await Log($"{ConnectionId}´left group {groupName}");
            return groupName;
        }
    }
}
