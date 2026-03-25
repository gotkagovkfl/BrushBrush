using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System;

/// <summary>
/// 데이터 매니저 마킹
/// </summary>

public interface IDataManager
{
    Type DataType { get; }
    UniTask Init();
}

/// <summary>
/// 데이터 매니저
/// </summary>
public interface IDataManager<TData> : IDataManager
{
    TData Get(string id);
}

/// <summary>
/// TDataSource를 소스로 갖는 TData의 목록
/// </summary>
public abstract class DataManager<TData, TDataSource> : IDataManager<TData>
                            where TData : BaseData<TDataSource>, new()
                            where TDataSource : IDataSource
{
    public bool Initialized { get; private set; }
    
    /// <summary>
    /// 해당 데이터 매니저의 데이터 타입
    /// </summary>
    public Type DataType => typeof(TData);
    
    /// <summary>
    /// 이넘 타입 - 이건 enum에서 사용중인 매니저를 빠르게 찾기 위함
    /// </summary>
    public abstract DataEnumType DataEnumType { get; }

    protected Dictionary<string, TData> _dic = new();

    //=============================================
    /// <summary>
    /// 데이터 매니저 초기화
    /// </summary>
    public async UniTask Init()
    {   
        Debug.Log($"[ Data - {GetType()}] 초기화");
        TDataSource[] sources = await GetSource();
        InitDic(sources);
        
        Initialized = true;
        Debug.Log($"  ==== [ {GetType()} 초기화 성공 ] ");
    }

    /// <summary>
    /// 초기화 전의 raw 데이터를 가져온다.
    /// </summary>
    protected abstract UniTask<TDataSource[]> GetSource();

    /// <summary>
    /// 데이터 dictionary 초기화
    /// </summary>
    protected void InitDic(TDataSource[] sources)
    {
        _dic ??= new();
        _dic.Clear();

        foreach (TDataSource source in sources)
        {
            TData data = new();
            data.Init(source);

            string id = data.Id;
            if (_dic.ContainsKey(data.Id))
            {
                Debug.LogError($" [ {GetType()} ] 이미 있는 id : {id})");
                continue;
            }

            _dic[id] = data;
        }
    }

    /// <summary>
    /// 데이터 얻기
    /// </summary>
    public TData Get(string id)
    {
        TData ret;
        _dic.TryGetValue(id, out ret);
        
        if (ret == null)
        {
            Debug.LogError($" [ {typeof(TData)} ] {id}에 해당하는 데이터가 null");
        }
        return ret;
    }
}
