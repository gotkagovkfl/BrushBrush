using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 캐릭터의 행동 결정 - 이동, 능력사용, 상호작용
/// </summary>
public class PlayerInputBrain : EntityBrain
{

    //=============================================================================
    public PlayerInputBrain(EntityBehaviorDecisionSystem actionSys) : base(actionSys)
    {

    }

    public override void Dispose()
    {

    }

    public override void Tick(float dt)
    {
        PushMoveIntent();   // 움직임 intent 생성
    }

    //=============================================================================

    /// <summary>
    /// 움직임 입력으로 생성
    /// </summary>
    void PushMoveIntent()
    {
        Vector2 moveInput = InputManager.Instance.LastMoveInputVector;
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        EntityMoveIntent moveIntent = new(moveDir);
        _actionSys.EnqueueIntent(moveIntent);
    }
}
