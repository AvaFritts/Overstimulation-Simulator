using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GlobalSettingsData
{
    public float masterVolume = .5f;
    public float musicVolume = .5f;
    public float sfxVolume = 1f;

    public int difficulty = 0;
    public bool extraStimulation = false;
    // Go to GlobalProgressData for the highScore and levels beaten.
}
