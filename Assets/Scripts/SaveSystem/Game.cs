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
    private int _level;
    private int _localizationIndex = 0;
    private bool _isReview;
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
    
    public int LocalizationIndex
    {
        get
        { return _localizationIndex; }
        set
        {
            if (_localizationIndex != value)
            {
                _localizationIndex = value;
                if (_localizationIndex > 2) _localizationIndex = 2;
                if (_localizationIndex < 0) _localizationIndex = 0;
                SaveData();
            }
        }
    }
    public bool IsReview
    {
        get
        { return _isReview; }
        set
        {
            if (_isReview != value)
            {
                _isReview = value;
                SaveData();
            }
        }
    }

    private void Awake()
    {
        if (!instance) instance = this;
         print(Application.persistentDataPath);
        LoadData();
    }

    public void ChangePlayButton(string text , TMP_FontAsset asset)
    {
        if (!_lvlNumber) return;
        _lvlNumber.text = $"{text} #{_level + 1}";
        _lvlNumber.font = asset;
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
        _localizationIndex = data.LocalizationIndex;
        _isReview = data.IsReview;
    }

    
}
