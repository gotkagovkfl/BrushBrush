using UnityEngine;
using Cysharp.Threading.Tasks;


/// <summary>
/// 전환될 씬의 파라미터
/// </summary>
public readonly struct OutroPayload : IStatePayload<OutroState>
{
    // 아직 뭐들어갈 지 모름
}

/// <summary>
/// 게임 종료후 연출 및 결과창 
/// </summary>
public class OutroState : CompositeState<OutroPayload>
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
