using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Gun : Weapon
{
    public Bullet Bullet;
    public GameObject MissleSpawnPoint1;
    public GameObject MissleSpawnPoint2;

    public override void Shoot()
    {
        switch (Level)
        {
            case 1:
                ShootLv1();
                break;
            case 2:
                ShootLv2();
                break;
            default:
                ShootLv3();
            break;
        }
    }

    public void ShootLv1()
    {
        Bullet Instantiate_Bullet = Instantiate(Bullet, MissleSpawnPoint1.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.Init(Variables.ByPlayer);

        Instantiate_Bullet = Instantiate(Bullet, MissleSpawnPoint2.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.Init(Variables.ByPlayer);
    }

    public void ShootLv2()
    {
        ShootLv1();
        Vector3 position = MissleSpawnPoint2.transform.position;
        position.x = (position.x + MissleSpawnPoint1.transform.position.x) / 2;

        Bullet Instantiate_Bullet = Instantiate(Bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.transform.localScale += new Vector3(1.0f, 3.0f, 0);
        Instantiate_Bullet.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level2);
    }

    public void ShootLv3()
    {
        Bullet Instantiate_Bullet = Instantiate(Bullet, MissleSpawnPoint1.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.transform.localScale += new Vector3(1.0f, 3.0f, 0);
        Instantiate_Bullet.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level2);

        Instantiate_Bullet = Instantiate(Bullet, MissleSpawnPoint2.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.transform.localScale += new Vector3(1.0f, 3.0f, 0);
        Instantiate_Bullet.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level2);

        Vector3 position = MissleSpawnPoint2.transform.position;
        position.x = (position.x + MissleSpawnPoint1.transform.position.x) / 2;

        Instantiate_Bullet = Instantiate(Bullet, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Bullet;
        Instantiate_Bullet.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
        Instantiate_Bullet.transform.localScale += new Vector3(2.0f, 4.0f, 0);
        Instantiate_Bullet.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level3);
    }
}
