using UnityEngine;
using static log4net.Appender.ColoredConsoleAppender;

namespace MemoryLabyrinth.Level.Objects.BonusLib
{
    public struct BonusStruct 
    {
        public Vec3 coords;
        public int id;
        public int bonusAmount;
    }

    [RequireComponent(typeof(BoxCollider2D))]
    public class Bonus : MonoBehaviour, IStructable<BonusStruct>
    {
        int _value = 10;
        int _id;
        
        public Bonus(int value, int id)
        {
            _value = value;
            _id = id;
        }

        public Bonus()
        {
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            BonusCompatible bonusObject = other.gameObject.GetComponent<BonusCompatible>();
            if (bonusObject != null)
            {
                Debug.Log($"Bonus {other.gameObject.name} for {_value}");
                bonusObject.getBonus(this);
                //Удалить себя
                DestroySelf();
            }
            else
            {
                Debug.Log($"Bonus {other.gameObject.name} failed");
            }
        }

        public void SetValue(int value)
        {
            if (value < 0)
                value = 0;

            _value = value;
        }

        public void SetID(int id)
        {
            _id = id;
        }

        public int GetID()
        {
            return _id;
        }

        public int GetValue()
        {
            return _value;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
        public BonusStruct ToStruct()
        {
            BonusStruct bonusStruct = new BonusStruct();
            bonusStruct.coords = new Vec3(transform.position);
            bonusStruct.id = _id;
            bonusStruct.bonusAmount = _value;
            return bonusStruct;
        }

        public void FromStruct(BonusStruct str)
        {
            transform.position = str.coords.ToVector3();
            _value = str.bonusAmount;
            _id = str.id;
        }
    }
}