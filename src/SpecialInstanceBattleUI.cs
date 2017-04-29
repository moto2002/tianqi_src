using System;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInstanceBattleUI : MonoBehaviour
{
	public Text WaveText;

	public Text CarNumTex;

	protected void OnEnable()
	{
		EventDispatcher.AddListener<int, EntityMonster, string>("BattleDialogTrigger", new Callback<int, EntityMonster, string>(this.OnDialogTrigger));
		EventDispatcher.AddListener<int>(EventNames.DefendFightCarReachTipsNty, new Callback<int>(this.OnRefreshCarWave));
		EventDispatcher.AddListener<int>(EventNames.DefendFightMonsterTipsNty, new Callback<int>(this.OnRefreshMonsterWave));
	}

	protected void OnDisable()
	{
		EventDispatcher.RemoveListener<int, EntityMonster, string>("BattleDialogTrigger", new Callback<int, EntityMonster, string>(this.OnDialogTrigger));
		EventDispatcher.RemoveListener<int>(EventNames.DefendFightCarReachTipsNty, new Callback<int>(this.OnRefreshCarWave));
		EventDispatcher.RemoveListener<int>(EventNames.DefendFightMonsterTipsNty, new Callback<int>(this.OnRefreshMonsterWave));
	}

	private void OnDialogTrigger(int type, EntityMonster entityMonster, string arg)
	{
		if (type == 10 && InstanceManager.CurrentInstanceData != null && InstanceManager.CurrentInstanceData.waveShow > 0)
		{
			this.WaveText.set_text(arg);
		}
	}

	private void OnRefreshCarWave(int wave)
	{
		this.CarNumTex.set_text(wave.ToString());
	}

	private void OnRefreshMonsterWave(int wave)
	{
		this.WaveText.set_text(wave.ToString());
	}

	public void init()
	{
		bool active = false;
		bool active2 = false;
		if (InstanceManager.CurrentInstanceData != null)
		{
			if (InstanceManager.CurrentInstanceData.type == 108 || InstanceManager.CurrentInstanceData.type == 110 || InstanceManager.CurrentInstanceData.type == 117)
			{
				active = true;
				active2 = false;
				this.WaveText.set_text(BattleDialogManager.Instance.wave.ToString());
			}
			else if (InstanceManager.CurrentInstanceData.type == 109)
			{
				active = false;
				active2 = true;
				this.CarNumTex.set_text(DefendFightManager.Instance.wave.ToString());
			}
		}
		this.WaveText.get_transform().get_parent().get_gameObject().SetActive(active);
		this.CarNumTex.get_transform().get_parent().get_gameObject().SetActive(active2);
	}
}
