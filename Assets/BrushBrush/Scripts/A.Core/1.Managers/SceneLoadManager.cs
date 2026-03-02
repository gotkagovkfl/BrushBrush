using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneLoadManager : MonoSingleton<SceneLoadManager>
{   
    /// <summary>
    /// 상태 타입과 씬 이름 매핑
    /// </summary>
    static readonly Dictionary<Type, string> _sceneMap = new ()
    {
        {typeof(PreloadState),"PreloadScene"},
        {typeof(LoadingState),"LoadingScene"},
        {typeof(TitleState),"TitleScene"},
        {typeof(LobbyState),"LobbyScene"},
        {typeof(InGameState),"InGameScene"},   
    };

    //================================================================

    enum ProgressState
    {
        None,
        IsLoading,
        IsUnloading,
    }
    ProgressState currProgressState = ProgressState.None;

    /// <summary>
    /// 로드하기 위해 씬 이름 가져오기
    /// </summary>
    string GetSceneName(Type type)
    {
        string ret = string.Empty;
        _sceneMap.TryGetValue(type, out ret);
        return ret;
    }

    //========================================================
    protected async override UniTask InitImpl()
    {
        
    }

    //===================================================================
    #region [ 로드 ]
    //===============================================================
    /// <summary>
    /// 타입으로 비동기 씬 로드
    /// </summary>
    public async UniTask LoadScene(Type type)
    {
        await LoadScene(GetSceneName(type));
    }

    /// <summary>
    /// 비동기 씬 호출
    /// </summary>
    public async UniTask LoadScene(string sceneName)
    {
        //
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"[LoadScene] 씬이름이 비어서 로드하지 않음.");
            return;
        }

        //
        if (currProgressState != ProgressState.None)
        {
            Debug.LogError($"[SceneLoad] 이미 로드 작업이 진행중임. ");
            return;
        }
        currProgressState = ProgressState.IsLoading;

        // 비동기 씬호출
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        //
        while (asyncLoad.progress < 0.9f)
            await UniTask.Yield();

        asyncLoad.allowSceneActivation = true;
        await UniTask.WaitUntil(() => asyncLoad.isDone);

        //
        currProgressState = ProgressState.None;
        Debug.Log($"[로드 완료] {sceneName} ");
    }

    #endregion
    //===================================================================
    #region [ 언로드 ]
    //===============================================================
    /// <summary>
    /// 타입으로 비동기 씬 언로드
    /// </summary>
    public async UniTask UnloadScene(Type type)
    {
        await UnloadScene(GetSceneName(type));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    public async UniTask UnloadScene(string sceneName)
    {
        // 
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid() == false || scene.isLoaded == false)
        {
            Debug.LogError($"[UnloadScene] '{sceneName}' 씬이 로드되어 있지 않습니다.");
            return;
        }

        //
        if (currProgressState != ProgressState.None)
        {
            Debug.LogError($"[SceneLoad] 이미 언로드 작업이 진행중임. ");
            return;
        }
        currProgressState = ProgressState.IsUnloading;

        //
        AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);
        await op.ToUniTask();

        //
        currProgressState = ProgressState.None;
        Debug.Log($"[ 언로드 완료 ] {sceneName} ");
    }
    
    #endregion
}
