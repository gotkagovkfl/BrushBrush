using System.Collections.Generic;
using UnityEngine;

public class EntityTransformModifiySystem : IEntitySystem
public class EntityTransformModifiySystem : EntitySystem
{
    public void Tick(float dt)
    public EntityTransformModifiySystem(Entity entity) : base(entity)
    {

    }
    public override void Tick(float dt)
    {
    }
    {
        
    }
}
