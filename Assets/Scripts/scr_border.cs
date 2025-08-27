using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scr_border : MonoBehaviour
{
    [Tooltip("hit this and limit [ borderEnd ] blink")] public GameObject borderS;  // border_TC_soft
    [Tooltip("hit this and game end")] public GameObject borderEnd;                 // border_TC
    [SerializeField] private ScriptAbleObject_PlayData ScrObjPlay;

    public ulong score = 0;
    public ulong HighScore = 0;
    //public bool GAMEOVER = false;

    [SerializeField] private Canvas canvasUI;
    [SerializeField] private Canvas canvasPopUp;

    [SerializeField] private scr_UI UIstuff;

    private Renderer borderEndRenderer;
    private bool isBlinking = false;
    private bool Trigger1x_GaOve = false;

    private Coroutine blinkCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Trigger1x_GaOve = false;

        borderEndRenderer = borderEnd.GetComponent<Renderer>();
        Color color = borderEndRenderer.material.color;
        color.a = 0f; // Set initial transparency to 0%
        borderEndRenderer.material.color = color;
        HighScore = ScrObjPlay.HighScore;

        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (UIstuff.GAMEOVER)
        {
            if (UIstuff != null && !Trigger1x_GaOve)
            {
                //GAMEOVER = true;
                score = UIstuff.score;

                if (ScrObjPlay != null && score > HighScore)
                {
                    ScrObjPlay.HighScore = score;
                    Debug.Log("storing highscore");
                }


                if (canvasUI != null) canvasUI.enabled = false;
                if (canvasPopUp != null) canvasPopUp.enabled = true;

                Trigger1x_GaOve = true;
            }
        }
        
        else
        {
            if (canvasUI != null) canvasUI.enabled = true;
            if (canvasPopUp != null) canvasPopUp.enabled = false;

        }
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            blinkCoroutine = StartCoroutine(BlinkBorderEnd());
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            StopCoroutine(blinkCoroutine);

            Color color = borderEndRenderer.material.color;
            color.a = 0.0f; // Reset alpha to fully opaque
            borderEndRenderer.material.color = color;

            //borderEndRenderer.material.color = Color.white; // Reset to default color

            isBlinking = false;
        }
    }

    private IEnumerator BlinkBorderEnd()
    {
        isBlinking = true;
        float blinkDuration = 1f; // Duration of one blink cycle (in seconds)
        float elapsedTime = 0f;

        // Ensure it goes to 100% transparency first
        while (elapsedTime < blinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / blinkDuration;
            Color color = borderEndRenderer.material.color;
            color.a = Mathf.Lerp(0f, 1f, t); // Change alpha value for transparency effect
            borderEndRenderer.material.color = color;
            Debug.Log($"Initial transition to 100% - Alpha: {color.a}");
            yield return null;
        }

        // Continue blinking between 100% and 0%
        while (isBlinking)
        {
            elapsedTime = 0f;
            // Transition to 0%
            while (elapsedTime < blinkDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / blinkDuration;
                Color color = borderEndRenderer.material.color;
                color.a = Mathf.Lerp(1f, 0f, t); // Change alpha value for transparency effect
                borderEndRenderer.material.color = color;
                Debug.Log($"Blinking to 0% - Alpha: {color.a}");
                yield return null;
            }

            elapsedTime = 0f;
            // Transition to 100%
            while (elapsedTime < blinkDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / blinkDuration;
                Color color = borderEndRenderer.material.color;
                color.a = Mathf.Lerp(0f, 1f, t); // Change alpha value for transparency effect
                borderEndRenderer.material.color = color;
                Debug.Log($"Blinking to 100% - Alpha: {color.a}");
                yield return null;
            }
        }
    }

    

}
