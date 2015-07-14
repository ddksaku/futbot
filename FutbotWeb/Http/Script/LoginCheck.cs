﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class LoginCheck : Super
    {
        public LoginCheck(Context context)
            : base("LoginCheck", context)
        {

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

                if (web_response.Cookies.Count == 0)
                    throw new RequestException<LoginCheck>("Unable to find Cookie Headers");
            }
            else
                throw new RequestException<LoginCheck>("Unable to find Location Header");
        }
    }
}
