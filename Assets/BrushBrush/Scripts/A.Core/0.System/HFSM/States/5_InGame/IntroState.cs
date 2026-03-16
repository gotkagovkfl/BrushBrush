using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 전환될 씬의 파라미터
/// </summary>
public readonly struct IntroPayload : IStatePayload<IntroState>
{
    // 아직 뭐들어갈 지 모름
}


/// <summary>
/// 본 게임 시작 전 준비 단계 
/// </summary>
public class IntroState : CompositeState<IntroPayload>
{
    /// <summary> event.인트로 종료되어 게임 플레이 가능 </summary>
    public event Action OnFinishIntro;


    //=====================================================================
    protected override async UniTask Enter_Impl()
    {
        PlayIntroAsnyc().Forget();
    }

    protected override async UniTask Exit_Impl()
    {

    }

    protected override void Update_Impl()
    {

    }

    //=======================================================
    /// <summary>
    /// 인트로 - 시작 카운트 다운 및 시작 알림 - startNum 보다 1초 많이 연출한다. (마지막 start 문구 때문에)
    /// </summary>
    async UniTask PlayIntroAsnyc()
    {
        // 카운트 다운
        const int COUNT = 3;
        for (int i = COUNT; i >= 0; i--)
        {
            Debug.Log($"[ intro - count down] {i}");
            await UniTask.WaitForSeconds(1f);
        }

        // 시작 알림 연출
        Debug.Log($"[ intro - start ]");
        await UniTask.WaitForSeconds(1f);

        // 시작
        OnFinishIntro?.Invoke();
    }
}
