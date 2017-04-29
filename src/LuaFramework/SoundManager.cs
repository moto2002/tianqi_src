using System;
using System.Collections;
using UnityEngine;

namespace LuaFramework
{
	public class SoundManager : Manager
	{
		private AudioSource audioSource;

		private Hashtable sounds = new Hashtable();

		private void Start()
		{
			this.audioSource = base.GetComponent<AudioSource>();
		}

		private void Add(string key, AudioClip value)
		{
			if (this.sounds.get_Item(key) != null || value == null)
			{
				return;
			}
			this.sounds.Add(key, value);
		}

		private AudioClip Get(string key)
		{
			if (this.sounds.get_Item(key) == null)
			{
				return null;
			}
			return this.sounds.get_Item(key) as AudioClip;
		}

		public AudioClip LoadAudioClip(string path)
		{
			AudioClip audioClip = this.Get(path);
			if (audioClip == null)
			{
				audioClip = (AudioClip)Resources.Load(path, typeof(AudioClip));
				this.Add(path, audioClip);
			}
			return audioClip;
		}

		public bool CanPlayBackSound()
		{
			string text = "LuaFramework_BackSound";
			int @int = PlayerPrefs.GetInt(text, 1);
			return @int == 1;
		}

		public void PlayBacksound(string name, bool canPlay)
		{
			if (this.audioSource.get_clip() != null && name.IndexOf(this.audioSource.get_clip().get_name()) > -1)
			{
				if (!canPlay)
				{
					this.audioSource.Stop();
					this.audioSource.set_clip(null);
					Util.ClearMemory();
				}
				return;
			}
			if (canPlay)
			{
				this.audioSource.set_loop(true);
				this.audioSource.set_clip(this.LoadAudioClip(name));
				this.audioSource.Play();
			}
			else
			{
				this.audioSource.Stop();
				this.audioSource.set_clip(null);
				Util.ClearMemory();
			}
		}

		public bool CanPlaySoundEffect()
		{
			string text = "LuaFramework_SoundEffect";
			int @int = PlayerPrefs.GetInt(text, 1);
			return @int == 1;
		}

		public void Play(AudioClip clip, Vector3 position)
		{
			if (!this.CanPlaySoundEffect())
			{
				return;
			}
			AudioSource.PlayClipAtPoint(clip, position);
		}
	}
}
