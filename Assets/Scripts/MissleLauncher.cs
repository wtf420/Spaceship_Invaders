using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class MissleLauncher : Weapon
{
    public Missle Missle;
    public GameObject MissleSpawnPoint1;
    public GameObject MissleSpawnPoint2;

    public override void Shoot()
    {
        Missle Instantiate_Missle1 = Instantiate(Missle, MissleSpawnPoint1.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Missle;
        Missle Instantiate_Missle2 = Instantiate(Missle, MissleSpawnPoint2.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Missle;

        switch (Level)
        {
            case 1:
                {
                    currentRateOfFire = RateOfFire * 1f;
                    Instantiate_Missle1.Init(Variables.ByPlayer, Variables.Damage_Missle_Default);
                    Instantiate_Missle2.Init(Variables.ByPlayer, Variables.Damage_Missle_Default);
                    return;
                }
            case 2:
                {
                    currentRateOfFire = RateOfFire * 1.25f;
                    Instantiate_Missle1.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level2);
                    Instantiate_Missle2.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level2);
                    return;
                }
            case 3:
                {
                    currentRateOfFire = RateOfFire * 1.5f;
                    Instantiate_Missle1.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level3);
                    Instantiate_Missle2.Init(Variables.ByPlayer, Variables.Damage_Bullet_Default_Level3);
                    return;
                }
            default:
                {
                    currentRateOfFire = RateOfFire * 1f;
                    return;
                }
        }
    }
}
