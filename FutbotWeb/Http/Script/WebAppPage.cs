using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace FutbotWeb.Http.Script
{
    class WebAppPage : Super
    {
        public WebAppPage(Context context)
            : base("WebAppPage", context)
        {

        }

        public override void Prepare(out HttpWebRequest web_request)
        {
            web_request = (HttpWebRequest)HttpWebRequest.Create(this._context.Fifa.next_url);

            this.InitGet(ref web_request);
        }

        public bool set_nucleus_id(string html)
        {
            string find_string = "userid : \"";

            int index = html.IndexOf(find_string);
            if(index == -1)
                return false;

            index += find_string.Length;

            int end_index = html.IndexOf('"', index);
            if (end_index == -1)
                return false;

            this._context.Fifa.nucleus_id = html.Substring(index, end_index - index);

            return true;
        }

        public override void Handle(HttpWebResponse web_response)
        {
            string str = this.ReadResponse(ref web_response);

            if (!set_nucleus_id(str))
                throw new RequestException<WebAppPage>("Unable to find nucleus id");
        }
    }
}
