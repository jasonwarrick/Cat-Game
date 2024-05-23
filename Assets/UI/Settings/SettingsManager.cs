using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public delegate void FilterToggled();
    public static FilterToggled filterToggled;

    public void ToggleFilterPressed() {
        filterToggled.Invoke();
    }

    public void ToggleAudioPressed() {

    }
}
