using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Transform character;
    public float maxSnowHeight = 50f;
    public float minSnowHeight = 7f;
    public float maxWindSpeed = 3.0f;
    public float minWindSpeed = 0.5f;

    private ParticleSystem snowParticleSystem;
    private float defaultSnowParticles;
    private float defaultWindSpeed;

    private void Start()
    {
        snowParticleSystem = gameObject.GetComponent<ParticleSystem>();
        defaultSnowParticles = snowParticleSystem.main.maxParticles;
        defaultWindSpeed = snowParticleSystem.main.simulationSpeed;
    }

    private void Update()
    {
        if (character)
        {
            float characterHeight = Mathf.Clamp(character.position.y - minSnowHeight, 0f, maxSnowHeight);
            float heightRatio = characterHeight / maxSnowHeight;

            var particleMain = snowParticleSystem.main;
            particleMain.maxParticles = (int)(defaultSnowParticles * heightRatio * 5.0f);
            particleMain.simulationSpeed = Mathf.Lerp(minWindSpeed, maxWindSpeed, heightRatio);
        }
        else
        {
            Debug.Log("User Character not attached");
        }
    }
}
