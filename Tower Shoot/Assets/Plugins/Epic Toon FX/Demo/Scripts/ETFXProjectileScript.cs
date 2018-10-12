using UnityEngine;
using System.Collections;
 
public class ETFXProjectileScript : MonoBehaviour
{
	public float lifespan;
	public float explosionPower = 10f;
	public float radius = 5f;
	public float upForce = 1f;
	public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;
	Transform launchSource;
	[HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.
 
    private bool hasCollided = false;
 
    void Start()
    {
		StartCoroutine(Destroy());

		launchSource = GameObject.Find("Launch Source").transform;
		transform.rotation = launchSource.rotation;

		projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
		
		if (muzzleParticle)
		{
			muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
			Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
		}
    }
 
    void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            hasCollided = true;
			//transform.DetachChildren();
			impactNormal = hit.contacts[0].normal;
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;

			explosionForceEffect();

			Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);
 
            if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
            {
                Destroy(hit.gameObject);
            }

			DestroyProjectile();
		}
    }

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(lifespan);
		DestroyProjectile();
	}

	private void explosionForceEffect()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

		foreach(Collider hit in colliders)
		{
			Rigidbody rigb = hit.GetComponent<Rigidbody>();

			if(rigb != null)
				rigb.AddExplosionForce(explosionPower, transform.position, radius, upForce, ForceMode.Impulse);
		}
	}

	public void DestroyProjectile()
	{
		foreach (GameObject trail in trailParticles)
		{
			GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
			curTrail.transform.parent = null;
			Destroy(curTrail, 3f);
		}
		Destroy(projectileParticle, 3f);
		if(impactParticle.activeInHierarchy)
			Destroy(impactParticle, 5f);
		Destroy(gameObject);
		//projectileParticle.Stop();

		ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
		//Component at [0] is that of the parent i.e. this object (if there is any)
		for (int i = 1; i < trails.Length; i++)
		{
			ParticleSystem trail = trails[i];
			if (!trail.gameObject.name.Contains("Trail"))
				continue;

			trail.transform.SetParent(null);
			Destroy(trail.gameObject, 2);
		}
	}
}