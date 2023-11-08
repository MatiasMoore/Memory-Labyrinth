using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.ClosePage(MenuManager.Page.OPTIONS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);
    }
}
