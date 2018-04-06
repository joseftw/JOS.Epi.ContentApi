using System;
using System.IO;
using System.Linq;
using System.Web;
using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace JOS.Epi.ContentApi
{
    public class ContentApiModule : IHttpModule
    {
        private HttpApplication _current;

        public void Init(HttpApplication context)
        {
            _current = context;
            _current.BeginRequest += BeginRequest;
        }

        public void Dispose() { }

        private void BeginRequest(object sender, EventArgs e)
        {
            var shouldSerializeStrategy = ServiceLocator.Current.GetInstance<IShouldSerializeResponseStrategy>();

            if (!shouldSerializeStrategy.Execute(this._current.Request))
            {
                return;
            }

            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
            var contentData = urlResolver.Route(new UrlBuilder(_current.Context.Request.Path));
            if (contentData == null)
            {
                return;
            }

            var serializer = ServiceLocator.Current.GetInstance<IContentApiSerializer>();
            var result = serializer.Serialize(contentData);

            this._current.Response.ContentType = _current.Request.AcceptTypes?.First();
            this._current.Response.Write(result);
            this._current.Response.Flush();
            this._current.Response.End();
        }
    }
}
