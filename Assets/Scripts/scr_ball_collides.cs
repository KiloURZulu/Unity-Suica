using System.Collections;
using UnityEngine;

public class scr_ball_collides : MonoBehaviour
{
    [SerializeField] private scr_SFX SFX;
    [SerializeField] private scr_ball BallParent;
    [SerializeField] private scr_UI UIstuff;
    [SerializeField] private ScriptAbleObject_PlayData ScrObjPlay;

    public ulong ID = 0;
    public int currentImage = 0;
    public ulong amount = 0;
    public ulong bonus = 0;

    //[SerializeField] private bool hasCollided = false; // Flag to prevent multiple destruction
    [SerializeField] private float collisionCooldown = 0.5f; // Cooldown period in seconds

    //private bool collisionChecked = false; // Flag to prioritize collision enter checks
    public bool possibleTriggerEnd = false; // Flag for triggering the end game, active only when hitting other "ball" objects

    
    // Start is called before the first frame update
    void Start()
    {
        if (UIstuff == null)
        {
            // Find the UIstuff script on the CanvasUI GameObject
            GameObject canvasUI = GameObject.Find("CanvasUI");
            if (canvasUI != null)
            {
                Debug.Log("CanvasUI found! in ball collide");

                UIstuff = canvasUI.GetComponentInChildren<scr_UI>();
                if (UIstuff == null)
                {
                    Debug.LogError("scr_UI component not found in CanvasUI.");
                }
            }
            else
            {
                Debug.LogError("CanvasUI not found!");
            }

        }

        if (SFX == null)
        {
            GameObject SFX_search = GameObject.Find("Main Camera");
            if (SFX_search != null)
            {
                Debug.Log("SFX_search found!");

                SFX = SFX_search.GetComponentInChildren<scr_SFX>();
                if (SFX == null)
                {
                    Debug.LogError("scr_SFX component not found in CanvasUI.");
                }
            }
            else
            {
                Debug.LogError("SFX not found!");
            }

        }

        collisionCooldown = ScrObjPlay.collisionDelay;
        currentImage = BallParent.currentImage;
        ID = BallParent.ID;
        amount = BallParent.amount;
        bonus = BallParent.bonus;
    }

    // Update is called once per frame
    void Update()
    {
        if (BallParent.needUpdate)
        {
            currentImage = BallParent.currentImage;
            ID = BallParent.ID;
            amount = BallParent.amount;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("ball"))
            {
                possibleTriggerEnd = true;
                HandleCollision(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("destroy")) // Check if the ball collides with an object tagged as "destroy"
            {
                Destroy(BallParent.gameObject);
            }
        }
        else
        {
            Debug.LogError("Collision is null");
        }
    }

    private void HandleCollision(GameObject collisionObject)
    {
        scr_ball_collides otherBall = collisionObject.GetComponent<scr_ball_collides>();

        //if (otherBall != null && !hasCollided)
        if (otherBall != null)
        {
            int collideCurrentImage = otherBall.currentImage;
            ulong collideID = otherBall.ID;

            if (currentImage == collideCurrentImage)
            {
               // hasCollided = true; // Set flag to true to prevent further destruction
                //StartCoroutine(ResetCollisionFlag());

                if (currentImage < BallParent.maxLV)
                {
                    if (ID > collideID)
                    {
                        UIstuff.score += amount;
                        Destroy(BallParent.gameObject);
                    }
                    else if (ID < collideID)
                    {
                        BallParent.currentImage += 1;
                        currentImage += 1;
                        BallParent.needUpdate = true;

                        SFX.combine = true;

                    }
                }
                else
                {
                    Destroy(BallParent.gameObject);
                    UIstuff.score += bonus;
                }
            }
        }
        else
        {
            Debug.LogWarning("The collided object does not have a scr_ball_collides component or collision flag is already set.");
        }
    }

    /*----------------------------------
    private IEnumerator ResetCollisionFlag()
    {
        yield return new WaitForSeconds(collisionCooldown);
        hasCollided = false; // Reset flag after cooldown
    }
    -------------------------------------*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the ball collides with an object tagged as "destroy"
        if (collision.CompareTag("destroy"))
        {
            Destroy(BallParent.gameObject);
        }
    }

    

}