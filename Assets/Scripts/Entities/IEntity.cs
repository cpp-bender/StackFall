using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    Tag tag { get; set; }
}

public enum Tag
{
    player, win, damageable, nondamageable
}
