using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public enum StatusEffectTypes
{
    None,
    Burn,
    Stun,
    OppositeDirection,

}

public static class StatusEffectManager
{
    public static void InflictStatusEffect(Entity e, StatusEffectTypes type, float duration, int damage = 1000)
    {
        if (e.statusEffects.ContainsKey(type))
        {
            e.statusEffects[type].OnInflicted();
            return;
        }

        switch (type)
        {
            case StatusEffectTypes.Burn:
                {
                    Burn b = new Burn(duration, e, damage);
                    e.statusEffects[type] = b;
                    b.OnInflicted();
                    return;
                }
            case StatusEffectTypes.OppositeDirection:
                {
                    OppositeDirection effect = new OppositeDirection(2.0f, e);
                    e.statusEffects[type] = effect;
                    effect.OnInflicted();

                    return;
                }
            case StatusEffectTypes.Stun:
                {
                    Stun effect = new Stun(2.0f, e);
                    e.statusEffects[type] = effect;
                    effect.OnInflicted();

                    return;
                }
            default:
                {
                    return;
                }
        }
    }
}

[System.Serializable]
public abstract class StatusEffect
{
    public Entity entity;
    public bool isEnd = false;
    protected float duration, remainingDuration;

    public StatusEffect(float t, Entity e)
    {
        duration = t;
        remainingDuration = t;
        entity = e;
    }

    public virtual void OnInflicted()
    {
        //dostuff
    }

    public virtual void OnUpdate()
    {
        //dostuff
    }

    public virtual void OnConditionalUpdate()
    {
        if (Condition())
        {
            //dostuff
        }
    }

    public virtual void OnEnd()
    {
        //dostuff
    }

    public virtual bool Condition()
    {
        return true;
    }
}
