using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Text.Unicode;


namespace EFDemo
{
    public class MessageFormat
    {
        public int errorCode { get; set; }
        public bool status { get; set; }
        public Dictionary<string, dynamic> data { get; set; } = new Dictionary<string, dynamic>();

        public MessageFormat()
        {
            errorCode = 300;
            status = false;
        }
        public string ReturnJson()
        {
            var options = new JsonSerializerOptions
            {
                //Encoder = JavaScriptEncoder.Create(UnicodeRanges.CjkUnifiedIdeographs, UnicodeRanges.CjkUnifiedIdeographsExtensionA),
                //Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs),
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All, UnicodeRanges.All),
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
    public class RegisterMessage : MessageFormat
    {
        public RegisterMessage()
        {
            data.Add("user_id", null);
            errorCode = 300;
            status = false;
        }
    }


}
