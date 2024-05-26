using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public delegate void MinigameStarted(bool isStarting);
    public static MinigameStarted minigameStarted;

    public string meterName;

    bool isStarted = false;
    [SerializeField] bool ignoreEvent = false;
    
    [SerializeField] GameObject cam;
    [SerializeField] GameObject player;
    [SerializeField] PickupParent pickupParent;
    CatBrain catBrain;
    
    Coroutine winCoroutine;

    void Start() {
        cam.SetActive(false);
        
        catBrain = FindObjectOfType<CatBrain>();

        // if (ignoreEvent) {
        //     StartMinigame();
        // }
    }

    public void StartMinigame() {
        isStarted = true;

        if (!ignoreEvent) {
            minigameStarted.Invoke(isStarted);
        }

        cam.SetActive(isStarted);

        Debug.Log("mingame started");

        player.SetActive(false);
        HUDManager.instance.SwitchCamera(cam.GetComponent<Camera>());
    }

    void Update() {
        if (isStarted && Input.GetKeyDown(KeyCode.Space)) {
            StopMinigame(false);
        }
    }

    public void StopMinigame(bool wonGame) {
        isStarted = false;
        player.SetActive(true);
        
        if (pickupParent != null && !wonGame) {
            pickupParent.TurnOnLight();
        }

        if (!ignoreEvent) {
            minigameStarted.Invoke(isStarted);
        }

        cam.SetActive(isStarted);

        HUDManager.instance.SwitchCamera(null);
    }

    public void CompleteMinigame() {
        winCoroutine = StartCoroutine("CompleteCoroutine");
    }

    IEnumerator CompleteCoroutine() {
        yield return new WaitForSeconds(1f);
        if (meterName != null) {
            catBrain.ResetMeter(meterName);
        }
        
        StopMinigame(true);
        StopCoroutine(winCoroutine);
    }
}
