using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void onClickBack()
    {
        MenuManager.OpenPage(Page.MAIN, gameObject);
    }
}
