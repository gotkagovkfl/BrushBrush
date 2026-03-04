using UnityEngine;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

// 대부분의 일반 상태 (Preload, Lobby, InGame, Title 등)
public abstract class BaseGameState<TPayload> : CompositeState<TPayload> where TPayload : IStatePayload
{
    public override async UniTask Enter(IStatePayload payload)
    {
        // 필요 ui 생성
        await base.Enter(payload);

        // ui 생성 후에 화면 보이게 하기 (페이드 아웃)
        await FadeOutAsync();   // 이거 때문에 enter에서 씬전환하면 페이드가 겹쳐서 이상해진다. 
        StartState();  // 페이드아웃 끝나고 시작할 로직
    }

    public override async UniTask Exit()
    {
        // 우선 화면 가리기 (페이드 인)
        await FadeInAsync();

        // UI 끄기, 정리 등
        await base.Exit();
    }

    /// <summary>
    /// 연출끝난 후 본격적으로 상태시작 로직
    /// </summary>
    protected abstract void StartState();

    async UniTask FadeInAsync()
    {
        await UIManager.Instance.FadeInAsync();
    }

    async UniTask FadeOutAsync()
    {
        await UIManager.Instance.FadeOutAsync();
    }


}
