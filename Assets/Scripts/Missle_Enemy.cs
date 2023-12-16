using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Missle_Enemy : Entity
    {
        public int Type { get; set; }

        public int Damage { get; set; }

        public float RotationSpeed;

        public float Speed;

        public float CircleCastRadius;

        //public GameObject Explosion;

        GameObject target;

        void Start()
        {

        }

        public void Init(int type = 10, int damage = Variables.Damage_Missle_Default)
        {
            ID = Variables.MISSLE;
            Type = type;
            Damage = damage;

            Body = GetComponent<Rigidbody2D>();
            Body.gravityScale = 0;

            RotationSpeed = Variables.PlayerMissleRotatingSpeed;
        }

        private void Update()
        {
            target = null;
            //find target to chase
            float minDistance = 100f;
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            Vector2 center = this.transform.position + this.transform.up.normalized * (Variables.PlayerMissleCircleCastRadius / 2);
            for (int i = 0; i < Physics2D.OverlapCircle(center, CircleCastRadius, filter, results); i++)
            {
                GameObject o = results[i].gameObject;
                Entity e = results[i].gameObject.GetComponent<Entity>();
                if (e != null)
                {
                    float distance = Vector2.Distance(this.transform.position, o.transform.position);
                    if ((e.ID == Variables.ENEMY || e.ID == Variables.ASTEROID) && (distance < minDistance) && (Type == Variables.ByPlayer))
                    {
                        minDistance = distance;
                        target = o;
                    }
                    else
                    if ((e.ID == Variables.PLAYER) && (distance < minDistance) && (Type == Variables.ByEnemy))
                    {
                        minDistance = distance;
                        target = o;
                        break;
                    }
                }
            }

            if (target != null) //follow target
            {
                Vector2 direction = (Vector2)target.transform.position - Body.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.up).z;

                Body.angularVelocity = -rotateAmount * RotationSpeed;
            }
            else
            {
                Body.angularVelocity = 0;
            }

            switch (Type)
            {
                case Variables.ByPlayer:
                    Body.velocity = Speed * transform.up;
                    break;

                default:
                    Body.velocity = Speed * transform.up;
                    break;
            }

            float halfHeight = Variables.ScreenHeight / 2;

            Vector2 Position = this.transform.position;

            //bullet out of screen, so delete it.
            if (Position.y > halfHeight || Position.y < -halfHeight)
            {
                //Debug.Log("Bullet out of screen");
                Destroy(this.gameObject);
            }

            //Debug.Log("update");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Entity entity = collision.GetComponent<Entity>();

            if (entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            GameObject explosion = Instantiate(Explosion, collision.ClosestPoint(transform.position), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(Damage);

                            Destroy(this.gameObject);
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

                            entity.DamageTaken(9999);
                            Destroy(this.gameObject);
                            Debug.Log("GameOver");
                        }
                        break;
                }
            }

        }
    }

}
