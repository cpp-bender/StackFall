using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image levelSlider;
    [SerializeField] private Image currentLevelImage;
    [SerializeField] private Image nextLevelImage;
    [SerializeField] private Transform player;
    [SerializeField] private Text scoreText;

    private Text currentLevelText;
    private Text nextLevelText;
    private Material playerMat;
    private int score;

    private void Start()
    {
        playerMat = player.GetChild(0).transform.GetComponent<MeshRenderer>().material;
        currentLevelImage.color = playerMat.color;
        nextLevelImage.color = playerMat.color;
        levelSlider.color = playerMat.color;
    }

    public void FillSlider(float amount)
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
}
