using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 데이터 마킹
/// </summary>
public interface IData {}

/// <summary>
/// 데이터 마킹 클래스
/// </summary>
public class BaseData : IData
{
    
}


/// <summary>
/// 모든 데이터의 기본형 - string 형태의 id가 있다. /DataSource로 초기화함
/// </summary>
public abstract class BaseData<TDataSource> : BaseData where TDataSource : IDataSource
{
    public string Id { get; protected set; }
    public abstract void Init(TDataSource source);
}
