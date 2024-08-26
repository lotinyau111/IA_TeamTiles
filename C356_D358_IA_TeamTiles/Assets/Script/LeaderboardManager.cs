using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    // private const string LEADERBOARD_KEY = "Leaderboard";
    // private const int MAX_LEADERBOARD_ENTRIES = 100;

    private ScoreEntry[] onScoreLeaderBoard;
    private ScoreEntry[] onRoundLeaderBoard;


    void Start()
    {
        LoadLeaderboard();
    }

    public void AddEntryToLeaderboard(ScoreEntry newEntry)
    {
        if (onScoreLeaderBoard == null)
        {
            onScoreLeaderBoard = new ScoreEntry[100];
            onScoreLeaderBoard[0] = newEntry;
            Debug.Log("leaderboard = null");
            return;
        }

        int insertIndex = 0;
        while (insertIndex < 100 && onScoreLeaderBoard[insertIndex] != null &&
            onScoreLeaderBoard[insertIndex].score > newEntry.score)
        {
            insertIndex++;
        }

        if (insertIndex < 100)
        {
            for (int i = 100 - 1; i > insertIndex; i--) onScoreLeaderBoard[i] = onScoreLeaderBoard[i - 1];

            onScoreLeaderBoard[insertIndex] = newEntry;
        }
    }

    public void LoadLeaderboard()
    {
        onScoreLeaderBoard = new ScoreEntry[100];
        string leaderboardData = PlayerPrefs.GetString("scoreLeaderboard", "");

        if (!string.IsNullOrEmpty(leaderboardData))
        {
            Debug.Log("LeaderboardData: " + leaderboardData);

            string[] entries = leaderboardData.Split('|');
            for (int i = 0; i < entries.Length; i++)
            {
                if (!string.IsNullOrEmpty(entries[i]))
                {
                    string[] parts = entries[i].Split(',');
                    string name = parts[0];
                    int score = int.Parse(parts[1]);
                    int round = int.Parse(parts[2]);
                    onScoreLeaderBoard[i] = new ScoreEntry(name, round, score);
                }
            }
        }
        else Debug.LogWarning("EMPTY");
    }

    public void SaveLeaderboard()
    {
        string leaderboardData = "";
        for (int i = 0; i < 100; i++)
        {
            if (onScoreLeaderBoard[i] != null)
            {
                leaderboardData += onScoreLeaderBoard[i].playerName + "," + onScoreLeaderBoard[i].round + "," + onScoreLeaderBoard[i].score + "|";
            }
        }

        PlayerPrefs.SetString("scoreLeaderboard", leaderboardData);
    }
}



public class ScoreEntry
{
    public string playerName;
    public int round, score;

    public ScoreEntry(string name, int r, int s)
    {
        playerName = name;
        round = r;
        score = s;
    }
}//115