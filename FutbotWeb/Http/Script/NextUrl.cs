using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class NextUrl : Super
    {
        public NextUrl(Context context, bool check_cookies = true)
            : base("NextUrl", context)
        {
            this.check_cookies_ = check_cookies;
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(this._context.Fifa.next_url);

            this.InitGet(ref web_request);
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string header = web_response.GetResponseHeader("Location");

            if (header != null)
            {
                this._context.Fifa.next_url = header;

                if (web_response.Cookies.Count == 0 && this.check_cookies_)
                    throw new RequestException<NextUrl>("Unable to find Cookie Headers");
            }
            else
                throw new RequestException<NextUrl>("Unable to find Location Header");
        }

        bool check_cookies_;
    }
}
