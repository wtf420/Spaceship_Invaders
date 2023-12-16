using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : Entity
    {
        public int Type { get; set; }

        public int Damage { get; set; }

        public Vector2 Direction { get; set; } = new Vector2(0, -1);

        //public GameObject Explosion;

        public void Init(int type = Variables.ByPlayer, int damage = Variables.Damage_Bullet_Default)
        {
            ID = Variables.BULLET;
            Type = type;
            Damage = damage;
        }

        private void Update()
        {
            Vector2 Position = Body.position;

            
            switch (Type)
            {
                case Variables.ByPlayer:
                    Position.y += Variables.PlayerBulletSpeed * Time.deltaTime;
                    //Debug.Log("Player shooting");
                    break;

                default:
                    Position += Variables.EnemyBulletSpeed * Direction * Time.deltaTime;
                    break;
            }

            Body.position = Position;

            float halfHeight = Variables.ScreenHeight / 2;
            float halfWidth = Variables.ScreenWidth / 2;

            Vector2 BulletPosition = this.transform.position;

            //bullet out of screen, so delete it.
            if (BulletPosition.y > halfHeight || BulletPosition.y < -halfHeight
                || BulletPosition.x > halfWidth || BulletPosition.x < -halfWidth)
            {
                //Debug.Log("Bullet out of screen");
                Destroy(gameObject);
            }

            //Debug.Log("update");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity == null && collision.GetComponentInParent<Entity>() != null)
            {
                entity = collision.GetComponentInParent<Entity>();
            }
            
            if(entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            GameObject explosion =  Instantiate(Explosion, collision.ClosestPoint(transform.position), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(Damage);

                            Destroy(gameObject);

                            if (entity is Boss)
                            {
                                HUD.Instance.Score += 500;
                                ItemManager.Instance.RandomItemHitBoss(entity.Body.position);
                            }
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            GameObject explosion = Instantiate(Explosion, collision.ClosestPoint(transform.position), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                            //destroy, but take player invincible into consideration
                            entity.DamageTaken(9999);
                            Destroy(gameObject);
                        }
                        break;
                }
            }    
            
        }
    }

}
