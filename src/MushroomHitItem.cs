using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomHitItem : BaseUIBehaviour
{
	public delegate void VoidMushroomDelegate(MushroomHitItem script);

	public delegate bool BoolMushroomDelegate(MushroomHitItem script);

	private GameObject m_BtnHit;

	private Transform m_panelMushroom;

	protected Animator m_animator;

	private Transform m_panelScore;

	private List<Image> m_scoreNumImages = new List<Image>();

	public int itemIndex;

	public int mushroomId;

	public float lifeTime = -1f;

	public MoGuBiao mushroomConfig;

	public float lifeAccel;

	public bool isPlanting;

	private MushroomHitItem.BoolMushroomDelegate checkClickCallBack;

	private MushroomHitItem.VoidMushroomDelegate hitCallBack;

	private MushroomHitItem.VoidMushroomDelegate wiltCallBack;

	private MushroomHitItem.VoidMushroomDelegate explodeCallBack;

	private MushroomHitItem.VoidMushroomDelegate afterExplodeCallBack;

	private List<int> spineIds = new List<int>();

	private string strPrefix = "mogu_font";

	public void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_panelMushroom = base.FindTransform("PanelMushroom");
		this.m_BtnHit = base.FindTransform("BtnHit").get_gameObject();
		this.m_BtnHit.GetComponent<ButtonCustom>().onClickCustom = delegate(GameObject go)
		{
			this.MushroomHit();
		};
		this.m_panelScore = base.FindTransform("PanelScore");
		for (int i = 0; i <= 3; i++)
		{
			this.m_scoreNumImages.Add(base.FindTransform("ScoreNum" + i).GetComponent<Image>());
		}
		this.m_animator = base.GetComponent<Animator>();
		this.m_animator.Play("AppearAction", 0, 0f);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<int>(EventNames.MushroomExplode, new Callback<int>(this.MushroomBeExplode));
		EventDispatcher.AddListener<float>(EventNames.MushroomLifeTime, new Callback<float>(this.MushroomLifeCountDown));
		EventDispatcher.AddListener<ShuaXinPinLv>(EventNames.MushroomAccel, new Callback<ShuaXinPinLv>(this.MushroomAccel));
		EventDispatcher.AddListener(EventNames.MushroomHitEnd, new Callback(this.MushroomHitEndClear));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<int>(EventNames.MushroomExplode, new Callback<int>(this.MushroomBeExplode));
		EventDispatcher.RemoveListener<float>(EventNames.MushroomLifeTime, new Callback<float>(this.MushroomLifeCountDown));
		EventDispatcher.RemoveListener<ShuaXinPinLv>(EventNames.MushroomAccel, new Callback<ShuaXinPinLv>(this.MushroomAccel));
		EventDispatcher.RemoveListener(EventNames.MushroomHitEnd, new Callback(this.MushroomHitEndClear));
	}

	public void InitMushroomItem(int index, MushroomHitItem.BoolMushroomDelegate checkClickCB, MushroomHitItem.VoidMushroomDelegate hitCB, MushroomHitItem.VoidMushroomDelegate wiltCB, MushroomHitItem.VoidMushroomDelegate explodeCB, MushroomHitItem.VoidMushroomDelegate afterExplodeCB)
	{
		this.itemIndex = index;
		this.lifeAccel = 1f;
		this.ClearField();
		this.checkClickCallBack = checkClickCB;
		this.hitCallBack = hitCB;
		this.wiltCallBack = wiltCB;
		this.explodeCallBack = explodeCB;
		this.afterExplodeCallBack = afterExplodeCB;
	}

	public void ClearField()
	{
		this.m_BtnHit.SetActive(false);
		this.isPlanting = false;
		this.mushroomId = 0;
		this.mushroomConfig = null;
		this.EndMushroomLife();
		this.ClearSpine();
		this.ResetNum();
	}

	public void ClearSpine()
	{
		for (int i = 0; i < this.spineIds.get_Count(); i++)
		{
			FXSpineManager.Instance.DeleteSpine(this.spineIds.get_Item(i), false);
		}
		this.spineIds.Clear();
	}

	public void ShowSpine(int id, int order = 0, Action callback = null)
	{
		this.spineIds.Add(FXSpineManager.Instance.PlaySpine(id, this.m_panelMushroom, "MushroomHitUI", 2000 + order + this.itemIndex * 10, callback, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
	}

	public bool AddMushroom(int id)
	{
		if (this.isPlanting)
		{
			return false;
		}
		this.ClearField();
		this.isPlanting = true;
		this.mushroomId = id;
		this.mushroomConfig = DataReader<MoGuBiao>.Get(this.mushroomId);
		this.MushroomAppear();
		return true;
	}

	public void MushroomAppear()
	{
		this.ShowSpine(this.mushroomConfig.animation4, 0, null);
		this.ShowSpine(3602, 1, new Action(this.MushroomLife));
		this.m_BtnHit.SetActive(true);
	}

	public void MushroomLife()
	{
		this.ClearSpine();
		this.ShowSpine(this.mushroomConfig.animation1, 0, null);
		this.lifeTime = 0f;
	}

	public void MushroomWilt()
	{
		this.ClearSpine();
		this.ShowSpine(this.mushroomConfig.animation5, 0, null);
		this.ShowSpine(3603, 1, new Action(this.OnMushroomWilt));
	}

	public void MushroomHit()
	{
		if (this.checkClickCallBack(this))
		{
			this.m_BtnHit.SetActive(false);
			this.EndMushroomLife();
			this.ClearSpine();
			this.ShowScore();
			if (this.mushroomConfig.result == 2)
			{
				this.explodeCallBack(this);
				SoundManager.PlayUI(302, false);
				this.ShowSpine(this.mushroomConfig.animation2, 0, new Action(this.OnMushroomAfterExplode));
			}
			else
			{
				this.ShowSpine(3601, 1, null);
				this.ShowSpine(this.mushroomConfig.animation2, 0, new Action(this.OnMushroomHit));
			}
		}
	}

	public void ResetNum()
	{
		this.DisableNum();
		this.m_animator.Play("NormalState");
	}

	private void DisableNum()
	{
		for (int i = 0; i < this.m_scoreNumImages.get_Count(); i++)
		{
			this.m_scoreNumImages.get_Item(i).get_gameObject().SetActive(false);
		}
	}

	private void EnableNum(Image imageNum, string iconName)
	{
		SpriteRenderer iconSprite = ResourceManager.GetIconSprite(iconName);
		imageNum.get_gameObject().SetActive(true);
		ResourceManager.SetSprite(imageNum, iconSprite);
	}

	public void ShowScore()
	{
		if (this.mushroomConfig.result == 3)
		{
			int mushroomAddTime = MushroomHitManager.Instance.mushroomHitConfig.mushroomAddTime;
			this.EnableNum(this.m_scoreNumImages.get_Item(0), this.strPrefix + "111");
			List<int> list = this.SplitNumber(mushroomAddTime);
			int num = 1;
			for (int i = list.get_Count(); i > 0; i--)
			{
				if (i < this.m_scoreNumImages.get_Count() - 2)
				{
					int num2 = list.get_Item(i - 1);
					this.EnableNum(this.m_scoreNumImages.get_Item(num), this.strPrefix + "10" + num2);
					num++;
				}
			}
			int num3 = Math.Min(this.m_scoreNumImages.get_Count() - 1, num);
			this.EnableNum(this.m_scoreNumImages.get_Item(num3), this.strPrefix + "112");
		}
		else
		{
			int num4 = this.mushroomConfig.num;
			if (num4 < 0)
			{
				this.EnableNum(this.m_scoreNumImages.get_Item(0), this.strPrefix + "010");
			}
			else
			{
				this.EnableNum(this.m_scoreNumImages.get_Item(0), this.strPrefix + "210");
			}
			List<int> list2 = this.SplitNumber(num4);
			int num5 = 1;
			for (int j = list2.get_Count(); j > 0; j--)
			{
				if (j < this.m_scoreNumImages.get_Count() - 1)
				{
					int num6 = list2.get_Item(j - 1);
					if (num4 < 0)
					{
						this.EnableNum(this.m_scoreNumImages.get_Item(num5), this.strPrefix + "00" + num6);
					}
					else
					{
						this.EnableNum(this.m_scoreNumImages.get_Item(num5), this.strPrefix + "20" + num6);
					}
					num5++;
				}
			}
		}
		this.m_animator.Play("AppearAction", 0, 0f);
	}

	public List<int> SplitNumber(int num)
	{
		num = Math.Abs(num);
		List<int> list = new List<int>();
		while (num > 0)
		{
			int num2 = num % 10;
			num /= 10;
			list.Add(num2);
		}
		return list;
	}

	public void MushroomBeExplode(int GroundZero)
	{
		if (GroundZero == this.itemIndex)
		{
			return;
		}
		if (!this.isPlanting)
		{
			return;
		}
		this.m_BtnHit.SetActive(false);
		this.EndMushroomLife();
		this.ClearSpine();
		if (this.mushroomConfig.result == 2)
		{
			this.ShowSpine(this.mushroomConfig.animation2, 0, new Action(this.OnMushroomAfterBeExplode));
		}
		else
		{
			this.ShowSpine(this.mushroomConfig.animation3, 0, new Action(this.ExplodeLight));
		}
	}

	public void ExplodeLight()
	{
		this.ClearSpine();
		this.ShowSpine(3604, 0, new Action(this.OnMushroomAfterBeExplode));
	}

	public void MushroomLifeCountDown(float dt)
	{
		if (this.isPlanting && this.lifeTime >= 0f)
		{
			this.lifeTime += dt;
			if (this.lifeTime > (float)this.mushroomConfig.lifeTime * this.lifeAccel)
			{
				this.EndMushroomLife();
				this.MushroomWilt();
			}
		}
	}

	public void EndMushroomLife()
	{
		this.lifeTime = -1f;
	}

	public void MushroomAccel(ShuaXinPinLv config)
	{
		this.lifeAccel = (float)config.speed / 1000f;
	}

	public void MushroomHitEndClear()
	{
		this.ClearField();
		this.lifeAccel = 1f;
	}

	public void OnMushroomHit()
	{
		this.hitCallBack(this);
		this.ClearField();
	}

	public void OnMushroomWilt()
	{
		this.wiltCallBack(this);
		this.ClearField();
	}

	public void OnMushroomAfterExplode()
	{
		this.afterExplodeCallBack(this);
		this.ClearField();
	}

	public void OnMushroomAfterBeExplode()
	{
		this.ClearField();
	}
}
