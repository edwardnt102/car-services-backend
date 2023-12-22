using System;

namespace Utility
{
    public class DateTimeUtility
    {
        public string ConvertDateTimeByFormat(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }
    }
}
