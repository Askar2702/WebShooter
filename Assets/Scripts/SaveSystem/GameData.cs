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
    public int LocalizationIndex;
    public bool IsReview;
    public GameData(Game data)
    {
        MusicVolume = data.MusicVolume;
        SoundVolume = data.SoundVolume;
        SpeedCamera = data.SpeedCamera;
        Level = data.Level;
        LocalizationIndex = data.LocalizationIndex;
        IsReview = data.IsReview;
    }


}
