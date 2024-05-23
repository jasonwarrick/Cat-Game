using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public void SettingsPressed() {
        CanvasManager.instance.ShowSettings();
    }
}
