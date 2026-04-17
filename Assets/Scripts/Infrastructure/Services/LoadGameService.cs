using System.Collections.Generic;
using System.Linq;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadGameService
{
    private SharedData _data;
    private AddressablesService _addressablesService;
    private UserInterface _ui;

    public LoadGameService(SharedData data, AddressablesService addressablesService, UserInterface ui)
    {
        _data = data;
        _addressablesService = addressablesService;
        _ui = ui;
    }

    public async UniTask StartGameLoad()
    {
        //Levels
        /*if (_data.StaticData.AlwaysLoadWorldId != -1)
            _data.PlayerData.LevelIdx = _data.StaticData.AlwaysLoadWorldId;*/
        /*var levelsHandle =
            Addressables.LoadAssetsAsync<GameObject>(_data.StaticData.Zones[_data.PlayerData.ZoneIdx][_gameData.PlayerData.CurrentLevelPackIndex], null);
            */

        //Other
        List<AsyncOperationHandle<GameObject>> allHandles = new List<AsyncOperationHandle<GameObject>>();

        //var xHandle = Addressables.LoadAssetAsync<GameObject>(_gameData.StaticData.PrefabData.x);

        //allHandels.Add(xHandle);

        await OnStartUpdateLoadingScreenProgress(allHandles);

       //Object.Instantiate(xHandle);
       
        HideLoadingScreen();
    }

    private async UniTask OnStartUpdateLoadingScreenProgress(List<AsyncOperationHandle<GameObject>> handles)
    {
        ShowLoadingScreen();
        float fakeLoadingTime = 1.62f;
#if UNITY_EDITOR
        fakeLoadingTime = 0.0f;
#endif
        int loadNum = 0;
        /*await DOTween.To(() => loadNum, x => loadNum = x, 100, fakeLoadingTime)
            .OnUpdate(() =>
            {
                _ui.LoadingScreen.loadingProcessText.text = $"{loadNum:D}%";
                _ui.LoadingScreen.loadinProgressBar.fillAmount = loadNum * 0.01f;
                
            }).ToUniTask();*/
        
        /*
        float progress = 0.0f;
        while (progress < 1.00f)
        {
            progress = handles.Sum(x => x.PercentComplete) / handles.Count;
            await UniTask.Yield();
        }*/
    }

    private void ShowLoadingScreen()
    {
        _ui.ShowScreen<LoadingScreen>();
    }

    private void HideLoadingScreen()
    {
        _ui.HideScreen<LoadingScreen>();
    }
}