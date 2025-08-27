using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_imageChange : MonoBehaviour
{
    [SerializeField] private scr_ball_collides ballColi;
    [SerializeField] private ScriptAbleObject_PlayData SCrObjPlay;

    [SerializeField] private int imageChange = 1;

    private SpriteRenderer ThisSpriteRender;
    // Start is called before the first frame update
    void Start()
    {
        imageChange = 1;
    }

    // Update is called once per frame
    void Update()
    {
        imageChange = ballColi.currentImage;


    }
}
