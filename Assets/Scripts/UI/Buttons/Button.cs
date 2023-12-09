using UnityEngine;
using UnityEngine.Events;

public abstract class Button : MonoBehaviour
{
    public static event UnityAction _buttonClickSound;

    public static void FireButtonClickSoundAction()
    {
        _buttonClickSound?.Invoke();
    }

    public abstract event UnityAction _buttonClick;

    public abstract void FireButtonClickAction();

    public abstract void OnClick();
}
