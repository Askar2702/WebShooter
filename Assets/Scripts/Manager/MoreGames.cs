using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreGames : MonoBehaviour
{
    public void OpenGamesStore()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
            Application.OpenURL("https://yandex.ru/games/developer?name=Askar-Developer");
        else Application.OpenURL("https://yandex.com/games/developer?name=Askar-Developer");
    }

    
}
