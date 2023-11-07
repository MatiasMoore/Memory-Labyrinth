using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.OpenPage(MenuManager.Page.MAIN, gameObject);
    }
}
