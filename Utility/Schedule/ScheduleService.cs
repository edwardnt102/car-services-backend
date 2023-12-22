using System;
using System.Collections.Generic;
using System.Threading;

namespace Utility.Schedule
{
    public class ScheduleService
    {
        private static ScheduleService _instance;
        private static List<Timer> timers = new List<Timer>();
        private static Dictionary<string, Timer> timersDictionary = new Dictionary<string, Timer>();

        private ScheduleService() { }

        public static ScheduleService Instance => _instance ?? (_instance = new ScheduleService());

        public static List<Timer> GetTimers()
        {
            return timers;
        }

        public static void KillScheduleByName(string scheduleName)
        {
            if (timersDictionary.ContainsKey(scheduleName))
            {
                timersDictionary[scheduleName].Dispose();
            }
        }

        void AddScheduleToDictionary(string scheduleName, Timer timer)
        {
            if (!timersDictionary.ContainsKey(scheduleName))
            {
                timersDictionary.Add(scheduleName, timer);
            }
        }

        public void ScheduleTask(int hour, int min, double intervalInHour, Action task)
        {
            DateTime now = DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, 0, 0);

            if (DateTime.Parse(now.ToString("yyyy-MM-dd HH:mm")) > DateTime.Parse(firstRun.ToString("yyyy-MM-dd HH:mm")))
            {
                firstRun = firstRun.AddDays(1);
            }

            TimeSpan timeToGo = firstRun - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }

            var timer = new Timer(x =>
            {
                task.Invoke();
            }, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            timers.Add(timer);

            //Add schedule to mem cache
            //AddScheduleToDictionary(task.Method.DeclaringType.DeclaringType.Name, timer);
        }
    }
}
