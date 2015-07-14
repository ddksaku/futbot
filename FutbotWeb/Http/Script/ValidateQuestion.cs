using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class ValidateQuestion : Super
    {
        public ValidateQuestion(Context context)
            : base("ValidateQuestion", context)
        {

        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(Constants.validate);

            this.InitPost(ref web_request);
            this.InitXmlHeader(ref web_request, this._context.Fifa.utas_url);
            this.InitNucHeader(ref web_request);
            this.InitSessionId(ref web_request);

            web_request.ContentType = Constants.www_encoded_content;

            this.SetData("answer=" + this._context.Authentication.SecretAnswer);
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var validate_info = this.ParseJson<FutbotWeb.Json.ValidateInfo.RootObject>(json);

            if (validate_info != null)
            {
                if (validate_info.code != null && validate_info.code == "200")
                {
                    this._context.Fifa.phising_token = web_response.Cookies[0].Value;
                    this._context.Fifa.online = true;
                }
                else
                    throw new RequestException<ValidateQuestion>("Unable to validate question");
            }
            else
                throw new RequestException<ValidateQuestion>("Unable to validate question");
        }
    }
}
