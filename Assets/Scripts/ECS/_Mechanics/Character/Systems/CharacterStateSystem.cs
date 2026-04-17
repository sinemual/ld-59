using Leopotam.Ecs;

namespace Client
{
    public class CharacterStateSystem<T> : IEcsRunSystem where T : struct
    {
        private EcsFilter<SetCharacterStateRequest<T>> _requestFilter;

        public void Run()
        {
            foreach (var idx in _requestFilter)
            {
                ref var entity = ref _requestFilter.GetEntity(idx);

                /*if(entity.Has<PlayerTagProvider>())
                    Debug.Log($"{typeof(T)}");*/

                entity.Del<CurrentCharacterState<IdleState>>();
                entity.Del<CurrentCharacterState<GangLeadState>>();
                entity.Del<CurrentCharacterState<UseInteractionState>>();
                entity.Del<CurrentCharacterState<DrivingState>>();
                entity.Del<CurrentCharacterState<ShootingState>>();

                entity.Get<CurrentCharacterState<T>>();
                entity.Del<SetCharacterStateRequest<T>>();
            }
        }
    }
}