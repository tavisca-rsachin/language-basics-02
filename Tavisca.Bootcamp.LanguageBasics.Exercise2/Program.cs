﻿using System;
using System.Collections;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int length = exactPostTime.Length;
            Hashtable hashtable = new Hashtable();
            TimeSpan maxTime =  TimeSpan.Parse("00:00:00");
        
            for(int i=0;i<length;i++)
            {
                if(hashtable.ContainsKey(exactPostTime[i]) && hashtable[exactPostTime[i]].ToString() != showPostTime[i])
                    return "impossible";

                if(!hashtable.ContainsKey(exactPostTime[i]))
                    hashtable.Add(exactPostTime[i], showPostTime[i]);

                TimeSpan timeSpan = TimeSpan.Parse(exactPostTime[i]);
                string[] showPostSplitted = showPostTime[i].Split(" ");

                if(showPostSplitted[1] == "minutes")
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromMinutes(double.Parse(showPostSplitted[0])));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                }
                else if(showPostSplitted[1] == "hours")
                {
                    timeSpan = timeSpan.Add(TimeSpan.FromHours(double.Parse(showPostSplitted[0])));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                }

                if(maxTime < timeSpan)
                    maxTime = timeSpan;

            }
            
            return maxTime.ToString();
        }
    }
}
