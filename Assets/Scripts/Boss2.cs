using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
namespace Assets.Scripts
{
    public class Boss2 : Boss
    {
        Animator animator;
        bool isLasering = false;
        [SerializeField] GameObject laserRaycast1, laserRaycast2, bulletSpawnPoint;
        LayerMask ignoreLayer;

        [SerializeField] LineRenderer laserline;
        float length = 20f;

        float ShootingDeltaTime;
        float ShootingMaxTime;
        [SerializeField]
        float MaxShootingTimeRandom;

        float maxHP;

        private void Start()
        {
            HP = Variables.HP_Boss2;
            maxHP = Variables.HP_Boss2;
            animator =  GetComponent<Animator>();
            ignoreLayer = ((1 << 6) | (1 << 7) | (1 << 9));

            animator = GetComponent<Animator>();
            laserline.enabled = false;
            animator.ResetTrigger("Laser");

            ShootingDeltaTime = 0;
            ShootingMaxTime = 1.5f;
        }
        // Update is called once per frame
        void Update()
        {
            if (HP <= 0)
                IsDeleted = true;
            if (isLasering)
            {
                Laser();
            }

            if (HP / maxHP >= 0.3)
            {
                if (ShootingDeltaTime < ShootingMaxTime)
                    ShootingDeltaTime += Time.deltaTime;
                else
                {
                    ShootingDeltaTime = 0;
                    Shooting(bulletSpawnPoint.transform.position,Vector2.down);
                    maxTime = Random.Range(1.5f, MaxShootingTimeRandom);
                }
            }
            if (TotalTime == 0)
            {

                if (DeltaTime < maxTime)
                    DeltaTime += Time.deltaTime;
                else
                {
                    Action();
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
            }
            else if (BurstTime >= TotalTime)
            {
                TotalTime = 0;
                BurstTime = 0;
            }
            else
                BurstTime += Time.deltaTime;
            UpdateStatusEffect();
        }

        protected void Shooting(Vector2 position, Vector2 direction)
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

        protected override void Action()
        {
            Vector2 position = Body.position;

            if (HP / maxHP < 0.1)
            {
                TotalTime = 0.2f;
                LaserFire();
                maxTime = Random.Range(5, 10);
            }
            else
            {
                int rd = Random.Range(0, Skills.Count);
                rd = 3;

                switch (Skills[rd])
                {
                    case Variables.Skill_Type.CircleShooting:
                        TotalTime = CIRCLE_SHOOTING_TIME * 3;
                        DeltaTime = 0;

                        StartCoroutine(CircleShooting(CIRCLE_SHOOTING_TIME, position, 10));
                        StartCoroutine(CircleShooting(CIRCLE_SHOOTING_TIME * 2, position, 10));
                        StartCoroutine(CircleShooting(CIRCLE_SHOOTING_TIME * 3, position, 10));

                        break;
                    case Variables.Skill_Type.DivineDeparture:
                        TotalTime = 0.2f;
                        DeltaTime = 0;
                        position.y -= Body.transform.localScale.y / 2;
                        StartCoroutine(DivineDeparture(0, position));

                        break;
                    case Variables.Skill_Type.EnergyWave:
                        TotalTime = 0.1f;
                        DeltaTime = 0;
                        position.y -= Body.transform.localScale.y / 2;
                        StartCoroutine(EnergyWave(0, position));
                        break;

                    case Variables.Skill_Type.SectorShooting:
                        TotalTime = CIRCLE_SHOOTING_TIME;
                        DeltaTime = 0;
                        StartCoroutine(SectorShoot(CIRCLE_SHOOTING_TIME, position, 5));

                        break;
                }
            }
        }

        protected void Laser()
        {
            Render(Vector2.down);
            RaycastHit2D laserhit1 = Physics2D.Raycast(laserRaycast1.transform.position, Vector2.down, 20f, ~ignoreLayer);
            RaycastHit2D laserhit2 = Physics2D.Raycast(laserRaycast2.transform.position, Vector2.down, 20f, ~ignoreLayer);

            if (laserhit1.collider != null || laserhit2.collider != null)
            {
                Entity entity = laserhit1.collider.GetComponent<Entity>();
                if (entity == null && laserhit2.collider.GetComponentInParent<Entity>() != null)
                {
                    entity = laserhit2.collider.GetComponentInParent<Entity>();
                }

                if (entity != null && entity.IsDeleted == false)
                {
                    switch (entity.ID)
                    {
                        case Variables.PLAYER:
                            {
                                GameObject explosion = Instantiate(Explosion, entity.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                                entity.DamageTaken(9999);
                                Debug.Log("GameOver");
                            }
                            break;
                    }
                }
            }
        }

        protected void LaserFire()
        {
            animator.SetTrigger("Laser");
            isLasering = true;
            laserline.enabled = true;
        }

        protected void LaserFireDone()
        {
            animator.ResetTrigger("Laser");
            isLasering = false;
            laserline.enabled = false;
            laserline.positionCount = 0;
            DeltaTime = 0;
        }

        public void Render(Vector2 direction)
        {
            laserline.enabled = true;
            //Debug.Log("Shooting");
            Vector2 position = this.transform.position;
            laserline.positionCount = 2;
            laserline.SetPosition(0, position);
            position += direction.normalized * length;
            laserline.SetPosition(1, position);
        }
    }
}
