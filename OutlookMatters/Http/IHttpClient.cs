﻿using System;

namespace OutlookMatters.Http
{
    public interface IHttpClient
    {
        IHttpRequest Request(Uri url);
    }
}
