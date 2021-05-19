public interface IEntity
{
    CollisionTag tag { get; set; }
}

public enum CollisionTag
{
    player, win, damageable, nondamageable
}
