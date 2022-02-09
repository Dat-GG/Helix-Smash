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
    public enum UIState
    {
        Prepare,
        Play,
        Dead,
        Finish
    }
    public UIState _state = UIState.Prepare;
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
        switch (_state)
        {
            case UIState.Prepare:
                if (player._state == PlayerController.PlayerState.Play)
                {
                    ChangeState(UIState.Play);
                    Home.SetActive(false);
                    inGame.SetActive(true);
                    finish.SetActive(false);
                    gameOver.SetActive(false);
                }
                break;
            case UIState.Play:
                if (player._state == PlayerController.PlayerState.Dead)
                {
                    ChangeState(UIState.Dead);
                }
                else if (player._state == PlayerController.PlayerState.Finish)
                {
                    ChangeState(UIState.Finish);
                }
                break;
            case UIState.Dead:

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
                break;
            case UIState.Finish:                
                    Home.SetActive(false);
                    inGame.SetActive(false);
                    finish.SetActive(true);
                    gameOver.SetActive(false);
                    finishLevelText.text = "Level " + FindObjectOfType<Levelling>().level;                
                break;
        }
    }
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
    private void ChangeState(UIState newstate)
    {
        if (newstate == _state) return;
        ExitCurrentState();
        _state = newstate;
        EnterNewState();
    }
    private void EnterNewState()
    {
        switch (_state)
        {
            case UIState.Prepare:
                break;
            case UIState.Play:
                break;
            case UIState.Dead:
                break;
            case UIState.Finish:
                break;
        }
    }
    private void ExitCurrentState()
    {
        switch (_state)
        {
            case UIState.Prepare:
                break;
            case UIState.Play:
                break;
            case UIState.Dead:
                break;
            case UIState.Finish:
                break;
        }
    }
}
