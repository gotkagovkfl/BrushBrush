using Cysharp.Threading.Tasks;
using UnityEngine;

public class PreloadPayload : IStatePayload<PreloadState>
{

}

/// <summary>
/// 게임 실행 첫씬. 게임 실행에 필요한 자원들을 초기화 하는 역할.
/// </summary>
public class PreloadState : BaseGameState<PreloadPayload>
{
    protected override async UniTask Enter_Impl()
    {

    }

    protected override void StartState()
    {
        PreloadGameAsync().Forget();
    }


    protected override async UniTask Exit_Impl()
    {

    }

    protected override void Update_Impl()
    {

    }

    //==========================================================
    /// <summary>
    /// 게임 진행에 필요한 것들 생성 요청
    /// </summary>
    async UniTask PreloadGameAsync()
    {
        await UniTask.WaitForSeconds(1f); // 일단 하는거 없으니까 1초 대기

        TitlePayload payload = new();
        GameStateManager.Instance.SwitchMainStateTo<TitleState>(payload);
    }
}

