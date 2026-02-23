using System;
using System.Collections.Generic;

/// <summary>
/// 내부 StateMachine을 보유한 State.
/// 자식을 등록하면 서브 루프, 등록하지 않으면 리프처럼 동작.
/// </summary>
public class StateMachine
{   
    Dictionary<Type, IState> _states = new();

    IState _currState; // 현재 상태
    public IState CurrState => _currState;

    Type _pendingType;   // 전이할 상태 타입
    IStatePayload _pendingPayload;
    bool _hasPending;

    StateMachine _parent; // 부모 머신

    //==============================================================================
    #region [ 초기화 ]
    //==============================================================================

    /// <summary>
    /// 상위 부모 머신 지정
    /// </summary>
    public void SetParent(StateMachine p)
        => _parent = p;

    /// <summary>
    /// 해당 머신에 상태 추가
    /// </summary>
    public void Add<T>(T state) where T : IState
    {
        _states[typeof(T)] = state;
        state.SetOuterMachine(this);
    }

    /// <summary>
    /// 해당 머신의 초기 상태 지정
    /// </summary>
    public void SetInitial<T>() where T : IState
    {
        _currState = _states[typeof(T)];
    }

    /// <summary>
    /// 해당 머신의 초기 상태와 페이로드 지정
    /// </summary>
    public void SetInitial<T, TPayload>(TPayload payload) where T : IState where TPayload : IStatePayload
    {
        _pendingPayload = payload;
        SetInitial<T>();
    }

    #endregion

    // =============================================================================
    #region [ 실행 ]
    //==============================================================================
    /// <summary>
    /// 상태머신이 상태간 전이가 아니라 직접 시작될 때, 페이로드도 직접 전달 
    /// </summary>
    public void StartWithPayload(IStatePayload payload)
    {
        _pendingPayload = payload;
        Start();
    }

    /// <summary>
    /// 상태 머신 시작
    /// </summary>
    public void Start()
    {
        // 머신의 시작 상태가 반드시 존재해야한다.
        if (_currState == null)
            throw new Exception($"Initial state not set in {GetType().Name}");

        //
        _currState.Enter(_pendingPayload);
        _pendingPayload = null;
    }
    public void Exit() => _currState?.Exit();

    /// <summary>
    /// Deferred Transition 적용. (전이 요청시 현재 상태의 업데이트 까지 온전히 끝나고 상태 전이)
    /// </summary>
    public void Update()
    {
        _currState?.Update();

        if (_hasPending)
            ApplyTransition();
    }
    #endregion

    //=================================================================================
    #region [ 상태 전이 ]
    //=================================================================================

    /// <summary>
    /// 상태 전환 예약
    /// 모든 전환은 Payload를 가진다. 없으면 Empty
    /// </summary>
    public void Request<T, TPayload>(TPayload payload) where T : IState where TPayload : IStatePayload
    {
        _pendingType    = typeof(T);
        _pendingPayload = payload;
        _hasPending     = true;
    }


    /// <summary>
    /// 지정된 순서에 전이 실행
    /// </summary>
    private void ApplyTransition()
    {
        var type    = _pendingType;
        var payload = _pendingPayload;

        _pendingType    = null;
        _pendingPayload = null;
        _hasPending     = false;

        //
        if (_states.TryGetValue(type, out var next))
        {
            _currState?.Exit();
            _currState = next;
            _currState.Enter(payload);
        }
        else if (_parent != null)
        {
            _parent.BubbleUp(type, payload);
        }
        else
        {
            // 루트까지 올라가도 못 찾으면 명시적으로 터트림
            throw new InvalidOperationException($"[StateMachine] State '{type.Name}' not found in any machine.");
        }
    }

    /// <summary>
    /// 해당 상태를 못찾을 경우. 부모에서 찾음
    /// </summary>
    internal void BubbleUp(Type type, IStatePayload payload)
    {
        _pendingType    = type;
        _pendingPayload = payload;
        _hasPending     = true;
    }
    #endregion
}