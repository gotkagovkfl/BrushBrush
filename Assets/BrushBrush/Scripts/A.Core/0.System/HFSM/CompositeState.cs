using System;
using System.Collections.Generic;


//=======================================================================================================================

public abstract class CompositeState<TPayload> : IState where TPayload : IStatePayload
{
    private readonly StateMachine _inner = new();   // 현재
    private StateMachine _outer;    // 부모
    protected TPayload Payload { get; private set; }

    // ==========================================================
    #region [ 초기화 ]
    // ==========================================================

    protected void Add<T>(T state) where T : IState
        => _inner.Add(state);

    protected void Initial<T>() where T : IState
        => _inner.SetInitial<T>();

    /// <summary>
    /// StateMachine.Add()에서 자동 호출. 직접 호출 불필요
    /// </summary>
    public void SetOuterMachine(StateMachine outer)
    {
        _outer = outer;
        _inner.SetParent(outer);
    }

    #endregion
    // =============================================================================
    #region [ 실행 ]
    //==============================================================================

    public void Enter(IStatePayload payload)
    {
        Payload = payload != null ? (TPayload)payload : default;    // 페이로드가 세팅되지 않은 경우, 기본 값으로 설정 (터짐 방지)
        Enter_Impl();
        _inner.Start();
    }

    public void Exit()
    {
        _inner.Exit();
        Exit_Impl();
    }


    public void Update()
    {
        Update_Impl();
        _inner.Update();
    }

    protected abstract void Enter_Impl();
    protected abstract void Exit_Impl();
    protected abstract void Update_Impl();

    #endregion
    //=================================================================================
    #region [ 상태 전이 ]
    //=================================================================================
 
    protected void Request<T, TNextPayload>(TNextPayload payload) where T : IState where TNextPayload : IStatePayload
        => _inner.Request<T, TNextPayload>(payload);

    protected void RequestToOuter<T, TNextPayload>(TNextPayload payload) where T : IState where TNextPayload : IStatePayload
        => _outer?.Request<T, TNextPayload>(payload);

        
    #endregion
}