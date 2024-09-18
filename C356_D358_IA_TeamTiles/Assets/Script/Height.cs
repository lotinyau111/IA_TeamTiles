using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Height : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject[] heightObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int x = 0, y = 0;

        for (int i = 0; i < heightObject.Length; i++)
        {
            string txt = gameManager.GetComponent<SetGameBoard>().gameTable[x][y].GetComponent<Text>().text;
            float position = -40.5f + 9 * getHeightValue(txt);
            MoveHeight(heightObject[i], position);
            Debug.Log("Moved for¡G " + i + ", position " + position + " x= " + x + " y= " + y);
            if (y < 5) y++;
            else { y = 0; x++; }
        }
    }

    public int getHeightValue(string heightValue)
    {
        if (heightValue == "A" || heightValue == "B") return 0;
        else
            return int.Parse(heightValue);
    }

    public void MoveHeight(GameObject heightObject, float position)
    {
        float pos_X = heightObject.gameObject.transform.position.x;
        float pos_Z = heightObject.gameObject.transform.position.z;

        if (heightObject.gameObject.transform.position.y < position)
        {
            Debug.Log("GO Po" + heightObject.gameObject.transform.position.y + ", targeted pos " + position);
            heightObject.gameObject.transform.position += new Vector3(0, 100 * Time.deltaTime, 0);
        }

        if (heightObject.gameObject.transform.position.y > position)
            heightObject.gameObject.transform.position -= new Vector3(0, 100 * Time.deltaTime, 0);
    }
}
