using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scr_UI : MonoBehaviour
{
    [SerializeField] private ScriptAbleObject_PlayData ScrObjPlay;
    [SerializeField] private scr_SFX SFX;
    public bool GAMEOVER = false;

    //public Button Test_spawn;
    [SerializeField] private ulong ID_ball = 1;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject gameArea;
    [SerializeField] private GameObject endLine;
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private GameObject ballsPlace;
    [SerializeField] private Image Next_Image;
    [SerializeField] private Image Current_Image;

    public Button Skill_guiding;
    [SerializeField] private Transform guidingLine;
    [SerializeField] private SpriteRenderer guidingImage;
    [SerializeField] private bool guidingActive = false;

    public ulong score = 0;
    public ulong bonusOverLv = 0;

    public int randomBall = 1;

    public bool isPressing = false; // Track whether a touch/mouse press is active
    public GameObject currentBall; // Track the current ball being interacted with

    [SerializeField] private bool canInteract = true; // To manage interaction delay
    [SerializeField] private float interactionDelay = 0.5f; // Time in seconds to wait between interactions
    [SerializeField] private int highestBall = 1;
    [SerializeField] private float clampedY;

    [SerializeField] private Transform gameAreaTransform;
    [SerializeField] private float gameAreaHalfWidth;
    [SerializeField] private Vector2 clampedPosition;

    
   

    // Start is called before the first frame update
    void Start()
    {
        //Test_spawn.onClick.AddListener(Spawn); // Add listener to button click
        bonusOverLv = ScrObjPlay.overLvBonus;
        interactionDelay = ScrObjPlay.BetweenTouchDelay;
        GAMEOVER = false;
        randomBall = 1;
        UpdateCurrentImage();

        if (Skill_guiding != null)
        {
            Skill_guiding.onClick.AddListener(ToggleGuidingLine);
        }

        gameAreaTransform = gameArea.transform;
        gameAreaHalfWidth = gameAreaTransform.localScale.x / 2;
    }

    void Update()
    {
        textScore.text = "SCORE\n" + score.ToString();

        Vector2 touchPosition = GetTouchPosition();

        if (!IsWithinGameArea(touchPosition))
        {
            if (isPressing && currentBall != null)
            {
                scr_ball ballScript = currentBall.GetComponent<scr_ball>();
                if (ballScript != null)
                {
                    ballScript.isDrop = true; // Set isDrop to true on release
                    guidingActive = false;
                    
                    //currentBall = null;
                    SFX.ballDrop = true;

                    StartCoroutine(returnButtonGuide());
                }
            }
            isPressing = false;
            return;
        }

        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && canInteract)
        {
            if (!isPressing)
            {
                isPressing = true;
                if (!GAMEOVER)
                {
                    Spawn(); // Spawn ball on touch/mouse press
                }

                StartCoroutine(InteractionCooldown());
            }
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount == 0 && isPressing))
        {
            if (currentBall != null)
            {
                scr_ball ballScript = currentBall.GetComponent<scr_ball>();
                if (ballScript != null)
                {
                    ballScript.isDrop = true; // Set isDrop to true on release
                    guidingActive = false;

                    //currentBall = null;
                    SFX.ballDrop = true;

                    StartCoroutine(returnButtonGuide());

                }
            }
            isPressing = false;
        }

        if (isPressing && currentBall != null)
        {
            clampedPosition = GetTouchPosition();
            Vector2 gameAreaCenter = new Vector2(gameAreaTransform.position.x, gameAreaTransform.position.y);

            Transform imageInner = currentBall.transform.Find("imageInner");
            if (imageInner != null)
            {
                SpriteRenderer spriteRenderer = imageInner.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    float ballHalfWidth = spriteRenderer.bounds.size.x / 2;
                    float clampedX = Mathf.Clamp(clampedPosition.x, gameAreaCenter.x - gameAreaHalfWidth + ballHalfWidth, gameAreaCenter.x + gameAreaHalfWidth - ballHalfWidth);
                    currentBall.transform.position = new Vector2(clampedX, ballsPlace.transform.position.y);
                }
            }
        }

        UpdateHighestBall();
        UpdateNextImage();

        if (guidingActive)
        {
            guidingLine.transform.position = new Vector3(currentBall.transform.position.x, guidingLine.transform.position.y, guidingLine.transform.position.z);
        }
    }

    private Vector2 GetTouchPosition()
    {
        Vector2 touchPosition = Vector2.zero;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        return touchPosition;
    }

    void Spawn()
    {
        
        Vector2 touchPosition = GetTouchPosition(); // Get touch position

        Vector2 gameAreaCenter = new Vector2(gameArea.transform.position.x, gameArea.transform.position.y);     // Calculate the position for the ball
        float gameAreaHeight = gameArea.transform.localScale.y;
        float gameAreaHalfHeight = gameAreaHeight / 2;

        clampedY = Mathf.Clamp(touchPosition.y, (gameAreaCenter.y/2) - gameAreaHalfHeight, (gameAreaCenter.y/2) + gameAreaHalfHeight);    // Clamp the touch position's Y-coordinate within the game area
        Vector2 spawnPosition = new Vector2(touchPosition.x, clampedY);

        currentBall = Instantiate(ball, spawnPosition, Quaternion.identity, ballsPlace.transform);  // Instantiate the ball under ballsPlace

        currentBall.name = ball.name + "_" + ID_ball;// Set the name for the ball

        scr_ball ballScript = currentBall.GetComponent<scr_ball>(); // Set the ID for the ball
        if (ballScript != null)
        {
            ballScript.ID = ID_ball;
            ballScript.currentImage = randomBall; // Use randomBall instead of random
            UpdateCurrentImage();

            ballScript.isDrop = false; // Ensure the ball's isDrop property is false
        }

        ID_ball++;  // Increment the ID for the next ball

        
        if (ballsPlace != null) // Update the random variable
        {
            UpdateHighestBall();
            if (highestBall > 1)
            {
                randomBall = Random.Range(1, highestBall - 1); 
            }
            else
            {
                randomBall = 1;
            }
        }
        else
        {
            randomBall = 1;
        }


    }


    private IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionDelay);
        canInteract = true;
    }

    private void UpdateHighestBall()
    {
        highestBall = 0;
        foreach (Transform child in ballsPlace.transform)
        {
            scr_ball ballScript = child.GetComponent<scr_ball>();
            if (ballScript != null && ballScript.currentImage > highestBall)
            {
                highestBall = ballScript.currentImage;
            }
        }
    }

    private void UpdateNextImage()
    {
        foreach (var ballImg in ScrObjPlay.ballImg)
        {
            if (ballImg.codeNumber == randomBall)
            {
                // Retain the size of the current image
                Vector2 size = Next_Image.rectTransform.sizeDelta;

                // Change the image
                Next_Image.sprite = ballImg.sprite;

                // Retain the size of the image
                Next_Image.rectTransform.sizeDelta = size;
                break;
            }
        }
    }

    private void UpdateCurrentImage()
    {

        foreach (var ballImg in ScrObjPlay.ballImg)
        {
            if (ballImg.codeNumber == randomBall)
            {
                // Retain the size of the current image
                Vector2 size = Current_Image.rectTransform.sizeDelta;

                // Change the image
                Current_Image.sprite = ballImg.sprite;

                // Retain the size of the image
                Current_Image.rectTransform.sizeDelta = size;
                break;
            }
        }
    }

    private bool IsWithinGameArea(Vector2 position)
    {
        RectTransform rectTransform = gameArea.GetComponent<RectTransform>();
        Vector2 localPosition = rectTransform.InverseTransformPoint(position);

        return Mathf.Abs(localPosition.x) <= rectTransform.rect.width / 2 &&
               Mathf.Abs(localPosition.y) <= rectTransform.rect.height / 2;
    }


    public void ToggleGuidingLine()
    {
        guidingActive = true;  // Set guidingActive to true
        guidingLine.gameObject.SetActive(guidingActive);  // Correctly activate/deactivate the GameObject


        if (Skill_guiding != null)
        {
            Skill_guiding.interactable = false;  // Disable the button

            Color color = guidingImage.color;  // Get the current color
            color.a = 1.0f;  // Set the alpha value
            guidingImage.color = color;  // Apply the updated color
        }
    }

    IEnumerator returnButtonGuide()
    {
        guidingLine.transform.position = new Vector3(-10, guidingLine.transform.position.y, guidingLine.transform.position.z);
        Color color = guidingImage.color;
        color.a = 0.01f;
        guidingImage.color = color;
        yield return new WaitForSecondsRealtime(2.0f);
        Skill_guiding.interactable = true;

    }

    

}

