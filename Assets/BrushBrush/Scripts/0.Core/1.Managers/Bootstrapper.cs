using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 게임 시작 관리
/// </summary>
public class Bootstapper : MonoSingleton<Bootstapper>
{

    //===============================================================
    protected override void Awake()
    {
        base.Awake();
        InitAsync().Forget();
    }

    //=================================================================

    /// <summary>
    /// 게임 초기화 
    /// </summary>
    protected async override UniTask InitImpl()
    {
    }
}
