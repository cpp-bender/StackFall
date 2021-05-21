using UnityEngine;

public class PlayerController : MonoBehaviour, IEntity
{
    enum State
    {
        playing, invincible, dead
    }

    [SerializeField] private PlayerData playerData;
    [SerializeField] private CameraData cameraData;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject fireParticle;
    [SerializeField] private new CollisionTag tag;

    private Rigidbody body;
    private Vector3 startPos;
    private State state;
    private float invincibleTime;
    private const float invincibleTimeFactor = 8;


    private float jumpVelocity;
    CollisionTag IEntity.tag { get => tag; set => tag = value; }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startPos = new Vector3(transform.position.x, levelData.ObstacleCount / 2 + .5f, -1.5f);
        transform.position = startPos;
        state = State.playing;
        fireParticle.SetActive(false);
    }

    private void Update()
    {
        CheckCurrentState();
        CheckInput();
    }

    private void CheckInput()
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

    private void CheckCurrentState()
    {
        if (state == State.invincible)
        {
            fireParticle.SetActive(true);
        }
        if (state == State.playing)
        {
            fireParticle.SetActive(false);
        }
        if (state == State.dead)
        {
            fireParticle.SetActive(false);
            Debug.Log("Game Over!");
        }
    }

    private void FixedUpdate()
    {
        MoveDown();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
            invincibleTime = 0;
            state = State.playing;
            cameraData.CanFollow = false;
        }
        else
        {
            var entity = collision.gameObject.GetComponent<IEntity>();
            if (entity.tag == CollisionTag.damageable)
            {
                collision.transform.parent.GetComponent<ObstacleManager>().Shatter();
                invincibleTime += Time.deltaTime * invincibleTimeFactor;
                if (invincibleTime >= 1)
                {
                    state = State.invincible;
                }
                cameraData.CanFollow = true;
            }
            else if (entity.tag == CollisionTag.nondamageable)
            {
                //Game ending
                invincibleTime = 0;
                state = State.dead;
                Debug.Log("Game Over!");
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
        if (playerData.CanHit)
        {

            body.velocity += Vector3.down * playerData.Speed * Time.fixedDeltaTime;
        }
    }
}
