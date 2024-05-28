using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    public void SetMessage(int gameResult) {
        switch (gameResult) {
            case 0: // Game Won
                messageText.text = "SUCCESS:\nYOUR WORK IS DONE";
                break;

            case 1: // Game Lost to Cat
                messageText.text = "FAILURE:\nTHE CAT WANTS MORE";
                break;

            case 2: // Game Lost to Work
                messageText.text = "FAILURE:\nYOUR WORK IS UNFINISHED";
                break;
        }
    }

    void OnEnable() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MainMenuHit() {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(0);
    }

    public void PlayAgainHit() {
        Time.timeScale = 1f;
        GameStateManager.instance.ReloadLevel();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
