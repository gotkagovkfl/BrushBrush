using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Cysharp.Threading.Tasks;


/// <summary>
/// 
/// </summary>
public class EntityManager : MonoSingleton<EntityManager>
{
    private List<Entity> _activeEntities = new();

    protected override async UniTask InitImpl()
    {
        
    }

    /// <summary>
    /// 매니저가 순서에 맞춰 엔티티들의 시스템 마다 일괄적으로 한 프레임 진행시킨다.
    /// </summary>
    private void Update()
    {
        float dt = Time.deltaTime;

        UpdateEntitySystem<EntityStatusEffectSystem>(dt);       // 1. 상태이상 업데이트
        UpdateEntitySystem<EntityBehaviorDecisionSystem>(dt);             // 2. 행동 결정
        UpdateEntitySystem<EntityAbilitySystem>(dt);            // 3. 능력 사용
        UpdateEntitySystem<EntityLocomotionSystem>(dt);         // 4. 운동 상태 적용
        UpdateEntitySystem<EntityTransformModifiySystem>(dt);   // 5. 이동, 회전, 스케일 적용
    }

    /// <summary>
    /// 활성화된 엔티티 목록을 순회하며 시스템을 한프레임 진행시킨다.
    /// </summary>
    void UpdateEntitySystem<T>(float dt) where T:IEntitySystem
    {
        for(int i = 0; i < _activeEntities.Count; i++)
        {
            _activeEntities[i].GetSys<T>().Tick(dt);
        }
    }
}
