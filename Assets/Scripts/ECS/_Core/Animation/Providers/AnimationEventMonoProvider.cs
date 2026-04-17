using Leopotam.Ecs;
using UnityEngine;

public class AnimationEventMonoProvider : MonoBehaviour
{
    [SerializeField] private MonoEntity monoEntity;

    /*public void CatchAnimationEvent(string animationEventName)
    {
        if (monoEntity.Entity.IsAlive())
        {
            monoEntity.Entity.Get<AnimationEventData>().
        }
    }*/
    
    public void CatchStepAnimationEvent()
    {
        if (monoEntity.Entity.IsAlive())
        {
            monoEntity.Entity.Get<StepSoundEvent>();
        }
    }
}