using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
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
