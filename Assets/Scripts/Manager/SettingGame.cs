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
        _musicAudio = WeaponHave.instance.AudioMusic;
        LoadData();
        var audioSources = FindObjectsOfType<AudioSource>(true);
        AddAudioSources(audioSources);
        
        _gameSoundSlider.onValueChanged.AddListener(delegate {
            VolumeSound = _gameSoundSlider.value;
            Game.instance.SoundVolume = VolumeSound;
            SetSoundVolume();
        });
        _gameMusicSlider.onValueChanged.AddListener(delegate {
            _musicAudio.volume = _gameMusicSlider.value;
            Game.instance.MusicVolume = _gameMusicSlider.value;
        });
        _speedCameraSlider.onValueChanged.AddListener(delegate {
            SpeedCamera = _speedCameraSlider.value;
            Game.instance.SpeedCamera = SpeedCamera;
            ChangeSpeed?.Invoke(SpeedCamera);
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClosePanel();
        }
    }

    public void ClosePanel()
    {
        bool activ = !_panelSettings.activeSelf;
        _panelSettings.SetActive(activ);
        if (activ)
        {
            GameManager.instance.OffEnemySound?.Invoke();
            UIManager.instance.ShowAimTarget(false);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            GameManager.instance.OnEnemySound?.Invoke();
            UIManager.instance.ShowAimTarget(true);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }
   

    public void SetSoundVolume(AudioSource[] audioSources = null)
    {
        AddAudioSources(audioSources);
        var audios = _audioSources;
        foreach (var a in audios)
        {
            if (a != _musicAudio)
            {
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
    }
    
}
