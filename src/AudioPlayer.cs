using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
	private enum MusicPlayerStatus
	{
		Playing,
		FadingOut,
		FadingIn
	}

	private AudioPlayer.MusicPlayerStatus status;

	private AudioClip nextclip;

	private AudioSource mAudioSource;

	public Action playerEnd;

	private long roleId;

	private float fadeOut;

	private float fadeIn;

	private float fadeStart;

	private float mVolume;

	public AudioClip clip
	{
		get
		{
			if (this.mAudioSource != null)
			{
				return this.mAudioSource.get_clip();
			}
			return null;
		}
		set
		{
			if (this.mAudioSource != null)
			{
				this.mAudioSource.set_clip(value);
			}
		}
	}

	public bool IsPlaying
	{
		get
		{
			return this.mAudioSource != null && this.mAudioSource.get_isPlaying();
		}
	}

	public bool loop
	{
		get
		{
			return this.mAudioSource.get_loop();
		}
		set
		{
			if (this.mAudioSource)
			{
				this.mAudioSource.set_loop(value);
			}
		}
	}

	public bool playOnAwake
	{
		get
		{
			return this.mAudioSource.get_playOnAwake();
		}
		set
		{
			if (this.mAudioSource != null)
			{
				this.mAudioSource.set_playOnAwake(value);
			}
		}
	}

	public float volume
	{
		get
		{
			return this.mAudioSource.get_volume();
		}
		set
		{
			this.mAudioSource.set_volume(value);
		}
	}

	public float pitch
	{
		get
		{
			return this.mAudioSource.get_pitch();
		}
		set
		{
			if (this.mAudioSource != null)
			{
				this.mAudioSource.set_pitch(value);
			}
		}
	}

	public bool Mute
	{
		get
		{
			return this.mAudioSource.get_mute();
		}
		set
		{
			if (this.mAudioSource != null)
			{
				this.mAudioSource.set_mute(value);
			}
		}
	}

	public long RoleId
	{
		get
		{
			return this.roleId;
		}
		set
		{
			this.roleId = value;
		}
	}

	public void Awake()
	{
		this.mAudioSource = base.get_gameObject().AddMissingComponent<AudioSource>();
		this.mAudioSource.set_spatialBlend(1f);
		this.mAudioSource.set_rolloffMode(1);
		if (SoundManager.Instance != null)
		{
			this.mAudioSource.set_minDistance(SoundManager.Instance.MinDistance);
			this.mAudioSource.set_maxDistance(SoundManager.Instance.MaxDistance);
			this.fadeIn = SoundManager.Instance.FadeIn;
			this.fadeOut = SoundManager.Instance.FadeOut;
		}
	}

	private void OnDisable()
	{
		if (this.playerEnd != null)
		{
			this.playerEnd.Invoke();
		}
	}

	private void Update()
	{
		if (this.status == AudioPlayer.MusicPlayerStatus.FadingOut)
		{
			if (Time.get_time() < this.fadeStart + this.fadeOut)
			{
				this.volume = this.mVolume - Auto.SoundInOut(Time.get_time() - this.fadeStart, 0f, this.mVolume, this.fadeOut);
			}
			else
			{
				base.GetComponent<AudioSource>().Stop();
				this.volume = 0f;
				this.status = AudioPlayer.MusicPlayerStatus.Playing;
				if (this.nextclip != null)
				{
					this.clip = this.nextclip;
					this.mAudioSource.Play();
					this.nextclip = null;
					this.fadeStart = Time.get_time();
					this.status = AudioPlayer.MusicPlayerStatus.FadingIn;
				}
			}
		}
		else if (this.status == AudioPlayer.MusicPlayerStatus.FadingIn)
		{
			if (Time.get_time() < this.fadeStart + this.fadeIn)
			{
				this.volume = Auto.SoundInOut(Time.get_time() - this.fadeStart, 0f, this.mVolume, this.fadeIn);
			}
			else
			{
				this.volume = this.mVolume;
				this.status = AudioPlayer.MusicPlayerStatus.Playing;
			}
		}
	}

	public void Play(AudioClip iClip, float mVolum)
	{
		this.mVolume = mVolum;
		if (this.mAudioSource != null && this.mAudioSource.get_enabled())
		{
			if (this.IsPlaying)
			{
				this.status = AudioPlayer.MusicPlayerStatus.FadingOut;
				this.fadeStart = Time.get_time();
				this.nextclip = iClip;
			}
			else
			{
				this.volume = 0f;
				this.clip = iClip;
				this.mAudioSource.Play();
				this.status = AudioPlayer.MusicPlayerStatus.FadingIn;
				this.fadeStart = Time.get_time();
			}
		}
	}

	public void PlayOneShot(float mVolum)
	{
		if (this.mAudioSource != null && this.mAudioSource.get_isActiveAndEnabled() && this.clip != null)
		{
			this.mAudioSource.PlayOneShot(this.clip, mVolum);
		}
	}

	public void PlayOneShot(AudioClip iClip, float mVolum)
	{
		if (this.mAudioSource != null && this.mAudioSource.get_isActiveAndEnabled())
		{
			this.mAudioSource.PlayOneShot(iClip, mVolum);
		}
	}

	public void Stop()
	{
		if (this.mAudioSource != null)
		{
			this.mAudioSource.Stop();
		}
	}

	private void SetFade(AudioClip iClip, AudioPlayer.MusicPlayerStatus playerStatus)
	{
		this.status = playerStatus;
		this.fadeStart = Time.get_time();
		this.nextclip = iClip;
	}
}
