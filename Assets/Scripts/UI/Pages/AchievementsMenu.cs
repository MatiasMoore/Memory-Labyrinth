using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu: MonoBehaviour
{
    [SerializeField]
    ScrollRect scrollRectObject;

    private void OnEnable()
    {
        scrollRectObject.verticalNormalizedPosition = 1;
    }

    public void OnClickBack()
    {
        MenuManager.ClosePage(MenuManager.Page.ACHIEVEMENTS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        MenuManager.FireButtonClickAction();
    }
}
