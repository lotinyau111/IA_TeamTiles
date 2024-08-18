using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class SetGameBoard : MonoBehaviour
{
    public string[] gameBoard = new string[36]; // real game board display (include ram
    public GameObject _00, _01, _02, _03, _04, _05;
    public GameObject _10, _11, _12, _13, _14, _15;
    public GameObject _20, _21, _22, _23, _24, _25;
    public GameObject _30, _31, _32, _33, _34, _35;
    public GameObject _40, _41, _42, _43, _44, _45;
    public GameObject _50, _51, _52, _53, _54, _55;
    //   public GameObject _003D;
    public GameObject[] _0, _1, _2, _3, _4, _5;
    public GameObject[][] table, gameTable;
    //    public GameObject pre0, pre1, pre2, pre3, pre4, pre5;
    // Start is called before the first frame update
    void Start()
    {
        gameTable = new GameObject[6][];
        gameTable[0] = new GameObject[6] { _00, _01, _02, _03, _04, _05 };
        gameTable[1] = new GameObject[6] { _10, _11, _12, _13, _14, _15 };
        gameTable[2] = new GameObject[6] { _20, _21, _22, _23, _24, _25 };
        gameTable[3] = new GameObject[6] { _30, _31, _32, _33, _34, _35 };
        gameTable[4] = new GameObject[6] { _40, _41, _42, _43, _44, _45 };
        gameTable[5] = new GameObject[6] { _50, _51, _52, _53, _54, _55 };



        for (int i = 0; i < gameBoard.Length; i++)
        {
            gameBoard[i] = ((int)Random.Range(0, 10)).ToString();
            Debug.Log("Gameboard " + i + ": " + gameBoard[i]);
        }

        gameBoard[Random.Range(0, 36)] = "A";
        gameBoard[Random.Range(0, 36)] = "B";



    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        for (int j = 0; j < gameTable.Length; j++)
        {
            for (int k = 0; k < gameTable[j].Length; k++)
            {
                gameTable[j][k].GetComponent<Text>().text = gameBoard[i];
                i++;
              //  Debug.Log("When j = " + j + ", k = " + k + ", i = " + i);//
            }
        }
    }

    public int getLocation(string player, char location)
    {
        for (int i = 0; i < gameTable.Length; i++)
            for (int j = 0; j < gameTable[i].Length; j++)
            {
                if (gameTable[i][j].GetComponent<Text>().text == player && location == 'X')
                    return i;

               if (gameTable[i][j].GetComponent<Text>().text == player && location == 'Y')
                   return j;
            }
        return -1;
    }
}
