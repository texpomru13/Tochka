using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TestTochka
{
    class TweetStatistic
    {
        public TweetStatistic(TwitterService service)
        {
            this.service = service;
        }

        private string json = "";
        public Statistic Statistic = new Statistic();

        private TwitterService service = new TwitterService();

        public double[] Freq(string s)
        {
            var result1 = new double[('Я' - 'А') + 1];
            foreach (var c in s.ToUpper())
                if ((c >= 'А') && (c <= 'Я')) result1[c - 'А']++;
            for (int i = 0; i < result1.Length; i++)
                result1[i] /= s.Length;

            var result2 = new double[('Z' - 'A') + 1];
            foreach (var c in s.ToUpper())
                if ((c >= 'A') && (c <= 'Z')) result2[c - 'A']++;
            for (int i = 0; i < result2.Length; i++)
                result2[i] /= s.Length;

            var result = new double[('Я' - 'А') + ('Z' - 'A') + 2];
            result1.CopyTo(result,0);
            result2.CopyTo(result, result1.Length);

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != 0)
                {
                    if (i < 32)
                        Statistic.DictionaryStat.Add((char)(i + 'А'), result[i].ToString("0.0000"));
                    else
                        Statistic.DictionaryStat.Add((char)(i + 'A'), result[i].ToString("0.0000"));
                }
            }

            json = JsonConvert.SerializeObject(Statistic.DictionaryStat);


            return result;
        }



        
        public void printStatistic(string username)
        {

            Console.WriteLine(username + ", статистика для последних 5 твитов:" + json);
        }

       
        public void postStatistic(string username)
        {
            string dop = "";
            string _status = username + ", статистика для последних 5 твитов:" + json;

            if (_status.Length > 140)
            {
                _status = _status.Remove(139) + "}";
                dop = "(только всё не влезло, ограничение 140 символов)";

            }
            

            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("У нас получилось, мы запостили! Ура! " + dop);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Что-то пошло не так  )`: ");
                    Console.WriteLine(_status);
                    Console.ResetColor();
                    //throw new Exception(response.StatusCode.ToString());
                }
            });
        }


    }
}
