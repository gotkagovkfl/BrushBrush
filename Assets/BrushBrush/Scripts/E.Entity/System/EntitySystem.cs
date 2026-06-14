using UnityEngine;

public abstract class EntitySystem
{
    protected Entity _entity;
    public EntitySystem(Entity entity)
    {
        _entity = entity;
    }

    public abstract void Tick(float dt);
}
