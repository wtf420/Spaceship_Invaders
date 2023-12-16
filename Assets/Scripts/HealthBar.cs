using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class HealthBar: MonoBehaviour
    {
        [SerializeField]
        Entity entity;
    
        //GameObject State;
        Vector3 originalScaleY;

        int totalHP = 1;
        
        int currentHP = 0;
        public int CurrentHP
        {
            get { return currentHP; }
            set
            {
                if (currentHP != value)
                {
                    currentHP = value > totalHP ? totalHP : value;

                    if(totalHP != 0)
                    {
                        Vector3 current = originalScaleY;
                        current.x = (currentHP * current.x / totalHP);
                        gameObject.transform.localScale = current;
                    }   
                    
                }
            }
        }

        private void Start()
        {
            originalScaleY = gameObject.transform.localScale;

            CurrentHP = totalHP = entity.HP;
        }

        private void Update()
        {
            CurrentHP = entity.HP;
        }


    }
}
