using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.FinishLib
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FinishPoint : MonoBehaviour
    {
        private bool _isOpen = true;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Sprite _openSprite;

        [SerializeField]
        private Sprite _closedSprite;

        private void Awake()
        {
            _openSprite = _spriteRenderer.sprite;
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        public void SetOpen(bool isOpen)
        {
            _isOpen = isOpen;
            if (_isOpen)
                _spriteRenderer.sprite = _openSprite;
            else
                _spriteRenderer.sprite = _closedSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            FinishCompatible finishObject =
                other.gameObject.GetComponent<FinishCompatible>();
            if (finishObject != null)
            {
                Debug.Log($"Finish {other.gameObject.name}");
                finishObject.OnFinish(other);
            }
            else
            {
                Debug.Log($"{other.gameObject.name} can't finish");
            }
        }
    }
}

