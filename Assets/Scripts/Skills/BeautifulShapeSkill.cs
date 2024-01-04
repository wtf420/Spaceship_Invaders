using UnityEngine;

namespace Assets.Scripts
{
    public class BeautifulShapeSkill : Skill
    {
        public new static void ActivateByPlayer(Player player, Skill skill)
        {
            int quantity = 10;
            float angle = 360.0f / quantity;
            Vector2 xAxis = new Vector2(1, 0);
            float originalAngle = Vector3.Angle(new Vector2(0, 1), xAxis);

            for (int i = 1; i <= quantity; i++)
            {
                Vector2 direction = Quaternion.Euler(0, 0, angle * i + originalAngle) * xAxis;
                Skill Instantiate_Skill = Instantiate(skill, player.GetPosition(), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                Instantiate_Skill.Init(Variables.ByPlayer, skill.Damage, direction, skill.Duration, skill.Effect);
            }
        }

        public new static void ActivateByEntity(Entity entity, Skill skill)
        {
            int quantity = 5;
            float angle = 180.0f / quantity;
            Vector2 xAxis = new Vector2(1, 0);
            float originalAngle = Vector3.Angle(new Vector2(0, -1), xAxis);

            var cross = Vector3.Cross(new Vector2(0, -1), xAxis);
            if (cross.z < 0) originalAngle = -originalAngle;

            float min, max;
            if (originalAngle > 0)
            {
                min = -180.0f;
                max = 0;
            }
            else
            {
                min = 0.0f;
                max = 180.0f;
            }
            // left
            for (int i = 1; i <= quantity / 2; i++)
            {
                Skill Instantiate_Skill = Instantiate(skill, entity.GetPosition(), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                float tempAngle = Mathf.Clamp(-angle * i - originalAngle, min, max);
                Vector2 direction = Quaternion.Euler(0, 0, tempAngle) * xAxis;
                Instantiate_Skill.Init(Variables.ByEnemy, skill.Damage, direction, skill.Duration, skill.Effect);
            }
            // right
            for (int i = 1; i <= quantity / 2; i++)
            {
                Skill Instantiate_Skill = Instantiate(skill, entity.GetPosition(), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                float tempAngle = Mathf.Clamp(angle * i - originalAngle, min, max);
                Vector2 direction = Quaternion.Euler(0, 0, tempAngle) * xAxis;
                Instantiate_Skill.Init(Variables.ByEnemy, skill.Damage, direction, skill.Duration, skill.Effect);
            }
        }
    }
}
