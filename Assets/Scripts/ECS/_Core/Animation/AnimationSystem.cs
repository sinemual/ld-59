using Client.DevTools.MyTools;
using Leopotam.Ecs;
using UnityEngine;
using static Client.Data.StaticData;

namespace Client
{
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<AnimatorProvider, SetAnimationRequest> _filter;
        private EcsFilter<AnimatorProvider, SetAnimationNumRequest> _numFilter;
        private EcsFilter<AnimatorProvider, SetAnimationBoolRequest> _boolFilter;
        private EcsFilter<AnimatorProvider, SetAnimationSpeedRequest> _setSpeedFilter;
        private EcsFilter<AnimatorProvider, ResetAnimationTriggerRequest> _resetTriggetFilter;

        public void Run()
        {
            foreach (var idx in _boolFilter)
            {
                ref var entity = ref _boolFilter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();
                ref SetAnimationBoolRequest setAnimationBoolRequest = ref entity.Get<SetAnimationBoolRequest>();

                AnimationTools.SetAnimationBool(entityAnimator.Value, setAnimationBoolRequest.StateAnimationName,
                    setAnimationBoolRequest.State);

                entity.Del<SetAnimationBoolRequest>(); // dont move 
            }

            foreach (var idx in _numFilter)
            {
                ref var entity = ref _numFilter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();
                ref SetAnimationNumRequest setAnimationNumRequest = ref entity.Get<SetAnimationNumRequest>();

                AnimationTools.SetAnimationNumber(entityAnimator.Value, setAnimationNumRequest.AnimationName,
                    setAnimationNumRequest.Number);

                entity.Del<SetAnimationNumRequest>(); // dont move 
            }

            foreach (var idx in _resetTriggetFilter)
            {
                ref var entity = ref _resetTriggetFilter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();

                ref ResetAnimationTriggerRequest resetAnimationTriggerRequest = ref entity.Get<ResetAnimationTriggerRequest>();

                AnimationTools.ResetAnimationTrigger(entityAnimator.Value, resetAnimationTriggerRequest.Animation);

                entity.Del<ResetAnimationTriggerRequest>(); // dont move 
            }
            
            foreach (var idx in _setSpeedFilter)
            {
                ref var entity = ref _setSpeedFilter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();

                ref SetAnimationSpeedRequest resetAnimationTriggerRequest = ref entity.Get<SetAnimationSpeedRequest>();

                AnimationTools.SetAnimationSpeed(entityAnimator.Value, resetAnimationTriggerRequest.MultiplierName,
                    resetAnimationTriggerRequest.Speed);

                entity.Del<SetAnimationSpeedRequest>(); // dont move 
            }

            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();
                ref SetAnimationRequest changeAnimationRequest = ref entity.Get<SetAnimationRequest>();

                AnimationTools.SetAnimation(entityAnimator.Value, changeAnimationRequest.Animation);

                entity.Del<SetAnimationRequest>(); // dont move 
            }
        }
    }
}