using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class WeaponStateBar : MonoBehaviour
    {
        [SerializeField]
        Player player;

        List<GameObject> Levels = null;
        List<GameObject> Types = null;

        int level = 0;
        public int Level
        {
            get { return level; }
            set 
            {
                if(level != value)
                {
                    level = value;
                    RenderNewState();
                }
            }
        }

        int type = 0;
        public int Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    RenderNewState();
                }
            }
        }

        private void Start()
        {
            Levels = new List<GameObject>();
            Types = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if(child.gameObject.name != "WeaponType")
                    Levels.Add(child.gameObject);
                else
                {
                    foreach (Transform e in child.transform)
                        Types.Add(e.gameObject);
                }
            }

            level = Weapon.Level;
            type = player.CurrentWeapon;

            RenderNewState();
        }

        private void Update()
        {
            Level = Weapon.Level;
            Type = player.CurrentWeapon;
        }

        public void RenderNewState()
        {
            for (int i = level * 3; i < Levels.Count; i++)
                Levels[i].SetActive(false);

            for (int i = 0; i <= (level * 3) && i < Levels.Count; i++)
                Levels[i].SetActive(true);

            foreach (var e in Types)
                e.SetActive(false);
            Types[type].SetActive(true);
        }
    }
}
