using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TeleportLib
{
    public struct TeleportStruct
    {
        Vector3 coords;
        Vector3 targetCoords;
    }
    [RequireComponent(typeof(BoxCollider2D))]
    public class Teleport : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        public Teleport(Transform target)
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
