using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
namespace Assets.Scripts
{
    public class Boss1 : Boss
    {

        private void Start()
        {
            HP = Variables.HP_Boss1;
        }
        // Update is called once per frame
        void Update()
        {
            if (HP <= 0)
                IsDeleted = true;

            if (TotalTime == 0)
            {

                if (DeltaTime < maxTime)
                    DeltaTime += Time.deltaTime;
                else
                {
                    DeltaTime = 0;
                    Action();
                    maxTime = Random.Range(1, 2);
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
            }
            else if (BurstTime >= TotalTime)
            {
                TotalTime = 0;
                BurstTime = 0;
            }
            else
                BurstTime += Time.deltaTime;
            UpdateStatusEffect();
        }



        protected override void Action()
        {
            Vector2 position = Body.position;

            if (HP / (float)Variables.HP_Enemy5 < 0.3)
            {
                position.y -= Body.transform.localScale.y / 2;

                TotalTime = 0.2f;
                StartCoroutine(DivineDeparture(0, position));
            }
            else
            {
                int rd = Random.Range(0, Skills.Count);
                rd = 3;

                switch (Skills[rd])
                {
                    case Variables.Skill_Type.CircleShooting:
                        TotalTime = CIRCLE_SHOOTING_TIME * 3;

                        StartCoroutine(CircleShooting(CIRCLE_SHOOTING_TIME, position, 10));
                        StartCoroutine(CircleShooting(CIRCLE_SHOOTING_TIME * 2, position, 10));
                        StartCoroutine(CircleShooting(CIRCLE_SHOOTING_TIME * 3, position, 10));

                        break;
                    case Variables.Skill_Type.DivineDeparture:
                        TotalTime = 0.2f;
                        position.y -= Body.transform.localScale.y / 2;
                        StartCoroutine(DivineDeparture(0, position));

                        break;
                    case Variables.Skill_Type.EnergyWave:
                        TotalTime = 0.1f;
                        position.y -= Body.transform.localScale.y / 2;
                        StartCoroutine(EnergyWave(0, position));
                        break;

                    case Variables.Skill_Type.SectorShooting:
                        TotalTime = CIRCLE_SHOOTING_TIME;

                        StartCoroutine(SectorShoot(CIRCLE_SHOOTING_TIME, position, 5));

                        break;
                }
            }

        }

    }
}
