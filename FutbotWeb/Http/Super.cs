using System.Net;
using FutbotWeb.Http.Script;
using System.Text;
using System.Web.Script.Serialization;
using System;
using System.IO;

namespace FutbotWeb.Http
{
    /// <summary>
    /// Super class for a scripted web request 
    /// </summary>
    public abstract class Super
    {
        protected Context _context;

        protected byte[] _data;

        public string RequestName { get; private set; }

        public Super(string name, Context context)
        {
            this.RequestName = name;
            this._context = context;
            this._data = null;
        }

        public abstract void Prepare(out HttpWebRequest web_request);
        
        public abstract void Handle(HttpWebResponse web_response);

        public byte[] GetData()
        {
            return this._data;
        }

        public void SetData(string str)
        {
            this._data = ASCIIEncoding.ASCII.GetBytes(str);
        }

        public T ParseJson<T>(string str)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return (T)serializer.Deserialize(str, typeof(T));
        }

        public bool TryParseJson<T>(string str, out T _object)
        {
            try
            {
                _object = this.ParseJson<T>(str);
                return true;
            }
            catch
            {
                _object = default(T);
                return false;
            }
        }

        public string EncodeJson<T>(T _object)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(_object);
        }

        public bool TryEncodeJson<T>(T _object, out string json)
        {
            try
            {
                json = this.EncodeJson(_object);
                return true;
            }
            catch
            {
                json = null;
                return false;
            }
        }

        public string GetUnixTime()
        {
            return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public void AppendCookies(CookieCollection cookies)
        {
            if (cookies != null && cookies.Count > 0)
            {
                foreach (Cookie cookie in cookies)
                    this._context.Fifa.cookie_container.Add(cookie);
            }
        }
        public CookieContainer GetCookies()
        {
            return this._context.Fifa.cookie_container;
        }
        public void ClearCookies()
        {
            this._context.Fifa.cookie_container = new CookieContainer();
        }

        public void InitGet(ref HttpWebRequest web_request, bool use_cookies = true)
        {
            if (use_cookies)
                web_request.CookieContainer = this._context.Fifa.cookie_container;

            web_request.KeepAlive = true;
            web_request.UserAgent = Constants.user_agent;
            web_request.Method = Constants.Method.GET.ToString();
            web_request.AllowAutoRedirect = false;
            web_request.ServicePoint.Expect100Continue = false;

            web_request.Timeout = Constants.timeout;

            if (Constants.use_decoding)
            {
                web_request.Headers.Add("Accept-Encoding", "gzip,deflate");
                web_request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
        }
        public void InitPost(ref HttpWebRequest web_request, bool use_cookies = true)
        {
            if (use_cookies)
                web_request.CookieContainer = this._context.Fifa.cookie_container;

            web_request.KeepAlive = true;
            web_request.UserAgent = Constants.user_agent;
            web_request.Method = Constants.Method.POST.ToString();
            web_request.AllowAutoRedirect = false;
            web_request.ServicePoint.Expect100Continue = false;

            web_request.Timeout = Constants.timeout;

            if (Constants.use_decoding)
            {
                web_request.Headers.Add("Accept-Encoding", "gzip,deflate");
                web_request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
        }
        public void InitXmlHeader(ref HttpWebRequest web_request, string ut_route = null, Constants.XMethod x_method = Constants.XMethod.INVALID)
        {
            web_request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            web_request.Headers.Add("X-UT-Embed-Error", "true");
            if (ut_route != null)
                web_request.Headers.Add("X-UT-Route", ut_route);
            if (x_method != Constants.XMethod.INVALID)
                web_request.Headers.Add("X-HTTP-Method-Override", x_method.ToString());
        }
        public void InitNucHeader(ref HttpWebRequest web_request)
        {
            web_request.Headers.Add("Easw-Session-Data-Nucleus-Id", this._context.Fifa.nucleus_id);
        }

        public void InitPhishingToken(ref HttpWebRequest web_request)
        {
            web_request.Headers.Add("X-UT-PHISHING-TOKEN", this._context.Fifa.phising_token);
        }

        public void InitSessionId(ref HttpWebRequest web_request)
        {
            web_request.Headers.Add("X-UT-SID", this._context.Fifa.session_id);
        }

        public string ReadResponse(ref HttpWebResponse web_response)
        {
            StreamReader reader = new StreamReader(web_response.GetResponseStream());
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }

        public string EncodePost(params string[] args)
        {
            string result = "?";
            bool is_name = true;
            foreach (string str in args)
            {
                if (result.Length != 1 && is_name)
                    result += "&";
                else if (!is_name)
                    result += "=";

                result += str;

                is_name ^= true;
            }

            return result;
        }
    }
}
