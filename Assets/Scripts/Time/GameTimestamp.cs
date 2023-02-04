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

    // create a new instance of GameTimestamp from existing one
    public GameTimestamp(GameTimestamp timestamp)
    {
        this.year = timestamp.year;
        this.season = timestamp.season;
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
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
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;

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
    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }

    // calculate the total hours for years, seasons, days, hours
    public static int totalHours(GameTimestamp timestamp)
    {
        int yearsInHours = DaysToHours(YearsToDays(timestamp.year));
        int seasonInHours = DaysToHours(SeasonsToDays(timestamp.season));
        int daysInHours = DaysToHours(timestamp.day);

        return yearsInHours + seasonInHours + daysInHours + timestamp.hour;
    }

    public static int CompareTimestamps(GameTimestamp timestamp1, GameTimestamp timestamp2)
    {
        int difference = totalHours(timestamp1) - totalHours(timestamp2);
        return Mathf.Abs(difference);
    }
}
    
