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
        // Здесь по идее должна ?перезагружаться сцена? и загружаться префаб нужного уровня
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        ResourceManager.LoadLevel(ResourceManager.Level.Level1);

        MenuManager.FireButtonClickAction();
    }
}
