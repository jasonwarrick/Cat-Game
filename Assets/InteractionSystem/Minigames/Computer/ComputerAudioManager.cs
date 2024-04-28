using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource key1;
    [SerializeField] AudioSource key2;
    [SerializeField] AudioSource key3;

    public void PlayKeyAudio() {
        int whichKey = Random.Range(1, 4);
        // Debug.Log(whichKey);

        switch (whichKey) {
            case 1:
                key1.Play();
                break;

            case 2:
                key2.Play();
                break;

            case 3:
                key3.Play();
                break;
        }
    }
}