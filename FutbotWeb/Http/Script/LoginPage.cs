using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FutbotWeb.Http.Script
{
    public class LoginPage : Super
    {
        public LoginPage(Context context)
            : base("LoginPage", context)
        {

        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            this.ClearCookies();

            web_request = (HttpWebRequest)HttpWebRequest.Create(Constants.login_page);

            this.InitGet(ref web_request);
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string header = web_response.GetResponseHeader("Location");

            if (header != null)
            {
                this._context.Fifa.next_url = header;
            }
            else
                throw new RequestException<LoginPage>("Unable to find Location Header");
        }
    }
}
