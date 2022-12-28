using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using WikiApp.Models;

namespace WikiApp.Services
{
    public class WikipediaService
    {
        private static HttpClient _client = new HttpClient();
        private static string Url1 { get; set; } = @"https://en.wikipedia.org/w/api.php?action=query&origin=*&format=json&generator=search&gsrnamespace=0&gsrlimit=5&gsrsearch=";
        private static string Url2 { get; set; } = @"https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro&explaintext&redirects=1&pageids=";
        public static async Task<List<WikiModel>> GetResult(string search)
        {
            string url = Url1 + $"\'{search}\'";
            var response = await _client.GetAsync(url);
            var str = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(str);
            var result = data.query.pages;
            List<WikiModel> models = new List<WikiModel>();
            foreach (var item in result)
            {
                var pageId = (item.Value.pageid).ToString();
                var title = (item.Value.title).ToString();
                var content = await GetData(pageId);
                var wikiItem = new WikiModel
                {
                    PageId = pageId,
                    Title = title,
                    Content = content
                };
                models.Add(wikiItem);
            }
            return models;
        }
        public static async Task<string> GetData(string pageId)
        {
            var url = Url2 + pageId;
            var response = await _client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            var strs = data.Split(new string[] { "\"extract\":\"" }, StringSplitOptions.None);
            var str = strs[1].Replace("\"}}}}", "");
            return str;
        }
    }
}
