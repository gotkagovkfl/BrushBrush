using System;
using UnityEngine;

/// <summary>
/// entity의 행동을 결정
/// </summary>
public abstract class EntityBrain : IDisposable
{
    protected EntityBehaviorDecisionSystem _actionSys; // 여기다가 생성되는 intent 전달해야함.

    public EntityBrain(EntityBehaviorDecisionSystem actionSys)
    {
        _actionSys = actionSys;
    }

    public abstract void Tick(float dt);

    public abstract void Dispose(); // 뇌 갈아끼울 때 처리
}
