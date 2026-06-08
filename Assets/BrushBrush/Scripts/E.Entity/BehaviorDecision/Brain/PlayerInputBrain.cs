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
    }


}
