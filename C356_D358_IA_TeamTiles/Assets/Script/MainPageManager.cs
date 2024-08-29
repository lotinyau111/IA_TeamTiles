using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageManager : MonoBehaviour
{
    public GameObject mainScene, howToPlayScene;
    public GameObject htpP1, htpP2, htpBtnPrevious, htpBtnNext, htpBtnHome;
    public GameObject displayRank, displayName, displayScore, displayRound, txtpage;
    public GameObject btnPrevious, btnNext;
    int leaderboardpage, orderby = 1;
    //  public GameObject mainpageManager;
    // Start is called before the first frame update
    void Start()
    {
        
       //PlayerPrefs.DeleteAll();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame() { GetComponent<ChangeScene>().changeScene(SceneManager.GetActiveScene().buildIndex + 1); }

    public void howToPlay()
    {
        mainScene.SetActive(false);
        howToPlayScene.SetActive(true);
        htpBtnHome.SetActive(true);
        htpP1.SetActive(true);
        htpP2.SetActive(false);
        htpBtnPrevious.SetActive(false);
        htpBtnNext.SetActive(true);
    }

    public void Quit() { Application.Quit(); }

    public void htpPreviousPage()
    {
        htpBtnPrevious.SetActive(false);
        htpBtnNext.SetActive(true);
        htpP1.SetActive(true);
        htpP2.SetActive(false);
    }
    public void htpNextPage()
    {
        htpBtnPrevious.SetActive(true);
        htpBtnNext.SetActive(false);
        htpP1.SetActive(false);
        htpP2.SetActive(true);
    }

    public void htpBackHome()
    {
        mainScene.SetActive(true);
        howToPlayScene.SetActive(false);
    }

    public void showLeaderboard()
    {
        leaderboardpage = 0;
        getLeaderboard();
    }

    public void printLeaderBoard(string leaderboardData)
    {
        // string leaderboardData = PlayerPrefs.GetString("scoreLeaderboard", "");
        if (!string.IsNullOrEmpty(leaderboardData))
        {
            string[] entries = leaderboardData.Split('|');

            int lastData = (leaderboardpage * 10) + 10;
            if (entries.Length < lastData) { lastData = entries.Length; }

            for (int i = leaderboardpage * 10; i < lastData; i++)
            {
                Debug.Log("last data " + lastData + " i = " + i);
                if (!string.IsNullOrEmpty(entries[i]))
                {
                    string[] parts = entries[i].Split(',');
                    displayName.GetComponent<Text>().text += parts[0] + "\n";
                    displayScore.GetComponent<Text>().text += parts[1] + "\n";
                    displayRound.GetComponent<Text>().text += parts[2] + "\n";
                }
            }
        }
        else Debug.LogWarning("EMPTY");
    }

    /* public void printLeaderBoard()
     {
         if (leaderboardpage == 0) btnPrevious.SetActive(false); else btnPrevious.SetActive(true);
         if (leaderboardpage == 9) btnNext.SetActive(false); else btnNext.SetActive(true);

         for (int i = 1; i <= 10; i++) displayRank.GetComponent<Text>().text += (leaderboardpage * 10 + i) + "\n";

         string leaderboardData = PlayerPrefs.GetString("scoreLeaderboard", "");
         if (!string.IsNullOrEmpty(leaderboardData))
         {
             string[] entries = leaderboardData.Split('|');

             int lastData = (leaderboardpage * 10) + 10;
             if (entries.Length < lastData) lastData = entries.Length;


             for (int i = leaderboardpage * 10; i < lastData; i++)
             {
                 Debug.Log("last data " + lastData + " i = " + i);
                 if (!string.IsNullOrEmpty(entries[i]))
                 {

                     string[] parts = entries[i].Split(',');
                     displayName.GetComponent<Text>().text += parts[0] + "\n";
                     displayScore.GetComponent<Text>().text += parts[1] + "\n";
                     displayRound.GetComponent<Text>().text += parts[2] + "\n";
                 }
             }
         }
         else Debug.LogWarning("EMPTY");
     }*/

    public void getLeaderboard()
    {
        txtpage.GetComponent<Text>().text = "Page " + (leaderboardpage + 1).ToString() + " of Page 10";
        displayRank.GetComponent<Text>().text = "";
        displayName.GetComponent<Text>().text = "";
        displayScore.GetComponent<Text>().text = "";
        displayRound.GetComponent<Text>().text = "";

        if (leaderboardpage == 0) btnPrevious.SetActive(false); else btnPrevious.SetActive(true);
        if (leaderboardpage == 9) btnNext.SetActive(false); else btnNext.SetActive(true);
        for (int i = 1; i <= 10; i++) displayRank.GetComponent<Text>().text += (leaderboardpage * 10 + i) + "\n";

        if (orderby == 1) printLeaderBoard(PlayerPrefs.GetString("scoreLeaderboard"));
        if (orderby == 2) printLeaderBoard(PlayerPrefs.GetString("roundLeaderboard"));
    }

    public void nextPage()
    {
        leaderboardpage++;
        getLeaderboard();
    }

    public void previousPage()
    {
        leaderboardpage--;
        getLeaderboard();
    }

    public void orderByScore()
    {
        orderby = 1;
        getLeaderboard();
    }
    public void orderByRound()
    {
        orderby = 2;
        getLeaderboard();
    }
}
