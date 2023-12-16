using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class BackGround: MonoBehaviour
    {
        static BackGround instance = null;

        public static BackGround Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        [SerializeField]
        List<GameObject> backGrounds;


        float max = 0;
        private void Start()
        {
            if (instance == null)
                instance = this;

            foreach (var element in backGrounds)
            {
                if (max < element.transform.position.y)
                    max = element.transform.position.y;
            }
        }

        private void Update()
        {
            // Make the background dynamic
            Vector2 position;
            float distance = Variables.BackGroundSpeed * Time.deltaTime;

            foreach (var element in backGrounds)
            {
                position = element.transform.position;
                position.y -= distance;

                if(position.y < -Variables.ScreenHeight)
                    position.y = max;
                

                element.transform.position = position;
            }

            //Debug.Log("Max position: " + max);
        }
    }
}
