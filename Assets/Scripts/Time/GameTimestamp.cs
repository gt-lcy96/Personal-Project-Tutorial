using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameTimestamp 
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    public Season season;

    public enum DayOfTheWeek
    {
        SaturDay,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }

    public int day;
    public int hour;
    public int minute;

    public GameTimestamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public void UpdateClock()
    {
        minute++;

        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if(hour >= 24)
        {
            hour = 0;
            day++;
        }

        if(day > 30)
        {
            day = 1;

            if(season == Season.Winter)
            {
                season = Season.Spring;
            } else 
            {
                season++;
            }

        }
    }

    public DayOfTheWeek GetDayOfTheWeek()
    {
        // Convert the total time passed into days
        int daysPassed = YearsToSeason(year) + SeasonsToDays(season) + day;

        //Remainder after dividing daysPassed by 7
        int dayIndex = daysPassed % 7;

        // Cast into Day of the week
        return (DayOfTheWeek)dayIndex;
    }

    // Convert hours to minute
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }

    // Convert days to hours
    public static int DaysToHours(int days)
    {
        return days * 24;
    }

    // Convert Seasons to days
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    // Years to Season
    public static int YearsToSeason(int years)
    {
        return years * 4 * 30;
    }
}
    