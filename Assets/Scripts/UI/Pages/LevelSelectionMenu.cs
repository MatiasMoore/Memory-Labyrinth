using UnityEngine;

public class LevelSelectionMenu : MonoBehaviour
{
    public void OnClickLoadLevel()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        // TODO: script with enum in inspector + add to each button level index from enum + load level prefab looking on level index

        MenuManager.FireButtonClickAction();
    }
}
