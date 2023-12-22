using System;

namespace Utility.Schedule
{
    public class SchedulerUtility
    {
        public static void IntervalInSeconds(TimeSpan time, double interval, Action task)
        {
            interval = interval / 3600;
            ScheduleService.Instance.ScheduleTask(GetTimeModelFrom(time).Hour, GetTimeModelFrom(time).Minute, interval, task);
        }
        public static void IntervalInMinutes(TimeSpan time, double interval, Action task)
        {
            interval = interval / 60;
            ScheduleService.Instance.ScheduleTask(GetTimeModelFrom(time).Hour, GetTimeModelFrom(time).Minute, interval, task);
        }
        public static void IntervalInHours(TimeSpan time, double interval, Action task)
        {
            ScheduleService.Instance.ScheduleTask(GetTimeModelFrom(time).Hour, GetTimeModelFrom(time).Minute, interval, task);
        }
        public static void IntervalInDays(TimeSpan time, double interval, Action task)
        {
            interval = interval * 24;
            ScheduleService.Instance.ScheduleTask(GetTimeModelFrom(time).Hour, GetTimeModelFrom(time).Minute, interval, task);
        }

        private static ServiceTimeModel GetTimeModelFrom(TimeSpan timeSpan)
        {
            ServiceTimeModel serviceTimeModel = new ServiceTimeModel();

            serviceTimeModel.Hour = timeSpan.Hours == TimeSpan.Zero.Hours ? DateTime.Now.Hour : timeSpan.Hours;
            serviceTimeModel.Minute = timeSpan.Minutes == TimeSpan.Zero.Minutes ? DateTime.Now.Minute : timeSpan.Minutes;

            return serviceTimeModel;
        }
    }

    public class ServiceTimeModel
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
