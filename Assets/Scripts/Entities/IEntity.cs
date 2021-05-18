using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    CollisionTag tag { get; set; }
}

public enum CollisionTag
{
    player, win, damageable, nondamageable
}
