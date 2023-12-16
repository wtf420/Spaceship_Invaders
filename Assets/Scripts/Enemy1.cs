using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy1 : Enemy
{
    float DeltaTime;
    float maxTime;

    void Awake()
    {
        this.Init();
        HP = Variables.HP_Enemy1;
        
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
        else if(OrbitPath != null)
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
        Vector2 direction = new Vector2(0, -1);
        Shoot(position, direction);

    }


}
