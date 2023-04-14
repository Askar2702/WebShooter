using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;

public class WeaponCatalog : MonoBehaviour
{
    public static WeaponCatalog instance;
    public UnityEvent GrenadeRemove;
    public int WeaponsCount => WeaponCurrentCatalog.Count;

    [SerializeField] private WeaponParent[] weaponCatalogs;

    private List<WeaponParent> WeaponCurrentCatalog;

    [Space(30)]
    [SerializeField] private Image[] WeaponCurrentCatalogIcons;

    [Space(10)]
    [SerializeField] private List<Image> _backgroundIcon;
    [field:SerializeField] public WeaponParent CurrentWeapon { get; private set; }

    [field: SerializeField] public PistolWeapon Pistol { get; private set; }
    [field: SerializeField] public BombWeapon Bomb { get; private set; }
    public int IndexCurrentWeapon = 0;

    private void Awake()
    {
        if (!instance) instance = this;
        CurrentWeapon = Pistol;
        CurrentWeapon.RigOn();
        WeaponInit();
        CurrentWeapon.gameObject.SetActive(true);
    }
    private void Start()
    {
        AnimationManager.instance.SetGun(CurrentWeapon.Gun);
        Player.instance.Gun = CurrentWeapon.FireGun;
        SelectIcon(0);
    }
    public void SelectWeapon(int indexWeapon , AudioSource audio)
    {
        if (indexWeapon >= WeaponCurrentCatalog.Count) return;

        audio.Play();
        int i = 0;
        SelectIcon(indexWeapon);
        foreach (var weapon in WeaponCurrentCatalog)
        {
            if (indexWeapon == i && weapon != null)
            {
                CurrentWeapon = weapon;
                Player.instance.Gun = CurrentWeapon.FireGun;
                CurrentWeapon.RigOn();
                IndexCurrentWeapon = i;
            }
            if (indexWeapon != i && weapon != null)
            {
                weapon.RigOff();
                weapon.gameObject.SetActive(false);
            }
            else if (indexWeapon == i && weapon == null)
            {
                CurrentWeapon = null;
            }
            i++;
        }
        SwitchingWeaponShowAnim();
    }
    private void ShowWeapon()
    {
        CurrentWeapon.gameObject.SetActive(true);
        AnimationManager.instance.SetGun(CurrentWeapon.Gun);
    }
    private void SwitchingWeaponShowAnim()
    {
        if (CurrentWeapon == null) return;
        AnimationState state;
        if (CurrentWeapon.GetType() == typeof(BaseWeapon))
        { 
            state = AnimationState.Idle;
        }
      
        else if (CurrentWeapon.GetType() == typeof(PistolWeapon))
        {
            state = AnimationState.Idle;
        }
        else state = AnimationState.StartGrenade;
        AnimationManager.instance.SwitchingWeaponAnim(ShowWeapon , state);
    }

    public void EnabledBomb(bool activ)
    {
        Bomb.gameObject.SetActive(activ);
    }

    private void WeaponInit()
    {
        WeaponCurrentCatalog = new List<WeaponParent>();
        WeaponCurrentCatalog.Add(Pistol);
        WeaponCurrentCatalog.Add(Bomb);

        var weapons = WeaponHave.instance.GetWeapons();
        for (int i = 0; i < WeaponHave.instance.GetWeapons().Count; i++)
        {
            WeaponCurrentCatalog.Add(weaponCatalogs[weapons[i].id]);
            WeaponCurrentCatalogIcons[i].GetComponent<Image>().sprite = weaponCatalogs[weapons[i].id].Icon;
            WeaponCurrentCatalogIcons[i].transform.parent.gameObject.SetActive(true);
        }
    }

    private void SelectIcon(int index)
    {
        for(int i = 0; i < _backgroundIcon.Count; i++)
        {
            _backgroundIcon[i].color = Color.white;
        }
        _backgroundIcon[index].color = Color.blue;
    }

    public void DeleteGrenade()
    {
        ///удаление 
        GrenadeRemove?.Invoke();
        WeaponCurrentCatalog.Remove(WeaponCurrentCatalog.FirstOrDefault(item => item == Bomb));
        _backgroundIcon[1].gameObject.SetActive(false);
        _backgroundIcon.Remove(_backgroundIcon[1]);
    }
}
