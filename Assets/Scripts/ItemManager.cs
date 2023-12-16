using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    [System.Serializable]
    public class Items
    {
        [SerializeField] public Item item;
        [SerializeField] public uint ratio;
    }

    public class ItemManager : MonoBehaviour
    {
        static ItemManager instance = null;

        public static ItemManager Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField]
        List<Items> items;

        [SerializeField]
        List<Items> SkillItems;

        private void Start()
        {
            if (instance == null)
                instance = this;
        }

        public void RandomItem(Vector2 position)
        {
            foreach (var item in items)
            {

                float ratio = Random.Range(0.0f, 1.0f);

                if (ratio <= item.ratio / 100.0f)
                {
                    Instantiate(item.item, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                }
            }
        }

        public void RandomItemHitBoss(Vector2 position)
        {
            foreach (var item in items)
            {

                float ratio = Random.Range(0.0f, 1.0f);

                if (ratio <= item.ratio / 100.0f / 10.0f)
                {
                    Instantiate(item.item, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                }
            }
        }

        public void CreateStar(Vector2 position)
        {
            Instantiate(items[0].item, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        }

        public void CreateFuel(Vector2 position)
        {
            Instantiate(items[1].item, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        }

    }
}
