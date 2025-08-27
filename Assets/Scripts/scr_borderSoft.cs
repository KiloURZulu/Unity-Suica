using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_borderSoft : MonoBehaviour
{
    [SerializeField] private scr_border bordeR;
    [SerializeField] private ScriptAbleObject_PlayData ScrObjPlay;

    [SerializeField ] private float stayTime = 0.0f; // Time in seconds to start blinking
    [SerializeField] private float stayTimer = 0.0f; // Timer to track how long the ball has stayed
    [SerializeField] private bool isBallInside = false; // Whether the ball is currently inside the trigger

    private void Awake()
    {
        //ScrObjPlay = GetComponent<ScriptAbleObject_PlayData>();


        if (ScrObjPlay != null)
        {
            stayTime = ScrObjPlay.timerSoftWarning;
        }
        else
        {
            Debug.LogError("ScriptAbleObject_PlayData component not found.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bordeR = GetComponentInParent<scr_border>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBallInside)
        {
            stayTimer += Time.deltaTime;
            if (stayTimer >= stayTime)
            {
                bordeR.StartBlinking();
                isBallInside = false; // Stop checking once blinking has started
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ball"))
        {
            isBallInside = true; // Ball is inside the trigger
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("ball"))
        {
            isBallInside = false; // Ball exited the trigger
            stayTimer = 0f; // Reset the timer
            bordeR.StopBlinking(); // Ensure blinking stops if ball exits
        }
    }

}
