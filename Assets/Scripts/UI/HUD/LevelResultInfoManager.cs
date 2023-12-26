using UnityEngine;
using TMPro;

namespace MemoryLabyrinth.UI.HUD
{
    public class LevelResultInfoManager : MonoBehaviour
    {
        public static void SetLevelTime(Timer timer, TextMeshProUGUI textMeshPro)
        {
            if (timer == null)
                throw new System.Exception("LevelResultInfoController: SetLevelTimeOnLose -> timer = null");

            textMeshPro.text = "Время: " + timer.GetTimerValue();
        }

        public static void SetLevelGemsCount(Gems gems, TextMeshProUGUI textMeshPro)
        {
            if (gems == null)
                throw new System.Exception("LevelResultInfoController: SetLevelGemsCountOnLose -> gems = null");

            textMeshPro.text = "Бонусы: " + gems.GetGemsCount();
        }
    }
}