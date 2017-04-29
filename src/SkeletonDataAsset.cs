using Spine;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class SkeletonDataAsset : ScriptableObject
{
	public AtlasAsset[] atlasAssets;

	public TextAsset skeletonJSON;

	public float scale = 1f;

	public string[] fromAnimation;

	public string[] toAnimation;

	public float[] duration;

	public float defaultMix;

	public RuntimeAnimatorController controller;

	private SkeletonData skeletonData;

	private AnimationStateData stateData;

	private void OnEnable()
	{
		if (this.atlasAssets == null)
		{
			this.atlasAssets = new AtlasAsset[0];
		}
	}

	public void Reset()
	{
		this.skeletonData = null;
		this.stateData = null;
	}

	public SkeletonData GetSkeletonData(bool quiet)
	{
		if (this.atlasAssets == null)
		{
			this.atlasAssets = new AtlasAsset[0];
			if (!quiet)
			{
				Debug.LogError("Atlas not set for SkeletonData asset: " + base.get_name(), this);
			}
			this.Reset();
			return null;
		}
		if (this.skeletonJSON == null)
		{
			if (!quiet)
			{
				Debug.LogError("Skeleton JSON file not set for SkeletonData asset: " + base.get_name(), this);
			}
			this.Reset();
			return null;
		}
		if (this.atlasAssets.Length == 0)
		{
			this.Reset();
			return null;
		}
		Atlas[] array = new Atlas[this.atlasAssets.Length];
		for (int i = 0; i < this.atlasAssets.Length; i++)
		{
			if (this.atlasAssets[i] == null)
			{
				this.Reset();
				return null;
			}
			array[i] = this.atlasAssets[i].GetAtlas();
			if (array[i] == null)
			{
				this.Reset();
				return null;
			}
		}
		if (this.skeletonData != null)
		{
			return this.skeletonData;
		}
		AttachmentLoader attachmentLoader = new AtlasAttachmentLoader(array);
		float num = this.scale;
		try
		{
			if (this.skeletonJSON.get_name().ToLower().Contains(".skel"))
			{
				MemoryStream input = new MemoryStream(this.skeletonJSON.get_bytes());
				this.skeletonData = new SkeletonBinary(attachmentLoader)
				{
					Scale = num
				}.ReadSkeletonData(input);
			}
			else
			{
				StringReader reader = new StringReader(this.skeletonJSON.get_text());
				this.skeletonData = new SkeletonJson(attachmentLoader)
				{
					Scale = num
				}.ReadSkeletonData(reader);
			}
		}
		catch (Exception ex)
		{
			if (!quiet)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Error reading skeleton JSON file for SkeletonData asset: ",
					base.get_name(),
					"\n",
					ex.get_Message(),
					"\n",
					ex.get_StackTrace()
				}), this);
			}
			return null;
		}
		this.stateData = new AnimationStateData(this.skeletonData);
		this.FillStateData();
		return this.skeletonData;
	}

	[DebuggerHidden]
	public IEnumerator GetSkeletonDataAsync(bool quiet, Action<SkeletonData> finishCallback)
	{
		SkeletonDataAsset.<GetSkeletonDataAsync>c__IteratorC <GetSkeletonDataAsync>c__IteratorC = new SkeletonDataAsset.<GetSkeletonDataAsync>c__IteratorC();
		<GetSkeletonDataAsync>c__IteratorC.quiet = quiet;
		<GetSkeletonDataAsync>c__IteratorC.finishCallback = finishCallback;
		<GetSkeletonDataAsync>c__IteratorC.<$>quiet = quiet;
		<GetSkeletonDataAsync>c__IteratorC.<$>finishCallback = finishCallback;
		<GetSkeletonDataAsync>c__IteratorC.<>f__this = this;
		return <GetSkeletonDataAsync>c__IteratorC;
	}

	public void FillStateData()
	{
		if (this.stateData == null)
		{
			return;
		}
		this.stateData.DefaultMix = this.defaultMix;
		if (this.fromAnimation == null)
		{
			return;
		}
		int i = 0;
		int num = this.fromAnimation.Length;
		while (i < num)
		{
			if (this.fromAnimation[i].get_Length() != 0 && this.toAnimation[i].get_Length() != 0)
			{
				this.stateData.SetMix(this.fromAnimation[i], this.toAnimation[i], this.duration[i]);
			}
			i++;
		}
	}

	public AnimationStateData GetAnimationStateData()
	{
		if (this.stateData != null)
		{
			return this.stateData;
		}
		this.GetSkeletonData(false);
		return this.stateData;
	}
}
