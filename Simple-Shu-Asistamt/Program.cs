using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Shu_Asistamt
{
    internal class Program
    {
        // Simple Shu assistant
            //"apikey": "0LTlYh3-Kt6uIe1eQ8ytijsuzdnEKq_jUs8pff49fXeM",
               //"iam_apikey_description": "Auto-generated for key crn:v1:bluemix:public:conversation:eu-gb:a/f5532d34a1324f0fa245f2c4399ae5ea:9105472d-0990-4acc-a349-661d4607d608:resource-key:c39c4170-0c37-48ad-bc10-a2bacb61cfb1",
            //"iam_apikey_name": "Auto-generated service credentials",
  //"iam_role_crn": "crn:v1:bluemix:public:iam::::serviceRole:Manager",
  //"iam_serviceid_crn": "crn:v1:bluemix:public:iam-identity::a/f5532d34a1324f0fa245f2c4399ae5ea::serviceid:ServiceId-455ef5af-5f8b-49ca-a533-7cbf3f807127",
  //"url": "https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/9105472d-0990-4acc-a349-661d4607d608"

    static void Main(string[] args)
        {



            IamAuthenticator authenticator = new IamAuthenticator(
            apikey: "0LTlYh3-Kt6uIe1eQ8ytijsuzdnEKq_jUs8pff49fXeM"
            );

            AssistantService assistant = new AssistantService("2023-01-17", authenticator);
            assistant.SetServiceUrl("https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/9105472d-0990-4acc-a349-661d4607d608");

            var result = assistant.CreateSession(
            assistantId: "74e78bca-b878-4493-92ad-f31e048b92cd"
            );

            Console.WriteLine(result.Response);

            var sessionId = result.Result.SessionId;



            var result2 = assistant.Message(
             assistantId: "74e78bca-b878-4493-92ad-f31e048b92cd",
             sessionId ,
             input: new MessageInput()
             {
                 Text = "I want to learn about cyber security"
             }
                     );

            Console.WriteLine(result2.Response);
        }
    }
}
