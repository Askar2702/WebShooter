using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePush;

public class MoreGames : MonoBehaviour
{
    [SerializeField] private GameObject _panelReview;
    private void Start()
    {
        int num = (Game.instance.Level + 1) % 3;
        if(!Game.instance.IsReview && num == 0)
        {
            _panelReview.SetActive(true);
        }
    }
    public void OpenGamesStore()
    {
        if (GP_App.Url().Contains(".ru"))
            Application.OpenURL("https://yandex.ru/games/developer?name=Askar-Developer");
        if (GP_App.Url().Contains(".com"))
            Application.OpenURL("https://yandex.com/games/developer?name=Askar-Developer");
        if (GP_App.Url().Contains(".com.tr"))
            Application.OpenURL("https://yandex.com.tr/games/developer?name=Askar-Developer");
    }

    public void ShowReview()
    {
        //// ссылка на игру после выпуска
        Debug.Log("show");
        _panelReview.SetActive(false);
        Game.instance.IsReview = true;
        if (GP_App.Url().Contains(".ru"))
            Application.OpenURL("https://yandex.ru/games/?utm_source=game_header_logo#app=231498");
        if (GP_App.Url().Contains(".com"))
            Application.OpenURL("https://yandex.com/games/?utm_source=game_header_logo#app=231498");
        if (GP_App.Url().Contains(".com.tr"))
            Application.OpenURL("https://yandex.com.tr/games/?utm_source=game_header_logo#app=231498");
    }


}
