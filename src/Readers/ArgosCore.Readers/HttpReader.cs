using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgosCore.Readers
{
    public class HttpReader : IResourceReader
    {
        private string Url;
        private string Method;
        private System.Net.Http.HttpClient client;
        public HttpReader(string url, string method)
        {
            this.Url = url;
            this.Method = method;
        }

        public void Read(Resource resource)
        {
            var url = ResolveString(Url, resource);
            var method = ResolveString(Method, resource);

            System.Net.Http.HttpMethod HttpMethod = new System.Net.Http.HttpMethod(method);
            if (client == null)
            {
                client = new System.Net.Http.HttpClient();
            }
            System.Diagnostics.Debug.WriteLine(string.Format("HttpReader {0}.Starting read: {1} {2}", resource.Name, HttpMethod.Method, url));
            var sendTask = client.SendAsync(new System.Net.Http.HttpRequestMessage(HttpMethod, url));
            sendTask.ContinueWith((response) =>
            {
                System.Diagnostics.Debug.WriteLine(string.Format("HttpReader {0}.Success: {1}. StatusCode: {2}", resource.Name, !response.IsFaulted, response.Result.StatusCode));
                resource.SetStatus(response.Result.StatusCode.ToString());
            });
        }

        private string ResolveString(string url, Resource resource)
        {
            foreach (var property in resource.Properties)
            {
                url = url.Replace("$" + property.Key + "$", property.Value);
            }
            return url;
        }
    }
}
