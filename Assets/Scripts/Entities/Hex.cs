using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour,IShapes
{
    [SerializeField] private new CollisionTag tag;
    [SerializeField] private float rotateSpeed = 100;

    float IShapes.rotateSpeed { get => rotateSpeed; set => rotateSpeed=value; }
    CollisionTag IEntity.tag { get => tag; set => tag = value; }

    private void Update()
    {
        RotateAround();
    }

    public void RotateAround()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
