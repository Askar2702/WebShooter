using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float MusicVolume;
    public float SoundVolume;
    public float SpeedCamera;
    public int Level;
    public GameData(Game data)
    {
        MusicVolume = data.MusicVolume;
        SoundVolume = data.SoundVolume;
        SpeedCamera = data.SpeedCamera;
        Level = data.Level;
    }


}
