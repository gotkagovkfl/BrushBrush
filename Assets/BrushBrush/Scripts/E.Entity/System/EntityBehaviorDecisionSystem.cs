using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 엔티티의 의사결정 시스템 (플레이어 조종 및 몬스터 ai)
/// </summary>
public class EntityBehaviorDecisionSystem : IEntitySystem
{
    EntityBrain _brain;
    Queue<IEntityIntent> _currFrameIntents;

    public EntityBehaviorDecisionSystem(EntityBrain brain)
    {
        _brain = brain;
        _currFrameIntents = new();
    }


    public void Tick(float dt)
    {
        _brain.Tick(dt);    // 1. 이번 프레임 intent 수집
        ProcessIntents();   // 2. 수집한 intent 실행
    }

    /// <summary>
    /// 이번 프레임에 하고자하는 행동을 추가한다. - brain에서 사용
    /// </summary>
    public void EnqueueIntent(IEntityIntent intent)
    {
        _currFrameIntents.Enqueue(intent);
    }
    
    /// <summary>
    /// 수집한 intent들을 전부 실행한다.
    /// </summary>
    void ProcessIntents()
    {
        while (_currFrameIntents.Count > 0)
        {
            IEntityIntent intent = _currFrameIntents.Dequeue();
            intent.Process();
        }
    }
}
