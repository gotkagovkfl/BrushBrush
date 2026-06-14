using System.Collections.Generic;
using UnityEngine;

public class EntityTransformModifiySystem : EntitySystem
{
    List<EntityPositionModifier> _pendingPosModifiers = new(); // 이번 프레임에 입력된 위치 수정 후보

    public EntityTransformModifiySystem(Entity entity) : base(entity)
    {

    }

    public override void Tick(float dt)
    {
        // 이동 수정.
        EntityPositionModifier selectedPositionModifier = ResolvePoisitionModifyAction(); // 우선순위 높은 수정자 하나 선택
        ExcecutePositionModifier(selectedPositionModifier);
    }

    //===========================================================
    /// <summary>
    /// 이동 수정 후보에 등록한다 
    /// </summary>
    public void RegisterPositionModifier(EntityPositionModifier entityPositionModifyAction)
    {
        _pendingPosModifiers.Add(entityPositionModifyAction);
    }

    /// <summary>
    /// 현재 등록된 이동 수정 목록들 중 실행할 하나를 선택한다.
    /// </summary>
    EntityPositionModifier ResolvePoisitionModifyAction()
    {
        // 입력이 없는 경우,
        if (_pendingPosModifiers.Count == 0)
        {
            return EntityPositionModifier.None; // IsValid 가 false 인 struct
        }

        // 우선순위가 가장 높은 한가지를 선택한다. - 추후 modifier 가 합쳐질 수도 있음.
        EntityPositionModifier ret = _pendingPosModifiers[0];
        for (int i = 1; i < _pendingPosModifiers.Count; i++)
        {
            if (_pendingPosModifiers[i].Priority > ret.Priority)
            {
                ret = _pendingPosModifiers[i];
            }
        }

        // 그리고 리스트 비우기.
        _pendingPosModifiers.Clear();
        return ret;
    }

    /// <summary>
    /// 선택된 이동수정자를 실행한다.
    /// </summary>
    void ExcecutePositionModifier(EntityPositionModifier positionModifier)
    {
        // 우효하지 않은 경우( 이번 프레임에 이동 변화가 없는 경우 ) 리턴
        if (positionModifier.IsValid == false)
        {
            return;
        }

        //
        _entity.Mono.SetPos(positionModifier.Dest);
    }
}
