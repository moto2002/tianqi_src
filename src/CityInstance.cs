using System;
using UnityEngine;

public class CityInstance : InstanceParent
{
	private static CityInstance instance;

	public bool hasEnteredCityBefore;

	public static CityInstance Instance
	{
		get
		{
			if (CityInstance.instance == null)
			{
				CityInstance.instance = new CityInstance();
			}
			return CityInstance.instance;
		}
	}

	public bool HasEnteredCityBefore
	{
		get
		{
			return this.hasEnteredCityBefore;
		}
		set
		{
			this.hasEnteredCityBefore = value;
		}
	}

	protected CityInstance()
	{
		base.Type = InstanceType.None;
	}

	public override void PreLoadData(int sceneID)
	{
		LoadingRes.ExtractCityUiPrefab();
		LoadingRes.ExtractCityFXSpines();
	}

	public override void SetCommonLogic()
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		bool flag = false;
		if (!this.HasEnteredCityBefore)
		{
			this.HasEnteredCityBefore = true;
			EventDispatcher.Broadcast("ReTriggerTaskOfLogin");
			if (Application.get_isMobilePlatform() && EntityWorld.Instance.EntSelf.Lv == 1)
			{
				flag = true;
			}
		}
		Action action = delegate
		{
			FXSpineManager.Instance.ShowBattleStart2(delegate
			{
				LoadingUIView.Close();
				TownUI.IsOpenAnimationOn = true;
				this.ShowTownUI();
				if (EntityWorld.Instance.EntSelf.IsNavigating)
				{
					EventDispatcher.Broadcast(EventNames.BeginNav);
				}
				MySceneManager.Instance.PlayBGM();
				EventDispatcher.BroadcastAsync(CityManagerEvent.EnteredCity);
			});
		};
		if (flag)
		{
			ClientApp.Instance.PlayCGMovie(action);
		}
		else
		{
			action.Invoke();
		}
	}

	protected void ShowTownUI()
	{
		if (SceneLoadedUIManager.Instance.IsFromBattleClick)
		{
			EventDispatcher.Broadcast(SceneLoadedUISetting.CurrentType);
			SceneLoadedUIManager.Instance.IsFromBattleClick = false;
		}
		else if ((!InstanceManager.IsLastInstanceWithTask || InstanceManager.LastInstanceType == InstanceType.Arena || InstanceManager.LastInstanceType == InstanceType.GangFight || InstanceManager.LastInstanceType == InstanceType.SurvivalChallenge || InstanceManager.LastInstanceType == InstanceType.Element || InstanceManager.LastInstanceType == InstanceType.Defence) && MySceneManager.Instance.LastSceneType == SceneType.Battle)
		{
			EventDispatcher.Broadcast("SHOW_PREVIOUS_UI");
		}
		else
		{
			EventDispatcher.Broadcast(SceneLoadedUISetting.CurrentType);
		}
		SceneLoadedUISetting.CurrentType = "SHOW_TOWN_UI";
	}
}
