using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class FuelStateBar : MonoBehaviour
    {
        List<GameObject> Contains;

        float contain = 0;
        public float Contain
        {
            get { return contain; }
            set
            {
                if (contain != value)
                {
                    contain = value > Contains.Count ? Contains.Count: value;
                    RenderNewState();

                }
            }
        }

        float distance = 0;
        public float Distance 
        {
            get { return distance; }
            set 
            {
                distance = value;

                if (distance >= 30)
                {
                    Contain = Contain <= 0 ? 0 : Contain - 1;
                    distance = 0;
                }
            }
        }

        float DeltaTime = 0;

        private void Start()
        {
            Contains = new List<GameObject>();
            foreach (Transform child in transform)
            {
                if (child.gameObject.name != "power" && child.gameObject.name != "Background")
                    Contains.Add(child.gameObject);
            }

            Contain = Contains.Count;
            
            RenderNewState();

        }

        private void Update()
        {
            contain = Mathf.Clamp(contain, 0 , 10);
            if (contain <= 2)
                RenderWarning();

            DataPersistence.DataPersistenceManager.Instance.gameData.Energy = (int)contain;
        }

        public void RenderNewState()
        {

            for (int i = 0; i < contain; i++)
                Contains[i].SetActive(true);

            for (int i = (int)contain; i < Contains.Count; i++)
                Contains[i].SetActive(false);

        }

        public void RenderWarning()
        {
            DeltaTime += Time.deltaTime;

            if(DeltaTime >= 0.2f)
            {

                if (Random.Range(0, 7) % 2 == 1)
                {
                    for (int i = 0; i < contain; i++)
                        Contains[i].SetActive(true);
                }
                else
                {
                    for (int i = 0; i < contain; i++)
                        Contains[i].SetActive(false);
                }

                DeltaTime = 0;
            }
        }

        public void Fill() { contain = Contains.Count; }

    }
}
