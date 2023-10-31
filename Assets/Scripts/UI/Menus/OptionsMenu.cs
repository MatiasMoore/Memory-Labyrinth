using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.OpenMenu(Menu.MAIN, gameObject);
    }
}
