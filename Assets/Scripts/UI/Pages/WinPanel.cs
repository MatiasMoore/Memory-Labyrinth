using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void OnClickNextLevel()
    {
        // ����� ������ ���� ������� �� ��������� ������� (?��������������� �����? � ����������� ������ ������� ������)
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);

        MenuManager.FireButtonClickAction();
    }

    public void OnClickRestart()
    {
        // ����� �� ���� ������ ?��������������� �����? � ����������� ������ ������� ������
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);

        MenuManager.FireButtonClickAction();
    }
}
