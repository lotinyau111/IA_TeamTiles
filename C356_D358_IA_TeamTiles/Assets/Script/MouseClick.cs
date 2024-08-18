using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    //  public SetGameBoard gameBoard;
    public GameObject gameManager;


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
        
    }

    public void MoveDown()
    {
        Debug.Log("Mouse Clicked - MoveDown");
    }

    public void MoveLeft()
    {
        Debug.Log("Mouse Clicked - MoveLeft");
    }

    public void MoveRight()
    {
        Debug.Log("Mouse Clicked - MoveRight");
    }

    public void MoveSkip()
    {
        gameManager.GetComponent<GameManager>().swapPlayer();
        Debug.Log("Mouse Clicked - MoveSkip");
    }


}
