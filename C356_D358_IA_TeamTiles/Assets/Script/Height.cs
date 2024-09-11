using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Height : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject ho1, ho2, ho3;
    GameObject[] ho;
    // Start is called before the first frame update
    void Start()
    {
        ho = new GameObject[] { ho1, ho2, ho3 };
    }

    // Update is called once per frame
    void Update()
    {

        int x = 0, y = 0;

        for (int i = 0; i < ho.Length; i++)
        {

            string txt = gameManager.GetComponent<SetGameBoard>().gameTable[x][y].GetComponent<Text>().text;
            int height = getHeightValue(txt);
            float position = -40.5f + 9 * height;
            float currX = ho[i].gameObject.transform.position.x;
            float currZ = ho[i].gameObject.transform.position.z;
            ho[i].gameObject.transform.position = new Vector3(currX, position, currZ);

            if (y < 5) y++;
            else { y = 0; x++; }
        }
    }

    public int getHeightValue(string heightValue)
    {
        if (heightValue == "A" || heightValue == "B")
            return 0;
        else
            return int.Parse(heightValue);
    }

    public void MoveHeight(GameObject go, float position)
    {
        float currX = go.gameObject.transform.position.x;
        float currZ = go.gameObject.transform.position.z;

        if(go.gameObject.transform.position.y < position)
        {
            go.gameObject.transform.position = new Vector3(currX, 0.1f*Time.deltaTime, currZ);

        }

        if (go.gameObject.transform.position.y > position)
        {
            go.gameObject.transform.position = new Vector3(currX, -0.1f * Time.deltaTime, currZ);

        }







    }
}
