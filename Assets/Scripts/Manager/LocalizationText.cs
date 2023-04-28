using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationText : MonoBehaviour
{
    public static LocalizationText instance { get; private set; }
    [SerializeField] private List<string> _rus;
    [SerializeField] private List<string> _eng;
    [SerializeField] private List<string> _tur;

    [SerializeField] private TextMeshProUGUI[] _textsScene;
    [SerializeField] private TMP_FontAsset _turkish;
    [SerializeField] private TMP_FontAsset _molot;
    private void Awake()
    {
        if (!instance) instance = this;
    }

    private void Start()
    {
        StartSceneSetLanguage(Game.instance.LocalizationIndex);
    }
    public void StartSceneSetLanguage(int langIndex)
    {
        List<string> lang = new List<string>();
        TMP_FontAsset asset = _molot;
        if (langIndex == 0)
        {
            lang = _rus;
        }
        if (langIndex == 1)
        {
            lang = _eng;
        }
        if (langIndex == 2)
        {
            lang = _tur;
            asset = _turkish;
        }
        for(int i = 0; i < _textsScene.Length; i++)
        {
            _textsScene[i].text = lang[i];
            _textsScene[i].font = asset;
        }
        Game.instance.ChangePlayButton(lang[0], asset);
        Game.instance.LocalizationIndex = langIndex;
    }

    public (string , TMP_FontAsset) GetText(int amount)
    {
        if (amount >= 5) amount = 5;
        amount--;
        if (Game.instance.LocalizationIndex == 0)
        {
            
            return (_rus[amount] , _molot);
        }
        else if (Game.instance.LocalizationIndex == 1)
        {

            return (_eng[amount], _molot);
        }
        else 
        {

            return (_tur[amount], _turkish);
        }
    }
}

