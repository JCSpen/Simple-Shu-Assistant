using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Shu_Asistamt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sessionId = 12345;
            IamAuthenticator authenticator = new IamAuthenticator(
            apikey: "{0LTlYh3-Kt6uIe1eQ8ytijsuzdnEKq_jUs8pff49fXeM}"
            );

            AssistantService assistant = new AssistantService("2021-06-14", authenticator);
            assistant.SetServiceUrl("{https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/9105472d-0990-4acc-a349-661d4607d608}");

            var result = assistant.CreateSession(
                assistantId: "{environmentId}"
                );

            Console.WriteLine(result.Response);

            sessionId = result.Result.SessionId;
        }
    }
}
