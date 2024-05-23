using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public delegate void FilterToggled();
    public static FilterToggled filterToggled;

    bool isMuted = false;

    [SerializeField] AudioMixer master;

    public void ToggleFilterPressed() {
        filterToggled.Invoke();
    }

    public void ToggleAudioPressed() {
        Debug.Log("toggle");
        float newVol;

        if (!isMuted) {
            newVol =  -80f;
        }  else {
            newVol = 0f;
        }

        isMuted = !isMuted;
        master.SetFloat("Volume", newVol);
    }
}
