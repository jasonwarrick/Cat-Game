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
                messageText.text = "YOUR WORK IS DONE";
                break;

            case 1: // Game Lost to Cat
                messageText.text = "THE CAT WANTS MORE";
                break;

            case 2: // Game Lost to Work
                messageText.text = "YOUR WORK IS UNFINISHED";
                break;
        }
    }

    void OnEnable() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MainMenuHit() {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(0);
    }

    public void PlayAgainHit() {
        GameStateManager.instance.ReloadLevel();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
