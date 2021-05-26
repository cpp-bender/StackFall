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
        ChangeMaterial();
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
        if (Input.GetMouseButtonDown(0) && !Utility.IgnoreUI() && playerData.State != State.dead)
        {
            playerData.CanHit = true;
            uiController.TapToPlayText.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerData.CanHit = false;
            uiController.ResetCircleSlider();
        }
    }

    private void CheckCurrentState()
    {
        if (playerData.State == State.dead)
        {
            uiController.OnGameOverUI();
            fireParticle.SetActive(false);
        }
        if (playerData.State == State.idle)
        {
            fireParticle.SetActive(false);
        }
        if (playerData.State == State.invincible)
        {
            fireParticle.SetActive(true);
        }
        if (playerData.State == State.win)
        {
            fireParticle.SetActive(false);
            levelSpawner.LoadNextLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!playerData.CanHit)
        {
            MoveUp();
            playerData.InvincibleTime = 0;
            cameraData.CanFollow = false;
        }
        else
        {
            var entity = collision.gameObject.GetComponent<IEntity>();
            if (entity.tag == CollisionTag.damageable && playerData.State != State.dead)
            {
                collision.transform.parent.GetComponent<ObstacleManager>().Shatter();
                levelData.ShatteredObstacleCount++;
                uiController.FillLevelSlider(levelData.ShatteredObstacleCount / (float)levelData.CurrentObstacleCount);
                playerData.InvincibleTime += Time.deltaTime * playerData.InvincibleTimeFactor;
                if (playerData.InvincibleTime >= 1)
                {
                    playerData.State = State.invincible;
                    SoundController.instance.PlayPlayerInvincibleMusic();
                }
                else
                {
                    SoundController.instance.PlayBreakStackMusic();
                }
                cameraData.CanFollow = true;
                GameManager.instance.AddScore();
            }
            else if (entity.tag == CollisionTag.nondamageable)
            {
                //Game ending
                playerData.InvincibleTime = 0;
                playerData.State = State.dead;
                SoundController.instance.PlayPlayerDeadMusic();
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
            uiController.FillCircleSlider(playerData.InvincibleTime);
        }
    }

    private void ChangeMaterial()
    {
        float r = Random.Range(20, 200) / (float)255;
        float g = Random.Range(50, 100) / (float)255;
        float b = Random.Range(30, 255) / (float)255;
        playerData.Material.color = new Color(r, g, b, 1);
    }
}
