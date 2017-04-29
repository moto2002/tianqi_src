using Foundation.Core.Databinding;
using Package;
using System;
using UnityEngine;

public class BountyBattleUI : BaseUIBehaviour
{
	protected RectTransform Self;

	protected RectTransform Enemy;

	private int startX = -848;

	private int width = 440;

	private int y = -38;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.Self = (base.FindTransform("Self") as RectTransform);
		this.Enemy = (base.FindTransform("Enemy") as RectTransform);
		this.Self.set_anchoredPosition(new Vector2((float)this.startX, (float)this.y));
		this.Enemy.set_anchoredPosition(new Vector2((float)this.startX, (float)this.y));
	}

	protected void OnEnable()
	{
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	protected void OnDisable()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	public void removeListeners()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	public void OnSecondsPast()
	{
		InstanceManager.QueryBattleSituation(BattleHurtInfoType.RoleMakeBossHurt, delegate(XDict<long, BattleSituationInfo> a)
		{
			for (int i = 0; i < a.Count; i++)
			{
				long num = a.ElementKeyAt(i);
				float num2 = (float)(a[num].num / BattleBlackboard.Instance.BossHpLmt);
				if (num == EntityWorld.Instance.EntSelf.ID)
				{
					this.Self.set_anchoredPosition(new Vector2((float)this.startX + num2 * (float)this.width, (float)this.y));
				}
				else
				{
					this.Enemy.set_anchoredPosition(new Vector2((float)this.startX + num2 * (float)this.width, (float)this.y));
				}
			}
		});
		InstanceManager.QueryBattleSituation(BattleHurtInfoType.RoleBeBossHurt, null);
		InstanceManager.QueryBattleSituation(BattleHurtInfoType.RoleBePkHurt, null);
	}
}
