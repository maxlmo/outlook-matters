﻿using System;
using OutlookMatters.Core.Chat;
using OutlookMatters.Core.Mattermost.Interface;

namespace OutlookMatters.Core.Mattermost
{
    public interface IChatPostFactory
    {
        IChatPost NewInstance(Uri baseUri, string token, string userId, Post posts);
    }
}