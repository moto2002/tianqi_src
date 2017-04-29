using System;
using System.IO;
using UnityEngine;

public class VoiceSDKManager : MonoBehaviour
{
	private const string SPEECH_PREFIX = "sp_";

	public const string SPEECH_RECORD_NAME = "sp_ro";

	private static VoiceSDKManager instance;

	private bool mIsWaittingOfRecordFinished;

	private Action mRecordFinished;

	private bool mIsInPlaying;

	private Action m_actionStop;

	public static VoiceSDKManager Instance
	{
		get
		{
			if (VoiceSDKManager.instance == null)
			{
				VoiceSDKManager.instance = NativeCallManager.UnityNativeBridgeRoot.AddComponent<VoiceSDKManager>();
				Object.DontDestroyOnLoad(VoiceSDKManager.instance);
			}
			return VoiceSDKManager.instance;
		}
	}

	private void Update()
	{
		if (this.mIsWaittingOfRecordFinished && NativeCallManager.Native_RecordFinished())
		{
			Debug.Log("***mIsWaittingOfRecordFinished finished!!!");
			this.mIsWaittingOfRecordFinished = false;
			if (this.mRecordFinished != null)
			{
				this.mRecordFinished.Invoke();
			}
		}
		if (this.mIsInPlaying && NativeCallManager.Native_IsPlayRecordingFinished())
		{
			Debug.Log("***speech play finished!!!");
			this.SpeechStopPlayWithResetMusic();
		}
	}

	public void Init()
	{
	}

	public void SpeechRecordStart()
	{
		SoundManager.Instance.TurnOnOff2VoiceTalk(false);
		this.SpeechStopPlay();
		string filePath = VoiceSDKManager.GetFilePath("sp_ro");
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
		NativeCallManager.Native_StartRecording(VoiceSDKManager.GetFilePath("sp_ro"));
	}

	public void SpeechRecordStop(Action recordFinished = null)
	{
		this.mIsWaittingOfRecordFinished = true;
		this.mRecordFinished = recordFinished;
		NativeCallManager.Native_StopRecording();
		SoundManager.Instance.TurnOnOff2VoiceTalk(true);
	}

	public byte[] GetSpeech()
	{
		return VoiceSDKManager.ReadBytes(VoiceSDKManager.GetFilePath("sp_ro"));
	}

	public void SpeechRecordCancel()
	{
		NativeCallManager.Native_CancelRecording();
		SoundManager.Instance.TurnOnOff2VoiceTalk(true);
	}

	public void SpeechPlay(byte[] msg, long uuid, Action actionStop)
	{
		if (msg.Length <= 10)
		{
			if (actionStop != null)
			{
				actionStop.Invoke();
			}
			return;
		}
		this.SpeechStopPlay();
		this.m_actionStop = actionStop;
		SoundManager.Instance.TurnOnOff2VoiceTalk(false);
		Debug.Log("***speech play, length = " + msg.Length);
		string unique_name = "sp_" + uuid;
		VoiceSDKManager.WriteBytes(VoiceSDKManager.GetFilePath(unique_name), msg);
		this.mIsInPlaying = true;
		NativeCallManager.Native_PlayRecording(VoiceSDKManager.GetFilePath(unique_name));
	}

	public void SpeechStopPlay()
	{
		this.mIsInPlaying = false;
		NativeCallManager.Native_StopPlayRecording();
		if (this.m_actionStop != null)
		{
			this.m_actionStop.Invoke();
			this.m_actionStop = null;
		}
	}

	public void SpeechStopPlayWithResetMusic()
	{
		this.SpeechStopPlay();
		SoundManager.Instance.TurnOnOff2VoiceTalk(true);
	}

	public int GetVolumn()
	{
		double num = NativeCallManager.Native_GetVolume();
		if (num <= 0.0)
		{
			return 0;
		}
		if (num >= 0.0 && num <= 20.0)
		{
			return 1;
		}
		if (num > 20.0 && num <= 40.0)
		{
			return 2;
		}
		if (num > 40.0 && num <= 60.0)
		{
			return 3;
		}
		if (num > 60.0 && num <= 80.0)
		{
			return 4;
		}
		if (num > 80.0 && num <= 90.0)
		{
			return 5;
		}
		return 6;
	}

	public static byte[] ReadBytes(string filePath)
	{
		if (File.Exists(filePath))
		{
			return File.ReadAllBytes(filePath);
		}
		return null;
	}

	public static void WriteBytes(string filePath, byte[] bytes)
	{
		File.WriteAllBytes(filePath, bytes);
	}

	public static string GetFilePath(string unique_name)
	{
		return Application.get_persistentDataPath() + "/" + unique_name + ".spx";
	}

	public void SpeechPlay(string name)
	{
		Debug.LogError("==>Voice: SpeechPlay file name = " + name);
		if (string.IsNullOrEmpty(name))
		{
			name = "sp_ro";
		}
		Debug.LogError("==>Voice: SpeechPlay file exist is " + File.Exists(VoiceSDKManager.GetFilePath(name)));
		NativeCallManager.Native_PlayRecording(VoiceSDKManager.GetFilePath(name));
	}
}
