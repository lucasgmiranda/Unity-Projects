using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace EpicToonFX
{
public class ETFXFireProjectile : MonoBehaviour 
{
    RaycastHit hit;
    public GameObject[] projectiles;
    public Transform spawnPosition;
   // [HideInInspector]
    public int currentProjectile = 0;
	public float speed = 1000;

//    MyGUI _GUI;
	ETFXButtonScript selectedProjectileButton;

	void Start () 
	{
		//selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextEffect();
        }

		if (Input.GetKeyDown(KeyCode.D))
		{
			nextEffect();
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			previousEffect();
		}
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previousEffect();
        }

		for (int i = 0; i < Input.touchCount; ++i)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Vector3 touchPos = Input.GetTouch(0).position;
				touchPos.z = -Camera.main.transform.position.z;
				Vector3 projectileTarget = Camera.main.ScreenToWorldPoint(touchPos);
				GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
				projectile.transform.LookAt(projectileTarget);
				projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed);
			}
		}       
        //Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction*100, Color.yellow);
	}

    public void nextEffect()
    {
        if (currentProjectile < projectiles.Length - 1)
            currentProjectile++;
        else
            currentProjectile = 0;
		selectedProjectileButton.getProjectileNames();
    }

    public void previousEffect()
    {
        if (currentProjectile > 0)
            currentProjectile--;
        else
            currentProjectile = projectiles.Length-1;
		selectedProjectileButton.getProjectileNames();
    }

	public void AdjustSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
}
}