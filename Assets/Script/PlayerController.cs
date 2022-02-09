using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float overpowerBuildUp;
    [SerializeField] private bool isClicked, isOverPowered;
    [SerializeField] private float moveSpeed = 500f;
    private float speedLimit = 30f;
    [SerializeField] private float bounceSpeed = 480f;
    public GameObject overpowerBar;
    public Image overpowerFill;
    public GameObject fireEffect;
    public GameObject splashEffect;
    public int count;

    public enum PlayerState
    {
        Prepare,
        Play,
        Dead,
        Finish
    }
    public PlayerState _state = PlayerState.Prepare;
    //public PlayerState playerState = PlayerState.Prepare;
    private int currentBrokenCircuts, totalCircuts;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBrokenCircuts = 0;
    }

    void Start()
    {
        totalCircuts = FindObjectsOfType<CircutController>().Length;
        count = 0;
    }

    void Update()
    {
        //if (playerState == PlayerState.Play)
        //{
        //    ClickCheck();
        //    OverpowerCheck();
        //}

        if (_state == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<Levelling>().IncreaseLevel();
            }
        }
        switch (_state)
        {
            case PlayerState.Prepare:
                break;
            case PlayerState.Play:
                ClickCheck();
                OverpowerCheck();               
                break;
            case PlayerState.Dead:
                break;
            case PlayerState.Finish:
                break;

        }
    }
    void FixedUpdate()
    {
        BallMovement();
    }

    private void BallMovement()
    {
        //if (playerState == PlayerState.Play)
        //{
            if (Input.GetMouseButton(0) && isClicked == true)
            {
                rb.velocity = new Vector3(0, -moveSpeed * Time.smoothDeltaTime, 0);
            }
        //}

        if (rb.velocity.y > speedLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, speedLimit, rb.velocity.z);
        }
    }

    public void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    public void PlusScore()
    {
        currentBrokenCircuts++;
        if (!isOverPowered)
        {
            ScoringController.instance.Scoring(1);
        }
        else
        {
            ScoringController.instance.Scoring(2);
        }
    }
    void OnCollisionEnter(Collision target)
    {
        if (!isClicked)
        {
            rb.velocity = new Vector3(0, bounceSpeed * Time.smoothDeltaTime, 0);
            Physics.gravity = new Vector3(0, -50, 0);
            _state = PlayerState.Play;
        }
        else
        {
            if (isOverPowered)
            {
                if (target.gameObject.tag == "GoodPart" || target.gameObject.tag == "BadPart")
                {
                    target.transform.parent.GetComponent<CircutController>().BreakAllCircuts();
                }
            }
            else
            {
                if (target.gameObject.tag == "GoodPart")
                {
                    target.transform.parent.GetComponent<CircutController>().BreakAllCircuts();
                    if (count == 0)
                    {
                        count++;
                    }
                }
                if (target.gameObject.tag == "BadPart")
                {
                    if (count == 1)
                    {
                        count--;
                    }
                    else
                    {
                        rb.isKinematic = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                        ChangeState(PlayerState.Dead);
                    }
                }
            }
        }
         FindObjectOfType<GameController>().LevelSliderFill(currentBrokenCircuts / (float)totalCircuts);

        if (target.gameObject.CompareTag("WinLocation") && _state == PlayerState.Play)
               ChangeState(PlayerState.Finish);
        
    }
    void OverpowerCheck()
    {
        if (isOverPowered)
        {
            overpowerBuildUp -= Time.deltaTime * .5f;
            if (!fireEffect.activeInHierarchy)
                fireEffect.SetActive(true);
        }
        else
        {
            if (fireEffect.activeInHierarchy)
               fireEffect.SetActive(false);
            if (isClicked)
                overpowerBuildUp += Time.deltaTime * .8f;
            else
                overpowerBuildUp -= Time.deltaTime * .5f;
        }

        if (overpowerBuildUp >= 0.3f || overpowerFill.color == Color.red)
            overpowerBar.SetActive(true);
        else
            overpowerBar.SetActive(false);

        if (overpowerBuildUp >= 1)
        {
            overpowerBuildUp = 1;
            isOverPowered = true;
            overpowerFill.color = Color.red;
        }
        else if (overpowerBuildUp <= 0)
        {
            overpowerBuildUp = 0;
            isOverPowered = false;
            overpowerFill.color = Color.white;
        }

        if (overpowerBar.activeInHierarchy)
            overpowerFill.fillAmount = overpowerBuildUp;
    }
    private void ChangeState(PlayerState newstate)
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
            case PlayerState.Prepare:
                break;
            case PlayerState.Play:
                break;
            case PlayerState.Dead:
                break;
            case PlayerState.Finish:
                break;

        }
    }
    private void ExitCurrentState()
    {
        switch (_state)
        {
            case PlayerState.Prepare:
                break;
            case PlayerState.Play:
                break;
            case PlayerState.Dead:
                break;
            case PlayerState.Finish:
                break;

        }
    }
}


