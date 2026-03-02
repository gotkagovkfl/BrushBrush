using System;
using System.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 전환될 씬의 파라미터
/// </summary>
public readonly struct LoadingPayload : IStatePayload<LoadingState>
{
    public readonly Type NextStateType;
    public readonly Action Invoke;

    LoadingPayload(Type stateType, Action invoke)
    {
        NextStateType = stateType;
        Invoke = invoke;
    }
    
    /// <summary>
    /// 외부에서 상태 전환 함수를 직접 인자에 넣는 방식이 마음에 안들어서 이렇게 수정
    /// </summary>
    public static LoadingPayload Create<TState>(IStatePayload<TState> payload) where TState : IState
    {
        return new LoadingPayload(
            typeof(TState),
            () => GameStateManager.Instance.Request<TState>(payload)
        );
    }

}

/// <summary>
/// 로딩 상태 - 씬을 불러오며, 기존 씬의 리소스를 해제하고, 새로운 씬의 리소스를 불러오고 세팅한다. 
/// 그동안은 로딩 이미지를 표시한다.
/// </summary>
public class LoadingState : BaseGameState<LoadingPayload>
{
    protected override void Enter_Impl()
    {
        
    }

    protected override void Exit_Impl()
    {
        
    }

    protected override void Update_Impl()
    {
        
    }

    //=============================================================

}