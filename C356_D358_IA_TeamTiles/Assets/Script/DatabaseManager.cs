using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


public class DatabaseManager : MonoBehaviour
{
    private string[,] result = new string[1, 1];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string hashPassword(string inputPassword)
    {
        return Hash128.Compute(inputPassword).ToString();
    }

    public string selectSQLtoString(string select, string from)
    {
        return "SELECT " + select + " FROM " + from;
    }

    public string selectSQLtoString(string select, string from, string where)
    {
        return selectSQLtoString(select, from) + " WHERE " + where;
    }


    public async Task<Task> receiveDBdata(string sql)
    {
        await searchDB(sql);
        return null;
    }

    private Task<string> searchDB(string sql)
    {
        var tcs = new TaskCompletionSource<string>();
        WWWForm form = new WWWForm();
        form.AddField("sqlpost", sql);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/teamtiles/DatabaseQuery.php", form);
        www.SendWebRequest().completed += (asyncOperation) =>
        {
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                tcs.SetException(new System.Exception(www.error));
            }
            else
            {
                string[] entries = www.downloadHandler.text.Split(",|");
                result = new string[entries.Length, entries[0].Split(',').Length];
                for (int i = 0; i < entries.Length; i++)
                {
                    string[] parts = entries[i].Split(',');
                    for (int j = 0; j < parts.Length; j++) { result[i, j] = parts[j]; }
                }
                tcs.SetResult(www.downloadHandler.text);
            }
        };

        return tcs.Task;
    }

    public string getData(int x, int y) { return result[x, y]; }
}
