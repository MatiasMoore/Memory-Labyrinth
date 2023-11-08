using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void OnClickNextLevel()
    {
        // ����� ������ ���� ������� �� ��������� ������� (?��������������� �����? � ����������� ������ ������� ������)
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);
    }

    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
    }

    public void OnClickRestart()
    {
        // ����� �� ���� ������ ?��������������� �����? � ����������� ������ ������� ������
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);
    }
}
