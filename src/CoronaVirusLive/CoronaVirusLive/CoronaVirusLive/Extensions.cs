using System;

namespace CoronaVirusLive
{
    static class Extensions
    {
        internal static string ToStandardDateString(this DateTime dateTime)
        {
            return dateTime.ToString("MM-dd-yyyy");
        }

    }
}
