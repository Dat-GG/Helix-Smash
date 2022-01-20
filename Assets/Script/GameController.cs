using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject WinUI;
    private PlayerController player;
    public GameObject Pole;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {

    }

    void Update()
    {
        Management();
    }
    private void Management()
    {
        if (player.playerState == PlayerController.PlayerState.Finish)
        {
            Pole.SetActive(false);
            WinUI.SetActive(true);
        }
    }
    
}
