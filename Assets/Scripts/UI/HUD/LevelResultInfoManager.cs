using UnityEngine;
using TMPro;

public class LevelResultInfoManager : MonoBehaviour
{
    public static void SetLevelTime(Timer timer, TextMeshProUGUI textMeshPro)
    {
        if (timer == null)
            throw new System.Exception("LevelResultInfoController: SetLevelTimeOnLose -> timer = null");

        textMeshPro.text = ": " + timer.GetTimerValue();
    }

    public static void SetLevelGemsCount(Gems gems, TextMeshProUGUI textMeshPro)
    {
        if (gems == null)
            throw new System.Exception("LevelResultInfoController: SetLevelGemsCountOnLose -> gems = null");

        textMeshPro.text = ": " + gems.GetGemsCount();
    }
}
