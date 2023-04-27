using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationText : MonoBehaviour
{
    [SerializeField] private List<string> _rus;
    [SerializeField] private List<string> _eng;
    [SerializeField] private List<string> _tur;

    [SerializeField] private TextMeshProUGUI[] _textsScene;

    private void Start()
    {
        StartSceneSetLanguage(Game.instance.LocalizationIndex);
    }
    public void StartSceneSetLanguage(int langIndex)
    {
        List<string> lang = new List<string>();
        if (langIndex == 0) lang = _rus;
        if (langIndex == 1) lang = _eng;
        if (langIndex == 2) lang = _tur;
        for(int i = 0; i < _textsScene.Length; i++)
        {
            _textsScene[i].text = lang[i];
        }
        Game.instance.LocalizationIndex = langIndex;
    }
}

