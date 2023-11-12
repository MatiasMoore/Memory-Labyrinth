using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void OnClickPause()
    {
        Time.timeScale = 0f;
        MenuManager.OpenPage(MenuManager.Page.PAUSE);
        PauseMenu.setPausedGame(true);
        Timer.SetTimerStatus(false);

        MenuManager.FireButtonClickAction();
    }
}
