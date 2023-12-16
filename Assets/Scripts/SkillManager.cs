using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SkillManager : MonoBehaviour
    {
        static SkillManager instance;

        public static SkillManager Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField]
        List<Skill> Skills;

        private void Start()
        {
            if (instance == null)
                instance = this;
        }

        int getIndex(Variables.Skill_Type name)
        {
            int value = (int)name;
            var enumDisplayStatus = (Variables.Skill_Type)value;
            string stringValue = enumDisplayStatus.ToString();

            int index = Skills.FindIndex(e => e.name == stringValue);

            return index;
        }

        public void CircleShoot(int Type, int quantity, Vector2 position, Vector2 direction)
        {
            int index = getIndex(Variables.Skill_Type.CircleShooting);
            if(index != -1)
            {
                BeautifulShapeSkill Instantiate_Skill = Instantiate(Skills[index] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as BeautifulShapeSkill;

                Instantiate_Skill.Type = Type;
                Instantiate_Skill.Circle(quantity, position, direction);
            }
        }

        public void DivineDeparture(int Type, Vector2 position, Vector2 direction)
        {
            int index = getIndex(Variables.Skill_Type.DivineDeparture);
            if (index != -1)
            {
                RageSkill Instantiate_Skill = Instantiate(Skills[index] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as RageSkill;
                Instantiate_Skill.Type = Type;
                Instantiate_Skill.DivineDeparture(direction);
            }
        }

        public void EnergyWave(int Type, Vector2 position, Vector2 direction)
        {
            int index = getIndex(Variables.Skill_Type.EnergyWave);
            if (index != -1)
            {
                Skill Instantiate_Skill = Instantiate(Skills[index] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;

                Instantiate_Skill.Init(Type, Variables.Damage_Bullet_Default, direction);
            }
        }

        public void Invincible(int Type, Vector2 position, Vector2 direction, GameObject parent)
        {
            int index = getIndex(Variables.Skill_Type.Invincible);
            if (index != -1)
            {
                Skill Instantiate_Skill = Instantiate(Skills[index] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;

                Instantiate_Skill.isMovable = false;
                Instantiate_Skill.SetParent(parent);
                Instantiate_Skill.Init(Type, Variables.Damage_Bullet_Default, direction);

            }
        }

        public void ElectricShooting(int Type, Vector2 position, Vector2 direction)
        {
            int index = getIndex(Variables.Skill_Type.ElectricShooting);
            if (index != -1)
            {
                Skill Instantiate_Skill = Instantiate(Skills[index] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as Skill;

                Instantiate_Skill.Init(Type, Variables.Damage_Bullet_Default, direction);
            }
        }

        public void SectorShooting(int Type, int quantity, Vector2 position, Vector2 direction)
        {
            int index = getIndex(Variables.Skill_Type.SectorShooting);
            Debug.Log("Sector shooting index: " + index);
            if (index != -1)
            {
                BeautifulShapeSkill Instantiate_Skill = Instantiate(Skills[index] as Object, position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as BeautifulShapeSkill;

                Instantiate_Skill.Type = Type;
                Instantiate_Skill.Sector(quantity, position, direction);
            }
        }

    }
}
