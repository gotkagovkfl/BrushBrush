using UnityEngine;
using Cysharp.Threading.Tasks;

public class InGamePayload : IStatePayload<InGameState>
{

}

/// <summary>
/// 게임이 진행되는 상태
/// </summary>
public class InGameState : BaseGameState<InGamePayload>
{
    // sub-states
    IntroState _introState; // 게임 준비
    GamePlayState _gamePlayState;   // 게임 진행
    OutroState _outroState; // 게임 종료 후 

    //=============================================================
    protected override async UniTask Enter_Impl()
    {
        //
        _introState = new();
        _gamePlayState = new();
        _outroState = new();

        //
        _introState.OnFinishIntro += StartGamePlay;

        //
        Add(_introState);
        Add(_gamePlayState);
        Add(_outroState);

        // 초기 State와 페이로드 지정
        Initial<IntroState>(default);
    }

    protected override void StartState()
    {

    }

    protected override async UniTask Exit_Impl()
    {
        _introState.OnFinishIntro -= StartGamePlay;
    }


    protected override void Update_Impl()
    {

    }

    //========================================================================
    /// <summary>
    /// Intro 종료 후, 게임을 시작한다.
    /// </summary>
    void StartGamePlay()
    {
        GamePlayPayload payload = new();
        Request<GamePlayState, GamePlayPayload>(payload);
    }

    /// <summary>
    /// 게임 플레이를 종료한다.
    /// </summary>
    void FinishGamePlay()
    {
        OutroPayload payload = new();
        Request<OutroState, OutroPayload>(payload);
    }

    /// <summary>
    /// 로비화면으로 돌아간다
    /// </summary>
    void ReturnToLobby()
    {
        LobbyPayload payload = new();
        GameStateManager.Instance.SwitchMainStateTo<LobbyState>(payload);
    }
}
