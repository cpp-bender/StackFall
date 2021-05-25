using UnityEngine;

public class PlayerController : MonoBehaviour, IEntity
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private CameraData cameraData;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject fireParticle;
    [SerializeField] private LevelSpawner levelSpawner;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private UIController uiController;
    [SerializeField] private new CollisionTag tag;

    private Rigidbody body;
    CollisionTag IEntity.tag { get => tag; set => tag = value; }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, levelData.CurrentObstacleCount / 2 + .5f, -1.5f);
        playerData.CanHit = false;
        cameraController.SetCamPosition(transform.position);
        playerData.State = State.idle;
        levelData.ShatteredObstacleCount = 0;
        GameManager.instance.ResetScore();
    }

    private void ChangeMaterial()
    {
        //TODO: Change player material each level
    }

    private void Update()
    {
        CheckCurrentState();
        CheckInput();
    }

    private void FixedUpdate()
    {
        MoveDown();
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
            playerData.State = State.idle;
        }
    }

    private void CheckCurrentState()
    {
        if (playerData.State == State.idle)
        {
            fireParticle.SetActive(false);
        }
        if (playerData.State == State.invincible)
        {
            fireParticle.SetActive(true);
            SoundController.instance.PlayPlayerInvincibleMusic();
        }
        if (playerData.State == State.dead)
        {
            fireParticle.SetActive(false);
            Debug.Log("Game Over!");
        }
        if (playerData.State == State.win)
        {
            fireParticle.SetActive(false);
            levelSpawner.NextLevel();
        }
        if (playerData.State == State.dead)
        {
            fireParticle.SetActive(false);
            Debug.Log("Game Over!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
            playerData.InvincibleTime = 0;
            playerData.State = State.idle;
            cameraData.CanFollow = false;
        }
        else
        {
            var entity = collision.gameObject.GetComponent<IEntity>();
            if (entity.tag == CollisionTag.damageable)
            {
                collision.transform.parent.GetComponent<ObstacleManager>().Shatter();
                levelData.ShatteredObstacleCount++;
                uiController.FillSlider(levelData.ShatteredObstacleCount / (float)levelData.CurrentObstacleCount);
                SoundController.instance.PlayBreakStackMusic();
                playerData.InvincibleTime += Time.deltaTime * playerData.InvincibleTimeFactor;
                if (playerData.InvincibleTime >= 1)
                {
                    playerData.State = State.invincible;
                }
                cameraData.CanFollow = true;
                GameManager.instance.AddScore();
            }
            else if (entity.tag == CollisionTag.nondamageable)
            {
                //Game ending
                playerData.InvincibleTime = 0;
                playerData.State = State.dead;
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
        SoundController.instance.PlayJumpMusic();
    }

    private void MoveDown()
    {
        if (playerData.CanHit)
        {
            body.velocity = Vector3.down * playerData.Speed * Time.fixedDeltaTime;
        }
    }
}
