using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public EntityMono Mono { get; private set; }
    public EntityState State { get; private set; }
    public EntityStats Stats { get; private set; }
    Dictionary<Type, EntitySystem> _sysDic;

    /// <summary>
    /// 기본 엔티티 생성 : 사용할 뇌랑 오브젝트 지정해야함
    /// </summary>
    public Entity(EntityMono mono, EntityBrain brain)
    {
        Mono = mono;
        State = new();
        Stats = new();

        _sysDic = new();
        _sysDic[typeof(EntityBehaviorDecisionSystem)] = new EntityBehaviorDecisionSystem(this, brain);
        _sysDic[typeof(EntityStatusEffectSystem)] = new EntityStatusEffectSystem(this);
        _sysDic[typeof(EntityAbilitySystem)] = new EntityAbilitySystem(this);
        _sysDic[typeof(EntityLocomotionSystem)] = new EntityLocomotionSystem(this);
    }

    /// <summary>
    /// 엔티티 내부의 시스템 반환 - 외부에서 시스템 접근할 때 사용
    /// </summary>
    public T GetSys<T>() where T : EntitySystem
    {
        if (_sysDic.TryGetValue(typeof(T), out EntitySystem ret))
        {
            return ret as T;
        }

        return null;
    }
    //================================================================



}
