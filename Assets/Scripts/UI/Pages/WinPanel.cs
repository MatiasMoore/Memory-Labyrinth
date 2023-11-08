using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void OnClickNextLevel()
    {
        // Здесь должен быть переход на следующий уровень (?перезапускаться сцена? и загружаться префаб нужного уровня)
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);
    }

    public void OnClickToMainMenu()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.MainMenu);
    }

    public void OnClickRestart()
    {
        // Здесь по идее должна ?перезагружаться сцена? и загружаться префаб нужного уровня
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);
    }
}
