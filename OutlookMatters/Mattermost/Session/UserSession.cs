﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OutlookMatters.Http;

namespace OutlookMatters.Mattermost.Session
{
    public class UserSession : ISession
    {
        private readonly Uri _baseUri;
        private readonly IHttpClient _httpClient;
        private readonly string _token;
        private readonly string _userId;

        public UserSession(Uri baseUri, string token, string userId, IHttpClient httpClient)
        {
            _baseUri = baseUri;
            _token = token;
            _userId = userId;
            _httpClient = httpClient;
        }

        public void CreatePost(string channelId, string message, string rootId = "")
        {
            var post = new Post {channel_id = channelId, message = message, user_id = _userId, root_id = rootId};
            var postUrl = PostUrl(channelId);
            _httpClient.Request(postUrl)
                .WithContentType("text/json")
                .WithHeader("Authorization", "Bearer " + _token)
                .PostAndForget(JsonConvert.SerializeObject(post));
        }

        public Post GetPostById(string postId)
        {
            var postUrl = "api/v1/posts/" + postId;
            var url = new Uri(_baseUri, postUrl);
            using (var response = _httpClient.Request(url)
                .WithHeader("Authorization", "Bearer " + _token)
                .Get())
            {
                var thread = JsonConvert.DeserializeObject<PostingThread>(response.GetPayload());
                return thread.posts[postId];
            }
        }

        private Uri PostUrl(string channelId)
        {
            var postUrl = "api/v1/channels/" + channelId + "/create";

            var url = new Uri(_baseUri, postUrl);
            return url;
        }

        private struct PostingThread
        {
            public string[] order;
            public Dictionary<string, Post> posts;
        }
    }
}