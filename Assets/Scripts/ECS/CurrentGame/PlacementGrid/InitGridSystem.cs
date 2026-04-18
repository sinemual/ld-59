using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class InitGridSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<GridProvider>.Exclude<InitedMarker> _gridFilter;

        public void Run()
        {
            foreach (var idx in _gridFilter)
            {
                ref var entity = ref _gridFilter.GetEntity(idx);
                ref var grid = ref entity.Get<GridProvider>().GridCells;

                for (int i = 0; i < grid.Length; i++)
                {
                    _world.NewEntity().Get<GridCell>() = new GridCell()
                    {
                        Id = i,
                        IsBusy = false,
                        Point = grid[i]
                    };
                }

                entity.Get<InitedMarker>();
            }
        }
    }
}