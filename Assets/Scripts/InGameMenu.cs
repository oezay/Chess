using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ResetGame()
    {
        BoardController.Instance.EndGame();
    }

}
