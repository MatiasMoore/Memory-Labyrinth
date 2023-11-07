using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void OnClickPause()
    {
        Debug.Log("PAUSED!");
        MenuManager.OpenPage(MenuManager.Page.PAUSE, null);
        Time.timeScale = 0f;
        PauseMenu._isPaused = true;
    }
}
