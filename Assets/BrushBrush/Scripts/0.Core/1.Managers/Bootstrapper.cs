using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 설정된 값으로 게임 진행에 필요한 요소들을 생성 한다. 
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
    /// 게임 초기화 및 시작
    /// </summary>
    protected async override UniTask InitImpl()
    {
        // 게임 상태 매니저 초기화
        await GameStateManager.CreateAndInitializeAsync();

        //
    }
}
