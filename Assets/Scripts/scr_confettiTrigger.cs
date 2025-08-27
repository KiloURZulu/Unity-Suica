using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_confettiTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem confettiReward;
    [SerializeField] private ParticleSystem confettiReward_1;
    [SerializeField] private ParticleSystem confettiReward_2;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerParticles()
    {
        if (confettiReward != null) confettiReward.Play();
        if (confettiReward_1 != null) confettiReward_1.Play();
        if (confettiReward_2 != null) confettiReward_2.Play();
    }


}
