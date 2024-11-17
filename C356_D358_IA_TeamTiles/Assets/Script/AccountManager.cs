using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{

    public GameObject username, password, warning, databaseManager;
    LanguageText lt = new LanguageText();

    private bool dbSearching = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void SignIn()
    {
        string user = username.GetComponent<InputField>().text;
        string pwd = databaseManager.GetComponent<DatabaseManager>().hashPassword(password.GetComponent<InputField>().text);
        string sql = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("COUNT(id)", "useraccount", " username = '" + user + "' AND password = '" + pwd + "'");

        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(sql);
        string result = databaseManager.GetComponent<DatabaseManager>().getData(0, 0);
        if (result == "0")
        {
            warning.SetActive(true);
            warning.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongLoginPw, 1);
        }
        else if (result == "1")
        {
            warning.SetActive(false);
            GetComponent<ChangeScene>().changeScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
