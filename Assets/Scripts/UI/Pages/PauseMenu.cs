using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*
     * 1) ���� ��� ������ (�� ��������) ������� � Scene Manager
     * 2) ���� ���-�� ��������� � ��������� ����� �� ������ ������ ��� ������� �� ������ �������� (������ ����������� ������ �������� ������� (�� ID))
    */

    private static bool _isPausedGame = false;

    public static void setPausedGame(bool flag)
    {
        _isPausedGame = flag;
    }

    public void OnClickResume()
    {
        MenuManager.ClosePage(MenuManager.Page.PAUSE);
        Time.timeScale = 1f;
        setPausedGame(false);
    }

    public void OnClickToMainMenu()
    {
        // ����� ������ ���� Scene Manager (��� ���� ������� ID �������� ����)
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        // ����� ������ ���� Scene Manager (��� ���� ������� ID �������� ������)
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
