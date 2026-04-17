using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesService
{
    public AddressablesService()
    {
    }

    public async UniTask<T> LoadAsset<T>(AssetReference key) where T : Object
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
        await handle.Task;
        T result = handle.Result;

        if (result is ScriptableObject scriptableObject)
            return scriptableObject as T;

        return result;
    }
    
    public async UniTask<AsyncOperationHandle<IList<T>>> LoadAssets<T>(AssetLabelReference key)
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(key, null);
        await handle.Task;
        return handle;
    }
    
    public void ReleaseAssets<T>(AsyncOperationHandle<IList<T>> handle) where T : Object
    {
        Addressables.Release(handle);
    }
    
    public void ReleaseGameObject(GameObject go)
    {
        Addressables.ReleaseInstance(go);
    }
}