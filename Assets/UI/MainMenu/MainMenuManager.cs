using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGamePressed() {
        SceneManager.LoadScene(1);
    }

    public void QuitPressed() {
        Application.Quit();
        Debug.Log("Quit pressed");
    }
}
