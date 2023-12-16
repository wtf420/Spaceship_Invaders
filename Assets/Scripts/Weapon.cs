using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    static int level = 1;
    public static int Level 
    {
        get { return level; }
        set 
        { 
            level = value;
            Assets.Scripts.DataPersistence.DataPersistenceManager.Instance.gameData.LevelBullet = value; 
        } 
    }

    public int ID { get; set; } //player, enemies, bullet

    public float RateOfFire; //How fast weapon shoot, shots per minute

    //for upgradeing purposes
    protected float currentRateOfFire;

    protected float DelayBetweenShots; //Reload between each shot, defined by RateOfFire

    protected float DelayTimer; //timer

    protected void Start()
    {
        currentRateOfFire = RateOfFire;
        if (currentRateOfFire != 0)
        {
            DelayBetweenShots = 1.0f / (currentRateOfFire / 60.0f);
        }
        else
        {
            DelayBetweenShots = 0.0f;
        }
        DelayTimer = 0.0f;
        //Debug.Log(DelayBetweenShots.ToString());
        //Debug.Log("Weapon shoot");
        Level = Assets.Scripts.DataPersistence.DataPersistenceManager.Instance.gameData.LevelBullet;
    }

    void Update()
    {
        //recalculate rate of fire for switching weapon
        if (currentRateOfFire != 0)
        {
            DelayBetweenShots = 1.0f / (currentRateOfFire / 60.0f);
        }
        else
        {
            DelayBetweenShots = 0.0f;
        }

        if (DelayTimer >= 0f)
        {
            DelayTimer -= Time.deltaTime;
        }

        ChildUpdate();
    }

    protected virtual void ChildUpdate()
    {

    }

    public void Trigger() //Attempt to shoot the weapon
    {
        if (DelayTimer <= 0f)
        {
            Shoot();
            Assets.Scripts.AudioManager.Instance.PlayPlayerShooting();
            DelayTimer = DelayBetweenShots;
        }
    }

    public virtual void Shoot() //Weapon shooting, spawn bullet and stuff, defined by individual weapon
    {
        
    }
}
