using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image levelSlider;
    [SerializeField] private Image currentLevelImage;
    [SerializeField] private Image nextLevelImage;
    [SerializeField] private Text scoreText;
    [SerializeField] private Image circleSlider;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image settingsImage;
    [SerializeField] private Button volumeButton;
    [SerializeField] private Text tapToPlayText;
    [SerializeField] private Image gamePanel;
    [SerializeField] private Image gameOverPanel;

    private Text currentLevelText;
    private Text nextLevelText;
    private int score;

    public Text TapToPlayText { get => tapToPlayText; set => tapToPlayText = value; }

    private void Start()
    {
        currentLevelImage.color = playerData.Material.color;
        nextLevelImage.color = playerData.Material.color;
        levelSlider.color = playerData.Material.color;
        circleSlider.color = playerData.Material.color;
    }

    public void FillCircleSlider(float amount)
    {
        circleSlider.color = playerData.Material.color;
        circleSlider.gameObject.SetActive(true);
        circleSlider.fillAmount = amount;
    }

    public void ResetCircleSlider()
    {
        circleSlider.fillAmount = 0f;
    }

    public void FillLevelSlider(float amount)
    {
        levelSlider.fillAmount = amount;
    }

    public void ChangeLevelText(int currentLevel)
    {
        currentLevelText = currentLevelImage.GetComponentInChildren<Text>();
        nextLevelText = nextLevelImage.GetComponentInChildren<Text>();
        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();
    }

    public void ChangeScore()
    {
        int point = Random.Range(20, 50);
        score += point;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void OnSettingButtonClicked()
    {
        SoundController.instance.PlayClickButtonMusic();
        volumeButton.gameObject.SetActive(!volumeButton.gameObject.activeInHierarchy);
    }

    public void OnSoundButtonClicked()
    {
        if (volumeButton.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            Debug.Log("Turned off");
            SoundController.instance.TurnOffSounds();
            volumeButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Turned on");
            SoundController.instance.TurnOnSounds();
            volumeButton.transform.GetChild(1).gameObject.SetActive(false);
        }
        volumeButton.transform.GetChild(0).gameObject.SetActive(!volumeButton.transform.GetChild(1).gameObject.activeInHierarchy);
    }

    public void OnGameOverUI()
    {
        gamePanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = $"Score:{score}";
    }
}
