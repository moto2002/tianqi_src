using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class CameraPathListener : MonoBehaviour
{
	private Vector3 followCameraPosition;

	private Quaternion followCameraRotation;

	private void Part1Begin()
	{
		InstanceManager.IsPauseTimeEscape = true;
		CameraPathGlobal.instance.SetEntityVisible(false);
		CameraGlobal.cameraType = CameraType.Timeline;
		this.followCameraPosition = CamerasMgr.MainCameraRoot.get_position();
		this.followCameraRotation = CamerasMgr.MainCameraRoot.get_rotation();
	}

	private void Part1End()
	{
		Debug.LogError("Part1End");
		InstanceManager.IsPauseTimeEscape = false;
		CameraPathGlobal.instance.plotCallback.Invoke();
		CameraPathGlobal.instance.SetEntityVisible(true);
		CameraPathGlobal.instance.ClearNpcs();
		CameraGlobal.cameraType = CameraType.Follow;
		CamerasMgr.MainCameraRoot.set_position(this.followCameraPosition);
		CamerasMgr.MainCameraRoot.set_rotation(this.followCameraRotation);
	}

	private void Part2Begin()
	{
		InstanceManager.IsPauseTimeEscape = true;
		FXManager.Instance.DeleteFXs();
		CameraPathGlobal.instance.SetEntityVisible(false);
		Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 6);
		CameraGlobal.cameraType = CameraType.None;
	}

	private void Part2End()
	{
		Debug.LogError("Part2End");
		InstanceManager.IsPauseTimeEscape = false;
		CameraPathGlobal.instance.plotCallback.Invoke();
		CameraPathGlobal.instance.ClearNpcs();
		CameraPathGlobal.instance.SetEntityVisible(true);
		UIManagerControl.Instance.HideAll();
	}

	private void DoNpc(JuQingShiJian plotInfo)
	{
		ActorModel npc = CameraPathGlobal.instance.GetNpc(plotInfo.modelId, plotInfo.nameId);
		new CameraPathNpc(npc, plotInfo.action, new Vector3(plotInfo.position.get_Item(0), plotInfo.position.get_Item(1), plotInfo.position.get_Item(2)), new Vector3(plotInfo.positionTo.get_Item(0), plotInfo.positionTo.get_Item(1), plotInfo.positionTo.get_Item(2)), 0f, (float)plotInfo.towardAngle, plotInfo.moveTime);
	}

	private void DoDiolague(JuQingShiJian plotInfo)
	{
		base.get_gameObject().GetComponent<CameraPathAnimator>().Pause();
		MainTaskManager.Instance.OpenTalkUI(plotInfo.diolague, false, delegate
		{
			base.get_gameObject().GetComponent<CameraPathAnimator>().Play();
		}, 0);
	}

	private void DoSubtile(JuQingShiJian plotInfo)
	{
		List<string> list = new List<string>();
		using (List<int>.Enumerator enumerator = plotInfo.word.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				list.Add(GameDataUtils.GetChineseContent(current, false));
			}
		}
		base.get_gameObject().GetComponent<CameraPathAnimator>().Pause();
		SubtitlesUI subtitlesUI = UIManagerControl.Instance.OpenUI("SubtitlesUI", null, false, UIType.NonPush) as SubtitlesUI;
		subtitlesUI.Init(list, delegate
		{
			base.get_gameObject().GetComponent<CameraPathAnimator>().Play();
		});
	}

	private void DoPlay(JuQingShiJian plotInfo)
	{
		if (plotInfo.anime == -1)
		{
			CamerasMgr.OpenEye();
		}
		else
		{
			GuideManager.Instance.guide_lock = true;
			FXManager.Instance.PlayFXOfUI(plotInfo.anime, UINodesManager.MiddleUIRoot, 0, 3001, delegate
			{
				GuideManager.Instance.guide_lock = false;
			});
		}
	}

	private void DoSound(JuQingShiJian plotInfo)
	{
		SoundManager.Instance.PlayPlayer(base.get_gameObject().GetComponent<AudioPlayer>(), plotInfo.voice);
	}

	private void DoFx(JuQingShiJian plotInfo)
	{
		FXManager.Instance.PlayFXOfDisplay(plotInfo.fx, null, new Vector3(plotInfo.position.get_Item(0), plotInfo.position.get_Item(1), plotInfo.position.get_Item(2)), Quaternion.AngleAxis((float)plotInfo.towardAngle, Vector3.get_up()), 1f, 1f, 0, false, null, null);
	}

	private void DoDel(JuQingShiJian plotInfo)
	{
		if (plotInfo.function == 1)
		{
			CameraPathGlobal.instance.DelNpc(plotInfo.modelId, plotInfo.nameId);
		}
	}

	private void Do(JuQingShiJian plotInfo)
	{
		if (plotInfo.eventType == 2)
		{
			this.DoNpc(plotInfo);
		}
		else if (plotInfo.eventType == 3)
		{
			this.DoDiolague(plotInfo);
		}
		else if (plotInfo.eventType == 4)
		{
			this.DoSubtile(plotInfo);
		}
		else if (plotInfo.eventType == 5)
		{
			this.DoPlay(plotInfo);
		}
		else if (plotInfo.eventType == 6)
		{
			this.DoSound(plotInfo);
		}
		else if (plotInfo.eventType == 7)
		{
			this.DoFx(plotInfo);
		}
		else if (plotInfo.eventType == 8)
		{
			this.DoDel(plotInfo);
		}
		else
		{
			Debug.LogError("[ERROR] eventType=" + plotInfo.eventType);
		}
	}

	private void PlotBegin()
	{
		if (CameraPathGlobal.instance.plotIndex == 0)
		{
			this.Part1Begin();
		}
		else
		{
			this.Part2Begin();
		}
	}

	private void PlotEnd()
	{
		Debug.LogError("PlotEnd");
		if (CameraPathGlobal.instance.plotIndex == 0)
		{
			this.Part1End();
		}
		else
		{
			this.Part2End();
		}
	}

	private void Start()
	{
		this.PlotBegin();
		base.get_gameObject().GetComponent<CameraPathAnimator>().AnimationCustomEvent += delegate(string strArg)
		{
			Debug.LogError("AnimationCustomEvent=" + strArg);
			string[] array = strArg.Split(new char[]
			{
				';'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				JuQingShiJian juQingShiJian = DataReader<JuQingShiJian>.Get(int.Parse(text));
				if (juQingShiJian != null)
				{
					this.Do(juQingShiJian);
				}
			}
		};
		base.get_gameObject().GetComponent<CameraPathAnimator>().AnimationPointReachedWithNumberEvent += delegate(int arg)
		{
			Debug.LogError("AnimationPointReachedWithNumberEvent=" + arg);
		};
		base.get_gameObject().GetComponent<CameraPathAnimator>().AnimationFinishedEvent += delegate
		{
			CameraPath nextPath = base.get_gameObject().GetComponent<CameraPath>().nextPath;
			Debug.LogError("AnimationFinishedEvent nextPath=" + nextPath);
			if (nextPath != null)
			{
				nextPath.get_gameObject().GetComponent<CameraPathAnimator>().animationObject = CamerasMgr.MainCameraRoot;
				GameObject gameObject = Object.Instantiate<GameObject>(nextPath.get_gameObject());
				gameObject.SetActive(true);
			}
			else
			{
				this.PlotEnd();
			}
			Object.Destroy(base.get_gameObject());
		};
	}

	private void Update()
	{
	}
}
