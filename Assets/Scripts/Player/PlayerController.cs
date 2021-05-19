using UnityEngine;

public class PlayerController : MonoBehaviour, IEntity
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CameraData cameraData;
    [SerializeField] private LevelData levelData;
    [SerializeField] private new CollisionTag tag;

    private Rigidbody body;
    CollisionTag IEntity.tag { get => tag; set => tag = value; }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, levelData.ObstacleCount / 2 + .5f, -1.5f);
        playerData.CanHit = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerData.CanHit = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerData.CanHit = false;
        }
    }

    private void FixedUpdate()
    {
        if (playerData.CanHit)
        {
            MoveDown();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
            cameraData.CanFollow = false;
        }
        else
        {
            var entity = collision.gameObject.GetComponent<IEntity>();
            if (entity.tag == CollisionTag.damageable)
            {
                Destroy(collision.transform.parent.gameObject);
                cameraData.CanFollow = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        body.velocity = Vector3.up * playerData.Speed * Time.deltaTime;
    }

    private void MoveDown()
    {
        body.velocity = Vector3.down * playerData.Speed * Time.fixedDeltaTime;
    }
}
