using System;
using System.Collections.Generic;


public interface IState
{
    void Enter();
    void Exit();
    void Update();
}



public abstract class CompositeState : IState
{
    protected StateMachine machine = new();

    //==============================================================================
    #region [ 초기화 ]
    //==============================================================================

    protected void Add<T>(T state) where T : IState
        => machine.Add(state);

    protected void Initial<T>() where T : IState
        => machine.SetInitial<T>();

    /// <summary>
    /// StateMachine.Add() 에서 자동 호출 - 직접 호출 불필요
    /// </summary>
    public void SetOuterMachine(StateMachine outer)
        => machine.SetParent(outer);

    #endregion
    // =============================================================================
    #region [ 실행 ]
    //==============================================================================

    public void Enter()
    {
        Enter_Impl();
        machine.Start();
    }

    public void Exit()
    {
        machine.Exit();
        Exit_Impl();
    }


    public void Update()
    {
        Update_Impl();
        machine.Update();
    }

    protected abstract void Enter_Impl();
    protected abstract void Exit_Impl();
    protected abstract void Update_Impl();

    #endregion
    //=================================================================================
    #region [ 상태 전이 ]
    //=================================================================================

    protected void Request<T>() where T : IState
        => machine.Request<T>();

    #endregion
}