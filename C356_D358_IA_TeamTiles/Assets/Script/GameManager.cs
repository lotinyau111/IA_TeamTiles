using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameMamager, btnUp, btnDown, btnLeft, btnRight;
    private int currentPlayer = -1;
    private int round = 0, score = 0;
    public GameObject playerDesc, menuPage, winPage, winStatement, playerName, lossPage;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = 0;
        round = 1;
        menuPage.SetActive(false);
        btnUp.GetComponent<Button>().enabled = true;
        btnDown.GetComponent<Button>().enabled = true;
        btnLeft.GetComponent<Button>().enabled = true;
        btnRight.GetComponent<Button>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // set player details

        playerDesc.GetComponent<Text>().text = "Round: " + round + " Score: " + score
            + "\nPlayer " + getCurrentPlayer() + " turns";

        // wait for player movement

        // disable button if not allowed
        // player A (1 = or 0) 

        int currX = gameMamager.GetComponent<SetGameBoard>().getLocation(getCurrentPlayer(), 'X');
        int currY = gameMamager.GetComponent<SetGameBoard>().getLocation(getCurrentPlayer(), 'Y');
        Debug.Log("Player: " + getCurrentPlayer() + ": [" + currX + "][" + currY + "]");
        if (currX == 0) btnUp.SetActive(false);
        if (currX == 5) btnDown.SetActive(false);
        if (currY == 0) btnLeft.SetActive(false);
        if (currY == 5) btnRight.SetActive(false);


        if (getCurrentPlayer() == "A")
        {
            try
            {
                // check value can move
                if (gameMamager.GetComponent<SetGameBoard>().getGameTableValue(currX - 1, currY) > 1) btnUp.SetActive(false);
            }
            catch (IndexOutOfRangeException) { }
            catch (FormatException) { }

            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableValue(currX + 1, currY) > 1) btnDown.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            catch (FormatException) { }

            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableValue(currX, currY - 1) > 1) btnLeft.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            catch (FormatException) { }

            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableValue(currX, currY + 1) > 1) btnRight.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            catch (FormatException) { }


            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX - 1, currY) == "B") btnUp.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX + 1, currY) == "B") btnDown.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX, currY - 1) == "B") btnLeft.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX, currY + 1) == "B") btnRight.SetActive(false); }
            catch (IndexOutOfRangeException) { }
        }
        else if (getCurrentPlayer() == "B")
        {
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX - 1, currY) == "A") btnUp.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX + 1, currY) == "A") btnDown.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX, currY - 1) == "A") btnLeft.SetActive(false); }
            catch (IndexOutOfRangeException) { }
            try { if (gameMamager.GetComponent<SetGameBoard>().getGameTableText(currX, currY + 1) == "A") btnRight.SetActive(false); }
            catch (IndexOutOfRangeException) { }
        }

        if (!menuPage.activeSelf && !winPage.activeSelf)
        {
            if (btnUp.activeSelf)
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    GetComponent<MouseClick>().MoveUp();

            if (btnDown.activeSelf)
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    GetComponent<MouseClick>().MoveDown();

            if (btnLeft.activeSelf)
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    GetComponent<MouseClick>().MoveLeft();

            if (btnRight.activeSelf)
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    GetComponent<MouseClick>().MoveRight();
        }

        if (menuPage.activeSelf || winPage.activeSelf)
        {
            btnUp.GetComponent<Button>().enabled = false;
            btnDown.GetComponent<Button>().enabled = false;
            btnLeft.GetComponent<Button>().enabled = false;
            btnRight.GetComponent<Button>().enabled = false;
        }
        else
        {
            btnUp.GetComponent<Button>().enabled = true;
            btnDown.GetComponent<Button>().enabled = true;
            btnLeft.GetComponent<Button>().enabled = true;
            btnRight.GetComponent<Button>().enabled = true;
        }
    } // EndUpdate

    public string getCurrentPlayer()
    {
        switch (currentPlayer)
        {
            case 0: return "A";
            case 1: return "B";
            default: return null;
        }
    }

    public void setCurrentPlayer(string player)
    {
        if (player == "A") this.currentPlayer = 0;
        else if (player == "B") this.currentPlayer = 1;
        else currentPlayer = -1;
    }

    public void swapPlayer()
    {
        btnUp.SetActive(true);
        btnDown.SetActive(true);
        btnLeft.SetActive(true);
        btnRight.SetActive(true);
        if (currentPlayer == 0) currentPlayer = 1;
        else if (currentPlayer == 1) currentPlayer = 0;
    }

    public void addRound() { round++; }

    public void move(int x, int y)
    {
        //playerlocation = playerlocation.x + x 
        int currX = gameMamager.GetComponent<SetGameBoard>().getLocation(getCurrentPlayer(), 'X');
        int currY = gameMamager.GetComponent<SetGameBoard>().getLocation(getCurrentPlayer(), 'Y');
        if (gameMamager.GetComponent<SetGameBoard>().gameTable[currX + x][currY + y].GetComponent<Text>().text == "0")
        {
            gameMamager.GetComponent<SetGameBoard>().gameTable[currX][currY].GetComponent<Text>().text
                = gameMamager.GetComponent<SetGameBoard>().gameTable[currX + x][currY + y].GetComponent<Text>().text;
            gameMamager.GetComponent<SetGameBoard>().gameTable[currX + x][currY + y].GetComponent<Text>().text = getCurrentPlayer();
        }
        else
        {
            gameMamager.GetComponent<SetGameBoard>().gameTable[currX][currY].GetComponent<Text>().text
                = (gameMamager.GetComponent<SetGameBoard>().getGameTableValue((currX + x), (currY + y)) - 1).ToString();
            gameMamager.GetComponent<SetGameBoard>().gameTable[currX + x][currY + y].GetComponent<Text>().text = getCurrentPlayer();
            addScore(1);
        }
    }

    public void checkWin()
    {
        bool all0 = true;
        for (int i = 0; i < gameMamager.GetComponent<SetGameBoard>().gameTable.Length; i++)
        {
            for (int j = 0; j < gameMamager.GetComponent<SetGameBoard>().gameTable[i].Length; j++)
            {
                if (gameMamager.GetComponent<SetGameBoard>().gameTable[i][j].GetComponent<Text>().text != "0" &&
                    gameMamager.GetComponent<SetGameBoard>().gameTable[i][j].GetComponent<Text>().text != "A" &&
                    gameMamager.GetComponent<SetGameBoard>().gameTable[i][j].GetComponent<Text>().text != "B")
                {
                    all0 = false;
                    break;
                }
            }

        }
        if (all0)
        {
            winPage.SetActive(true);
            winStatement.GetComponent<Text>().text = "You win at Round " + round + " at score " + score + ". \n" +
                "Enter your name to add record to the leaderboard.";

        }
    }

    public void gameLoss()
    {
        btnUp.SetActive(false);
        btnDown.SetActive(false);
        btnLeft.SetActive(false);
        btnRight.SetActive(false);
    }

    public void addScore(int score)
    {
        this.score += score;
        Debug.Log("Score Added: " + score + ", current score: " + this.score);
    }

    public void resetGame()
    {
        GetComponent<SetGameBoard>().resetGame();
        Start();
    }

    public void onMenuPage()
    {

        //   if (menuPage.activeSelf) 
        //     int[] score = new int[] { 1, 2, 3};

    }

    public void changeScene()
    {
        GetComponent<ChangeScene>().changeScene(0);
    }

    public void addLeaderboardValue()
    {

        //playerName = GetComponent<LeaderboardManager>().
        ScoreEntry newEntry = new ScoreEntry(playerName.GetComponent<InputField>().text, round, score);
        GetComponent<LeaderboardManager>().AddEntryToLeaderboard(newEntry);
        GetComponent<LeaderboardManager>().SaveLeaderboard();
        // GetComponent<LeaderboardManager>().LoadLeaderboard();
    }


}
