using Assets.Code.Model;
using UnityEngine;

public abstract class EntityScript : MonoBehaviour { }

public abstract class EntityScript<T> : EntityScript where T : Entity {
    public abstract EntityScript<T> Init(T entity);
}
