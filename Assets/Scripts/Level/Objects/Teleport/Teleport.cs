using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TeleportLib
{
    public struct TeleportStruct
    {
        public Vec3 coords;
        public Vec3 targetCoords;
    }
    [RequireComponent(typeof(BoxCollider2D))]
    public class Teleport : MonoBehaviour, IStructable<TeleportStruct>
    {
        [SerializeField]
        private Vector2 _target;

        public Teleport(Vector2 target)
        {
            _target = target;
        }

        public TeleportStruct ToStruct()
        {
            return new TeleportStruct { coords = new Vec3(transform.position), targetCoords = new Vec3(_target) };
        }

        public void FromStruct(TeleportStruct str)
        {
            transform.position = str.coords.ToVector3();
            _target = str.targetCoords.ToVector3();
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
    }
}
