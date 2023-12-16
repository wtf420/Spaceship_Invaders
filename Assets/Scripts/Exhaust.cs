using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : MonoBehaviour
{   
    [SerializeField] ParticleSystem smoke1;
    [SerializeField] ParticleSystem smoke2;

    public void SetSmokeState(string State)
    {
        switch (State)
        {
            case "Idle":
            {
                smoke1.Play();
                smoke1.playbackSpeed = 1f;
                smoke2.Play();
                smoke2.playbackSpeed = 1f;
                return;
            }
            case "Move":
            {
                smoke1.Play();
                smoke1.playbackSpeed = 1.5f;
                smoke2.Play();
                smoke2.playbackSpeed = 1.5f;
                return;
            }
            case "Boost":
            {
                smoke1.Play();
                smoke1.playbackSpeed = 2f;
                smoke2.Play();
                smoke2.playbackSpeed = 2f;
                return;
            }
            case "Destroyed":
            {
                smoke1.Stop();
                smoke2.Stop();
                return;
            }
            default:
                return;
        }
    }
}
