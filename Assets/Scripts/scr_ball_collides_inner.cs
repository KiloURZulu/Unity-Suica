using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ball_collides_inner : MonoBehaviour
{
    [SerializeField] private scr_UI UIstuff;
    [SerializeField] private scr_ball_collides BallColiParents;

    public bool possibleGAMEOVER = false;
    [SerializeField] private bool triggerPossible1x = false;

    private void Awake()
    {
        possibleGAMEOVER = false;

    }

    private void Start()
    {
        // Find the UIstuff script on the Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            UIstuff = canvas.GetComponentInChildren<scr_UI>();
            if (UIstuff == null)
            {
                Debug.LogError("scr_UI script not found on the Canvas.");
            }
        }
        else
        {
            Debug.LogError("Canvas not found in the scene.");
        }


    }

    private void Update()
    {
        possibleGAMEOVER = BallColiParents.possibleTriggerEnd;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("border_end"))
        {
            if (!triggerPossible1x)
            {
                if (possibleGAMEOVER)
                {
                    UIstuff.GAMEOVER = true;
                    triggerPossible1x = true;
                    Debug.Log("GAMEOVER is triggered");
                }

            }
        }
    }
}