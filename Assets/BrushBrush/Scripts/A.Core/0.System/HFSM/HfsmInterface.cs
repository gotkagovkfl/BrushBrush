using Cysharp.Threading.Tasks;


/// <summary>
/// 게임 스테이트 페이로드 마킹
/// </summary>
public interface IState<TPayload>  : IState where TPayload : IStatePayload
{
    
}

/// <summary>
/// 상태머신 인터페이스
/// </summary>
public interface IState
{
    UniTask Enter(IStatePayload payload);
    UniTask Exit();
    void Update();
    void SetOuterMachine(StateMachine outer);
}

/// <summary>
/// 게임 스테이트 페이로드 마킹
/// </summary>
public interface IStatePayload
{
    
}

/// <summary>
/// 게임 스테이트 페이로드 마킹
/// </summary>
public interface IStatePayload<TState> : IStatePayload where TState : IState
{
    
}

/// <summary>
/// 파라미터가 없는 State에 사용하는 빈 타입
/// </summary>
public struct Empty : IStatePayload
{
    
}
