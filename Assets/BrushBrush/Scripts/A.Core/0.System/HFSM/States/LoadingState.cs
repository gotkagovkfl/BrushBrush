using System;
using System.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 전환될 씬의 파라미터
/// </summary>
public readonly struct LoadingPayload : IStatePayload<LoadingState>
{
    public readonly Type PrevStateType;
    public readonly Type NextStateType;
    public readonly Action SwitchStateAction;

    public LoadingPayload(Type prevStateType, Type nextStateType, Action action)
    {
        PrevStateType = prevStateType;
        NextStateType = nextStateType;
        SwitchStateAction = action;
    }

    /// <summary>
    /// 컴파일 단계 타입 검증을 위해 상태 전환 기능을 페이로드에 넣었다.
    /// </summary>
    public static LoadingPayload Create<TNextState>(Type prevStateType, IStatePayload<TNextState> payload) where TNextState : IState
    {
        return new(prevStateType, typeof(TNextState), () => GameStateManager.Instance.Request<TNextState>(payload));
    }
}

/// <summary>
/// 로딩 상태 - 씬을 불러오며, 기존 씬의 리소스를 해제하고, 새로운 씬의 리소스를 불러오고 세팅한다. 
/// 그동안은 로딩 이미지를 표시한다.
/// </summary>
public class LoadingState : BaseGameState<LoadingPayload>
{
    protected override async UniTask Enter_Impl()
    {
        // 로딩 씬 로드해놓기
        await SceneLoadManager.Instance.LoadScene(typeof(LoadingState));
    }

    protected override void StartState()
    {
        SwitchSceneAsnyc().Forget();
    }


    protected override async UniTask Exit_Impl()
    {
        await SceneLoadManager.Instance.UnloadScene(typeof(LoadingState));
    }

    protected override void Update_Impl()
    {

    }

    //=============================================================
    /// <summary>
    /// payload 로 받은 상태로 전이한다.
    /// </summary>
    async UniTask SwitchSceneAsnyc()
    {
        // 사용하지 않는 씬 파괴
        Type prevStateType = Payload.PrevStateType;
        await SceneLoadManager.Instance.UnloadScene(prevStateType);

        // 새로운 씬 로드
        Type nextStateType = Payload.NextStateType;
        await SceneLoadManager.Instance.LoadScene(nextStateType);

        // 다됐으면 상태 전환
        await UniTask.Yield();  // 씬로드 후 안정적으로 한프레임 대기후에 상태가 전환된다.

        Payload.SwitchStateAction?.Invoke(); // 다시한번 페이드인/아웃 하면서 상태 전환
    }

}