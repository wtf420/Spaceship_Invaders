using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy1 : Enemy
{
    void Awake()
    {
        this.Init();
        HP = Variables.HP_Enemy1;
        
        DeltaTime = 0;
        maxTime = Random.Range(1, MaxTimeRandom);
        Body = GetComponent<Rigidbody2D>();
        nextDestinationNode = 1;
    }

    protected override void Shooting()
    {
        Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust);
        Vector2 direction = new Vector2(0, -1);
        Shoot(position, direction);

    }
}
