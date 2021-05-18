using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShapes : IEntity
{
    float rotateSpeed { get; set; }
    void RotateAround();
}
