using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _losePanel;
    #region Result text
    [SerializeField] private TextMeshProUGUI _doubleKill;
    [SerializeField] private TextMeshProUGUI _tripleKill;
    [SerializeField] private TextMeshProUGUI _monsterKill;
    [SerializeField] private TextMeshProUGUI _rampage;
    [SerializeField] private TextMeshProUGUI _headShot;
    #endregion

    [Space(30)]
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _finishClip;
    [SerializeField] private AudioClip _loseClip;

    public UnityEvent OffEnemySound = new UnityEvent();
    public UnityEvent OnEnemySound = new UnityEvent();
    private void Awake()
    {
        Time.timeScale = 1;
        if (!WeaponHave.instance.AudioMusic.isPlaying) WeaponHave.instance.AudioMusic.Play();
        if (!instance) instance = this;
    }
    private void Start()
    {
        ScoreManager.instance.Finished.AddListener(ShowFinish);
        Player.instance.PlayerDead?.AddListener(ShowLose);
    }
    public void RestartGame()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
    public void ExitGame()
    {
        Destroy(WeaponHave.instance.gameObject);
        SceneManager.LoadScene("Menu");
    }

   
    public void NextGame()
    {
        Game.instance.Level++;
        ExitGame();
    }
    private void ShowFinish(FinishParametrs parametrs)
    {
        StartCoroutine(DelayShowfinish(parametrs));
    }

    private void ShowLose()
    {
        StartCoroutine(DelayShowLose());
    }

    IEnumerator DelayShowfinish(FinishParametrs parametrs)
    {
        yield return new WaitForSeconds(2f);
        OffEnemySound?.Invoke();
        WeaponHave.instance.AudioMusic.Stop();
        _audio.clip = _finishClip;
        _audio.Play();
        _doubleKill.text = "X" + parametrs.DoubleKillCount.ToString();
        _tripleKill.text = "X" + parametrs.TripleKillCount.ToString();
        _monsterKill.text = "X" + parametrs.MonsterKillCount.ToString();
        _rampage.text = "X" + parametrs.RampageCount.ToString();
        _headShot.text = "X" + parametrs.HeadShotCount.ToString();

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        Cursor.visible = true;

        _finishPanel.SetActive(true);
    }

    IEnumerator DelayShowLose()
    {
        OffEnemySound?.Invoke();
        WeaponHave.instance.AudioMusic.Stop();
        _audio.clip = _loseClip; 
        _audio.Play();
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        Cursor.visible = true;
        
        _losePanel.SetActive(true);
    }
}
