using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameMamager, btnUp, btnDown, btnLeft, btnRight;
    private int currentPlayer = -1;
    private int round = 0, score = 0;
    public GameObject playerDesc;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = 0;
        round = 1;
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
        if (currX == 0) btnUp.SetActive(false); else btnUp.SetActive(true);
        if (currX == 5) btnDown.SetActive(false); else btnDown.SetActive(true);
        if (currY == 0) btnLeft.SetActive(false); else btnLeft.SetActive(true);
        if (currY == 5) btnRight.SetActive(false); else btnRight.SetActive(true);

        if (getCurrentPlayer() == "A")
        {
            try
            {
                // check value can move
                if (gameMamager.GetComponent<SetGameBoard>().getGameTableValue(currX - 1, currY) > 1)
                    btnUp.SetActive(false);
                else btnUp.SetActive(true);
            }
            catch (IndexOutOfRangeException e) { Debug.Log(e.ToString()); }

            try
            {
                if (gameMamager.GetComponent<SetGameBoard>().getGameTableValue(currX + 1, currY) > 1)
                    btnDown.SetActive(false);
                else btnDown.SetActive(true);
            }
            catch (IndexOutOfRangeException e) { }
        }

        // check player position
        // check able to move that direction
        // check table
        // move
    }

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
        if (currentPlayer == 0) currentPlayer = 1;
        else if (currentPlayer == 1) currentPlayer = 0;
    }
}
