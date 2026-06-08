using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    EntityState _state;
    Dictionary<Type, IEntitySystem> _sysDic;

    /// <summary>
    /// 기본 엔티티 생성 : 사용할 뇌 지정해야함
    /// </summary>
    public Entity(EntityBrain brain)
    {
        _state = new();

        _sysDic = new();
        _sysDic[typeof(EntityBehaviorDecisionSystem)] = new EntityBehaviorDecisionSystem(brain);
        _sysDic[typeof(EntityStatusEffectSystem)] = new EntityStatusEffectSystem();
        _sysDic[typeof(EntityAbilitySystem)] = new EntityAbilitySystem();
        _sysDic[typeof(EntityLocomotionSystem)] = new EntityLocomotionSystem();
    }

    /// <summary>
    /// 엔티티 내부의 시스템 반환 - 외부에서 시스템 접근할 때 사용
    /// </summary>
    public IEntitySystem GetSys<T>() where T:IEntitySystem
    {
        if (_sysDic.TryGetValue(typeof(T), out var ret))
        {
            return ret;
        }
    
        return null; 
    }
    //================================================================
    


}
