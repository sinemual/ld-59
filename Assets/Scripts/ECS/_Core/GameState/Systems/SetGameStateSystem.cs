using Client.Data.Core;
using Leopotam.Ecs;

namespace Client
{
    public class SetGameStateSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        
        private EcsFilter<SetGameStateRequest> _requestFilter;

        public void Run()
        {
            foreach (var request in _requestFilter)
            {
                ref var entity = ref _requestFilter.GetEntity(request);
                ref var setGameStateRequest = ref entity.Get<SetGameStateRequest>();

                _data.RuntimeData.PreviousGameStateType = _data.RuntimeData.CurrentGameStateType;
                _data.RuntimeData.CurrentGameStateType = setGameStateRequest.NewGameStateType;
                _world.NewEntity().Get<GameStateChangedEvent>();
                
                entity.Del<SetGameStateRequest>();
            }
        }
    }
}