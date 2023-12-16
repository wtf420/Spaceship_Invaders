using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Laser : Weapon
{
    LineRenderer laserLine;
    Animator animator;
    LayerMask ignoreLayer;

    public GameObject laserSpawnPoint;

    float length = 0f;

    public float maxLength;

    public int Type { get; set; }

    public int Damage { get; set; }

    public GameObject Explosion;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        laserLine = this.GetComponent<LineRenderer>();
        animator = this.GetComponent<Animator>();
        Init(Variables.ByPlayer);

        //Debug.Log("Laser shoot");
    }

    public void Init(int type = 10, int damage = Variables.Damage_Laser_Default)
    {
        Type = type;
        Damage = damage;
        ignoreLayer = ((1 << 6) | (1 << 7) | (1 << 8));
    }

    
    protected override void ChildUpdate()
    {
        //base.Update();
        //RaycastHit2D laserhit = Physics2D.Raycast(laserSpawnPoint.transform.position, Vector2.up, 20f, ~ignoreLayer);
        //if (laserhit.collider == null)
        //length = maxLength; 
        //else 
        //length = Mathf.Clamp(Vector2.Distance(laserSpawnPoint.transform.position, laserhit.point), 0f, 20f);

    }

    public void Nothing() { }

    public override void Shoot()
    {
        GetHitInfo();
        switch (Level)
        {
            case 1:
                {
                    Damage = Variables.Damage_Laser_Default;
                    laserLine.startColor = Color.white;
                    laserLine.endColor = Color.white;
                    animator.SetTrigger("Shoot");
                    break;
                }
            case 2:
                {
                    Damage = Variables.Damage_Laser_Default_Level2;
                    laserLine.startColor = Color.yellow;
                    laserLine.endColor = Color.yellow;
                    animator.SetTrigger("ShootLv2");
                    break;
                }
            case 3:
                {
                    Damage = Variables.Damage_Laser_Default_Level3;
                    laserLine.startColor = Color.red;
                    laserLine.endColor = Color.red;
                    animator.SetTrigger("ShootLv3");
                    break;
                }
            default:
                {
                    laserLine.startColor = Color.white;
                    laserLine.endColor = Color.white;
                    animator.SetTrigger("Shoot");
                    break;
                }
        }
    }
    
    public void GetHitInfo()
    {
        Debug.Log("Getting hit info");
        //float halfHeight = Variables.ScreenHeight / 2;
        //float maxDis = halfHeight - laserSpawnPoint.transform.position.y;
        RaycastHit2D laserhit = Physics2D.Raycast(laserSpawnPoint.transform.position, Vector2.up, 20f, ~ignoreLayer);
        
        if (laserhit.collider != null)
        {
            length = Mathf.Clamp(Vector2.Distance(laserSpawnPoint.transform.position, laserhit.point), 0f, 20f);
            Render();

            Entity entity = laserhit.collider.GetComponent<Entity>();
            if (entity == null && laserhit.collider.GetComponentInParent<Entity>() != null)
            {
                entity = laserhit.collider.GetComponentInParent<Entity>();
            }
            
            if (entity != null && entity.IsDeleted == false)
            {
                switch (entity.ID)
                {
                    case Variables.ASTEROID:
                    case Variables.ENEMY:
                        if (this.Type == Variables.ByPlayer)
                        {
                            GameObject explosion = Instantiate(Explosion, laserhit.point, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(Damage);
                            Debug.Log("LaserHit");

                            if (entity is Boss)
                            {
                                HUD.Instance.Score += 500;
                                ItemManager.Instance.RandomItemHitBoss(entity.Body.position);
                            }
                            int r = Random.Range(0, 100);
                            if (r <= 50 )
                            {
                                StatusEffectManager.InflictStatusEffect(entity, StatusEffectTypes.Burn, 3.0f);
                            }
                        }
                        break;

                    case Variables.PLAYER:
                        if (this.Type == Variables.ByEnemy)
                        {
                            GameObject explosion = Instantiate(Explosion, laserhit.point, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

                            entity.DamageTaken(9999);
                            Debug.Log("GameOver");
                        }
                        break;
                }
            }
        }
        else
        {
            length = maxLength;

            Render();
        }
    }

    public void Render()
    {
        Vector2 position = laserSpawnPoint.transform.position;
        laserLine.SetPosition(0, position);
        position.y += length;
        laserLine.SetPosition(1, position);

    }


}
