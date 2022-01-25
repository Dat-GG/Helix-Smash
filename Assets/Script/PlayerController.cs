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
    private float speedLimit = 6f;
    [SerializeField] private float bounceSpeed = 250f;
    [SerializeField] private float StretchSquashFactor = 4;
    public GameObject overpowerBar;
    public Image overpowerFill;
    public GameObject fireEffect;
    public GameObject splashEffect;
    private float diameter;
    private Vector3 lastPos;

    public enum PlayerState
    {
        Prepare,
        Play,
        Dead,
        Finish
    }
    public PlayerState playerState = PlayerState.Prepare;
    private int currentBrokenCircuts, totalCircuts;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBrokenCircuts = 0;
    }

    void Start()
    {
        totalCircuts = FindObjectsOfType<CircutController>().Length;
        diameter = transform.localScale.x;
        lastPos = transform.position;
    }

    void Update()
    {
        //BallMovement();
        if (playerState == PlayerState.Play)
        {
            ClickCheck();
            OverpowerCheck();
        }

        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<Levelling>().IncreaseLevel();
            }
        }
        float yScale = diameter + (transform.position.y - lastPos.y) * StretchSquashFactor;
        transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
        lastPos = transform.position;
    }
    void FixedUpdate()
    {
        BallMovement();
    }

    private void BallMovement()
    {
        if (playerState == PlayerState.Play)
        {
            if (Input.GetMouseButton(0) && isClicked == true)
            {
                rb.velocity = new Vector3(0, -moveSpeed * Time.fixedDeltaTime, 0);
            }
        }

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
            if (!target.gameObject.CompareTag("Finish"))
            {
                GameObject splash = Instantiate(splashEffect);
                splash.transform.SetParent(target.transform);
                splash.transform.localEulerAngles = new Vector3(90, Random.Range(0, 359), 0);
                float randomScale = Random.Range(0.15f, 0.20f);
                splash.transform.localScale = new Vector3(randomScale, randomScale, 1);
                splash.transform.position =
                   new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                splash.GetComponent<SpriteRenderer>().color = GetComponent<MeshRenderer>().material.color;
            }
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
                }

                if (target.gameObject.tag == "BadPart")
                {
                    rb.isKinematic = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    playerState = PlayerState.Dead;
                }
            }
        }
         FindObjectOfType<GameController>().LevelSliderFill(currentBrokenCircuts / (float)totalCircuts);

                if (target.gameObject.CompareTag("WinLocation") && playerState == PlayerState.Play)
                {
                     playerState = PlayerState.Finish;
                }
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

}


