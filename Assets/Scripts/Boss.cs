using System.Collections;
using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Boss : Enemy
    {
        public const float CIRCLE_SHOOTING_TIME = 0.2f;
        [SerializeField]
        protected List<Variables.Skill_Type> Skills;

        protected float TotalTime = 0;
        protected float BurstTime = 0;
        

        protected float DeltaTime;
        protected float maxTime;

        void Awake()
        {
            Init();
            HP = Variables.HP_Enemy5;

            DeltaTime = 0;
            maxTime = Random.Range(MaxTimeRandom / 2, MaxTimeRandom);
            Body = GetComponent<Rigidbody2D>();
            nextDestinationNode = 1;

        }

        protected virtual void Action()
        {

        }

        protected IEnumerator CircleShooting(float delayTime, Vector2 position, int quantity)
        {
            yield return new WaitForSeconds(delayTime);
            SkillManager.Instance.CircleShoot(Variables.ByEnemy, quantity, position, new Vector2(0, -1));

        }

        protected IEnumerator DivineDeparture(float delayTime, Vector2 position)
        {
            yield return new WaitForSeconds(delayTime);
            SkillManager.Instance.DivineDeparture(Variables.ByEnemy, position, new Vector2(0, -1));
        }

        protected IEnumerator EnergyWave(float delayTime, Vector2 position)
        {
            yield return new WaitForSeconds(delayTime);
            SkillManager.Instance.EnergyWave(Variables.ByEnemy, position, new Vector2(0, -1));
        }

        protected IEnumerator ElectricShoot(float delayTime, Vector2 position)
        {
            yield return new WaitForSeconds(delayTime);
            SkillManager.Instance.ElectricShooting(Variables.ByEnemy, position, new Vector2(0, -1));
        }

        protected IEnumerator SectorShoot(float delayTime, Vector2 position, int quantity)
        {
            yield return new WaitForSeconds(delayTime);
            SkillManager.Instance.SectorShooting(Variables.ByEnemy, quantity, position, new Vector2(0, -1));
        }

    }
}
