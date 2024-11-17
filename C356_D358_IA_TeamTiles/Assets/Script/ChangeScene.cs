using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void changeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void nextScene()
    {
        changeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
