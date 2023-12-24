using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.CheckpointLib
{
   public struct CheckpointStruct
    {
        public Vec3 coords;
        public int queue;
    }
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Checkpoint : MonoBehaviour, IStructable<CheckpointStruct>
    {
        [SerializeField]
        private int _queue;

        private bool _isReached = false;

        public Checkpoint(int queue)
        {
            _queue = queue;
        }

        public CheckpointStruct ToStruct()
        {
            return new CheckpointStruct { coords = new Vec3(transform.position), queue = GetQueue() };
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
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
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else
                {
                    Debug.Log($"Checkpoint {other.gameObject.name} failed");
                }
            }
        }
    }
}

