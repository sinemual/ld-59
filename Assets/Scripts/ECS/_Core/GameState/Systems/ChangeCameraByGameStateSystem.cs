using Client.Data;
using Client.Data.Core;
using Leopotam.Ecs;

namespace Client
{
    public class ChangeCameraByGameStateSystem : IEcsRunSystem
    {
        private CameraService _camera;
        private SharedData _data;
        private EcsWorld _world;
        
        private EcsFilter<GameStateChangedEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                /*if(_data.RuntimeData.CurrentGameStateType == GameStateType.Ride)
                    _camera.SetCamera(CameraType.InMenu);
                else if (_data.RuntimeData.CurrentGameStateType == GameStateType.Garage)
                    _camera.SetCamera(CameraType.Ride);*/
                /*
                if (_data.RuntimeData.CurrentGameStateType == GameStateType.Garage)
                    _camera.SetCamera(CameraType.InGarage, _carFilter.Get2(0).Value.transform,_carFilter.Get2(0).Value.transform );
            */
            }
        }
    }
}