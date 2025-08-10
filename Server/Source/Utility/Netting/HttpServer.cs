using System;
using System.Net;

namespace ProjectMethylamine.Source.Utility.Netting
{
    public class HttpServer
    {
        private readonly HttpListener _listener;
        private readonly string[] _prefixes;
        private readonly Func<HttpListenerContext, Task> _responderMethod;

        public HttpServer(string[] prefixes, Func<HttpListenerContext, Task> method)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException("HttpListener is not supported on this platform.");

            _listener = new HttpListener();
            _prefixes = prefixes ?? throw new ArgumentNullException(nameof(prefixes));
            foreach (string prefix in _prefixes)
                _listener.Prefixes.Add(prefix);

            _responderMethod = method ?? throw new ArgumentNullException(nameof(method));
        }

        public async Task StartAsync()
        {
            _listener.Start();

            while (_listener.IsListening)
            {
                var context = await _listener.GetContextAsync();
                _ = Task.Run(() => _responderMethod(context));
            }
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
