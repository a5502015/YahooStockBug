﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Http;
using HtmlAgilityPack;

namespace YahooStockBug
{
    class Stock
    {
        private string url;
        public Stock(string link)
        {
            string rule2 = @"(https?:\/\/[\w-\.]+(:\d+)?(\/[~\w\/\.]*)?(\?\S*)?(#\S*)?)";
            string rule = @"^(?:([A-Za-z]+):)?(\/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(?:\/([^?#]*))?(?:\?([^#]*))?(?:#(.*))?$";
            if (new Regex(rule).IsMatch(link))
            {
                url = link;
            }
            else
            {
                url = @"https://tw.stock.yahoo.com/us/worldidx.php";
            }
        }
        public async Task<string> getUrlResponAsync()
        {
            string html = "";
            using (HttpClient client = new HttpClient()) //抄來的 爽
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);
                    html = responseBody;
                    //Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }

            return html;
        }
        public string analysisHtml(string html,string rule)
        {
            string ans = "";
            HtmlDocument doc = new HtmlDocument();
            //doc.Load("./page.html", Encoding.Default);
            doc.LoadHtml(html);

            //HtmlWeb webClient = new HtmlWeb();
            //HtmlDocument doc = webClient.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(rule);

            foreach (HtmlNode node in nodes)
            {
                //Console.WriteLine(node.InnerText.Trim());
                ans += node.InnerText;
                //ans += "---------------------------\n";


                //foreach (HtmlNode chNode in node.ChildNodes)
                //{
                //    Console.Write(chNode.InnerText);
                //    ans += chNode.InnerHtml;
                //}
                //Console.Write("---------------------------");
            }

            return ans;
        }

    }
}
