using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.CheckpointLib
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Sprite _deactivatedSprite;

        [SerializeField]
        private Sprite _activatedSprite;

        [SerializeField]
        private int _queue;

        private bool _isReached = false;

        public Checkpoint(int queue)
        {
            _queue = queue;
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            if (_spriteRenderer != null)
                _deactivatedSprite = _spriteRenderer.sprite;
        }

        public int GetQueue()
        {
            return _queue;
        }

        public void SetQueue(int queue)
        {
            _queue = queue;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isReached)
            {
                CheckpointCompatable checkpointObject =
                    other.gameObject.GetComponent<CheckpointCompatable>();
                if (checkpointObject != null)
                {
                    Debug.Log($"Checkpoint {other.gameObject.name} for {_queue}");
                    checkpointObject.getCheckpoint(this);
                    _isReached = true;
                    // Change color to yellow
                    if (_spriteRenderer != null)
                        _spriteRenderer.sprite = _activatedSprite;
                }
                else
                {
                    Debug.Log($"Checkpoint {other.gameObject.name} failed");
                }
            }
        }
    }
}

