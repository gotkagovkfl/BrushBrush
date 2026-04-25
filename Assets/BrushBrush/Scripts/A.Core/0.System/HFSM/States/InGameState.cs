using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// 게임에 사용되는 정보 : 유저 정보, 캐릭터 정보, 스테이지 정보, 난이도 등
/// </summary>
public struct InGamePayload : IStatePayload<InGameState>
{
    public string CharacterId;  // 플레이어 캐릭터
    public string StageId;  // 스테이지
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

    // events
    public event Action OnRequestReturnLobby;   // 로비로 돌아가기 요청
    public event Action OnRequestReplay;        // 다시하기 요청

    //
    StageManager _stageManager;


    //=============================================================
    protected override async UniTask Enter_Impl()
    {
        //
        await InitStageAsync();
        await InitPlayerAsync();

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

    protected override void StartState_Impl()
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
        OnRequestReturnLobby?.Invoke();
    }

    //=======================================================================
    /// <summary>
    ///  스테이지를 생성 및 초기화한다.
    /// </summary>
    async UniTask InitStageAsync()
    {
        // 스테이지 매니저 초기화
        string stageId = Payload.StageId;
        StageData stageData = BBData.Get<StageData>(stageId);
        _stageManager = new(stageData);

        //
        await _stageManager.GenerateStageAsync();
    }

    /// <summary>
    /// 플레이어 캐릭터를 생성 및 초기화한다.
    /// </summary>
    async UniTask InitPlayerAsync()
    {
        // 스테이지 매니저 초기화
        string charId = Payload.CharacterId;
    }
}
