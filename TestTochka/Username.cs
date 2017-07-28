using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TestTochka
{
    class Username
    {
        public Username(string user, TwitterService service)
        {
            this.service = service;
            this.User = user;
            
            
        }

        private string user = "";

        private TwitterService service = new TwitterService();

        public string User
        {
            get { return user; }
            set
            {
                user = value;
                if (user.IndexOf("@") != 0)
                    user = "@" + user;

                if (validate(user) == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Пользователь найден, собираем статистику!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("К сожалению введён не верный логин либо у данного пользователя отсутствуют твиты(");
                    Console.ResetColor();
                }
            }
        }

        private int validate(string User)
        {
            ListTweetsOnUserTimelineOptions userOptions = new ListTweetsOnUserTimelineOptions();
            userOptions.ScreenName = User;
            var tweets = service.ListTweetsOnUserTimeline(userOptions);

            int i = 0;
            try
            {
                foreach (var tweet in tweets)
                {
                    i++;
                    break;
                }
            }
            catch (NullReferenceException)
            { }
            return i;
        }
    }
}
