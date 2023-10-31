using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onClickPlay()
    {
        Debug.Log("PLAY BUTTON CLICKED!");
        SceneManager.LoadScene("Scenes/LevelPathFindingTest");
    }

    public void onClickOptions()
    {
        MenuManager.OpenMenu(Menu.OPTIONS, gameObject);
    }

    public void onClickAchievements()
    {
        MenuManager.OpenMenu(Menu.ACHIEVMENTS, gameObject);
    }
}
