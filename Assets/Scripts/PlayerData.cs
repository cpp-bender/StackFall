using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stack Fall/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private bool canHit;
    public float Speed { get => speed; }
    public bool CanHit { get => canHit; set => canHit = value; }
}
