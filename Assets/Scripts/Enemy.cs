using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : Entity
    {
        //maybe will add some methods or properties of Enemy in the future 

        public Bullet bullet;
        [SerializeField]
        protected float MaxTimeRandom;

        public Path path;
        public int nextDestinationNode { get; set; }

        public Path OrbitPath;
        public int nextNode { get; set; }

        public void Init()
        {
            ID = Variables.ENEMY;
            nextNode = 0;
        }


        protected void Movement()
        {
            if (path.GetNodePosition(nextDestinationNode) != null)
            {
                Vector2 destination = new Vector2(path.GetNodePosition(nextDestinationNode).x, path.GetNodePosition(nextDestinationNode).y);
                Body.position = Vector2.Lerp(Body.position, destination, Variables.EnemyFlySpeed * Time.deltaTime * Ratio);

                if (Vector2.Distance(Body.position, destination) < 3f)
                {
                    nextDestinationNode++;
                }
            }
        }

        protected void OrbitMovement()
        {
            if (OrbitPath.GetNodePosition(nextNode) != null)
            {
                Vector2 destination = new Vector2(OrbitPath.GetNodePosition(nextNode).x, OrbitPath.GetNodePosition(nextNode).y);
                Body.position = Vector2.Lerp(Body.position, destination, Variables.EnemyFlySpeed / 2 * Time.deltaTime * Ratio);

                if (Vector2.Distance(Body.position, destination) < 3f)
                {
                    nextNode++;
                }
            }
        }

        protected void Shoot(Vector2 position, Vector2 direction)
        {
            if (bullet != null)
            {
                Bullet Instantiate_Bullet = Instantiate(bullet as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
                //Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);

                float angle = Vector2.Angle(direction, new Vector2(1, 0));
                Instantiate_Bullet.transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
                //Debug.Log(angle);

                Instantiate_Bullet.Init(Variables.ByEnemy);
                Instantiate_Bullet.Direction = direction;
            }
        }

        private void OnDestroy()
        {
            if(HP <= 0)
                AudioManager.Instance.PlayEnemyExplosion();
        }
    }
}
