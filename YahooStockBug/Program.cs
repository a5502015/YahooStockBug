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

            //Console.WriteLine(ans);

            using (StreamWriter sw = new StreamWriter("./page.html", false))
            {
                 sw.Write(ans);
            }
            //string log =  st.analysisHtml(ans, @"/html/body/center/table/tr/td/table/tr"); //抓不到歐洲 但比較好看
            string log = st.analysisHtml(ans, @"/html/body/center/table/tr"); //抓的到歐洲

            using (StreamWriter sw = new StreamWriter("./log2.txt", false))
            //using (StreamWriter sw = new StreamWriter("./log.txt", false))
            {
                sw.Write(log);
            }
            char[] charr = {'\n','\a' };

            string[] logArr = log.Split(charr,StringSplitOptions.RemoveEmptyEntries);

            foreach(var tmp in logArr)
            {
                Console.WriteLine(tmp);
            }
            Console.ReadKey(true);
        }
    }
}
