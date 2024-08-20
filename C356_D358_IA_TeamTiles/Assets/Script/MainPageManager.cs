using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageManager : MonoBehaviour
{

    public GameObject mainScene, howToPlayScene;

    public GameObject htpP1, htpP2, htpBtnPrevious, htpBtnNext, htpBtnHome;
    //  public GameObject mainpageManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        GetComponent<ChangeScene>().changeScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void howToPlay()
    {
        mainScene.SetActive(false);
        howToPlayScene.SetActive(true);
        htpBtnHome.SetActive(true);
        htpP1.SetActive(true);
        htpP2.SetActive(false);
        htpBtnPrevious.SetActive(false);
        htpBtnNext.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void htpPreviousPage()
    {
        htpBtnPrevious.SetActive(false);
        htpBtnNext.SetActive(true);
        htpP1.SetActive(true);
        htpP2.SetActive(false);
    }
    public void htpNextPage()
    {
        htpBtnPrevious.SetActive(true);
        htpBtnNext.SetActive(false);
        htpP1.SetActive(false);
        htpP2.SetActive(true);
    }

    public void htpBackHome()
    {
        mainScene.SetActive(true);
        howToPlayScene.SetActive(false);
    }
}
