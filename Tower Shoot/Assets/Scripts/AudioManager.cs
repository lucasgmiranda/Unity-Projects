using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour 
{
	public Sound[] sounds;

	void Awake () 
	{
		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}

		Play("Rain");
		Play("Heavy Rain");
	}	

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Play();
	}

	public void Pause(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Pause();
	}

	public void Volume(string name, float volume)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.volume = volume;
	}
}
