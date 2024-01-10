using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Cheats : MonoBehaviour
{
    public bool Activated = false;

    void Update()
    {
        if (Activated)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Player.Instance.WeaponLevelUp(1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Player.Instance.WeaponLevelUp(-1);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                HUD.Instance.Life++;
                HUD.Instance.DisplayFloatingText("Life +1", Player.Instance.Body.position);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                HUD.Instance.Coin++;
            }
            else if (Input.GetKeyDown(KeyCode.N))
                GameManager.Instance.NextLevel();
        }
    }
}
