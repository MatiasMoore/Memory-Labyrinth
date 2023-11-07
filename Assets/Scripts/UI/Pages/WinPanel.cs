using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void OnClickNextLevel()
    {
        // Здесь должен быть переход на следующий уровень (?перезапускаться сцена? и загружаться префаб нужного уровня)
        SceneManager.LoadScene(0);
    }

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
