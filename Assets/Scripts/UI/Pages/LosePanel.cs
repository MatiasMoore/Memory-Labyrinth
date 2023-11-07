using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public void OnClickToMainMenu()
    {
        // Здесь должен быть Scene Manager (при этом браться BuildIndex главного меню)
        SceneManager.LoadScene(1);
    }

    public void OnClickRestart()
    {
        // Здесь по идее должна ?перезагружаться сцена? и загружаться префаб нужного уровня
        SceneManager.LoadScene(0);
    }
}
