using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraTyping.Helpers
{
    public static class MyUtils
    {
        public static Time TimeConverter(int militaryHours, int minutes, int seconds)
        {
            // convert the hours and minutes to seconds
            int totalSeconds = (militaryHours * 3600) + (minutes * 60) + seconds;
            int timeSince430;
            if (totalSeconds >= 16200)
            {
                timeSince430 = totalSeconds - 16200;
            }
            else
            {
                timeSince430 = totalSeconds + 70200;
            }
            bool day = true;
            if (timeSince430 >= 54000)
            {
                timeSince430 -= 54000;
                day = false;
            }
            return new Time(timeSince430, day);
        }
        public static Time TimeConverter(int hours, int minutes, int seconds, bool am)
        {
            // convert to military hours
            // 12:00 -> 0000
            int militaryHours = hours;
            if (militaryHours >= 12)
            {
                militaryHours -= 12;
            }
            if (!am)
            {
                militaryHours += 12;
            }
            return TimeConverter(militaryHours, minutes, seconds);
        }
        public static Time TimeConverter(int militaryHours, int minutes) => TimeConverter(militaryHours, minutes, 0);
        public static Time TimeConverter(int hours, int minutes, bool am) => TimeConverter(hours, minutes, 0, am);
    }

    public struct Time
    {
        public double time;
        public bool dayTime;

        public Time(double time, bool dayTime)
        {
            this.time = time;
            this.dayTime = dayTime;
        }
    }
}
