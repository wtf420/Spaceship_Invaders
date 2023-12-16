using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class HUD: MonoBehaviour
    {
        static HUD instance = null;

        public GameObject floatingText;
        public GameObject Canvas;

        public static HUD Instance
        {
            get { return instance; }
            private set { instance = value; }
        }

        public TMP_Text Scores;
        public TMP_Text Lifes;
        public TMP_Text Coins;

        int score = 0;
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                Scores.text = string.Format("{0: 000,000}", score);
                DataPersistence.DataPersistenceManager.Instance.gameData.Score = value;
            }
        }

        int life = 3;
        public int Life
        {
            get { return life; }
            set
            {
                life = value;
                Lifes.text = life.ToString();
                DataPersistence.DataPersistenceManager.Instance.gameData.Life = value;
            }
        }

        int coin = 0;
        public int Coin
        {
            get { return coin; }
            set
            {
                coin = value;
                Coins.text = coin.ToString();
                DataPersistence.DataPersistenceManager.Instance.gameData.Coin = value;
            }
        }

        public void DisplayFloatingText(string displayText, Vector2 position)
        {
            int randomCoorX = Random.Range(1, 9);
            int randomCoorY = Random.Range(1, 9);
            Vector2 location = Camera.main.WorldToScreenPoint(position);
            location.x += -0.5f + randomCoorX * 0.1f;
            location.y += -0.5f + randomCoorY * 0.1f;
            GameObject text = Instantiate(floatingText, location, new Quaternion(0,0,0,0));
            text.transform.SetParent(Canvas.transform);
            text.GetComponentInChildren<Text>().text = displayText;
        }

        // Get score and Life, ... from file
        private void Start()
        {
            if (instance == null) 
                instance = this;
            //Life = 30;

            Score = DataPersistence.DataPersistenceManager.Instance.gameData.Score;
            Life = DataPersistence.DataPersistenceManager.Instance.gameData.Life;
            Coin = DataPersistence.DataPersistenceManager.Instance.gameData.Coin;
            Debug.Log(string.Format("Load HUD: Score: {0}, Life: {1}, Coin: {2}", score, life, coin));
        }

    }
}
