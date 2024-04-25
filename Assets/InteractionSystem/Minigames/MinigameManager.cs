using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public delegate void MinigameStarted(bool isStarting);
    public static MinigameStarted minigameStarted;

    public string meterName;

    bool isStarted = false;
    
    [SerializeField] GameObject cam;
    GameObject player;
    CatBrain catBrain;
    
    Coroutine winCoroutine;

    void Start() {
        cam.SetActive(false);
        
        player = FindObjectOfType<FirstPersonMovement>().gameObject;
        catBrain = FindObjectOfType<CatBrain>();
    }

    public void StartMinigame() {
        isStarted = true;

        minigameStarted.Invoke(isStarted);

        cam.SetActive(isStarted);

        Debug.Log("mingame started");

        player.SetActive(false);
        HUDManager.instance.SwitchCamera(cam.GetComponent<Camera>());
    }

    void Update() {
        if (isStarted && Input.GetKeyDown(KeyCode.Space)) {
            StopMinigame();
        }
    }

    public void StopMinigame() {
        isStarted = false;
        player.SetActive(true);
        
        minigameStarted.Invoke(isStarted);

        cam.SetActive(isStarted);
        Debug.Log("mingame ended");

        HUDManager.instance.SwitchCamera(null);
    }

    public void CompleteMinigame() {
        winCoroutine = StartCoroutine("CompleteCoroutine");
    }

    IEnumerator CompleteCoroutine() {
        yield return new WaitForSeconds(1f);
        catBrain.ResetMeter(meterName);
        StopMinigame();
        StopCoroutine(winCoroutine);
    }
}
