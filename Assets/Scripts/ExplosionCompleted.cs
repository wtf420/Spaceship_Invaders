using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCompleted : MonoBehaviour
{
    public void ExplosionEnd(int i)
    {
        //Debug.Log("ExplosionCompleted");
        Destroy(gameObject);
    }

    public void DeleteWithParent(int i)
    {
        //Debug.Log("ExplosionCompleted");
        //GameObject parent = gameObject.transform.parent.gameObject;
        //gameObject.transform.SetParent(null);
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
