using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    //  public SetGameBoard gameBoard;
    public GameObject gameManager;
    private bool bothSkip;


    // Start is called before the first frame update
    void Start()
    {
        //setGameBoard = gameManager.GetComponent<SetGameBoard>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void MoveUp()
    {
        bothSkip = false;
        gameManager.GetComponent<GameManager>().move(-1, 0);
        commonCheck();
    }

    public void MoveDown()
    {
        bothSkip = false;
        gameManager.GetComponent<GameManager>().move(1, 0);
        commonCheck();
    }

    public void MoveLeft()
    {
        bothSkip = false;
        gameManager.GetComponent<GameManager>().move(0, -1);
        commonCheck();
    }

    public void MoveRight()
    {
        bothSkip = false;
        gameManager.GetComponent<GameManager>().move(0, 1);
        commonCheck();

    }

    public void MoveSkip()
    {
        if (bothSkip) gameManager.GetComponent<GameManager>().gameLoss();
        else
        {
            gameManager.GetComponent<GameManager>().addScore(-1);
            commonCheck();
            bothSkip = true;
        }
        
    }

    public void commonCheck()
    {
        gameManager.GetComponent<GameManager>().checkWin();
        gameManager.GetComponent<GameManager>().swapPlayer();
        gameManager.GetComponent<GameManager>().addRound();
    }


}
