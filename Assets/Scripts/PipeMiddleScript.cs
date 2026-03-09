using System;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LogicScript.Instance.AddScore();
             
        }
    }
}
