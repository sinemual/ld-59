using UnityEngine;

namespace Client.DevTools.MyTools
{
    public static class AnimationTools
    {
        public static void ResetAnimator(Animator animator)
        {
            animator.Rebind();
            animator.Update(0f);
            //animator.Play("Idle", 0, 0f);
        }

        public static void SetAnimation(Animator animator, int animation, int randomAnimation = -1,
            string animationRandom = null)
        {
            if (animator.GetBool(animation))
                return;

            //ResetAnimator(animator);

            if (animationRandom != null)
                animator.SetInteger(animationRandom, randomAnimation);

            animator.SetTrigger(animation);
        }

        public static void SetAnimationSpeed(Animator animator, string multiplierName, float speed) =>
            animator.SetFloat(multiplierName, speed);

        public static void SetAnimationBool(Animator animator, string stateName, bool state) =>
            animator.SetBool(stateName, state);

        public static void SetAnimationNumber(Animator animator, string stateName, int num) =>
            animator.SetInteger(stateName, num);

        public static void ResetAnimationTrigger(Animator animator, int animation) =>
            animator.ResetTrigger(animation);
    }
}