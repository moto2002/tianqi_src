using System;
using System.Collections.Generic;
using UnityEngine;

public class PetTipsFX : BaseUIBehaviour
{
	public List<Transform> PetFx = new List<Transform>();

	private bool[] PetEnable = new bool[3];

	private bool[] PetCDing = new bool[3];

	private bool[] CountDownIng = new bool[3];

	private int[] FxUid = new int[3];

	public void ResetAll()
	{
		for (int i = 0; i < 3; i++)
		{
			this.PetEnable[i] = false;
			this.PetCDing[i] = false;
			this.CountDownIng[i] = false;
			FXSpineManager.Instance.DeleteSpine(this.FxUid[i], true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.PetCountDownEnd, new Callback<int>(this.OnPetCountDownEnd));
		EventDispatcher.AddListener<int>(EventNames.PetCountDownStart, new Callback<int>(this.OnPetCountDownStart));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.PetCountDownEnd, new Callback<int>(this.OnPetCountDownEnd));
		EventDispatcher.RemoveListener<int>(EventNames.PetCountDownStart, new Callback<int>(this.OnPetCountDownStart));
	}

	public void PetEnableSetPetFx(bool isEnable, int index)
	{
		if (isEnable && !this.PetEnable[index] && !this.PetCDing[index] && !this.CountDownIng[index])
		{
			this.FxEnable(index);
		}
		this.PetEnable[index] = isEnable;
		this.RemoveFx();
	}

	public void PetCdStartSetPetFx(int index)
	{
		this.PetCDing[index] = true;
		FXSpineManager.Instance.DeleteSpine(this.FxUid[index], true);
		this.RemoveFx();
	}

	public void PetCdEndSetPetFx(int index)
	{
		this.PetCDing[index] = false;
		if (this.PetEnable[index] && !this.CountDownIng[index])
		{
			this.FxEnable(index);
		}
	}

	public void OnPetCountDownEnd(int index)
	{
		this.CountDownIng[index] = false;
		if (this.PetEnable[index] && !this.PetCDing[index])
		{
			this.FxEnable(index);
		}
	}

	public void OnPetCountDownStart(int index)
	{
		FXSpineManager.Instance.DeleteSpine(this.FxUid[index], true);
		if (!this.CountDownIng[index])
		{
			this.FxUse(index);
		}
		this.CountDownIng[index] = true;
		this.RemoveFx();
	}

	private void FxEnable(int index)
	{
		FXSpineManager.Instance.DeleteSpine(this.FxUid[index], true);
		this.FxUid[index] = FXSpineManager.Instance.PlaySpine(307, this.PetFx.get_Item(index), "BattleUI", 2001, delegate
		{
			this.FxUid[index] = FXSpineManager.Instance.PlaySpine(306, this.PetFx.get_Item(index), "BattleUI", 2001, null, "UI", 0f, -5.8f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}, "UI", 0f, -5.8f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void FxUse(int index)
	{
		FXSpineManager.Instance.DeleteSpine(this.FxUid[index], true);
		this.FxUid[index] = FXSpineManager.Instance.PlaySpine(308, this.PetFx.get_Item(index), "BattleUI", 2001, null, "UI", 0f, -5.8f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	private void RemoveFx()
	{
		for (int i = 0; i < 3; i++)
		{
			if (!this.PetEnable[i] || this.PetCDing[i] || this.CountDownIng[i])
			{
				FXSpineManager.Instance.DeleteSpine(this.FxUid[i], true);
			}
		}
	}
}
