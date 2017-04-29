using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class SoundManager : MonoBehaviour
{
	private const float BGM_VOLUMN_WEAKEN = 0.4f;

	private const float BGM_FADE_DURATION = 2f;

	private AudioListener mAudioListener;

	private AudioSource mBGMSource;

	private AudioSource mUISource;

	private AudioSource mNPCSource;

	[Range(0f, 1f)]
	public float bgmVolumeScale;

	[Range(0f, 1f)]
	public float uimVolumeScale;

	[Range(0f, 1f)]
	public float playerVolumeScale;

	[Range(0f, 1f)]
	public float npcVolumeScale = 1f;

	private float currentBGVolum;

	private float currentUIVolum;

	private float minDistance;

	private float maxDistance;

	private float fadeOut;

	private float fadeIn;

	private static SoundManager mInstance;

	private bool mIsPlayerSoundOn = true;

	private AudioClip mNowBGMClip;

	private float mBGMFadeVolumeBegin;

	private float mBGMFadeVolumeEnd;

	private bool mIsBGMFadeInOuting;

	private float time;

	private int circulation;

	private Dictionary<long, AudioPlayer> SoundPlayer = new Dictionary<long, AudioPlayer>();

	private List<long> SoundPlayerMute = new List<long>();

	private float pitchPlayer = 1f;

	private long addObjID;

	public bool NPCIsGuideAudio;

	private int mLastNpcAudioId;

	private bool mCanPlaySame;

	private uint mDelayId;

	private bool mVoiceTalkOn = true;

	public float MinDistance
	{
		get
		{
			return this.minDistance;
		}
	}

	public float MaxDistance
	{
		get
		{
			return this.maxDistance;
		}
	}

	public float FadeOut
	{
		get
		{
			return this.fadeOut;
		}
	}

	public float FadeIn
	{
		get
		{
			return this.fadeIn;
		}
	}

	public static SoundManager Instance
	{
		get
		{
			if (SoundManager.mInstance == null)
			{
				SoundManager.CreateInstance();
			}
			return SoundManager.mInstance;
		}
	}

	public static SoundManager CreateInstance()
	{
		if (SoundManager.mInstance == null && CamerasMgr.CameraMain != null)
		{
			GameObject gameObject = new GameObject("AddListener");
			SoundManager.mInstance = gameObject.AddComponent<SoundManager>();
			gameObject.get_transform().set_parent(CamerasMgr.CameraMain.get_transform());
		}
		return SoundManager.mInstance;
	}

	public void SetAddListenerPos(Vector3 pos)
	{
		Transform transform = CamerasMgr.CameraMain.get_transform().FindChild("AddListener");
		if (transform != null)
		{
			transform.set_position(pos);
		}
	}

	private void Awake()
	{
		if (SoundManager.mInstance != null)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			Object.DontDestroyOnLoad(this);
			SoundManager.mInstance = this;
			this.mNowBGMClip = null;
			this.mBGMSource = base.get_gameObject().AddComponent<AudioSource>();
			this.mUISource = base.get_gameObject().AddComponent<AudioSource>();
			this.mNPCSource = base.get_gameObject().AddComponent<AudioSource>();
			this.mBGMSource.set_loop(true);
			this.mAudioListener = base.GetComponent<AudioListener>();
			this.currentBGVolum = (this.bgmVolumeScale = float.Parse(DataReader<GlobalParams>.Get("audio_bgm_f").value));
			this.playerVolumeScale = float.Parse(DataReader<GlobalParams>.Get("audio_se_f").value);
			this.minDistance = float.Parse(DataReader<GlobalParams>.Get("soundFadeMinDistance").value);
			this.maxDistance = float.Parse(DataReader<GlobalParams>.Get("soundFadeMaxDistance").value);
			this.fadeIn = 0.1f;
			this.fadeOut = 0f;
			this.uimVolumeScale = 1f;
			this.TurnOnOff2Music(SystemConfig.IsMusicOn);
			this.TurnOnOff2Sound(SystemConfig.IsMusicOn);
		}
	}

	private void Update()
	{
		this.PlayPlayerDequeue();
		this.BGMFadeInOutInterval();
		if (this.NPCIsGuideAudio && this.mNPCSource != null && !this.mNPCSource.get_isPlaying())
		{
			SoundManager.SetBGMFade(true);
			this.NPCIsGuideAudio = false;
		}
	}

	private void OnDestroy()
	{
		Object.Destroy(this.mAudioListener.get_gameObject());
	}

	public void TurnOnOff2Music(bool isOn)
	{
		SystemConfig.IsMusicOn = isOn;
		this.SetBGMMute(!isOn, false);
		this.SetNPCMute(!isOn);
	}

	public void TurnOnOff2Sound(bool on)
	{
		SystemConfig.IsSoundOn = on;
		this.SetUIMute(!on);
		this.mIsPlayerSoundOn = on;
	}

	public static void TurnOnOff2Player(bool isOn)
	{
		if (SoundManager.mInstance != null)
		{
			SoundManager.mInstance.JustTurnOnOff2Player(isOn);
		}
	}

	private void JustTurnOnOff2Player(bool isOn)
	{
		if (isOn)
		{
			this.mIsPlayerSoundOn = SystemConfig.IsMusicOn;
			return;
		}
		this.mIsPlayerSoundOn = false;
		using (Dictionary<long, AudioPlayer>.ValueCollection.Enumerator enumerator = this.SoundPlayer.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AudioPlayer current = enumerator.get_Current();
				current.Stop();
			}
		}
	}

	public void SetBGMMute(bool bMute, bool isFromVoice)
	{
		if (this.IsBGMMute() == bMute)
		{
			return;
		}
		if (this.mBGMSource != null)
		{
			this.mBGMSource.set_mute(bMute);
			if (!isFromVoice)
			{
				this.mBGMSource.set_enabled(!bMute);
				if (!bMute)
				{
					MySceneManager.Instance.PlayBGM();
				}
			}
		}
	}

	public bool IsBGMMute()
	{
		return this.mBGMSource != null && this.mBGMSource.get_mute();
	}

	public float GetBGMVolume()
	{
		if (this.mBGMSource != null)
		{
			return this.mBGMSource.get_volume();
		}
		return 0f;
	}

	public void PauseBGM()
	{
		if (this.mBGMSource != null)
		{
			this.mBGMSource.Pause();
		}
	}

	public void SetBGMPitch(float pitch)
	{
		pitch = Mathf.Max(Mathf.Min(pitch, 3f), -3f);
		this.mBGMSource.set_pitch(pitch);
	}

	public static void SetBGMVolume(float Volume)
	{
		if (SoundManager.mInstance != null)
		{
			SoundManager.mInstance.BGMVolume(Volume);
		}
	}

	public void BGMVolume(float Volume)
	{
		Volume = Mathf.Max(Mathf.Min(Volume, 1f), 0f);
		this.mBGMSource.set_volume(Volume);
	}

	public void SetBGMVolumeScale(float iScale)
	{
		this.bgmVolumeScale = iScale;
		this.BGMVolume(this.bgmVolumeScale);
	}

	public void PlayBGMByID(int templateId)
	{
		if (!this.IsBGMOn())
		{
			return;
		}
		if (this.mBGMSource == null || this.mBGMSource.get_mute())
		{
			return;
		}
		if (templateId <= 0)
		{
			return;
		}
		AudioAssetData audioAssetNow = AudioAssetPool.GetAudioAssetNow(templateId);
		if (audioAssetNow != null)
		{
			this.bgmVolumeScale = audioAssetNow.volumeScale;
			this.currentBGVolum = this.bgmVolumeScale;
			this.PlayBGM(audioAssetNow.audio, 0.8f, null);
		}
	}

	private void PlayBGM(AudioClip iClip, float iSoomthSpeed = 2f, Action iFinishCallback = null)
	{
		if (iClip == null)
		{
			return;
		}
		this.mBGMSource.set_volume(0f);
		this.mBGMSource.set_clip(iClip);
		this.mBGMSource.set_loop(true);
		this.mBGMSource.Stop();
		this.mBGMSource.Play();
		this.mNowBGMClip = this.mBGMSource.get_clip();
		base.StartCoroutine(this.FadinAudio(this.mBGMSource, this.mNowBGMClip, this.bgmVolumeScale, iSoomthSpeed, iFinishCallback));
	}

	private bool IsBGMOn()
	{
		return SystemConfig.IsMusicOn;
	}

	public void StopBGM(Action ac = null)
	{
		this.StopBGM(5f, ac);
	}

	private void StopBGM(float iSoomthSpeed = 2f, Action iFinishCallback = null)
	{
		if (!this.IsBGMOn() || this.mBGMSource.get_clip() == null)
		{
			if (iFinishCallback != null)
			{
				iFinishCallback.Invoke();
			}
			return;
		}
		base.StartCoroutine(this.FadoutAudio(this.mBGMSource, iSoomthSpeed, iFinishCallback));
	}

	[DebuggerHidden]
	private IEnumerator FadoutAudio(AudioSource iAudioSource, float iSpeed = 1f, Action iFinishCallback = null)
	{
		SoundManager.<FadoutAudio>c__Iterator63 <FadoutAudio>c__Iterator = new SoundManager.<FadoutAudio>c__Iterator63();
		<FadoutAudio>c__Iterator.iAudioSource = iAudioSource;
		<FadoutAudio>c__Iterator.iSpeed = iSpeed;
		<FadoutAudio>c__Iterator.iFinishCallback = iFinishCallback;
		<FadoutAudio>c__Iterator.<$>iAudioSource = iAudioSource;
		<FadoutAudio>c__Iterator.<$>iSpeed = iSpeed;
		<FadoutAudio>c__Iterator.<$>iFinishCallback = iFinishCallback;
		return <FadoutAudio>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator FadinAudio(AudioSource iAudioSource, AudioClip iClip, float iVolume = 1f, float iSpeed = 1f, Action iFinishCallback = null)
	{
		SoundManager.<FadinAudio>c__Iterator64 <FadinAudio>c__Iterator = new SoundManager.<FadinAudio>c__Iterator64();
		<FadinAudio>c__Iterator.iVolume = iVolume;
		<FadinAudio>c__Iterator.iAudioSource = iAudioSource;
		<FadinAudio>c__Iterator.iSpeed = iSpeed;
		<FadinAudio>c__Iterator.iFinishCallback = iFinishCallback;
		<FadinAudio>c__Iterator.<$>iVolume = iVolume;
		<FadinAudio>c__Iterator.<$>iAudioSource = iAudioSource;
		<FadinAudio>c__Iterator.<$>iSpeed = iSpeed;
		<FadinAudio>c__Iterator.<$>iFinishCallback = iFinishCallback;
		return <FadinAudio>c__Iterator;
	}

	public static void SetBGMFade(bool isIn)
	{
		if (SoundManager.mInstance != null)
		{
			SoundManager.mInstance.BGMFade(isIn);
		}
	}

	private void BGMFade(bool isIn)
	{
		if (isIn)
		{
			this.BGMFadeInOut(this.bgmVolumeScale, this.currentBGVolum);
		}
		else
		{
			this.BGMFadeInOut(this.bgmVolumeScale, 0.4f);
		}
	}

	private void BGMFadeInOut(float volume_begin, float volume_end)
	{
		this.mBGMFadeVolumeBegin = volume_begin;
		this.mBGMFadeVolumeEnd = volume_end;
		this.mIsBGMFadeInOuting = true;
	}

	private void BGMFadeInOutInterval()
	{
		if (this.mIsBGMFadeInOuting)
		{
			if (this.time < 2f)
			{
				this.time += Time.get_deltaTime();
				this.SetBGMVolumeScale(Mathf.Lerp(this.mBGMFadeVolumeBegin, this.mBGMFadeVolumeEnd, this.time / 2f));
			}
			else
			{
				this.SetBGMVolumeScale(this.mBGMFadeVolumeEnd);
				this.time = 0f;
				this.mIsBGMFadeInOuting = false;
			}
		}
	}

	public void SetUIMute(bool bMute)
	{
		if (this.mUISource != null)
		{
			this.mUISource.set_mute(bMute);
			this.mUISource.set_enabled(!bMute);
		}
	}

	public bool IsUIMute()
	{
		return this.mUISource.get_mute();
	}

	public float GetUIVolume()
	{
		return this.mUISource.get_volume();
	}

	public void SetUIVolume(float Volume)
	{
		Volume = Mathf.Max(Mathf.Min(Volume, 1f), 0f);
		this.mUISource.set_volume(Volume);
	}

	public void SetUIVolumeScale(float iScale)
	{
		this.currentUIVolum = iScale;
		this.uimVolumeScale = this.currentUIVolum;
		this.SetUIVolume(this.uimVolumeScale);
	}

	public void PauseUI()
	{
		this.mUISource.Pause();
	}

	public void StopUI()
	{
		if (this.mUISource.get_isPlaying())
		{
			this.mUISource.Stop();
		}
	}

	public void SetUIPitch(float pitch)
	{
		pitch = Mathf.Max(Mathf.Min(pitch, 3f), -3f);
		this.mUISource.set_pitch(pitch);
	}

	public static void PlayUI(GameObject goWidget)
	{
		if (SoundManager.mInstance == null)
		{
			return;
		}
		SoundManager.mInstance.DoPlayUI(goWidget);
	}

	public static void PlayUI(int templateId, bool loop = false)
	{
		if (SoundManager.mInstance == null)
		{
			return;
		}
		SoundManager.mInstance.DoPlayUI(templateId, loop);
	}

	private void DoPlayUI(GameObject goWidget)
	{
		if (this.mUISource == null || this.mUISource.get_mute())
		{
			return;
		}
		if (goWidget == null)
		{
			return;
		}
		string name = goWidget.get_name();
		string uiName = string.Empty;
		UIBase targetParent = UGUITools.GetTargetParent<UIBase>(goWidget);
		if (targetParent != null)
		{
			uiName = targetParent.prefabName;
		}
		int audioId = AudioIdManager.GetAudioId(uiName, name);
		if (audioId != 0)
		{
			if (audioId > 0)
			{
				SoundManager.PlayUI(audioId, false);
			}
			else
			{
				SoundManager.PlayUI(10033, false);
			}
		}
	}

	private void DoPlayUI(int templateId, bool loop = false)
	{
		if (templateId == 0)
		{
			return;
		}
		if (!this.IsUIOn())
		{
			return;
		}
		if (this.mUISource == null || this.mUISource.get_mute())
		{
			return;
		}
		if (templateId < 0)
		{
			Debug.Log("<color=red>Error:</color>ID不合法:" + templateId);
			return;
		}
		AudioAssetData audioAssetNow = AudioAssetPool.GetAudioAssetNow(templateId);
		if (audioAssetNow != null)
		{
			this.uimVolumeScale = audioAssetNow.volumeScale;
			this.PlayUI(audioAssetNow.audio, audioAssetNow.isAdditional, loop);
		}
	}

	public void CirculationPlayUI(int templateId, bool loop = false)
	{
		if (this.mUISource == null || this.mUISource.get_mute())
		{
			return;
		}
		if (templateId == this.circulation && this.mUISource.get_isPlaying())
		{
			return;
		}
		if (!this.mUISource.get_isPlaying())
		{
			this.circulation = 0;
		}
		SoundManager.PlayUI(templateId, loop);
		this.circulation = templateId;
	}

	private void PlayUI(AudioClip iClip, bool iOneShot = false, bool loop = false)
	{
		if (this.mUISource == null || this.mUISource.get_mute())
		{
			return;
		}
		this.mUISource.set_volume(this.uimVolumeScale);
		this.mUISource.set_loop(loop);
		if (iOneShot)
		{
			this.mUISource.PlayOneShot(iClip);
		}
		else
		{
			if (this.mUISource.get_isPlaying())
			{
				this.mUISource.Stop();
			}
			this.mUISource.set_clip(iClip);
			this.mUISource.Play();
		}
	}

	private bool IsUIOn()
	{
		return SystemConfig.IsMusicOn;
	}

	public void SetPlayerPitch(float pitch)
	{
		this.pitchPlayer = pitch;
		using (Dictionary<long, AudioPlayer>.Enumerator enumerator = this.SoundPlayer.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, AudioPlayer> current = enumerator.get_Current();
				if (current.get_Value() != null && current.get_Value().IsPlaying)
				{
					current.get_Value().pitch = pitch;
				}
			}
		}
	}

	public void SetPlayerMute(long id, bool isMute)
	{
		if (this.SoundPlayer.ContainsKey(id))
		{
			this.SoundPlayerMute.Remove(id);
			if (isMute)
			{
				this.SoundPlayerMute.Add(id);
			}
			if (!this.mVoiceTalkOn)
			{
				this.DoSetPlayerMute(id, isMute);
			}
			else
			{
				this.DoSetPlayerMute(id, isMute);
			}
		}
	}

	private void SetPlayerMuteByVoice(bool isMute)
	{
		using (Dictionary<long, AudioPlayer>.Enumerator enumerator = this.SoundPlayer.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, AudioPlayer> current = enumerator.get_Current();
				this.DoSetPlayerMute(current.get_Key(), isMute);
			}
		}
	}

	private void DoSetPlayerMute(long id, bool isMute)
	{
		if (this.SoundPlayer.ContainsKey(id))
		{
			AudioPlayer audioPlayer = this.SoundPlayer.get_Item(id);
			if (audioPlayer != null)
			{
				audioPlayer.Mute = isMute;
			}
		}
	}

	public void PlayPlayer(AudioPlayer iPlayer, int templateId)
	{
		if (!this.IsPlayerOn())
		{
			return;
		}
		if (iPlayer != null && templateId > 0)
		{
			if (iPlayer.RoleId == 0L)
			{
				this.addObjID += 1L;
				iPlayer.RoleId = this.addObjID;
			}
			AudioAssetPool.LoadAudioAsset(templateId, delegate(bool isSuccess)
			{
				if (isSuccess)
				{
					if (!this.IsPlayerOn())
					{
						return;
					}
					if (iPlayer == null)
					{
						return;
					}
					AudioAssetData audioAssetNow = AudioAssetPool.GetAudioAssetNow(templateId);
					if (audioAssetNow == null)
					{
						return;
					}
					if (this.SoundPlayer.ContainsKey(iPlayer.RoleId))
					{
						iPlayer = this.SoundPlayer.get_Item(iPlayer.RoleId);
					}
					else
					{
						this.SoundPlayer.Add(iPlayer.RoleId, iPlayer);
						iPlayer.playerEnd = delegate
						{
							this.ClearPlayer(iPlayer.RoleId);
						};
					}
					iPlayer.loop = false;
					iPlayer.playOnAwake = false;
					iPlayer.pitch = this.pitchPlayer;
					this.PlayPlayerEnqueue(iPlayer, audioAssetNow, 1f);
				}
			});
		}
	}

	private void PlayPlayerEnqueue(AudioPlayer iTarget, AudioAssetData aad, float iVolumeScale = 1f)
	{
		if (iTarget != null)
		{
			AudioAssetPool.EnqueueAudioRequest(iTarget, aad);
		}
	}

	private void PlayPlayerDequeue()
	{
		AudioRequest audioRequest = AudioAssetPool.DequeueAudioRequest();
		if (audioRequest != null)
		{
			this.PlayPlayerNow(audioRequest);
		}
	}

	private void PlayPlayerNow(AudioRequest iRequest)
	{
		if (!this.IsPlayerOn())
		{
			return;
		}
		if (iRequest != null && iRequest.iPlayer != null && iRequest.iCache != null)
		{
			AudioPlayer iPlayer = iRequest.iPlayer;
			AudioAssetData iCache = iRequest.iCache;
			if (iCache.isAdditional)
			{
				iPlayer.PlayOneShot(iCache.audio, iCache.volumeScale);
			}
			else
			{
				iPlayer.Play(iCache.audio, iCache.volumeScale);
			}
		}
	}

	public void StopPlayer(long RoleId)
	{
		if (this.SoundPlayer.ContainsKey(RoleId))
		{
			this.StopPlayer(this.SoundPlayer.get_Item(RoleId));
		}
	}

	private void StopPlayer(AudioPlayer iAudioPlayer)
	{
		if (iAudioPlayer != null)
		{
			iAudioPlayer.Stop();
		}
	}

	public void ClearPlayer(long RoleId)
	{
		if (this.SoundPlayer.ContainsKey(RoleId))
		{
			this.StopPlayer(this.SoundPlayer.get_Item(RoleId));
			this.SoundPlayer.Remove(RoleId);
		}
	}

	private bool IsPlayerOn()
	{
		return SystemConfig.IsMusicOn && this.mIsPlayerSoundOn && this.mVoiceTalkOn;
	}

	public void SetNPCMute(bool bMute)
	{
		if (this.IsNPCMute() == bMute)
		{
			return;
		}
		if (this.mNPCSource != null)
		{
			this.mNPCSource.set_mute(bMute);
		}
	}

	public bool IsNPCMute()
	{
		return this.mNPCSource != null && this.mNPCSource.get_mute();
	}

	public void PlayNPC(int npcId)
	{
		if (this.mNPCSource != null && this.mNPCSource.get_isPlaying() && this.NPCIsGuideAudio)
		{
			return;
		}
		NPC nPC = DataReader<NPC>.Get(npcId);
		if (nPC != null && nPC.sound != null && nPC.sound.get_Count() > 0 && (this.mCanPlaySame || this.mLastNpcAudioId != nPC.sound.get_Item(0)))
		{
			this.mLastNpcAudioId = nPC.sound.get_Item(0);
			this.PlayNpcAudio(this.mLastNpcAudioId);
			Debug.Log(string.Concat(new object[]
			{
				"播放NPC[",
				nPC.id,
				"]语音:",
				this.mLastNpcAudioId
			}));
			uint start = 10000u;
			PaoHuanRenWuPeiZhi paoHuanRenWuPeiZhi = DataReader<PaoHuanRenWuPeiZhi>.Get("intervalTime");
			if (paoHuanRenWuPeiZhi != null)
			{
				start = (uint)float.Parse(paoHuanRenWuPeiZhi.value);
			}
			if (this.mDelayId > 0u)
			{
				TimerHeap.DelTimer(this.mDelayId);
			}
			this.mDelayId = TimerHeap.AddTimer(start, 0, new Action(this.DelayNpcPlay));
			this.mCanPlaySame = false;
		}
	}

	public void PlayNpcAudio(int audioId)
	{
		if (!this.IsNPCOn())
		{
			return;
		}
		if (this.mNPCSource == null || this.mNPCSource.get_mute())
		{
			return;
		}
		if (audioId <= 0)
		{
			Debug.Log("<color=red>Error:</color>ID[" + audioId + "]不合法!");
			return;
		}
		AudioAssetData audioAssetNow = AudioAssetPool.GetAudioAssetNow(audioId);
		if (audioAssetNow != null)
		{
			this.npcVolumeScale = audioAssetNow.volumeScale;
			this.PlayNPC(audioAssetNow.audio, audioAssetNow.isAdditional, false);
		}
		else
		{
			Debug.Log("<color=red>Error:</color>找不到声音资源[" + audioId + "]");
		}
	}

	public void StopNPCAudio()
	{
		if (this.mNPCSource != null && this.mNPCSource.get_isPlaying())
		{
			this.mNPCSource.Stop();
		}
	}

	private void PlayNPC(AudioClip iClip, bool iOneShot = false, bool loop = false)
	{
		if (this.mNPCSource == null || this.mNPCSource.get_mute())
		{
			return;
		}
		this.mNPCSource.set_volume(this.npcVolumeScale);
		this.mNPCSource.set_loop(loop);
		if (this.mNPCSource.get_isPlaying())
		{
			this.mNPCSource.Stop();
		}
		if (iOneShot)
		{
			this.mNPCSource.PlayOneShot(iClip);
		}
		else
		{
			this.mNPCSource.set_clip(iClip);
			this.mNPCSource.Play();
		}
	}

	private bool IsNPCOn()
	{
		return SystemConfig.IsMusicOn;
	}

	private void DelayNpcPlay()
	{
		this.mCanPlaySame = true;
		this.mLastNpcAudioId = 0;
		if (this.mDelayId > 0u)
		{
			TimerHeap.DelTimer(this.mDelayId);
		}
		this.mDelayId = 0u;
	}

	public void TurnOnOff2VoiceTalk(bool isOn)
	{
		this.mVoiceTalkOn = isOn;
		if (!SystemConfig.IsMusicOn)
		{
			this.mVoiceTalkOn = false;
		}
		this.SetBGMMute(!this.mVoiceTalkOn, true);
		this.SetUIMute(!this.mVoiceTalkOn);
		this.SetPlayerMuteByVoice(!this.mVoiceTalkOn);
		this.SetNPCMute(!this.mVoiceTalkOn);
	}
}
