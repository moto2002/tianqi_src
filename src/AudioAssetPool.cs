using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;

public class AudioAssetPool
{
	private static Dictionary<int, AudioAssetData> mAudioClipCache = new Dictionary<int, AudioAssetData>();

	private static List<string> mAudioClipPaths = new List<string>();

	private static Queue<AudioRequest> mSEQueue = new Queue<AudioRequest>();

	public static void LoadAudioAsset(int templateId, Action<bool> finish_callback)
	{
		if (AudioAssetPool.mAudioClipCache.ContainsKey(templateId) && AudioAssetPool.mAudioClipCache.get_Item(templateId) != null)
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(true);
			}
			return;
		}
		Audio dataAudio = DataReader<Audio>.Get(templateId);
		string path = AudioAssetPool.GetPath(dataAudio, templateId);
		if (string.IsNullOrEmpty(path))
		{
			if (finish_callback != null)
			{
				finish_callback.Invoke(false);
			}
			return;
		}
		AssetManager.AssetOfNoPool.LoadAsset(path, typeof(Object), delegate(Object obj)
		{
			if (!AudioAssetPool.mAudioClipPaths.Contains(path))
			{
				AudioAssetPool.mAudioClipPaths.Add(path);
			}
			AudioClip iClip = obj as AudioClip;
			AudioAssetPool.PushToPool(templateId, iClip, dataAudio.mode == 1, dataAudio.volumeSize);
			if (finish_callback != null)
			{
				finish_callback.Invoke(true);
			}
		});
	}

	public static AudioAssetData GetAudioAssetNow(int templateId)
	{
		if (AudioAssetPool.mAudioClipCache.ContainsKey(templateId))
		{
			return AudioAssetPool.mAudioClipCache.get_Item(templateId);
		}
		Audio audio = DataReader<Audio>.Get(templateId);
		string path = AudioAssetPool.GetPath(audio, templateId);
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}
		if (!AudioAssetPool.mAudioClipPaths.Contains(path))
		{
			AudioAssetPool.mAudioClipPaths.Add(path);
		}
		AudioClip iClip = AssetManager.AssetOfNoPool.LoadAssetNow(path, typeof(Object)) as AudioClip;
		return AudioAssetPool.PushToPool(templateId, iClip, audio.mode == 1, audio.volumeSize);
	}

	private static AudioAssetData PushToPool(int iAssetID, AudioClip iClip, bool iAdd, float iVolumeScale)
	{
		AudioAssetData audioAssetData = new AudioAssetData
		{
			assetID = iAssetID,
			audio = iClip,
			isAdditional = iAdd,
			volumeScale = iVolumeScale
		};
		AudioAssetPool.mAudioClipCache.set_Item(iAssetID, audioAssetData);
		return audioAssetData;
	}

	private static string GetPath(Audio dataAudio, int templateId)
	{
		if (dataAudio == null)
		{
			Debug.LogError("Audio表找不到, Id = " + templateId);
			return null;
		}
		return dataAudio.path.Trim();
	}

	public static void EnqueueAudioRequest(AudioPlayer apTarget, AudioAssetData aad)
	{
		Queue<AudioRequest> queue = AudioAssetPool.mSEQueue;
		lock (queue)
		{
			AudioRequest audioRequest = new AudioRequest
			{
				iPlayer = apTarget,
				iCache = aad
			};
			AudioAssetPool.mSEQueue.Enqueue(audioRequest);
		}
	}

	public static AudioRequest DequeueAudioRequest()
	{
		if (AudioAssetPool.mSEQueue.get_Count() != 0)
		{
			return AudioAssetPool.mSEQueue.Dequeue();
		}
		return null;
	}

	public static void ClearAll()
	{
		for (int i = 0; i < AudioAssetPool.mAudioClipPaths.get_Count(); i++)
		{
			AssetLoader.UnloadAsset(AudioAssetPool.mAudioClipPaths.get_Item(i), null);
		}
		AudioAssetPool.mAudioClipPaths.Clear();
		AudioAssetPool.mAudioClipCache.Clear();
		AudioAssetPool.mSEQueue.Clear();
	}
}
