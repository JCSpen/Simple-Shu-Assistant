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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using IBM.Cloud.SDK.Core.Http;
using static System.Net.Mime.MediaTypeNames;

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
            // extract the source titles and links


            string userQuery = " ";
            IamAuthenticator authenticator = new IamAuthenticator(
            apikey: "0LTlYh3-Kt6uIe1eQ8ytijsuzdnEKq_jUs8pff49fXeM"
            );

            AssistantService assistant = new AssistantService("2023-01-17", authenticator);
            assistant.SetServiceUrl("https://api.eu-gb.assistant.watson.cloud.ibm.com/instances/9105472d-0990-4acc-a349-661d4607d608");

            var result = assistant.CreateSession(
            assistantId: "74e78bca-b878-4493-92ad-f31e048b92cd"
            );
            List<string> sourceTitles = new List<string>();
            List<string> sourceLinks = new List<string>();
            List<string> lables = new List<string>();
            bool resolved = false;

            var sessionId = result.Result.SessionId;
            string mainTitle = "";



            while (userQuery != "0")
            {

                Console.WriteLine("Please Ask me a question :");
                userQuery = Console.ReadLine();


                var result2 = assistant.Message(
                 assistantId: "74e78bca-b878-4493-92ad-f31e048b92cd",
                 sessionId,
                 input: new MessageInput()
                 {
                     Text = userQuery
                 }
                 );
                //JObject output = JObject.Parse(result2.Response);
                //string txt = (string)output["output"]["generic"][0]["text"];
                //txt = txt.Replace("\n", "\n").Replace("- ", "");
                //var ouput = new {result2.Response};  // replace with the actual output



                // parse the JSON response
                JObject response = JObject.Parse(result2.Response);
                //Console.WriteLine(result2.Response);
                // extract the main title

                mainTitle = response?["output"]?["generic"]?[0]?["text"]?.ToString();


                if (mainTitle == "")
                {
                    mainTitle = response["output"]["generic"][0]["title"].Value<string>();

                }
                if (mainTitle != null && mainTitle.Length > 20)
                {
                    int index = mainTitle.IndexOf(':');

                    mainTitle = index >= 0 ? mainTitle.Substring(0, index) : mainTitle;
                }




                linkSeprater(response);

                //sourceTitles.RemoveAll(s => s.Contains("Is there anything else I can help you with"));

                // print the results
                Console.WriteLine(mainTitle);
                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < sourceTitles.Count; i++)
                {
                    Console.WriteLine(sourceTitles[i] + "\n" + sourceLinks[i]);
                }
                try
                {

                    JArray suggestionArr = response?["output"]?["generic"]?[0]?["suggestions"] as JArray;
                    JArray optionsArr = response?["output"]?["generic"]?[1]?["options"] as JArray;
                    if (suggestionArr != null)
                    {

                        foreach (JObject suggestion in suggestionArr)
                        {
                            string label = suggestion["label"].Value<string>();
                            lables.Add(label);
                        }
                    }
                    if (optionsArr != null)
                    {
                        foreach (JToken option in optionsArr)
                        {
                            string extractedText = (string)option["label"];
                            lables.Add(extractedText); // Output: "I have no knowledge" and "I have some knowledge"
                        }
                    }
                    printLables();
                }
                catch (Exception e)
                {

                }





            }
            void linkSeprater(JObject response)
            {
                for (int i = 0; i < response["output"]["generic"].Count(); i++)
                {
                    string inputText = response?["output"]?["generic"]?[i]?["text"]?.ToString();
                    string linkPattern = @"\[(.*?)\]\((.*?)\)";
                    Regex regex = new Regex(linkPattern);
                    if (inputText != null)
                    {
                        MatchCollection matches = regex.Matches(inputText);
                        foreach (Match match in matches)
                        {
                            string link = Regex.Match(match.Value, linkPattern).Groups[2].Value;
                            string title = match.Groups[1].Value;
                            sourceTitles.Add(title);
                            sourceLinks.Add(link);
                        }
                    }

                }
            }
            void printLables()
            {
                if (lables.Count() != 0)
                {

                    for (int i = 0; i < lables.Count; i++)
                    {
                        Console.WriteLine(lables[i]);
                    }
                }
            }
        }
    }
}

