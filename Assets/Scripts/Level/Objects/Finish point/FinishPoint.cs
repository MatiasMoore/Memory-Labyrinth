using UnityEngine;

namespace MemoryLabyrinth.Level.Objects.FinishLib
{
    public struct FinishPointStruct
    {
        public Vec3 coords;
    }
    [RequireComponent(typeof(BoxCollider2D))]
    public class FinishPoint : MonoBehaviour, IStructable<FinishPointStruct>
    {
        public FinishPoint()
        {
        }
        
        public FinishPointStruct ToStruct()
        {
            return new FinishPointStruct { coords = new Vec3(transform.position) };
        }

        public void FromStruct(FinishPointStruct str)
        {
            transform.position = str.coords.ToVector3();
        }

        public void Start()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
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

