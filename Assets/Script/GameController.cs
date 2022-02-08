using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image levelSlider, levelSliderFill;
    public Image currentLevel;
    public Image nextLevel;
    public GameObject Home, inGame, finish, gameOver;
    private Material playerMaterial;
    public Text currentLevelText, nextLevelText, finishLevelText, gameOverScoreText, gameOverBestText;
    private PlayerController player;
    void Awake()
    {
        playerMaterial = FindObjectOfType<PlayerController>().GetComponent<MeshRenderer>().material;
        player = FindObjectOfType<PlayerController>();
        levelSlider.color = playerMaterial.color;
        levelSliderFill.color = playerMaterial.color + Color.gray;
        nextLevel.color = playerMaterial.color;
        currentLevel.color = playerMaterial.color;
    }

    void Start()
    {
        currentLevelText.text = FindObjectOfType<Levelling>().level.ToString();
        nextLevelText.text = FindObjectOfType<Levelling>().level + 1 + "";
    }

    void Update()
    {
        UIManagement();
    }

    private void UIManagement()
    {
        if (Input.GetMouseButtonDown(0) && player.playerState == PlayerController.PlayerState.Prepare)
            player.playerState = PlayerController.PlayerState.Play;
        {

            Home.SetActive(false);
            inGame.SetActive(true);
            finish.SetActive(false);
            gameOver.SetActive(false);
        }

        if (player.playerState == PlayerController.PlayerState.Finish)
        {
            Home.SetActive(false);
            inGame.SetActive(false);
            finish.SetActive(true);
            gameOver.SetActive(false);

            finishLevelText.text = "Level " + FindObjectOfType<Levelling>().level;
        }

        if (player.playerState == PlayerController.PlayerState.Dead)
        {
            Home.SetActive(false);
            inGame.SetActive(false);
            finish.SetActive(false);
            gameOver.SetActive(true);

            gameOverScoreText.text = ScoringController.instance.score.ToString();
            gameOverBestText.text = PlayerPrefs.GetInt("Highscore").ToString();

            if (Input.GetMouseButtonDown(0))
            {
                ScoringController.instance.ResetScore();
                SceneManager.LoadScene(0);
            }
        }
    }
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }


}
