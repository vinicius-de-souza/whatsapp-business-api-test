using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Define protocolo de segurança exigido pela API
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        // Variaveis necessárias para construir o endpoint
        string version = "v18.0";
        string phoneNumberId = "279487821904085";

        string apiUrl = $"https://graph.facebook.com/{version}/{phoneNumberId}/messages";
        string accessToken = ""; 

        string jsonBody = @"
        {
            ""messaging_product"": ""whatsapp"",
            ""to"": ""5521997229701"",
            ""type"": ""template"",
            ""template"": {
                ""name"": ""hello_world"",
                ""language"": {
                    ""code"": ""en_US""
                }
            }
        }";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Bearer authentication 
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                // JSON Body para StringContent
                StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Post Request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Checa status da resposta
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("POST request successful!");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }

        Console.ReadKey();
    }
}
