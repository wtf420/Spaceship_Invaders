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
            BeautifulShapeSkill.ActivateByEntity(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.CircleShooting)]);
        }

        protected IEnumerator DivineDeparture(float delayTime, Vector2 position)
        {
            yield return new WaitForSeconds(delayTime);
            Skill.ActivateByEntity(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.DivineDeparture)]);
        }

        protected IEnumerator EnergyWave(float delayTime, Vector2 position)
        {
            yield return new WaitForSeconds(delayTime);
            Skill.ActivateByEntity(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.EnergyWave)]);
        }

        protected IEnumerator ElectricShoot(float delayTime, Vector2 position)
        {
            yield return new WaitForSeconds(delayTime);
            Skill.ActivateByEntity(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.ElectricShooting)]);
        }

        protected IEnumerator SectorShoot(float delayTime, Vector2 position, int quantity)
        {
            yield return new WaitForSeconds(delayTime);
            BeautifulShapeSkill.ActivateByEntity(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.SectorShooting)]);
        }

    }
}
