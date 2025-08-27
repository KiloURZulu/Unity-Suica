using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class scr_ball : MonoBehaviour
{
    public ScriptAbleObject_PlayData ScrObjPlay;
    [SerializeField] private SpriteRenderer imageInnerRenderer; // Reference to the SpriteRenderer of the child
    [SerializeField] private Rigidbody2D imageInner_Rb;
    [SerializeField] private SpriteRenderer picture;
    public scr_UI UIstuff;

    public ulong ID = 0;
    public ulong collide_ID = 0;
    public int currentImage = 0;
    public int maxLV = 0;
    public bool needUpdate = false;
    
    public ulong amount = 0;
    public ulong bonus = 0;
    public bool isDrop =false;
    public bool hasDropped;

    [SerializeField] private float JmpPw = 0.01f;
    [SerializeField] private float JmpDly = 0.01f;

    [SerializeField] private int previousImage = -1; // Track previous image to detect changes
    [SerializeField] private int changeImageTimes = 0;




    // Start is called before the first frame update
    void Start()
    {
        isDrop = false;
        changeImageTimes = 0;

        // Find the child GameObject and get its SpriteRenderer
        Transform imageInner = transform.Find("imageInner");
        if (imageInner != null)
        {
            imageInnerRenderer = imageInner.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("imageInner spriteRender not found as a child.");
        }

        // Find the child GameObject and get its SpriteRenderer
        Transform pictureFind = transform.Find("picture");
        if (pictureFind != null)
        {
            picture = pictureFind.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("picture spriteRender not found as a child.");
        }

        // Find the UIstuff script on the CanvasUI GameObject
        GameObject canvasUI = GameObject.Find("CanvasUI");
        if (canvasUI != null)
        {
            Debug.Log("CanvasUI found! in ball spawn");

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


        /*
        // Optionally find UIstuff if not set via Inspector
        if (UIstuff == null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                UIstuff = canvas.GetComponentInChildren<scr_UI>();
            }
        }
        */

        JmpPw = ScrObjPlay.jumpPower;
        JmpDly = ScrObjPlay.JumpDelay;

        UpdateImage();

        maxLV = ScrObjPlay.ballLv.Count;
        bonus = ScrObjPlay.overLvBonus;

        UpdateYFreeze();
    }

    // Update is called once per frame
    void Update()
    {
        if (needUpdate)
        {
            //UIstuff.score += amount;
            UpdateImage();
            needUpdate = false; // Reset the flag after updating

            UpdateYFreeze();
        }

        // Check if this ball is the current ball in UIstuff
        if (UIstuff.currentBall != null && UIstuff.currentBall.name.EndsWith(ID.ToString()))
        {
            isDrop = !UIstuff.isPressing;
            UpdateYFreeze(); // Ensure the constraints are updated based on isDrop
        }

        // Check if the current image has changed
        if (currentImage != previousImage)
        {
            if (changeImageTimes == 0)
            {
                changeImageTimes += 1;

            }
            else
            {
                //Jump(); // Perform jump when currentImage changes
                StartCoroutine(DelayedJump(JmpDly)); // Perform jump with delay when currentImage changes
                
            }
            previousImage = currentImage; // Update the previous image

        }

        // Destroy the ball if it falls below Y threshold
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }

    }

    private void UpdateImage()
    {
        if (imageInnerRenderer == null)
            return;

        // Check if currentImage matches the codeNumber
        foreach (var ballLv in ScrObjPlay.ballLv)
        {
            if (ballLv.codeNumber == currentImage)
            {
                // Update sprite image
                imageInnerRenderer.sprite = ballLv.sprite;

                // Update sprite scale
                imageInnerRenderer.transform.localScale = new Vector3(ballLv.scale, ballLv.scale, 1);

                amount= ballLv.amount;

                foreach (var ballImg in ScrObjPlay.ballImg)
                {
                    if (ballImg.codeNumber == currentImage)
                    {
                        // Update sprite image
                        picture.sprite = ballImg.sprite;

                    }
                }

                break;
            }
        }
    }

    private void UpdateYFreeze()
    {
        if (imageInner_Rb != null)
        {
            if (hasDropped)
            {
                return; // stops checking this code block after ball has dropped
            }

            if (isDrop)
            {
                imageInner_Rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; // Unfreeze Y position
                Debug.Log("unfreeze");

                // There is a bug where they don't fall if no force is applied.
                imageInner_Rb.AddForce(new Vector2(0, -0.1f), ForceMode2D.Force);
                hasDropped = true;
            }
            else
            {
                imageInner_Rb.constraints |= RigidbodyConstraints2D.FreezePositionY; // Freeze Y position
                Debug.Log("freeze");
            }
        }
    }

    private IEnumerator DelayedJump(float jumpDelay)
    {
        yield return new WaitForSeconds(jumpDelay); // Wait for the delay
        Jump(); // Perform the jump after the delay
    }

    private void Jump()
    {
        if (imageInner_Rb != null)
        {
            // Example jump logic
            Vector2 jumpForce = new Vector2(UnityEngine.Random.Range(-JmpPw, JmpPw + 1), JmpPw); // Set the jump force
            imageInner_Rb.AddForce(jumpForce, ForceMode2D.Impulse);

            // Debug log the jump force
            Debug.Log($" ID:{ID}  Jump force applied: {jumpForce}");
        }
        else
        {
            // Debug log if imageInner_Rb is null
            Debug.Log("imageInner_Rb is null. Cannot apply jump force.");
        }
    }

}
