using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
    }

    public void OnClickRestart()
    {
        // ����� �� ���� ������ ?��������������� �����? � ����������� ������ ������� ������
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);
    }
}
