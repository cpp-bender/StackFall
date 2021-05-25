using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Stack Fall/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private bool canHit;
    [SerializeField] private float invincibleTime;
    [SerializeField] private float invincibleTimeFactor = 2;
    [SerializeField] private State state;
    [SerializeField] private Material material;
    public float Speed { get => speed; }
    public bool CanHit { get => canHit; set => canHit = value; }
    public float InvincibleTimeFactor => invincibleTimeFactor;
    public float InvincibleTime { get => invincibleTime; set => invincibleTime = value; }
    public State State { get => state; set => state = value; }
    public Material Material { get => material; set => material = value; }
}
public enum State
{
    idle, invincible, dead, win
}
