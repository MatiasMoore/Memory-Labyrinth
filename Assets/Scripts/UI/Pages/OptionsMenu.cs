using UnityEngine;
using UnityEngine.Events;

public class OptionsMenu : MonoBehaviour
{
    public void OnClickBack()
    {
        MenuManager.ClosePage(MenuManager.Page.OPTIONS);
        MenuManager.OpenPage(MenuManager.Page.MAIN);

        MenuManager.FireButtonClickAction();
    }
}
