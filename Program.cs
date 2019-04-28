using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestApiClientExample
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await Upload();
        }

        private static async Task Upload()
        {
            var client = new HttpClient();
            var multiForm = new MultipartFormDataContent();

            var byteArray = Encoding.ASCII.GetBytes("admin:admin");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var path = "/Users/golf/Downloads/Alfresco SecureShareBoxs.pdf";

            // add API method parameters
            multiForm.Add(new StringContent("20190525"), "date");
            multiForm.Add(new StringContent("InsName"), "insname");
            multiForm.Add(new StringContent("Inslc"), "inslc");
            multiForm.Add(new StringContent("PolicyNo"), "policyno");
            multiForm.Add(new StringContent("EmailAddr"), "emailaddr");
            multiForm.Add(new StringContent("Product"), "product");
            multiForm.Add(new StringContent("SubDocTypeCode"), "subdoctypecode");
            multiForm.Add(new StringContent("SubDocTypeName"), "subdoctypename");
            multiForm.Add(new StringContent("SystemId"), "systemid");
            multiForm.Add(new StringContent("InsName+Doctype"), "doctype");
            multiForm.Add(new StringContent("2019-05-25"), "effectdt");

            // add file and directly upload it
            FileStream fs = File.OpenRead(path);
            multiForm.Add(new StreamContent(fs), "filedata", Path.GetFileName(path));

            // send request to API
            var url = "http://localhost:8080/alfresco/s/api/tni/upload";
            var response = await client.PostAsync(url, multiForm);

            Console.WriteLine(response.StatusCode);
            Task<string> result = response.Content.ReadAsStringAsync();
            Console.WriteLine(result.Result);
        }

        private static async Task GetContent()
        {
            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("admin:admin");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = "http://localhost:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/b2fe7d8c-7255-4c49-9adc-e152fc9fd8ff/content";
            var response = await client.GetAsync(url);

            Console.WriteLine(response.StatusCode);
            Task<string> result = response.Content.ReadAsStringAsync();
            Console.WriteLine(result.Result);
        }

        private static async Task Delete()
        {
            var client = new HttpClient();

            var byteArray = Encoding.ASCII.GetBytes("admin:admin");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var url = "http://localhost:8080/alfresco/api/-default-/public/alfresco/versions/1/nodes/b2fe7d8c-7255-4c49-9adc-e152fc9fd8ff";
            var response = await client.DeleteAsync(url);

            Console.WriteLine(response.StatusCode);
            Task<string> result = response.Content.ReadAsStringAsync();
            Console.WriteLine(result.Result);
        }
    }
}
