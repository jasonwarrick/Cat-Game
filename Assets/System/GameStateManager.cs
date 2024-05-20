using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public delegate void TimeChanged(int hours, int minutes);
    public static TimeChanged timeChanged;

    [SerializeField] float timeFactor; // The higher the factor, the quicker time passes
    [SerializeField] int[] startTime = new int[2];
    [SerializeField] int timeIncrement;
    
    int[] time = new int[2] { 10, 0 } ; // { Hours, Minutes }
    float timeCounter = 0f;
    bool isPM = false;

    bool inMinigame = false;
    bool paused = false;
    public GameObject heldObject;
    GameObject player;

    [SerializeField] GameObject hud;
    [SerializeField] GameObject gameEndScreen;
    [SerializeField] GameObject pauseScreen;

    void Start() {
        player = FindObjectOfType<FirstPersonMovement>().gameObject;
        instance = this;
        time = startTime;
        timeChanged.Invoke(time[0], time[1]);
        SetGameCanvases(true);

        MinigameManager.minigameStarted += SetInMinigame;
        Meter.meterFull += GameLost;
        Meter.meterDanger += Danger;
    }

    void OnDestroy() {
        MinigameManager.minigameStarted -= SetInMinigame;
        Meter.meterFull -= GameLost;
        Meter.meterDanger -= Danger;
    }
    
    void Update() {
        timeCounter += Time.deltaTime;

        // if (heldObject != null) {
        //     Debug.Log(heldObject.name);
        // }

        if (timeCounter >= timeIncrement / timeFactor) {
            timeCounter = 0f;
            AddMinute();
        }
    }

    void AddMinute() {
        time[1] += timeIncrement;

        if (time[1] >= 60) {
            time[1] = 0;
            time[0]++;

            if (time[0] > 12) {
                time[0] = 1;
                isPM = !isPM;
            }
        }

        timeChanged.Invoke(time[0], time[1]);

        if (time[0] >= 10 && isPM) {
            GameLost(false);
        }
    }

    public void SetInMinigame(bool isInMinigame) {
        inMinigame = isInMinigame;

        // Debug.Log(inMinigame);
    }

    public bool GetInMinigame() { return inMinigame; }

    public void PauseGame() {
        if (!paused) { 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0f;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }

        paused = !paused;
        pauseScreen.SetActive(paused);
    }

    void Danger(Meter meter) {
        Debug.Log(meter.Name + " is in danger");
    }

    void SetGameCanvases(bool inGame) {
        hud.SetActive(inGame);
        gameEndScreen.SetActive(!inGame);
    }
    
    void GameLost(bool isMeter) {
        SetGameCanvases(false);

        if (!isMeter) {
            Debug.Log("no meter");
            gameEndScreen.GetComponent<GameEndManager>().SetMessage(2);
        } else {
            gameEndScreen.GetComponent<GameEndManager>().SetMessage(1);
        }

        LockPlayer();
        Debug.Log("game lost");
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ReloadLevel() {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LockPlayer() {
        player.GetComponent<FirstPersonMovement>().enabled = false;
        player.GetComponentInChildren<FirstPersonLook>().enabled = false;
        player.GetComponent<PlayerInteraction>().enabled = false;
        player.GetComponent<InputReader>().enabled = false;
    }

    public void GameWon() {
        LockPlayer();
        SetGameCanvases(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        gameEndScreen.GetComponent<GameEndManager>().SetMessage(0);
    }
}
