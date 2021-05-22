using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "Stack Fall/Camera/Camera Data")]
public class CameraData : ScriptableObject
{
    [SerializeField] private Vector3 camOffset;
    [SerializeField] private float camAngle;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private bool canFollow;
    public Vector3 CamOffset { get => camOffset; }
    public float CamAngle { get => camAngle; }
    public float CameraFollowSpeed { get => cameraFollowSpeed; }
    public bool CanFollow { get => canFollow; set => canFollow = value; }
}
