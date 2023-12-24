using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TeleportLib
{
    public struct TeleportStruct
    {
        public Vector3 coords;
        public Vector3 targetCoords;
    }
    [RequireComponent(typeof(BoxCollider2D))]
    public class Teleport : MonoBehaviour, IStructable<TeleportStruct>
    {
        [SerializeField]
        private Transform _target;

        public Teleport(Transform target)
        {
            _target = target;
        }

        public TeleportStruct ToStruct()
        {
            return new TeleportStruct { coords = transform.position, targetCoords = _target.position };
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
                Debug.Log($"Teleporting {other.gameObject.name} to {_target.position}");
                teleportableObject.Teleport(_target.position);
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
    }
}
