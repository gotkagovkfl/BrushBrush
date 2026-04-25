using UnityEngine;
using Cysharp.Threading.Tasks;

public class TitlePayload : IStatePayload<TitleState>
{
    
}


public class TitleState: BaseGameState<TitlePayload>
{
    protected override async UniTask Enter_Impl()
    {

    }

    protected override void StartState_Impl()
    {
        Test().Forget();
    }

    protected override async UniTask Exit_Impl()
    {
        
    }

    protected override void Update_Impl()
    {
        
    }


    //=================================
    
    /// <summary>
    /// 테스트니까 이거하위 상태로 쪼개서 그 안에 넣어야함 (데이터로드 스테이트)
    /// </summary>
    async UniTask Test()
    {
        await BBData.InitStreamingData();
        GoLobby();
    }

    /// <summary>
    /// 현재 설정대로 게임을 플레이 한다.
    /// </summary>
    void GoLobby()
    {
        LobbyPayload payload = new();
        GameStateManager.Instance.SwitchMainStateTo<LobbyState>(payload);
    }
}
