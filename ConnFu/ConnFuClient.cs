using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ConnFu
{
    public class ConnFuClient
    {
        public class TwitterEventArgs : EventArgs
        {
            public string From { get; set; }
            public string Content { get; set; }
        };
        public delegate void TwitterHandler(object sender, TwitterEventArgs ev);
        public event TwitterHandler Twitter;

        //public const string URI = "https://stream.connfu.com/voice";
        public const string URI = "http://www.google.com";
        public string Token { get; set; }

        public ConnFuClient(string token)
        {
            Token = token;
        }

        private class ReadState
        {
            public ConnFuClient Client { get; set; }
            public Stream Stream { get; set; }
            public byte[] Buffer { get; set; }
        }

        public void Start()
        {
            var request = WebRequest.Create(URI);

            var stream = request.GetResponse().GetResponseStream();

            var buffer = new byte[1024];
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReadCallBack), new ReadState
            {
                Client = this,
                Stream = stream,
                Buffer = buffer
            });
        }

        private static void ReadCallBack(IAsyncResult asyncResult)
        {
            var state = (ReadState)asyncResult.AsyncState;
            var client = state.Client;
            var stream = state.Stream;

            int read = stream.EndRead(asyncResult);

            client.Twitter(client, new TwitterEventArgs { From = "test", Content = "tweet" });
        }
    }
}
