using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepEffectControllor : MonoBehaviour
{
    public Transform character;
    public float minSnowHeight = 5f;

    public float maxSnowHeight = 15f;

    public float speedThreshold = 1f;
    public Color highMountainStepColor = Color.white;
    public Color lowMountainStepColor = new Color(99, 130, 60);

    private ParticleSystem footStepParticleSystem;
    private float defaultSnowParticles;

    private void Start()
    {
        footStepParticleSystem = gameObject.GetComponent<ParticleSystem>();
        defaultSnowParticles = footStepParticleSystem.main.maxParticles;
    }

    private void Update()
    {
        if (character)
        {
            var particleMain = footStepParticleSystem.main;

            float characterSpeed = character.GetComponent<Rigidbody>().velocity.magnitude;
            
            if (characterSpeed > 0.2f )
            {
                float speedRatio = Mathf.Clamp01(characterSpeed / speedThreshold);
                particleMain.maxParticles = (int)Mathf.Lerp(defaultSnowParticles, defaultSnowParticles * 2f, speedRatio);
            } else
            {
                particleMain.maxParticles = 0;
            }
            

            if (character.position.y < minSnowHeight)
            {
                particleMain.startColor = lowMountainStepColor;
            } else if (character.position.y > maxSnowHeight)
            {
                particleMain.startColor = highMountainStepColor;

            } else
            {
                particleMain.maxParticles = 0;
            }     

        }
        else
        {
            Debug.Log("User Character not attached");
        }
    }
}


