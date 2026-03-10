using UnityEngine;

namespace _FlappyBird._Scripts
{
    public class MiddlePipe : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Logic.Instance.AddScore();
            }
        }
    }
}
