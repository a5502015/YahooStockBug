using System;
using System.IO;
using System.Threading.Tasks;

namespace YahooStockBug
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = @"https://tw.stock.yahoo.com/us/worldidx.php";
            Stock st = new Stock(url);

            //string ans = await.getUrlResponAsync();
            string ans = await st.getUrlResponAsync();

            Console.WriteLine(ans);

            using (StreamWriter sw = new StreamWriter("./page.html", false))
            {
                 sw.Write(ans);
            }

            Console.ReadKey(true);
        }
    }
}
