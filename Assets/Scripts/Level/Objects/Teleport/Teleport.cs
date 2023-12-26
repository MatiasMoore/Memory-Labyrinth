using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TeleportLib
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Teleport : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _target;

        public Teleport(Vector2 target)
        {
            _target = target;
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TeleportableObject teleportableObject = other.gameObject.GetComponent<TeleportableObject>();
            if (teleportableObject != null)
            {
                Debug.Log($"Teleporting {other.gameObject.name} to {_target}");
                teleportableObject.Teleport(_target);
            }
            else
            {
                Debug.Log($"Teleporting {other.gameObject.name} failed");
            }
        }
        public void DisableTeleportPath()
        {
            GetComponent<LineRenderer>().enabled = false;
        }

        public void SetTeleportPosition(Vector2 position)
        {
            _target = position;
        }

        public Vector2 GetTeleportPosition()
        {
            return _target;
        }
    }
}
