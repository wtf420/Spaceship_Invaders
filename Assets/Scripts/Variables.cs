using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Variables
    {
        public const int PLAYER = 0;
        public const int ENEMY = 1;
        public const int BULLET = 2;
        public const int LASER = 3;
        public const int MISSLE = 4;
        public const int ASTEROID = 5;
        public const int ITEM = 6;
        public const int SKILL = 7;

        public const float ScreenHeight = 16.0f;
        public const float ScreenWidth = 28.4f;

        public const float BackGroundSpeed = 1.3f;

        public const float Adjust = 0.5f;

        #region Player
        public const int PlayerHPDefault = 100;

        public const float PlayerBulletSpeed = 15.0f;
        public const float PlayerMissleSpeed = 10.0f;
        public const float PlayerMissleRotatingSpeed = 240.0f;
        public const float PlayerMissleCircleCastRadius = 2.5f;

        // state
        public const int Player_IDLE = 0;
        public const int Player_MOVE = 1;
        public const int Player_BOOST = 2;
        public const int Player_DESTROYED = 4;

        #endregion

        #region Enemy
        public const float EnemyFlySpeed = 1.0f;

        public const float EnemyBulletSpeed = 5.0f;
        public const float EnemyMissleSpeed = 5.0f;

        public const int HP_Enemy1 = 200; //1000
        public const int HP_Enemy2 = 1000;  //2000
        public const int HP_Enemy3 = 3000;  //5000
        public const int HP_Enemy4 = 5000;
        public const int HP_Enemy5 = 8000;
        public const int HP_Enemy6 = 10000;

        public const int HP_Boss1 = 10000;
        public const int HP_Boss2 = 100000;
        public const int HP_Boss3 = 200000;


        public const int Enemy1 = 0;
        public const int Enemy2 = 1;
        public const int Enemy3 = 2;
        public const int Enemy4 = 3;
        public const int Enemy5 = 4;
        public const int Enemy6 = 5;

        #endregion

        #region Bullet
        public const int ByPlayer = 10;
        public const int ByEnemy = 20;

        public const int DAMAGE_Bullet1 = 50;
        public const int Damage_Bullet_Default = 100;
        public const int Damage_Bullet_Default_Level2 = 200;
        public const int Damage_Bullet_Default_Level3 = 500;

        #endregion

        #region Laser
        public const int DAMAGE_Laser1 = 200;
        public const int Damage_Laser_Default = 500;
        public const int Damage_Laser_Default_Level2 = 1200;
        public const int Damage_Laser_Default_Level3 = 3000;

        #endregion

        #region Missle
        public const int DAMAGE_Missle1 = 50;
        public const int Damage_Missle_Default = 100;
        public const int Damage_Missle_Default_Level2 = 500;
        public const int Damage_Missle_Default_Level3 = 1000;

        #endregion

        #region Item
        public enum ItemType
        {
            Star,
            Fuel,
            Coin,
        }

        public const float ItemSpeed = 2.0f;
        #endregion

        #region Asteroid
        public const float AsteroidSpeed = 0.8f;

        #endregion

        #region Skill
        public enum Skill_Type
        {
            CircleShooting = 0,
            DivineDeparture = 1,
            EnergyWave = 2,
            Invincible = 3,
            ElectricShooting = 4,
            SectorShooting = 5,
        }

        public enum Skill_Effect
        {
            None = 0,
            OppositeDirection = 1,
        }

        #endregion


    }
}
