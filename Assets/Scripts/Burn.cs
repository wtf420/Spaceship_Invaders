using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Burn : StatusEffect
{
    int damage;
    float tickrate = 1; //How many tick per second
    float delay;


    public Burn(float t, Entity e, int i) : base(t,e)
    {
        damage = i;
        delay = 1.0f / tickrate;
    }

    public override void OnInflicted()
    {
        remainingDuration = duration;
        isEnd = false;
        HUD.Instance.DisplayFloatingText("Burn!", entity.transform.position);
    }

    public override void OnUpdate()
    {
        if (isEnd) return;

        remainingDuration -= Time.deltaTime;
        if (remainingDuration <= 0)
        {
            OnEnd();
        }
        else
        {

            delay -= Time.deltaTime;
            if (Condition())
            {
                OnConditionalUpdate();
            }
        }

    }

    public override void OnConditionalUpdate()
    {
        entity.DamageTaken(damage);
        HUD.Instance.DisplayFloatingText(damage.ToString(), entity.transform.position);
        Debug.Log("IsBurning");
    }

    public override void OnEnd()
    {
        HUD.Instance.DisplayFloatingText("Burn over", entity.transform.position);
        isEnd = true;
    }

    public override bool Condition()
    {
        if (delay <= 0)
        {
            delay = 1.0f / tickrate;
            return true;
        } else
        {
            return false;
        }
    }
}
