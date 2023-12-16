using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts
{
    public class ShopMenuScript: MonoBehaviour
    {
        [SerializeField]
        List<GameObject> BulletStates;

        [SerializeField]
        FuelStateBar fuel;
        [SerializeField]
        List<GameObject> EnergyStates;

        [SerializeField]
        TMP_Text UpgradeBulletCoin;
        [SerializeField]
        TMP_Text BuyEnergyCoin;

        // Call when this setActive = true
        private void OnEnable()
        {
            RenderNewState();
        }

        public void UpLevelBullet()
        {
            if(HUD.Instance.Coin >= int.Parse(UpgradeBulletCoin.text) && Weapon.Level < 3)
            {
                HUD.Instance.Coin -= int.Parse(UpgradeBulletCoin.text);

                Weapon.Level++;
                RenderNewState();
            }
        }

        public void UpEnergy()
        {
            if(HUD.Instance.Coin >= int.Parse(BuyEnergyCoin.text) && fuel.Contain < EnergyStates.Count)
            {
                HUD.Instance.Coin -= int.Parse(BuyEnergyCoin.text);
                fuel.Contain++;

                RenderNewState();
            }
        }

        public void RenderNewState()
        {
            //// Bullet Level
            for (int i = 0; i < (Weapon.Level * 3) && i < BulletStates.Count; i++)
                BulletStates[i].SetActive(true);

            for (int i = Weapon.Level * 3; i < BulletStates.Count; i++)
                BulletStates[i].SetActive(false);


            //// Energy
            if(fuel != null)
            {
                for (int i = 0; i < fuel.Contain; i++)
                    EnergyStates[i].SetActive(true);

                for (int i = (int)fuel.Contain; i < EnergyStates.Count; i++)
                    EnergyStates[i].SetActive(false);
            }
        }

    }
}
