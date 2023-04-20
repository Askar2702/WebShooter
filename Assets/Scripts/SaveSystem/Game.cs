using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Game : MonoBehaviour
{
    public static Game instance;
    private float _musicVolume = 0.3f;
    private float _soundVolume = 1f;
    private float _speedCamera = 1.2f;
    private int _level = 1;
    [SerializeField] private TextMeshProUGUI _lvlNumber;
    public float MusicVolume
    {
        get
        { return _musicVolume; }
        set
        {
            if (_musicVolume != value)
            {
                _musicVolume = value;
                SaveData();
            }
        }
    }

    public float SoundVolume
    {
        get
        { return _soundVolume; }
        set
        {
            if (_soundVolume != value)
            {
                _soundVolume = value;
                SaveData();
            }
        }
    }

    public float SpeedCamera
    {
        get
        { return _speedCamera; }
        set
        {
            if (_speedCamera != value)
            {
                _speedCamera = value;
                SaveData();
            }
        }
    }

    [System.Obsolete]
    public int Level
    {
        get
        { return _level; }
        set
        {
            if (_level != value)
            {
                _level = value;
                SaveData();
            }
        }
    }


    private void Awake()
    {
        if (!instance) instance = this;
         print(Application.persistentDataPath);
        LoadData();
        print(_level);
        _lvlNumber.text = $"play #{_level}";
    }

    [System.Obsolete]
    private void SaveData()
    {
        SaveSystem.SaveData(this);
    }
    private void LoadData()
    {
        GameData data = SaveSystem.LoadData();
        if (data == null) return;
        _musicVolume = data.MusicVolume;
        _soundVolume = data.SoundVolume;
        _speedCamera = data.SpeedCamera;
        _level = data.Level;
    }

    
}
