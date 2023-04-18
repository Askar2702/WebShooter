using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingGame : MonoBehaviour
{
    public static SettingGame instance { get; private set; }
    [SerializeField] private Slider _gameSoundSlider;
    [SerializeField] private Slider _gameMusicSlider;
    [SerializeField] private Slider _speedCameraSlider;

    [SerializeField] private AudioSource _musicAudio;
    [SerializeField] private GameObject _panelSettings;

    [field:SerializeField]  public float SpeedCamera { get; private set; }
    [field: SerializeField] public float VolumeSound { get; private set; }

    [SerializeField] private List<AudioSource> _audioSources;

    public UnityEvent<float> ChangeSpeed = new UnityEvent<float>();
    private void Awake()
    {
        if (!instance) instance = this;
    }
    private void Start()
    {
        print(WeaponHave.instance.AudioMusic);
        _musicAudio = WeaponHave.instance.AudioMusic;
        LoadData();
        var audioSources = FindObjectsOfType<AudioSource>(true);
        AddAudioSources(audioSources);
        
        _gameSoundSlider.onValueChanged.AddListener(delegate {
            VolumeSound = _gameSoundSlider.value;
            Game.instance.SoundVolume = VolumeSound;
            SetSoundVolume();
           // SaveSetting();
        });
        _gameMusicSlider.onValueChanged.AddListener(delegate {
            _musicAudio.volume = _gameMusicSlider.value;
            Game.instance.MusicVolume = _gameMusicSlider.value;
            //  SaveSetting();
        });
        _speedCameraSlider.onValueChanged.AddListener(delegate {
            SpeedCamera = _speedCameraSlider.value;
            Game.instance.SpeedCamera = SpeedCamera;
            ChangeSpeed?.Invoke(SpeedCamera);
          //  SaveSetting();
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClosePanel(true);
        }
    }

    public void ClosePanel(bool activ)
    {
        _panelSettings.SetActive(activ);
        if (activ)
        {
            UIManager.instance.ShowAimTarget(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            UIManager.instance.ShowAimTarget(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }
    private void SetSound()
    {
        _musicAudio.volume = _gameMusicSlider.value;
    }

    public void SetSoundVolume(AudioSource[] audioSources = null)
    {
        AddAudioSources(audioSources);
        var audios = _audioSources;
        foreach (var a in audios)
        {
            if (a != _musicAudio)
            {
              //  a.volume = VolumeSound;
                a.volume = Game.instance.SoundVolume; 
            }
        }
    }

    private void AddAudioSources(AudioSource[] audioSources)
    {
        if (audioSources == null) return;
        foreach (var a in audioSources)
        {
            if (!_audioSources.Contains(a)) _audioSources.Add(a);
        }
    }
    private void LoadData()
    {
        _gameSoundSlider.value = Game.instance.SoundVolume;
        _speedCameraSlider.value = Game.instance.SpeedCamera;
        Player.instance.playerInput.SetRotateCamera(Game.instance.SpeedCamera);
        _gameMusicSlider.value = Game.instance.MusicVolume;

        //if (PlayerPrefs.HasKey("soundVolume"))
        //{
        //    VolumeSound = PlayerPrefs.GetFloat("soundVolume");
        //    _gameSoundSlider.value = VolumeSound;
        //}
        //else VolumeSound = _gameSoundSlider.value;
        //if (PlayerPrefs.HasKey("speedCamera"))
        //{
        //    SpeedCamera = PlayerPrefs.GetFloat("speedCamera");
        //    _speedCameraSlider.value = SpeedCamera;
        //    Player.instance.playerInput.SetRotateCamera(SpeedCamera);
        //}
        //else SpeedCamera = _speedCameraSlider.value;
        //if (PlayerPrefs.HasKey("musicVolume"))
        //{
        //    _gameMusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //    SetSound();
        //}
    }
    //private void SaveSetting()
    //{
    //    PlayerPrefs.SetFloat("soundVolume", VolumeSound);
    //    PlayerPrefs.SetFloat("musicVolume", _gameMusicSlider.value);
    //    PlayerPrefs.SetFloat("speedCamera", SpeedCamera);
    //}

    
}
