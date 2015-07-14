using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class Authentication : Super
    {
        public Authentication(Context context)
            : base("Authentication", context)
        {

        }

        string parse_auth_json()
       {
            FutbotWeb.Json.Authentication.RootObject auth = new FutbotWeb.Json.Authentication.RootObject();

            auth.clientVersion = 1;
            auth.identification = new FutbotWeb.Json.Authentication.Identification();
            auth.identification.authCode = "";

            auth.isReadOnly = false;
            auth.locale = Constants.auth_locale;
            auth.method = "authcode";
            auth.nuc = Convert.ToInt64(this._context.Fifa.nucleus_id);
            auth.nucleusPersonaDisplayName = this._context.Fifa.persona_name;
            auth.nucleusPersonaId = this._context.Fifa.persona_id;
            auth.nucleusPersonaPlatform = this._context.Fifa.plattform;
            auth.priorityLevel = 4;
            auth.sku = "FUT14WEB";

            return this.EncodeJson(auth);
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(Constants.authenticate);

            this.InitPost(ref web_request);
            this.InitXmlHeader(ref web_request, this._context.Fifa.utas_url);
            this.InitNucHeader(ref web_request);

            web_request.ContentType = Constants.json_content;

            this.SetData(this.parse_auth_json());
        }

        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var connect_info = this.ParseJson<FutbotWeb.Json.ConnectionInfo.RootObject>(json);

            if (connect_info != null)
            {
                if (connect_info.sid != null)
                    this._context.Fifa.session_id = connect_info.sid;
                else
                    throw new RequestException<Authentication>("Unable to find session id");
            }
            else
                throw new RequestException<Authentication>("Unable to find session id");
        }
    }
}
