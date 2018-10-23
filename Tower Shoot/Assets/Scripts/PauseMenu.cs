using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseMenuUI;
	public GameObject[] DisableOnPause;

	public static bool GameIsPaused = false;
	[HideInInspector]
	public int DropdownIndex;

	AudioManager AM;

	private void Awake()
	{
		AM = FindObjectOfType<AudioManager>();

		QualitySettings.vSyncCount = 1;
		SetQualityLevel(3);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
				Resume();
			else
				Pause();
		}
	}

	public void Resume()
	{
		EnableObjects();
		GameIsPaused = false;
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;

		AM.Play("Rain");
		AM.Play("Heavy Rain");
	}

	public void Pause()
	{
		DisableObjects();
		GameIsPaused = true;
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;

		AM.Pause("Rain");
		AM.Pause("Heavy Rain");
	}

	public void set30FPS()
	{
		QualitySettings.vSyncCount = 2;
	}

	public void set60FPS()
	{
		QualitySettings.vSyncCount = 1;
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void SetQualityLevel(int index)
	{
		QualitySettings.SetQualityLevel(index);
	}

	public void EnableObjects()
	{
		foreach (GameObject obj in DisableOnPause)
		{
			obj.SetActive(true);
		}
	}
	public void DisableObjects()
	{
		foreach(GameObject obj in DisableOnPause)
		{
			obj.SetActive(false);
		}
	}
}
