// Licensed under the MIT License.

using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;


namespace EchoBot.Models
{
    public class CustomQnAMaker
    {
        /* 以下の3種類のキーを入力してください */

        private const string KnowledgebaseId = "9bff38f0-f112-4923-89d9-fcb44bc67a84";
        private const string Host = "https://myveriqna.azurewebsites.net/qnamaker";
        private const string EndpointKey = "e538dc60-9f6d-4d23-a6f2-6c57b3f1bdaf";

        private const string RequestUri = Host + "/knowledgebases/" + KnowledgebaseId + "/generateAnswer/";
        public static async Task<string> GetResults(string message)
        {
            var question = $"{{\"question\": \"{message}\", \"top\": 5}}";

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(RequestUri);
                    request.Content = new StringContent(question, Encoding.UTF8, "application/json");
                    request.Headers.Add("Authorization", "EndpointKey " + EndpointKey);

                    using (var response = await client.SendAsync(request))
                    {
                        if(response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            return json;
                        }

                        string failure = "failure";
                        return failure;
                    }

                }
            }

        }
    }
}
