using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenuUI;

	public static bool GameIsPaused = false;

	private void Awake()
	{
		QualitySettings.SetQualityLevel(3);
		QualitySettings.vSyncCount = 1;
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
		GameIsPaused = false;
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}

	public void Pause()
	{
		GameIsPaused = true;
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
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
}
