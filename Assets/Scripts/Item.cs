using UnityEngine;

namespace Assets.Scripts
{
    public class Item : Entity
    {
        [SerializeField]
        public Variables.ItemType Type;

        [SerializeField]
        float Duration;

        private void Start()
        {
            ID = Variables.ITEM;

            if(Type == Variables.ItemType.Coin)
                Body.velocity = new Vector2(Random.Range(-3, 3), Random.Range(3, 5));
        }

        private void Update()
        {
            Vector2 Position = Body.position;

            switch(Type)
            {
                case Variables.ItemType.Fuel:
                    Position.y -= Variables.ItemSpeed * Time.deltaTime;
                    break;

                case Variables.ItemType.Star:
                    Position.y -= Variables.ItemSpeed * Time.deltaTime * 2;
                    break;

                case Variables.ItemType.Coin:
                    
                    break;
            }

            Duration -= Time.deltaTime;

            if (Duration <= 0)
                Destroy(gameObject);


            Body.position = Position;
        }
    }
}
