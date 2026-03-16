using UnityEngine;
using Cysharp.Threading.Tasks;


/// <summary>
/// 전환될 씬의 파라미터
/// </summary>
public readonly struct GamePlayPayload : IStatePayload<GamePlayState>
{
    // 아직 뭐들어갈 지 모름
}

/// <summary>
/// 게임 플레이 하는 상태 - 스테이지 진행 / 
/// </summary>
public class GamePlayState : CompositeState<GamePlayPayload>
{
    protected override async UniTask Enter_Impl()
    {
        
    }

    protected override async UniTask Exit_Impl()
    {
        
    }

    protected override void Update_Impl()
    {
        
    }
}