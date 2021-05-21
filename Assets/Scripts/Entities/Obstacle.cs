using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IShapes
{
    [SerializeField] private new CollisionTag tag;
    [SerializeField] private float rotateSpeed = 100;

    private Rigidbody body;
    private MeshRenderer renderer;
    private Collider collider;

    float IShapes.rotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }
    CollisionTag IEntity.tag { get => tag; set => tag = value; }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        RotateAround();
    }

    public void RotateAround()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    public void PlayShatterAnimation()
    {
        body.isKinematic = false;
        collider.enabled = false;
        Vector3 forcePoint = transform.parent.position;
        float parentPosX = forcePoint.x;
        float localX = renderer.bounds.center.x;

        Vector3 dir = parentPosX - localX < 0 ? Vector3.right : Vector3.left;
        dir += Vector3.up * 1.5f;
        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);
        body.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        body.AddTorque(Vector3.left * torque);
        Destroy(transform.parent.gameObject, 1);
    }
}
