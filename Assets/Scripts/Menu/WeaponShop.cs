using GamePush;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public static WeaponShop instance { get; private set; }
    [SerializeField] private Image _left;
    [SerializeField] private Image _Right;
    [SerializeField] private Color _normColor;
    [SerializeField] private Color _transparentColor;

    [Space(30)]
    [SerializeField] private WeaponParent[] _weaponItems;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _RightPoint;
    [SerializeField] private float _speed;
    private int _currentIndex;

    [SerializeField] private GameObject _adsPanel;
    [SerializeField] private GameObject _inInventoryPanel;

    private void Awake()
    {
        if (!instance) instance = this;
    }
    private void Start()
    {
        _currentIndex = 0;
        _weaponItems[_currentIndex].transform.position = Vector3.zero;
        _weaponItems[_currentIndex + 1].transform.position = _RightPoint.position;
        ColorChange();
        CheckPurchase(_currentIndex);
       // GP_Ads.ShowFullscreen();
    }
    public void Left()
    {
        ColorChange();
        if (_currentIndex == 0)
            return;

        _weaponItems[_currentIndex].transform.position = _RightPoint.position;
        _currentIndex--;
        ColorChange();
        CheckPurchase(_currentIndex);
        _weaponItems[_currentIndex].transform.position = Vector3.zero;
    }

    public void Right()
    {
        if (_currentIndex == _weaponItems.Length - 1)
            return;

        _weaponItems[_currentIndex].transform.position = _leftPoint.position;
        _currentIndex++;
        ColorChange();
        CheckPurchase(_currentIndex);
        _weaponItems[_currentIndex].transform.position = Vector3.zero;
        if (_currentIndex == _weaponItems.Length - 1) return;
        _weaponItems[_currentIndex + 1].transform.position = _RightPoint.position;

    }

    private void ColorChange()
    {
        if (_currentIndex == _weaponItems.Length - 1)
        {
            _Right.color = _transparentColor;
            _left.color = _normColor;
        }

        if (_currentIndex == 0)
        {
            _left.color = _transparentColor;
            _Right.color = _normColor;
        }
        else if(_currentIndex > 0 && _currentIndex < _weaponItems.Length - 1) 
        {
            _left.color = _normColor;
            _Right.color = _normColor;
        }
    }

    public void ShowAdsReward()
    {
        GameScoreAdsManager.instance.ShowReward(_currentIndex.ToString());
    }
    public void BuyWeapon(int index)
    {
        WeaponHave.instance.AddWeapon(_weaponItems[index]);
        CheckPurchase(index);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void CheckPurchase(int index)
    {
        if (WeaponHave.instance.GetWeapons().Contains(_weaponItems[index]))
        {
            _adsPanel.SetActive(false);
            _inInventoryPanel.SetActive(true);
        }
        else
        {
            _adsPanel.SetActive(true);
            _inInventoryPanel.SetActive(false);
        }
    }
}
