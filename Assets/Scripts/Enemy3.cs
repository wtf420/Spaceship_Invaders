﻿using UnityEngine;
using Assets.Scripts;
namespace Assets.Scripts
{
    public class Enemy3: Enemy
    {
        void Awake()
        {
            this.Init();
            HP = Variables.HP_Enemy3;

            DeltaTime = 0;
            maxTime = Random.Range(3, MaxTimeRandom);
            Body = GetComponent<Rigidbody2D>();
            nextDestinationNode = 1;
        }

        protected override void Shooting()
        {
            Vector2 position = new Vector2(Body.position.x, Body.position.y - Variables.Adjust * 3);
            Vector2 direction = new Vector2(1, -1) ;

            Shoot(position, direction);

            direction = new Vector2(- 1, - 1) ;

            Shoot(position, direction);

            direction = new Vector2(0, - 1);

            Shoot(position, direction);
        }
    }
}
