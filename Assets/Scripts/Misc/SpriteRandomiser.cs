using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomiser : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _sprites;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
    }
}
