using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour, IShapes
{
    [SerializeField] private new CollisionTag tag;
    [SerializeField] private float rotateSpeed = 100;

    CollisionTag IEntity.tag { get => tag; set => tag = value; }
    float IShapes.rotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }

    private void Update()
    {
        RotateAround();
    }

    public void RotateAround()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
