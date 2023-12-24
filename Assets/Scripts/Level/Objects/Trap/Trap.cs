using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.Trap
{
    public struct TrapStruct
    {
        public Vector3 coords;
        public int damage;
    }
    [RequireComponent(typeof(BoxCollider2D))]
    public class Trap : MonoBehaviour, IStructable<TrapStruct>
    {
        [SerializeField]
        private int _damage = 1;

        public Trap(int damage)
        {
            _damage = damage;
        }

        public TrapStruct ToStruct()
        {
            return new TrapStruct { coords = transform.position, damage = _damage };
        }

        public Trap()
        {
            //TODO: get damage from config
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamagableObject damagableObject = other.gameObject.GetComponent<DamagableObject>();
            if (damagableObject != null)
            {
                Debug.Log($"Damaging {other.gameObject.name} for {_damage} damage");
                damagableObject.Damage(_damage);
            }
            else
            {
                Debug.Log($"Damaging {other.gameObject.name} failed");
            }
        }

    }
}