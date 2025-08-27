using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_GameOver : MonoBehaviour
{
    [SerializeField] private scr_UI UIStuff;
    [SerializeField] private ScriptAbleObject_PlayData ScrObjPlay;
    [SerializeField] private SpriteRenderer ThisSpriteRend;
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D ballRb;

    public bool GAMEOVER = false;
    private bool check_GaOv_1 = false;
    private bool check_GaOv_2 = false;
    private bool check_GaOv_3 = false;

    void resetVariables()
    {
        GAMEOVER = false;

        check_GaOv_1 = false;
        check_GaOv_2 = false;
        check_GaOv_3 = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        resetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (check_GaOv_1)
        {
            if (check_GaOv_2)
            {
                if (check_GaOv_3)
                {
                    GAMEOVER = true;
                    UIStuff.GAMEOVER = true;
                    Debug.Log("ITS GaOv.... GAME OVER");

                }
            }
        }

        if (ThisSpriteRend.material.color.a > 0)
        {
            check_GaOv_3 = true;
            Debug.Log("GaOv_3 warning ON thus Line-END-up CHECKED");
        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            ball = collision.gameObject;
            ballRb = ball.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                check_GaOv_1 = true;
                Debug.Log("GaOv_1 Tag 'ball' CHECKED");

                // Check if the ball is moving or not
                if (ballRb.velocity.magnitude < 0.1f) // Adjust the threshold as needed
                {
                    check_GaOv_2 = true;
                    Debug.Log("GaOv_2 velocity < 0.1f CHECKED");
                }
                else
                {
                    check_GaOv_2 = false;
                }
            }
            else
            {
                check_GaOv_1 = false;
                check_GaOv_2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            check_GaOv_1 = false;
            check_GaOv_2 = false;
        }
    }


}
