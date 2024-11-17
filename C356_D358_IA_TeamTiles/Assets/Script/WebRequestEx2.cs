using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestEx2 : MonoBehaviour
{
    // Start is called before the first frame update
    public string inputUsername;
    public string inputPassword;
    public string inputEmail;
    public string pn;
    string CreateUserURL = "http://localhost/teamtiles/CreateAccount.php";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(CreateUser(inputUsername, inputPassword, inputEmail, pn));

    }
    
    IEnumerator CreateUser(string username, string password, string email, string pn)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("passwordPost", password);
        form.AddField("emailPost", email);
        form.AddField("pnpost", pn);

        using (UnityWebRequest www = UnityWebRequest.Post(CreateUserURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) { Debug.Log(www.error); }
            else { Debug.Log("Form post complete!"); }
        }
    }

}
