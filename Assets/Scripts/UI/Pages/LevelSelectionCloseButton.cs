using UnityEngine;

public class LevelSelectionCloseButton : MonoBehaviour
{
    public void OnClickBack()
    {
        MenuManager.ClosePage(MenuManager.Page.LEVEL_SELECTION);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        MenuManager.FireButtonClickAction();
    }
}
