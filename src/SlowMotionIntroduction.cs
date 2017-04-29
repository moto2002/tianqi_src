using System;
using UnityEngine;
using XEngineCommand;

public class SlowMotionIntroduction : MonoBehaviour
{
	protected void Awake()
	{
		EventDispatcher.AddListener<SlowMotionIntroductionCmd, string>("OnSlowMotionIntroductionEvent", new Callback<SlowMotionIntroductionCmd, string>(this.OnSlowMotionIntroductionEvent));
	}

	protected virtual void OnDestroy()
	{
		EventDispatcher.RemoveListener<SlowMotionIntroductionCmd, string>("OnSlowMotionIntroductionEvent", new Callback<SlowMotionIntroductionCmd, string>(this.OnSlowMotionIntroductionEvent));
	}

	private void OnSlowMotionIntroductionEvent(SlowMotionIntroductionCmd cmd, string icon)
	{
	}
}
