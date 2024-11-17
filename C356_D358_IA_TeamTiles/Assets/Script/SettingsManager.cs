using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class SettingsManager : MonoBehaviour
{
    public string a, b, c, d;

    public string select;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(CreateUser(a, b, c, d));
       // if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Creat
         //   StartCoroutine(GetData(select, "useraccount")));
        //  StartCoroutine(GetData("*", "useraccount", ""));

    }



    IEnumerator CreateUser(string user, string pwd, string pn, string details)
    {

        string sql = "INSERT INTO useraccount (username, email,password, playername) values ('" + user + "','" + pwd + "','" + pn + "','" + details + "')";

        WWWForm form = new WWWForm();
        form.AddField("sqlpost", sql);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/teamtiles/DatabaseQuery.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) { Debug.Log(www.error); }
            else
            {
                Debug.Log("Form post complete!");

            }
        }

    }
    IEnumerator GetData()
    {
        return null;
    }

   


    IEnumerator GetData(string sql)
    {
        WWWForm form = new WWWForm();
        form.AddField("sqlpost", sql);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/teamtiles/DatabaseQuery.php", form))
        {
            yield return www.SendWebRequest();
            //   yield return www;

            string wwwString = www.ToString();
            print(wwwString);
            print(www.result);
            print("Result from database: " + www.downloadHandler.text);

            string CreateLog = "";
            //string[][] result;
            string[] entries = www.downloadHandler.text.Split(",|");
            //int y = entries[0].Split(',').Length;
            string[,] result = new string[entries.Length, entries[0].Split(',').Length];
            for (int i = 0; i < entries.Length; i++)
            {
                print("entries = " + entries[i]);
                string[] parts = entries[i].Split(',');

                print(parts[0]);
                for (int j = 0; j < parts.Length; j++)
                {

                    result[i,j] = parts[j];
                    print("Create log¡G" + ( CreateLog += parts[j]));
                    
                }
            }

            string bugString = "";
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                    bugString += "[" + result[i,j] + "]";
                bugString += "\n";
            }
            Debug.LogWarning("debug" + bugString);

            /* if (www.result != UnityWebRequest.Result.Success) { Debug.Log(www.error); }
             else
             {
                 Debug.Log("Form post complete!");
                 Debug.Log(sql + www);
                 Debug.Log(www.)


             }*/


        }
    }
    //"

    //      StartCoroutine(CreateUser(inputUsername, hashPassWord(inputPassword), inputEmail, pn));
}
