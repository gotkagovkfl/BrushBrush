using UnityEngine;
using Cysharp.Threading.Tasks;

public struct LobbyPayload : IStatePayload<LobbyState>
{

}


public class LobbyState : BaseGameState<LobbyPayload>
{
    protected override async UniTask Enter_Impl()
    {

    }

    protected override void StartState_Impl()
    {
        StartGame();
    }


    protected override async UniTask Exit_Impl()
    {

    }

    protected override void Update_Impl()
    {

    }

    //=========================================================
    /// <summary>
    /// 현재 설정대로 게임을 플레이 한다.
    /// </summary>
    void StartGame()
    {
        InGamePayload payload = CreateInGamePayload();
        GameStateManager.Instance.SwitchMainStateTo<InGameState>(payload);
    }
    
    /// <summary>
    /// 현재 세팅에 맞춰 인게임 페이로드 생성
    /// </summary>
    InGamePayload CreateInGamePayload()
    {
        return new InGamePayload
        { 
            StageId = "Test",
            CharacterId = "Test",
        };
    }
}