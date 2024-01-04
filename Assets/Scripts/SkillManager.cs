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
        public List<Skill> Skills;

        private void Start()
        {
            if (instance == null)
                instance = this;
        }

        public int getIndex(Variables.Skill_Type name)
        {
            int value = (int)name;
            var enumDisplayStatus = (Variables.Skill_Type)value;
            string stringValue = enumDisplayStatus.ToString();

            int index = Skills.FindIndex(e => e.name == stringValue);

            return index;
        }
    }
}
