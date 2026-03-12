using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _FlappyBird._Scripts
{
    public enum BirdAnimationClips
    {
        TiltingAnim,
        FlapWingsAnim
    }
    [RequireComponent(typeof(Animator))]
    public class BirdAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            FlapWings();
            BirdAnimation();
        }

        private void FlapWings()
        {
            animator.Play(BirdAnimationClips.FlapWingsAnim.ToString());
        }

        private void BirdAnimation()
        {
            animator.Play(BirdAnimationClips.TiltingAnim.ToString(), -1, 0f);
        }
    }
}