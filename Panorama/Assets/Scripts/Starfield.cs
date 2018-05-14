using UnityEngine;

public class Starfield : MonoBehaviour 
{

    public int maxStars = 1000;
	public float minStarSize = 0.1f;
	public float maxStarSize = 0.15f;
	public int universeSize = 10;
	public float hueMin = 1f;
	public float hueMax = 1f;
	public float satMin = 1f;
	public float satMax = 1f;

	private ParticleSystem.Particle[] points;

    private ParticleSystem _particleSystem;

    private void Create()
    {

        points = new ParticleSystem.Particle[maxStars];

        for (int i = 0; i < maxStars; i++)
        {
            points[i].position = Random.onUnitSphere * universeSize;
            points[i].startSize = Random.Range(minStarSize, maxStarSize);
			points[i].startColor = Random.ColorHSV(hueMin, hueMax, satMin, satMax);
		}

        _particleSystem = gameObject.GetComponent<ParticleSystem>();

       _particleSystem.SetParticles(points, points.Length);
    }

    void Start()
    {
        Create();
    }

    void Update()
    {
        //You can access the particleSystem here if you wish
    }
}
