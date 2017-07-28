using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TweetSharp;

namespace TestTochka
{
    class Program
    {
        private static string customer_key = "HTmosaOs99ZsAaa4zS18Hkzyx";
        private static string customer_key_secret = "QG0CQX05abtryTXnzHUCA8NJXY62l2tCC4EXmHpIa1Y1O5plxR";
        private static string access_token = "722361449767649280-62JE5RCBTYHoTCc9DmW9Rg4tlYpjMOn";
        private static string access_token_secret = "eh7RzBbLehs1xlOvMY5ts1jyerl682GA6R3ZVHj1WTZzt";


        public static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);
        public static List<double[]> TwitteList = new List<double[]>();


        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
                string login = "";
                Console.WriteLine("Введител имя пользователя ");
                login = Console.ReadLine();
                Username username = new Username(login, service);

                ListTweetsOnUserTimelineOptions userOptions = new ListTweetsOnUserTimelineOptions();
                userOptions.ScreenName = username.User;
                var tweets = service.ListTweetsOnUserTimeline(userOptions);

                TweetStatistic tweetStatistic = new TweetStatistic(service); ;

                string Text = "";

                try
                {
                    foreach (var tweet in tweets)
                    {
                        if (TwitteList.Count < 5)
                            Text += tweet.Text;
                        else
                            break;
                    }
                }
                catch (NullReferenceException)
                { }

                TwitteList.Add(tweetStatistic.Freq(Text));


                tweetStatistic.printStatistic(username.User);

                Console.WriteLine("Чтобы запостить нажмите любую клавишу, иначе ESC");
                KeyInfo = Console.ReadKey();
                if(KeyInfo.Key != ConsoleKey.Escape)
                    tweetStatistic.postStatistic(username.User);

                Console.WriteLine();
                Console.WriteLine("Выйти и зпрограммы ESC,  продолжить любая клавиша");
                KeyInfo = Console.ReadKey();
                if (KeyInfo.Key == ConsoleKey.Escape)
                    break;
            }
        }
    }
}



