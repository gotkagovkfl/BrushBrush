using UnityEngine;

public struct EntityMoveIntent : IEntityIntent
{
    public Vector3 MoveDir;

    public EntityMoveIntent(Vector3 moveDir)
    {
        MoveDir = moveDir;
    }


    public void Process()
    public void Proceed(Entity entity)
    {
        
    }
}
