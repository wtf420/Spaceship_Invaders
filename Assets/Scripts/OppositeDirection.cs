using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts;


namespace Assets.Scripts
{
    public class OppositeDirection: StatusEffect
    {
        int damage;


        public OppositeDirection(float t, Entity e) : base(t, e)
        {
           
        }

        public override void OnInflicted()
        {
            remainingDuration = duration;
            isEnd = false;
            if(entity is Player)
                HUD.Instance.DisplayFloatingText("Opposite Direction!", entity.transform.position);
        }

        public override void OnUpdate()
        {
            if (isEnd) return;

            remainingDuration -= Time.deltaTime;
            if (remainingDuration > 0)
                OnConditionalUpdate();
            else
                OnEnd();
            
        }

        public override void OnConditionalUpdate()
        {
            if(entity.Ratio != 0 && !(entity as Enemy))
                entity.Ratio = -1;

            if (entity as Player && ((Player)entity).invincible == true)
                entity.Ratio = 1;
        }

        public override void OnEnd()
        {
            entity.Ratio = 1;
            isEnd = true;
            HUD.Instance.DisplayFloatingText("Opposite Direction over", entity.transform.position);
        }

        public override bool Condition()
        {
            return true;
        }
    }
}
