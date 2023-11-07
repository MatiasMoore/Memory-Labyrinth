using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : MonoBehaviour
{
    public void OnClickPause()
    {
        Debug.Log("PAUSED!");
        MenuManager.OpenPage(Page.PAUSE, null);
        PauseMenu._isPaused = true;
    }
}
