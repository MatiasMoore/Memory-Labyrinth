using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);

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
