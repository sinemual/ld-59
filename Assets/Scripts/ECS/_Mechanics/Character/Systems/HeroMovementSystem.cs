using Client.Data;
using Client.Data.Core;
using Client.DevTools.MyTools;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HeroMovementSystem : IEcsRunSystem
    {
        private SharedData _data;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<HeroProvider, CharacterProvider, RigidbodyProvider>.Exclude<LadleEquippedState> _filter;
        private EcsFilter<MovementHandledTag> _movementFilter;

        public void Run()
        {
            foreach (var heroIdx in _filter)
            foreach (var idx in _movementFilter)
            {
                ref var entity = ref _movementFilter.GetEntity(idx);
                ref var joystickInput = ref entity.Get<JoystickInput>();

                ref var heroEntity = ref _filter.GetEntity(heroIdx);
                ref var heroGo = ref heroEntity.Get<CharacterProvider>().View;
                ref var heroAnimator = ref heroEntity.Get<AnimatorProvider>().Value;
                ref var heroRb = ref heroEntity.Get<RigidbodyProvider>().Value;

                Transform cameraTransform = _cameraService.GetCurrentVC().transform;
                Vector3 cameraForward = cameraTransform.forward;
                Vector3 cameraRight = cameraTransform.right;
                cameraForward.y = 0;
                cameraRight.y = 0;
                cameraForward.Normalize();
                cameraRight.Normalize();

                Vector2 input;
                bool fromJoystick = joystickInput.Direction.sqrMagnitude >= 0.01f;

                if (fromJoystick)
                    input = joystickInput.Direction;
                else
                    input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                if (input == Vector2.zero)
                {
                    heroRb.linearVelocity = new Vector3(0, heroRb.linearVelocity.y, 0);
                    heroAnimator.SetBool(Animations.IsRun, false);
                    heroAnimator.SetBool(Animations.IsRunWithItem, false);
                    continue;
                }

                if (heroEntity.Has<StepSoundEvent>())
                {
                    if (Random.value > 0.5f)
                        _audioService.Play(Sounds.Step1);
                    else
                        _audioService.Play(Sounds.Step2);
                }


                Vector3 moveDirection = (cameraForward * input.y + cameraRight * input.x).normalized;
                float speed = 1;
                Vector3 velocity = moveDirection * speed;
                velocity.y = heroRb.linearVelocity.y;
                heroRb.linearVelocity = velocity;

                bool isGettingInput = Mathf.Abs(joystickInput.Direction.x) > 0.1f || Mathf.Abs(joystickInput.Direction.y) > 0.1f;

                if (joystickInput.Direction.magnitude == 0.0f)
                {
                    moveDirection = (cameraForward * Input.GetAxis("Vertical") + cameraRight * Input.GetAxis("Horizontal")).normalized;
                    speed = 1;
                    velocity = moveDirection * speed;
                    velocity.y = heroRb.linearVelocity.y;
                    heroRb.linearVelocity = velocity;

                    isGettingInput = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
                }

                heroAnimator.SetFloat(Animations.MovementSpeed, speed * 0.5f);
                if (isGettingInput && velocity.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    heroRb.MoveRotation(Quaternion.RotateTowards(heroRb.rotation, targetRotation,
                        _data.BalanceData.CharacterRotateSpeed * Time.fixedDeltaTime));
                }
            }
        }
    }

    internal struct LadleEquippedState
    {
    }
}