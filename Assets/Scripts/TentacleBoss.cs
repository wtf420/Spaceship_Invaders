using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class TentacleBoss : Enemy
{
    [SerializeField] public List<Tentacle> tentacles;
    public List<bool> tentaclesAlive;

    void Awake()
    {
        this.Init();
        HP = 1000000;

        foreach (Tentacle t in tentacles)
        {
            bool b = true;
            tentaclesAlive.Add(b);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tentacles.Count.ToString());
        if (HP <= 0 || TentacleCheck() == 0)
            IsDeleted = true;
        UpdateStatusEffect();
    }

    int TentacleCheck()
    {
        int result = tentacles.Count;
        foreach (Tentacle t in tentacles)
        {
            if (t == null)
            {
                if (tentaclesAlive[tentacles.IndexOf(t)] == true )
                {
                    tentaclesAlive[tentacles.IndexOf(t)] = false;
                    AudioManager.Instance.PlayEnemyExplosion();
                    Instantiate(Explosion, Body.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    HP -= 250000;
                    //spawn explosion
                }
                result--;
            }
        }
        return result;
    }
}
