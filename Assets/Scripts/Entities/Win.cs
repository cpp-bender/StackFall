using UnityEngine;

public class Win : MonoBehaviour, IShapes
{
    [SerializeField] private new CollisionTag tag;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private float rotateSpeed = 100;

    float IShapes.rotateSpeed { get => rotateSpeed; set => rotateSpeed = value; }
    CollisionTag IEntity.tag { get => tag; set => tag = value; }

    private void Update()
    {
        RotateAround();
    }

    public void RotateAround()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var entity = collision.gameObject.GetComponent<IEntity>();
        if (entity.tag == CollisionTag.player)
        {
            Debug.Log("You Win!");
            playerData.State = State.win;
        }
    }
}
