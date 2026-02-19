using System;
using System.Collections.Generic;

public class StateMachine
{   
    Dictionary<Type, IState> states = new();

    IState current; // 현재 상태
    Type pending;   // 전이할 상태 타입

    StateMachine parent; // 부모 머신

    //==============================================================================
    #region [ 초기화 ]
    //==============================================================================

    /// <summary>
    /// 상위 부모 머신 지정
    /// </summary>
    public void SetParent(StateMachine p)
        => parent = p;

    /// <summary>
    /// 해당 머신에 상태 추가
    /// </summary>
    public void Add<T>(T state) where T : IState
    {
        states[typeof(T)] = state;
        
        // 해당 머신의 상태들의 부모 머신을 자기 자신으로 설정
        if (state is CompositeState composite)
            composite.SetOuterMachine(this);
    }

    /// <summary>
    /// 해당 머신의 초기 상태 지정
    /// </summary>
    public void SetInitial<T>() where T : IState
        => current = states[typeof(T)];

    #endregion

    // =============================================================================
    #region [ 실행 ]
    //==============================================================================

    public void Start()
    {
        // 머신의 시작 상태가 반드시 존재해야한다.
        if (current == null)
            throw new Exception($"Initial state not set in {GetType().Name}");

        //
        current.Enter();
    }
    public void Exit() => current?.Exit();

    /// <summary>
    /// Deferred Transition 적용. (전이 요청시 현재 상태의 업데이트 까지 온전히 끝나고 상태 전이)
    /// </summary>
    public void Update()
    {
        current?.Update();

        if (pending != null)
            Apply();
    }
    #endregion

    //=================================================================================
    #region [ 상태 전이 ]
    //=================================================================================

    /// <summary>
    /// 상태 전이 요청 (해당 머신을 보유한 상태에서 호출)
    /// </summary>
    public void Request<T>() where T : IState
        => pending = typeof(T);

    /// <summary>
    /// 상태 전이 요청
    /// </summary>
    void RequestByType(Type t)
        => pending = t;

    /// <summary>
    /// 상태 전이 적용 - 해당 머신에 상태가 없다면 부모한테 요청
    /// </summary>
    void Apply()
    {
        if (states.TryGetValue(pending, out var next))
        {
            current?.Exit();
            current = next;
            current.Enter();
        }
        else
        {
            parent?.RequestByType(pending);
        }

        pending = null;
    }



    #endregion
}