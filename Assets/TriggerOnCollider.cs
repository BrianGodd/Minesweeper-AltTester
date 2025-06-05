using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnCollider : MonoBehaviour
{
    public DetectManager DManager;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "Can")
        {
            DManager.AddOBJ(this.gameObject);
        }

        if(other.gameObject.tag == "Ground")
        {
            DManager.RemoveOBJ(this.gameObject);
        }
    }

    // void OnCollisionExit(Collision other)
    // {
    //     if(other.gameObject.tag == "PaperBall")
    //     {
    //         DManager.RemoveOBJ(other.gameObject);
    //     }
    // }
}