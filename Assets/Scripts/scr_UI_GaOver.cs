using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class scr_UI_GaOver : MonoBehaviour
{
    [SerializeField] private scr_SFX SFX;
    [SerializeField] private ScriptAbleObject_PlayData ScrObjPlay;
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private TMP_Text textHighScore;
    [SerializeField] private scr_UI UIstuff;
    [SerializeField] private ParticleSystem gameOverParticlesL;
    [SerializeField] private ParticleSystem gameOverParticlesR;
    [SerializeField] private ParticleSystem gameOverParticlesC;
    [SerializeField] private Button buttonBack;

    [SerializeField] private ulong score;
    [SerializeField] private ulong highscore;
    [SerializeField] private bool GAMEOVER = false; // Assuming you have a way to determine if the game is over
    [SerializeField] private bool GaOve_trigger1x = false;

   


    // Start is called before the first frame update
    void Start()
    {
        GAMEOVER = false;
        GaOve_trigger1x = false;
        score = 0;

        highscore = ScrObjPlay.HighScore;
        buttonBack.interactable = false;

        //textScore.text = "SCORE\n" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = UIstuff.score;
        GAMEOVER = UIstuff.GAMEOVER;
        

        if (GAMEOVER && !GaOve_trigger1x)
        {
            if (ScrObjPlay != null)
            {

                if (score > ScrObjPlay.HighScore) 
                
                {
                    PlayGameOverParticles();
                    ScrObjPlay.HighScore = score;
                    textHighScore.text = "HIGHSCORE\n" + highscore.ToString(); 
                    textScore.text = "SCORE\n" + score.ToString();
                    GaOve_trigger1x = true;
                }
                else
                {
                    textHighScore.text = "HIGHSCORE\n" + highscore.ToString();
                    textScore.text = "SCORE\n" + score.ToString();
                    GaOve_trigger1x = true;
                }
                    
            }

            //PlayGameOverParticles();
            
            

            StartCoroutine(waitButton());
        }
    }

    // Method to play game over particles
    private void PlayGameOverParticles()
    {
        gameOverParticlesL.Play();
        gameOverParticlesR.Play(); 
        gameOverParticlesC.Play();
        
        SFX.GaOv = true;


        /*
        if (gameOverParticlesL != null)
        {
            gameOverParticlesL.Play();
            SFX.GaOv = true;

        }

        if (gameOverParticlesR != null)
        {
            gameOverParticlesR.Play();
            SFX.GaOv = true;

        }

        if (gameOverParticlesC != null)
        {
            gameOverParticlesC.Play();
            SFX.GaOv = true;

        }

        */
    }

    IEnumerator waitButton()
    {
        yield return new WaitForSeconds(2.0f);
        buttonBack.interactable = true;
        yield return null;

    }

    public void ButtonBackPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    


}
