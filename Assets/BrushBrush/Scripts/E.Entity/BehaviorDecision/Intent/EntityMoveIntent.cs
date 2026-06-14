using UnityEngine;

public struct EntityMoveIntent : IEntityIntent
{
    public Vector3 MoveDir { get; private set; }
    public bool IsConsumed { get; private set; } // 시스템에 의해 사용되었는지,

    public EntityMoveIntent(Vector3 moveDir)
    {
        MoveDir = moveDir;
        IsConsumed = false;
    }


    public void Proceed(Entity entity)
    {
        entity.GetSys<EntityLocomotionSystem>().RegisterMoveIntent(this);
    }

    public void Comsume()
    {
        IsConsumed = true;
    }
}
