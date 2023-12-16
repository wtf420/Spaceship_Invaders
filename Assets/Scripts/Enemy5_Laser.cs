using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy5_Laser : Enemy
{
    float DeltaTime;
    float maxTime;

    public Missle_Enemy missle;

    void Awake()
    {
        this.Init();
        HP = Variables.HP_Enemy5;

        DeltaTime = 0;
        maxTime = Random.Range(1, MaxTimeRandom);
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
            maxTime = Random.Range(1, MaxTimeRandom);
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
        Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust);
        Vector2 direction = new Vector2(-1, 0);

        Missle_Enemy Instantiate_Missle = Instantiate(missle as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Missle_Enemy;

        float angle = Vector2.Angle(direction, new Vector2(1, 0));
        Instantiate_Missle.transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        //Debug.Log(angle);

        Instantiate_Missle.Init(Variables.ByEnemy);

    }
}
