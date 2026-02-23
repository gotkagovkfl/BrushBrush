using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 게임 상태 관리 - HFSM - 싱글톤이자, CompositeState라 보면 됨.
/// </summary>
public class GameStateManager : MonoSingleton<GameStateManager>
{
    readonly StateMachine _machine = new(); // root 머신

    //======================================================
    #region [ 초기화 ]
    //======================================================
    /// <summary>
    /// 상태 머신이 가동되면서 본격적으로 게임이 시작된다.
    /// </summary>
    protected async override UniTask InitImpl()
    {
        RegisterStates();
    }

    #endregion
    //======================================================
    #region [ 기능 ]
    //======================================================
    
    void RegisterStates()
    {
        // 최상위 State 등록
        _machine.Add(new LoadingState());
        _machine.Add(new PreloadState());
        _machine.Add(new TitleState());
        _machine.Add(new InGameState());

        // 초기 State와 페이로드 지정
        _machine.SetInitial<PreloadState, PreloadPayload>(default);
    }

    void Start()
    {
        _machine.Start();
    }

    void Update()
    {
        _machine.Update();
    }

    #endregion
    // ==========================================================
    #region [ 상태 전이 ]
    // ==========================================================

    /// <summary>
    /// 전환
    /// </summary>
    public void Request<T, TPayload>(TPayload payload) where T : IState where TPayload : IStatePayload
        => _machine.Request<T, TPayload>(payload);


    // todo : 현재 상태 디버깅 텍스트 ( 유틸 ) , 전이 관련
    
    #endregion
}
