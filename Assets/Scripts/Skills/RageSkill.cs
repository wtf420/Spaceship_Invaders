using UnityEngine;

namespace Assets.Scripts
{
    public class RageSkill : Skill
    {

        public void DivineDeparture(Vector2 direction)
        {
            this.Init(Type, Damage, direction);
            this.isMovable = false;
        }

        public override void HandleDestroy()
        {
            if (this.Type == Variables.ByPlayer)
                Skill.ActivateByPlayer(GameObject.FindObjectOfType<Player>(), SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.EnergyWave)]);
            else
                Skill.ActivateByEntity(this, SkillManager.Instance.Skills[SkillManager.Instance.getIndex(Variables.Skill_Type.EnergyWave)]);
            Destroy(gameObject);
        }
        
    }
}
    

