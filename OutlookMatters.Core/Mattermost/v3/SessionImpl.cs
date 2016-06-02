using System;
using System.Collections.Generic;
using System.Linq;
using OutlookMatters.Core.Chat;
using OutlookMatters.Core.Mattermost.v3.Interface;

namespace OutlookMatters.Core.Mattermost.v3
{
    public class SessionImpl : ISession
    {
        private readonly IRestService _restService;
        private readonly Uri _uri;
        private readonly string _token;
        private readonly string _userId;
        private readonly string _teamId;
        private readonly IChatChannelFactory _channelFactory;

        public SessionImpl(IRestService restService, Uri uri, string token, string userId, string teamId, IChatChannelFactory channelFactory)
        {
            _restService = restService;
            _uri = uri;
            _token = token;
            _userId = userId;
            _teamId = teamId;
            _channelFactory = channelFactory;
        }

        public IEnumerable<IChatChannel> GetChannels()
        {
            return _restService.GetChannelList(_uri, _token, _teamId)
                .Channels.Select(c => _channelFactory.NewInstance(_restService, _uri, _token, _userId, _teamId, c));
        }

        public IChatChannel GetChannel(string channelId)
        {
            return _channelFactory.NewInstance(_restService, _uri, _token, _userId, _teamId,
                new Channel {ChannelId = channelId});
        }

        public IChatPost GetPost(string postId)
        {
            throw new NotImplementedException();
        }
    }
}