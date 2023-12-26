using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.TrapLib
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Trap : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 1;

        public Trap(int damage)
        {
            _damage = damage;
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

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public int GetDamage()
        {
            return _damage;
        }
    }
}