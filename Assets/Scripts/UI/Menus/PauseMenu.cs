using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) ���� ��� ������ (�� ��������) ������� � Scene Manager
     * 2) ���� ���-�� ��������� � ��������� ����� �� ������ ������ ��� ������� �� ������ �������� (������ ����������� ������ �������� ������� (�� ID))
    */

    public static bool _isPaused = false;

    public void OnClickResume()
    {
        Debug.Log("RESUMED!");
        MenuManager.OpenPage(MenuManager.Page.GAME, gameObject);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void OnClickToMainMenu()
    {
        Debug.Log("MAIN MENU!");
        // ����� ������ ���� Scene Manager (��� ���� ������� ID �������� ����)
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        Debug.Log("RESTARTED!");
        // ����� ������ ���� Scene Manager (��� ���� ������� ID �������� ������)
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
