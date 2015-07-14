using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class QuestionPage : Super
    {
        public QuestionPage(Context context)
            : base("QuestionPage", context)
        {
        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(string.Format(Constants.question,this.GetUnixTime()));

            this.InitGet(ref web_request);
            this.InitXmlHeader(ref web_request, this._context.Fifa.utas_url);
            this.InitNucHeader(ref web_request);
            this.InitSessionId(ref web_request);
        }
        public override void Handle(HttpWebResponse web_response)
        {
            string json = this.ReadResponse(ref web_response);

            var question_info = this.ParseJson<FutbotWeb.Json.QuestionInfo.RootObject>(json);

            if (question_info != null)
            {
                if (question_info.question == null)
                    throw new RequestException<QuestionPage>("Unable to find question");
            }
            else
                throw new RequestException<QuestionPage>("Unable to find question");
        }

    }
}
