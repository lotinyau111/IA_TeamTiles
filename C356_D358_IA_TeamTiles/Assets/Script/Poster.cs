using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poster : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject[] posterObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int x = 0, y = 0;

        for (int i = 0; i < posterObject.Length; i++)
        {
            string txt = gameManager.GetComponent<SetGameBoard>().gameTable[x][y].GetComponent<Text>().text;
            float position = getPosterValue(txt);
            editPic(posterObject[i], position);
            //  Debug.Log("Moved for¡G " + i + ", position " + position + " x= " + x + " y= " + y);
            if (y < 5) y++;
            else { y = 0; x++; }
        }
    }

    public int getPosterValue(string posterValue)
    {
        if (posterValue == "A" || posterValue == "B") return 1;
        else return int.Parse(posterValue) + 1;
    }

    public void editPic(GameObject posterObject, float tilingValue)
    {
        if (posterObject.GetComponent<Renderer>().material.mainTextureScale.x < tilingValue)
            posterObject.GetComponent<Renderer>().material.mainTextureScale += new Vector2(5 * Time.deltaTime, 5 * Time.deltaTime);
        if (posterObject.GetComponent<Renderer>().material.mainTextureScale.x > tilingValue)
            posterObject.GetComponent<Renderer>().material.mainTextureScale -= new Vector2(5 * Time.deltaTime, 5 * Time.deltaTime);
    }
}
