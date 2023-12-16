using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataPersistence
{
    [System.Serializable]
    public class GameData
    {
        public int Score;
        public int Life;
        public int Coin;
        public int LevelBullet;
        public int Energy;
        public int Level;

        // When not have data to load
        public GameData()
        {
            Score = 0;
            Life = 3;
            Coin = 0;
            LevelBullet = 1;
            Energy = 9;
            Level = 1;
        }
    }
}
