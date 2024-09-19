using UnityEngine;

public class Starfield : MonoBehaviour
{
    public int maxStars = 1000;
    public float starSize = 0.1f;
    public float starSizeRange = 0.5f;
    public float fieldWidth = 20f;
    public float fieldHeight = 20f;
    public float fieldDepth = 20f;
    public float moveSpeed = 20f;
    public float starBrightness = 1f;

    private ParticleSystem.Particle[] stars;
    private ParticleSystem particleSys;
    private ParticleSystem.MainModule particleSettings;

    private bool movingTowardsCamera = true;  // New variable to track direction
    private bool isReady = false; // Control readiness

    void Start()
    {
        stars = new ParticleSystem.Particle[maxStars];
        particleSys = GetComponent<ParticleSystem>();

        if (particleSys == null)
        {
            particleSys = gameObject.AddComponent<ParticleSystem>();
        }

        particleSettings = particleSys.main;
        particleSettings.startSize = starSize;
        particleSettings.startColor = Color.white;
        particleSettings.simulationSpace = ParticleSystemSimulationSpace.World;
        particleSettings.loop = false;

        particleSys.GetComponent<Renderer>().material = new Material(Shader.Find("Particles/Standard Unlit"));
        particleSys.gameObject.SetActive(false);

        CreateStars();
    }

    void CreateStars()
    {
        for (int i = 0; i < maxStars; i++)
        {
            stars[i].position = GetRandomPosition();
            stars[i].startColor = new Color(1, 1, 1, starBrightness);
            stars[i].startSize = Random.Range(starSize * starSizeRange, starSize);
        }

        particleSys.SetParticles(stars, stars.Length);

        // Mark as ready after stars are created
        isReady = true;

        // Show the particle system only when ready
        particleSys.gameObject.SetActive(true);
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-fieldWidth, fieldWidth),
            Random.Range(-fieldHeight, fieldHeight),
            Random.Range(-fieldDepth, fieldDepth)
        );
    }

    void Update()
    {
        if (isReady)
        {

            // Check for space bar press
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movingTowardsCamera = !movingTowardsCamera;  // Flip the direction
            }

            for (int i = 0; i < maxStars; i++)
            {
                Vector3 pos = stars[i].position;

                // Move stars based on current direction
                if (movingTowardsCamera)
                {
                    pos.z += moveSpeed * Time.deltaTime;
                    if (pos.z > fieldDepth)
                    {
                        pos.z = -fieldDepth;
                    }
                }
                else
                {
                    pos.z -= moveSpeed * Time.deltaTime;
                    if (pos.z < -fieldDepth)
                    {
                        pos.z = fieldDepth;
                    }
                }

                stars[i].position = pos;
                stars[i].startColor = new Color(1, 1, 1, starBrightness);
            }

            particleSys.SetParticles(stars, stars.Length);
        }
    }
}