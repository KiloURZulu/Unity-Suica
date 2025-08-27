using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SFX : MonoBehaviour
{
    public ScriptAbleObject_PlayData ScrObjPlay;
    [SerializeField] private float BgmVol;
    [SerializeField] private float FxVol;

    [SerializeField] private int randomAmbien;
    [SerializeField] private AudioSource SFXAmiben;

    public bool ballDrop = false;
    [SerializeField] private int randomDrop;
    [SerializeField] private AudioSource SFXDrop;

    public bool combine = false;
    [SerializeField] private int randomCombine;
    [SerializeField] private AudioSource SFXCombine;

    public bool GaOv = false;
    [SerializeField] private int randomConvetti;
    [SerializeField] private AudioSource SFXConfetti;

    // Start is called before the first frame update
    void Start()
    {
        ballDrop = false;
        combine = false;
        GaOv = false;

        BgmVol = ScrObjPlay.audioVolume;
        FxVol = ScrObjPlay.FxVolume;

        ambien();
    }

    // Update is called once per frame
    void Update()
    {
        BgmVol = ScrObjPlay.audioVolume;

        if (SFXAmiben != null)
        {
            SFXAmiben.volume = BgmVol; // Set the volume
        }

        FxVol = ScrObjPlay.FxVolume;

        if (SFXCombine != null)
        {
            SFXCombine.volume = FxVol; // Set the volume
        }

        if (SFXDrop != null)
        {
            SFXDrop.volume = FxVol; // Set the volume
        }

        if (SFXConfetti != null)
        {
            SFXConfetti.volume = FxVol; // Set the volume
        }

        if (combine)
        {
            randomCombine = Random.Range(21, 23);
            BallCombine();
        }

        if (ballDrop)
        {
            randomConvetti = 91;
            BallDrop();
        }

        if (GaOv)
        {
            randomDrop = Random.Range(11, 13);
            convettiEnd();
        }

    }

    void BallDrop()
    {
        //randomDrop = Random.Range(11, 13);
        //Debug.Log("BallDrop called with randomDrop: " + randomDrop);
        PlayDrop(randomDrop);
    }

    void BallCombine()
    {
        //randomCombine = Random.Range(21, 23);
        //Debug.Log("BallCombine called with randomCombine: " + randomCombine);
        PlayCombine(randomCombine);
    }

    void ambien()
    {
        randomAmbien = 81;
        PlayAmbien(randomAmbien);
    }

    void convettiEnd()
    {
        //randomAmbien = 91;
        PlayConfetti(randomConvetti);
    }

    private void PlayCombine(int codeNumber)
    {
        combine = false;

        // Stop the currently playing audio if there is one
        if (SFXCombine != null && SFXCombine.isPlaying)
        {
            SFXCombine.Stop();
            Destroy(SFXCombine.gameObject);
        }

        foreach (var sfx in ScrObjPlay.SFX)
        {
            if (sfx.codeNumber == codeNumber)
            {
                AudioClip clip = sfx.sound; // Get the audio clip
                if (clip != null)
                {
                    // Create a new GameObject for the AudioSource
                    GameObject audioObject = new GameObject("SFXCombine");
                    SFXCombine = audioObject.AddComponent<AudioSource>();
                    SFXCombine.clip = clip;
                    SFXCombine.volume = FxVol; // Set the volume
                    SFXCombine.Play();

                    // Destroy the GameObject after the clip has finished playing
                    Destroy(audioObject, clip.length);

                    break;
                }
            }
        }
    }

    private void PlayAmbien(int codeNumber)
    {
        // Stop the currently playing audio if there is one
        if (SFXAmiben != null && SFXAmiben.isPlaying)
        {
            SFXAmiben.Stop();
            Destroy(SFXAmiben.gameObject);
        }

        foreach (var sfx in ScrObjPlay.SFX)
        {
            if (sfx.codeNumber == codeNumber)
            {
                AudioClip clip = sfx.sound; // Get the audio clip
                if (clip != null)
                {
                    // Create a new GameObject for the AudioSource
                    GameObject audioObject = new GameObject("SFXAmiben");
                    SFXAmiben = audioObject.AddComponent<AudioSource>();
                    SFXAmiben.clip = clip;
                    SFXAmiben.volume = BgmVol; // Set the volume
                    SFXAmiben.loop = true; // Set looping
                    SFXAmiben.Play();

                    // Destroy(audioObject, clip.length);   // Destroy the GameObject after the clip has finished playing

                    break;
                }
            }
        }
    }

    private void PlayDrop(int codeNumber)
    {
        ballDrop = false;

        // Stop the currently playing audio if there is one
        if (SFXDrop != null && SFXDrop.isPlaying)
        {
            SFXDrop.Stop();
            Destroy(SFXDrop.gameObject);
        }

        foreach (var sfx in ScrObjPlay.SFX)
        {
            if (sfx.codeNumber == codeNumber)
            {
                AudioClip clip = sfx.sound; // Get the audio clip
                if (clip != null)
                {
                    // Create a new GameObject for the AudioSource
                    GameObject audioObject = new GameObject("SFXDrop");
                    SFXDrop = audioObject.AddComponent<AudioSource>();
                    SFXDrop.clip = clip;
                    SFXDrop.volume = FxVol; // Set the volume
                    SFXDrop.Play();

                    // Destroy the GameObject after the clip has finished playing
                    Destroy(audioObject, clip.length);

                    break;
                }
            }
        }
    }

    private void PlayConfetti(int codeNumber)
    {
        GaOv = false;

        foreach (var sfx in ScrObjPlay.SFX)
        {
            if (sfx.codeNumber == codeNumber)
            {
                AudioClip clip = sfx.sound; // Get the audio clip
                if (clip != null)
                {
                    // Create a new GameObject for the AudioSource
                    GameObject audioObject = new GameObject("SFXConfetti");
                    SFXConfetti = audioObject.AddComponent<AudioSource>();
                    SFXConfetti.clip = clip;
                    SFXConfetti.volume = FxVol; // Set the volume
                    SFXConfetti.Play();

                    // Destroy the GameObject after the clip has finished playing
                    Destroy(audioObject, clip.length);

                    break;
                }
            }
        }
    }

}