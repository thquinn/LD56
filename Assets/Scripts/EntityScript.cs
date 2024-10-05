using Assets.Code.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public abstract class EntityScript : MonoBehaviour { }

public abstract class EntityScript<T> : EntityScript where T : Entity {
    public abstract EntityScript<T> Init(T entity);
}
