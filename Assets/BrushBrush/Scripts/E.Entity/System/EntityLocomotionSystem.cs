using System;
using UnityEngine;

/// <summary>
/// 자의에 의한 움직임 적용
/// </summary>
public class EntityLocomotionSystem : EntitySystem
{
    EntityMoveIntent _registeredMoveIntent;
    public Vector3 LastMoveDir { get; private set; }    // 전 프레임에 이동한 방향 ( 스킬 이동이 우선이기 때문에 실제로 이동한 방향과 맞지 않을 수도 있음. )


    //================================================================
    public EntityLocomotionSystem(Entity entity) : base(entity)
    {

    }

    public override void Tick(float dt)
    {
        // 움직임 제어가 가능한 경우
        if (_entity.State.CanControlMovement)
        {
            ConsumeCurrMoveIntent(dt); // 현재 지정된 moveIntent 실행
        }
        else
        {
            LastMoveDir = Vector3.zero;
        }
    }

    //================================================================
    /// <summary>
    /// 현재 프레임에 처리할 의도를 세팅한다. - Behavior Decision Sys 에 의해 세팅됨.
    /// </summary>
    public void RegisterMoveIntent(EntityMoveIntent moveIntent)
    {
        _registeredMoveIntent = moveIntent;
    }

    /// <summary>
    /// 현재 설정된 MoveIntent을 행동으로 실행한다.
    /// </summary>
    void ConsumeCurrMoveIntent(float dt)
    {
        // 이미 사용된 intent 는 실행하지 않음.
        if (_registeredMoveIntent.IsConsumed)
        {
            LastMoveDir = Vector3.zero;
            return;
        }

        // intent를 보고 각 시스템에 전달
        _registeredMoveIntent.Comsume(); // intent 사용처리
        LastMoveDir = _registeredMoveIntent.MoveDir;    // 마지막 이동 방향 설정
        float movementSpeed = _entity.Stats.FinalMovementSpeed;
        Vector3 diff = LastMoveDir * movementSpeed * dt;

        // 이동 수정자 생성해서 시스템에 등록 (오브젝트 위치 실제로 변경)
        Vector3 origin = _entity.Mono.GetCurrPos();
        Vector3 dest = origin + diff;
        const int PRIORITY = 1;
        EntityPositionModifier positionModifier = new(origin, dest, PRIORITY);
        _entity.GetSys<EntityTransformModifiySystem>().RegisterPositionModifier(positionModifier);

        // TODO. 그 외 상태 및 애니메이션 설정 등
    }
}
