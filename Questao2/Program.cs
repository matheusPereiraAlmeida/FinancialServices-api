﻿using Questao2.Service;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        int totalScoredGoals = 0;
        var api = new APIConnector(new HttpClient(), "https://jsonmock.hackerrank.com/api/football_matches?");

        totalScoredGoals += api.GetGoalsAsync(year, team, true).Result;
        totalScoredGoals += api.GetGoalsAsync(year, team, false).Result;

        return totalScoredGoals;
    }
}