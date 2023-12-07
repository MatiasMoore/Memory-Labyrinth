using UnityEngine;
using UnityEngine.Events;

public abstract class Button : MonoBehaviour
{
    public abstract event UnityAction _buttonClick;

    public abstract void FireButtonClickAction();

    public abstract void OnClick();
}
