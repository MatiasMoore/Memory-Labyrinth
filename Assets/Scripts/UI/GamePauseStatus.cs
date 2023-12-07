using UnityEngine;

public class GamePauseStatus : MonoBehaviour
{
    private static bool _isGamePaused = false;

    public static void SetPausedGame(bool status)
    {
        _isGamePaused = status;
    }
}
