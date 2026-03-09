using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PipeMovement : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private float moveSpeed = 5f;

    private const float DeadZone = -45f;

    private void Update()
    {
        transform.position = transform.position + Vector3.left * (moveSpeed * Time.deltaTime);

        if (transform.position.x < DeadZone)
        {
            PipeSpawnerScript.Instance.ReturnToPool(this.gameObject);
        }
    }
}
