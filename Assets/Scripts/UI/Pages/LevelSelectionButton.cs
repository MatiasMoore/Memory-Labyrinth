using MemoryLabyrinth.Resources;
using MemoryLabyrinth.SaveLoad;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField]
    public ResourceManager.Level _level;

    public void OnClick()
    {
        ResourceManager.LoadScene(ResourceManager.AvailableScene.GameField);
        CurrentLevel.Load(_level);
        MenuManager.FireButtonClickAction();
    }
}
