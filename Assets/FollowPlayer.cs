using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class FollowPlayer : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
