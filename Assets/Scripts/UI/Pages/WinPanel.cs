using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void OnClickNextLevel()
    {
        // ����� ������ ���� ������� �� ��������� ������� (?��������������� �����? � ����������� ������ ������� ������)
        SceneManager.LoadScene(0);
    }

    public void OnClickToMainMenu()
    {
        // ����� ������ ���� Scene Manager (��� ���� ������� BuildIndex �������� ����)
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        // ����� �� ���� ������ ?��������������� �����? � ����������� ������ ������� ������
        SceneManager.LoadScene(0);
    }
}
