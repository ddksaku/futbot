using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class Login : Super
    {
        public Login(Context context)
            : base("Login", context)
        {

        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(this._context.Fifa.next_url);

            this.InitPost(ref web_request);
            web_request.ContentType = Constants.www_encoded_content;

            this.SetData(string.Format(Constants.login_parameter, this._context.Authentication.UserName, this._context.Authentication.Password));


        }
        public override void Handle(HttpWebResponse web_response)
        {
            string header = web_response.GetResponseHeader("Location");

            if (header != null)
            {
                this._context.Fifa.next_url = header;

                Cookie cookie = web_response.Cookies["webun"];
                if (cookie != null)
                {
                    if (!cookie.Value.Contains(this._context.Authentication.UserName))
                        throw new RequestException<Login>("Wrong Login Info");
                }
                else
                    throw new RequestException<Login>("Unable to find webun Cookie");

            }
            else
                throw new RequestException<Login>("Unable to find Location Header");
        }
    }
}
