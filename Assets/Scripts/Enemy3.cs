using UnityEngine;
using Assets.Scripts;
namespace Assets.Scripts
{
    public class Enemy3: Enemy
    {
        float DeltaTime;
        float maxTime;

        void Awake()
        {
            this.Init();
            HP = Variables.HP_Enemy3;

            DeltaTime = 0;
            maxTime = Random.Range(3, MaxTimeRandom);
            Body = GetComponent<Rigidbody2D>();
            nextDestinationNode = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (HP <= 0)
                IsDeleted = true;

            if (DeltaTime < maxTime)
                DeltaTime += Time.deltaTime;
            else
            {
                DeltaTime = 0;
                Shooting();
                maxTime = Random.Range(3, MaxTimeRandom);
            }

            if (nextDestinationNode < path.NodeCount())
            {
                Movement();
            }
            else if (OrbitPath != null)
            {
                if (nextNode < OrbitPath.NodeCount())
                    OrbitMovement();
                else
                    nextNode = 0;
            }
            UpdateStatusEffect();
        }

        private void Shooting()
        {
            Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust * 3);
            Vector2 direction = new Vector2(1, -1) ;

            Shoot(position, direction);

            direction = new Vector2(- 1, - 1) ;

            Shoot(position, direction);

            direction = new Vector2(0, - 1);

            Shoot(position, direction);
        }
    }
}
