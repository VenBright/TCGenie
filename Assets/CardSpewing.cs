using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpewing : MonoBehaviour
{
    public ParticleSystem system;
    public int queuedAmount = 0;
    float cardsSpewed = 0;

    public int debugAmt = 20;
    public int debugPartCount;

    public void QueueCardParticles(int amt) {
        system.Play();
        queuedAmount += amt;
    }

    int particlesLastFrame, newParticles;
    private void Update()
	{
        newParticles = Mathf.Max(0, system.particleCount - particlesLastFrame);
        debugPartCount = system.particleCount;

        //Try to more accurately reflect lower amounts
        if (queuedAmount < system.emission.rateOverTime.constant / 2)
            cardsSpewed += newParticles;
        else
            cardsSpewed += system.emission.rateOverTime.constant * Time.deltaTime;

        if (Mathf.FloorToInt(cardsSpewed) < queuedAmount)
        {
            
        }
        else {
            queuedAmount = 0;
            cardsSpewed = 0;
            system.Stop();
        }

        if (Input.GetKeyDown(KeyCode.K))
            QueueCardParticles(debugAmt);

        particlesLastFrame = system.particleCount;
	}

}
