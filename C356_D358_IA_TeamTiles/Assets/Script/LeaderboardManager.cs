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
        //LoadLeaderboard();
    }

    public void AddEntryToLeaderboard(ScoreEntry newEntry)
    {
        if (onScoreLeaderBoard == null && onRoundLeaderBoard == null)
        {
            onScoreLeaderBoard = new ScoreEntry[100];
            onRoundLeaderBoard = new ScoreEntry[100];
            onScoreLeaderBoard[0] = newEntry;
            onRoundLeaderBoard[0] = newEntry;
            Debug.LogWarning("array empty");
            return;
        }

        int insertScoreIndex = 0;
        while (insertScoreIndex < 100 && onScoreLeaderBoard[insertScoreIndex] != null &&
            onScoreLeaderBoard[insertScoreIndex].score > newEntry.score) { insertScoreIndex++; }

        if (insertScoreIndex < 100)
        {
            for (int i = 100 - 1; i > insertScoreIndex; i--) onScoreLeaderBoard[i] = onScoreLeaderBoard[i - 1];

            onScoreLeaderBoard[insertScoreIndex] = newEntry;
        }

        int insertRoundIndex = 0;
        while (insertRoundIndex < 100 && onRoundLeaderBoard[insertRoundIndex] != null &&
            onRoundLeaderBoard[insertRoundIndex].round < newEntry.round) { insertRoundIndex++; }

        if (insertRoundIndex < 100)
        {
            for (int i = 100 - 1; i > insertRoundIndex; i--) onRoundLeaderBoard[i] = onRoundLeaderBoard[i - 1];

            onRoundLeaderBoard[insertRoundIndex] = newEntry;
        }

        
    }

    public void LoadLeaderboard()
    {
        onScoreLeaderBoard = new ScoreEntry[100];
        onRoundLeaderBoard = new ScoreEntry[100];
        string scoreData = PlayerPrefs.GetString("scoreLeaderboard", ""),
            roundData = PlayerPrefs.GetString("roundLeaderboard", "");

        Debug.Log("SCORE: " + scoreData + "\n"+"ROUND: "+ roundData );
        if (!string.IsNullOrEmpty(scoreData))
        {
            Debug.Log("LeaderboardData: " + scoreData);

            string[] entries = scoreData.Split('|');
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

        if (!string.IsNullOrEmpty(roundData))
        {
            Debug.Log("LeaderboardData: " + roundData);

            string[] entries = roundData.Split('|');
            for (int i = 0; i < entries.Length; i++)
            {
                if (!string.IsNullOrEmpty(entries[i]))
                {
                    string[] parts = entries[i].Split(',');
                    string name = parts[0];
                    int score = int.Parse(parts[1]);
                    int round = int.Parse(parts[2]);
                    onRoundLeaderBoard[i] = new ScoreEntry(name, round, score);
                }
            }
        }
        else Debug.LogWarning("EMPTY");
    }

    public void SaveLeaderboard()
    {
        string scoreData = "", roundData = "";
        for (int i = 0; i < 100; i++)
        {
            if (onScoreLeaderBoard[i] != null)
            {
                scoreData += onScoreLeaderBoard[i].playerName + "," + onScoreLeaderBoard[i].round + "," + onScoreLeaderBoard[i].score + "|";
            }
            if (onRoundLeaderBoard[i] != null)
            {
                roundData += onRoundLeaderBoard[i].playerName + "," + onRoundLeaderBoard[i].round + "," + onRoundLeaderBoard[i].score + "|";
            }
        }

        Debug.Log(scoreData + "\n round " + roundData);

        PlayerPrefs.SetString("scoreLeaderboard", scoreData);
        PlayerPrefs.SetString("roundLeaderboard", roundData);
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