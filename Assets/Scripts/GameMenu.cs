using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void GoToScene()
    {
        SceneManager.LoadScene("XR Toolkit VR");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
