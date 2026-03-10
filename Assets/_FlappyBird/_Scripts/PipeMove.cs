using System;
using UnityEngine;

namespace _FlappyBird._Scripts
{
    public class PipeMovement : MonoBehaviour
    {
        [Header("Pipe Settings")]
        [Tooltip("The speed at which the pipe moves into the scene")]
        [Range(0, 20)]
        [SerializeField] private float moveSpeed = 5f;

        private const float DeadZone = -45f;
        private bool IsPipeDead => transform.position.x < DeadZone;
        private void FixedUpdate()
        {
            MovePipe();
            if (IsPipeDead)
            {
                PipeSpawner.Instance.ReturnToPool(this.gameObject);
            }
        }
        private void MovePipe()
        {
            transform.position = transform.position + Vector3.left * (moveSpeed * Time.deltaTime);
        }
    }
}