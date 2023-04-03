using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
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

    private void Start()
    {
        _currentIndex = 0;
        _weaponItems[_currentIndex].transform.position = Vector3.zero;
        _weaponItems[_currentIndex + 1].transform.position = _RightPoint.position;
        ColorChange();
    }
    public void Left()
    {
        ColorChange();
        if (_currentIndex == 0)
            return;

        _weaponItems[_currentIndex].transform.position = _RightPoint.position;
        _currentIndex--;
        ColorChange();
        _weaponItems[_currentIndex].transform.position = Vector3.zero;
    }

    public void Right()
    {
        if (_currentIndex == _weaponItems.Length - 1)
            return;

        _weaponItems[_currentIndex].transform.position = _leftPoint.position;
        _currentIndex++;
        ColorChange();
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

    public void BuyWeapon()
    {
        WeaponHave.instance.AddWeapon(_weaponItems[_currentIndex]);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
