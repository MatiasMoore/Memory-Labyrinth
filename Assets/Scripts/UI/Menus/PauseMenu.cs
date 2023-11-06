using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) ���� ��� ������ (�� ��������) ������� � Menu Manager � Scene Manager
     * 2) ���� ���-�� ��������� � ��������� ����� �� ������ ������ ��� ������� �� ������ �������� (������ ����������� ������ �������� ������� (�� ID))
    */

    public GameObject _pauseMenu;

    public static bool _isPaused = false;

    public void OnClickPause()
    {
        // ����� ������ ���� Menu Manager (���������� Pause Menu ����� Menu Manager �� ����)
        Debug.Log("PAUSED!");
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void OnClickResume()
    {
        // ����� ������ ���� Menu Manager (����������� Pause Menu ����� Menu Manager �� ����)
        Debug.Log("RESUMED!");
        _pauseMenu.SetActive(false);
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
