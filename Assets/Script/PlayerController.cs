using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float overpowerBuildUp;
    [SerializeField] private bool isClicked, isOverPowered;
    [SerializeField] private float moveSpeed = 700f;
    private float speedLimit = 6f;
    [SerializeField] private float bounceSpeed = 275f;

    public enum PlayerState
    {
        Prepare,
        Play,
        Dead,
        Finish
    }
    public PlayerState playerState = PlayerState.Prepare;
    private int currentBrokenPlatforms, totalPlatforms;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBrokenPlatforms = 0;
    }

    void Start()
    {
        totalPlatforms = FindObjectsOfType<CircutController>().Length;
    }

    void Update()
    {
        if (playerState == PlayerState.Prepare && Input.GetMouseButton(0))
        {
            playerState = PlayerState.Play;
        }
        if (playerState == PlayerState.Play)
        {
            ClickCheck();
        }

        if (playerState == PlayerState.Finish)
        {

        }
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
        currentBrokenPlatforms++;
        ScoringController.instance.Scoring(1);
    }
        void OnCollisionEnter(Collision target)
    {
        if (!isClicked)
        {
            rb.velocity = new Vector3(0, bounceSpeed * Time.deltaTime, 0);
            if (!target.gameObject.CompareTag("Finish"))
            {

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
       

        if (target.gameObject.CompareTag("WinLocation") && playerState == PlayerState.Play)
        {
            playerState = PlayerState.Finish;

        }
    }

}


