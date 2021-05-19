using UnityEngine;

public class Win : MonoBehaviour, IEntity
{
    [SerializeField] private new CollisionTag tag;
    CollisionTag IEntity.tag { get => tag; set => tag = value; }
}
