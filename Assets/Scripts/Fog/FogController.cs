using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    private FogMaskTarget[] _masks = { };
    private SpriteRenderer _spriteRenderer;

    public static FogController Instance { get; private set; }

    public void Init()
    {
        if (Instance != null) return;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        Instance = this;
    }

    public void FadeInToAllTargets(float timeToFadeIn)
    {
        _masks = FindObjectsOfType<FogMaskTarget>();
        foreach (var mask in _masks)
        {
            mask.CreateMask();

            Vector2 maskPos = mask.GetPos();
            Vector2 topLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 5));
            Vector2 topRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 5));
            Vector2 bottomRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 5));
            Vector2 bottomLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 5));

            float toTopLeft = (topLeft - maskPos).magnitude;
            float toTopRight = (topRight - maskPos).magnitude;
            float toBottomRight = (bottomRight - maskPos).magnitude;
            float toBottomLeft = (bottomLeft - maskPos).magnitude;

            float startRadius = Mathf.Max(toTopLeft, toTopRight, toBottomRight, toBottomLeft) * 1.1f;
            mask.FadeMaskRadius(startRadius, mask.GetPreferredRadius(), timeToFadeIn);
        }
    }

    public void SetFogVisibile(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }
}
