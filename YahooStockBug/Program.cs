using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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

            //using (StreamWriter sw = new StreamWriter("./page.html", false))
            //{
            //    sw.Write(ans);
            //}

            //string log =  st.analysisHtml(ans, @"/html/body/center/table/tr/td/table/tr"); //抓不到歐洲 但比較好看
            string log = st.analysisHtml(ans, @"/html/body/center/table/tr"); //抓的到歐洲

            //using (StreamWriter sw = new StreamWriter("./log2.txt", false))
            ////using (StreamWriter sw = new StreamWriter("./log.txt", false))
            //{
            //    sw.Write(log);
            //}
            char[] charr = { '\n', '\a' };

            string[] logArr = log.Split(charr, StringSplitOptions.RemoveEmptyEntries);

            int i = 0;
            using (StreamWriter sw = new StreamWriter("./outpute.txt", false))
            {
                foreach (var tmp in logArr)
                {
                    string tmp2 = tmp;
                    //Console.WriteLine(tmp);
                    if (new Regex(@"^&nbsp;").IsMatch(tmp))
                    {
                        sw.Write(tmp);
                        Console.Write(tmp);
                        i = 0;
                    }
                    else if (new Regex(@"&nbsp;$").IsMatch(tmp))
                    {
                        sw.WriteLine(tmp);
                        Console.WriteLine(tmp);
                        i = 0;
                    }
                    else
                    {
                        
                        if (tmp.IndexOf("台灣時間")!= -1)
                        {
                            tmp2 = tmp.Replace("台灣時間","GMT+8");
                        }
                        

                        if (i == 5)
                        {
                            sw.Write(tmp2 + "\n\n");
                            Console.Write(tmp2 + "\n\n");
                            i = 0;
                        }
                        else
                        {
                            sw.Write(tmp2);
                            Console.Write(tmp2);
                            for (int j = 0; j < 22 - Encoding.Default.GetByteCount(tmp2); j++)
                            {
                                sw.Write(" ");
                                Console.Write(" ");
                            }

                            i++;
                        }
                    }



                }
            }

            Console.ReadKey(true);
        }

        static void loop()
        {

        }
    }
}
