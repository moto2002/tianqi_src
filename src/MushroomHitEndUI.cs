using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomHitEndUI : UIBase
{
	public delegate void VoidDelegate();

	private Text m_TextEvaluate;

	private Gradient m_GradientEvaluate;

	private Text m_TextScore;

	private Text m_TextBrief;

	private Transform m_PanelRewards;

	private Transform m_SpineRoot;

	private List<Transform> m_MushroomCountList = new List<Transform>();

	public MushroomHitEndUI.VoidDelegate closeCallBack;

	private int scoreCache;

	private Dictionary<int, int> mushroomsCache = new Dictionary<int, int>();

	private List<ItemBriefInfo> rewardsCache = new List<ItemBriefInfo>();

	public Dictionary<int, Color32[]> EvaluateColor;

	private int fx_WinExplode;

	private int fx_WinStart;

	private int fx_WinIdle;

	public MushroomHitEndUI()
	{
		Dictionary<int, Color32[]> dictionary = new Dictionary<int, Color32[]>();
		dictionary.Add(1, new Color32[]
		{
			new Color32(132, 189, 120, 255),
			new Color32(132, 189, 120, 255)
		});
		dictionary.Add(2, new Color32[]
		{
			new Color32(226, 255, 126, 255),
			new Color32(84, 216, 37, 255)
		});
		dictionary.Add(3, new Color32[]
		{
			new Color32(126, 238, 255, 255),
			new Color32(37, 163, 216, 255)
		});
		dictionary.Add(4, new Color32[]
		{
			new Color32(254, 203, 249, 255),
			new Color32(255, 41, 248, 255)
		});
		dictionary.Add(5, new Color32[]
		{
			new Color32(253, 255, 110, 255),
			new Color32(254, 103, 13, 255)
		});
		this.EvaluateColor = dictionary;
		base..ctor();
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		Transform transform = base.FindTransform("TextEvaluate");
		this.m_TextEvaluate = transform.GetComponent<Text>();
		this.m_GradientEvaluate = transform.GetComponent<Gradient>();
		this.m_TextScore = base.FindTransform("TextScore").GetComponent<Text>();
		this.m_PanelRewards = base.FindTransform("PanelRewards");
		this.m_SpineRoot = base.FindTransform("FXRoot");
		Transform transform2 = base.FindTransform("MushroomBrief");
		for (int i = 0; i < transform2.get_childCount(); i++)
		{
			Transform transform3 = transform2.Find("MushroomCount" + (i + 1));
			this.m_MushroomCountList.Add(transform3);
		}
		base.FindTransform("BtnExit").GetComponent<ButtonCustom>().onClickCustom = delegate(GameObject go)
		{
			this.Show(false);
		};
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		SoundManager.PlayUI(304, false);
	}

	protected override void OnDisable()
	{
		if (this.closeCallBack != null)
		{
			this.closeCallBack();
		}
		this.DeleteAllFX();
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	private void SetRewards(List<ItemBriefInfo> list)
	{
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			int cfgId = list.get_Item(i).cfgId;
			long count = list.get_Item(i).count;
			if (DataReader<Items>.Get(cfgId) != null)
			{
				ItemShow.ShowItem(this.m_PanelRewards, cfgId, count, false, UINodesManager.T2RootOfSpecial, 2001);
			}
		}
	}

	public void SetEvaluate(int score)
	{
		FenShuChengHao evaluate = MushroomHitManager.GetEvaluate(score);
		if (evaluate != null)
		{
			this.m_TextEvaluate.set_text(GameDataUtils.GetChineseContent(evaluate.title, false));
			Color32[] array = this.EvaluateColor.get_Item(evaluate.id);
			this.m_GradientEvaluate.topColor = array[0];
			this.m_GradientEvaluate.bottomColor = array[1];
		}
	}

	public void RefreshUI(Dictionary<int, int> mushrooms, int score, List<ItemBriefInfo> rewards, MushroomHitEndUI.VoidDelegate closeCB)
	{
		this.closeCallBack = closeCB;
		this.scoreCache = score;
		this.rewardsCache.Clear();
		this.rewardsCache.AddRange(rewards);
		this.mushroomsCache.Clear();
		int num = 0;
		using (Dictionary<int, int>.Enumerator enumerator = mushrooms.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				if (current.get_Key() != 0)
				{
					Transform transform = this.m_MushroomCountList.get_Item(num);
					num++;
					MoGuBiao moGuBiao = DataReader<MoGuBiao>.Get(current.get_Key());
					transform.Find("TextMushroom").GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(moGuBiao.nameID, false));
					Transform transform2 = transform.Find("TextCount");
					transform2.GetComponent<Text>().set_text("0");
					this.mushroomsCache.Add(current.get_Key(), current.get_Value());
				}
			}
		}
		this.m_TextScore.set_text("0");
		this.SetEvaluate(score);
		for (int i = 0; i < this.m_PanelRewards.get_childCount(); i++)
		{
			Object.Destroy(this.m_PanelRewards.GetChild(i).get_gameObject());
		}
	}

	public void PlayAnimation()
	{
		this.DeleteAllFX();
		TimerHeap.AddTimer(50u, 0, delegate
		{
			this.fx_WinStart = FXSpineManager.Instance.PlaySpine(430, this.m_SpineRoot, "MushroomHitEndUI", 3002, delegate
			{
				FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
				this.fx_WinIdle = FXSpineManager.Instance.ReplaySpine(this.fx_WinIdle, 429, this.m_SpineRoot, "MushroomHitEndUI", 3003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_WinExplode = FXSpineManager.Instance.PlaySpine(431, this.m_SpineRoot, "MushroomHitEndUI", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		});
	}

	private void DeleteAllFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fx_WinExplode, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinStart, true);
		FXSpineManager.Instance.DeleteSpine(this.fx_WinIdle, true);
	}

	public void OnEndAnimation()
	{
		int num = 0;
		using (Dictionary<int, int>.Enumerator enumerator = this.mushroomsCache.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				if (current.get_Key() != 0)
				{
					Transform transform = this.m_MushroomCountList.get_Item(num);
					num++;
					Transform transform2 = transform.Find("TextCount");
					if (current.get_Value() > 0)
					{
						transform2.GetComponent<ChangeNumAnim>().ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, transform2.GetComponent<Text>(), 0L, (long)current.get_Value(), null, null, null);
					}
					else
					{
						transform2.GetComponent<Text>().set_text(current.get_Value().ToString());
					}
				}
			}
		}
		if (this.scoreCache > 0)
		{
			this.m_TextScore.GetComponent<ChangeNumAnim>().ShowChangeNumAnim(ChangeNumAnim.AnimType.Normal, this.m_TextScore, 0L, (long)this.scoreCache, null, null, delegate
			{
				this.SetRewards(this.rewardsCache);
			});
		}
		else
		{
			this.m_TextScore.set_text(this.scoreCache.ToString());
			this.SetRewards(this.rewardsCache);
		}
	}
}
