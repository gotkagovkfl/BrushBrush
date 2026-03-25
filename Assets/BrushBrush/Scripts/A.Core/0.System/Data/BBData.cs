using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 게임에서 쓰이는 데이터 메니저 집합
/// </summary>
public static class BBData
{
    /// <summary>
    /// 데이텅에 접근하기 위해 캐싱
    /// </summary>
    static Dictionary<Type, IDataManager> _dataManagers = new();

    //===============================================================
    #region [ 초기화 ]
    //===============================================================
    /// <summary>
    /// 게임 시작 전에 반드시 초기화 되어야하는 데이터
    /// </summary>
    public static async UniTask InitPreloadData()
    {

    }

    /// <summary>
    /// 용량 감소를 위해 실행 후에 받는 데이터 - titleScene에서 불러옴
    /// </summary>
    public static async UniTask InitStreamingData()
    {
        await CreateAndInit<StageDataManager>();
    }

    /// <summary>
    /// 데이터 매니저 생성 및 초기화
    /// </summary>
    static async UniTask CreateAndInit<T>() where T : IDataManager, new()
    {
        // 초기화
        T mng = new();
        await mng.Init();

        // 사전에 캐싱
        _dataManagers[mng.DataType] = mng;
    }

    #endregion
    //=================================================================
    /// <summary>
    /// id에 대응하는 T 데이터 획득
    /// </summary>
    public static T Get<T>(string id) where T : BaseData
    {
        // 캐싱되어 있고, typed
        if (_dataManagers.TryGetValue(typeof(T), out IDataManager dataManager) && dataManager is IDataManager<T> typed)
        {
            return typed.Get(id);
        }

        // 예외
        Debug.Log($"[ BBData ] 유요하지 않는 데이터 type: {typeof(T)}, id: {id} ");
        return null;
    }
}


