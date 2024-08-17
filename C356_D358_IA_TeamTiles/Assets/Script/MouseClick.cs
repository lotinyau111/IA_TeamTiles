using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    //  public SetGameBoard gameBoard;
    public GameObject go;
    private SetGameBoard setGameBoard;

    // Start is called before the first frame update
    void Start()
    {
        setGameBoard = go.GetComponent<SetGameBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveUp();
        }
    }

    void MoveUp()
    {
        Debug.Log(go.GetComponent<SetGameBoard>().getLocation('A', 'X'));
    }


}
