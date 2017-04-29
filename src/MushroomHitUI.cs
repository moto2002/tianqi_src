using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomHitUI : UIBase
{
	public static MushroomHitUI Instance;

	public ButtonCustom m_BtnExit;

	public Image m_SecondImage;

	public Image m_SecondTenImage;

	public Text m_TextScore;

	public Transform m_SpineRoot;

	public ComboControl m_ComboControl;

	private Dictionary<int, Transform> m_HoleList = new Dictionary<int, Transform>();

	private Dictionary<int, bool> m_MushroomPlantList = new Dictionary<int, bool>();

	private List<int> m_RestoreList = new List<int>();

	private MushroomHitStatus status;

	private float gameTime;

	public float gameCD;

	public float refreshGameTimeCount;

	private int accelStage;

	private float accelCD;

	private ShuaXinPinLv accelConfig;

	private bool isMaxAccel;

	private float newMushroomCD;

	private int _combo;

	private float comboMissCD;

	private int plantTimeMushroomCount;

	private Dictionary<int, int> m_MushroomKillList = new Dictionary<int, int>();

	private int _score;

	private float defaultTime;

	private float comboMissTime;

	private int comboScoreTimes;

	private int comboMushroomTimes;

	private int comboScoreNum;

	private int mushroomAddTime;

	public int combo
	{
		get
		{
			return this._combo;
		}
		set
		{
			this._combo = value;
			this.SetCombineControl();
		}
	}

	public int score
	{
		get
		{
			return this._score;
		}
		set
		{
			this._score = value;
			this.SetScoreText();
		}
	}

	protected override void Preprocessing()
	{
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		MushroomHitUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.m_BtnExit = base.FindTransform("BtnExit").GetComponent<ButtonCustom>();
		this.m_BtnExit.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnExit);
		this.m_SecondImage = base.FindTransform("SecondImage").GetComponent<Image>();
		this.m_SecondTenImage = base.FindTransform("Second10Image").GetComponent<Image>();
		this.m_ComboControl = base.FindTransform("ComboNumRoot").GetComponent<ComboControl>();
		this.m_ComboControl.AwakeSelf();
		this.m_ComboControl.SetFontStr("mogu_font30", string.Empty);
		this.m_TextScore = base.FindTransform("TextScore").GetComponent<Text>();
		this.m_SpineRoot = base.FindTransform("FXRoot");
		Transform transform = base.FindTransform("PanelHoles");
		for (int i = 0; i < transform.get_childCount(); i++)
		{
			Transform child = transform.GetChild(i);
			string text = child.get_name().Substring("Hole".get_Length());
			int num = int.Parse(text);
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("MushroomHitItem");
			instantiate2Prefab.get_transform().SetParent(child.get_transform(), false);
			instantiate2Prefab.set_name("MushroomHitItem" + num);
			instantiate2Prefab.SetActive(true);
			instantiate2Prefab.GetComponent<MushroomHitItem>().InitMushroomItem(i, new MushroomHitItem.BoolMushroomDelegate(this.CheckMushroomCanHit), new MushroomHitItem.VoidMushroomDelegate(this.OnMushroomHit), new MushroomHitItem.VoidMushroomDelegate(this.OnMushroomWilt), new MushroomHitItem.VoidMushroomDelegate(this.OnMushroomBeforeExplode), new MushroomHitItem.VoidMushroomDelegate(this.OnMushroomAfterExplode));
			this.m_HoleList.Add(num, instantiate2Prefab.get_transform());
			this.m_MushroomPlantList.Add(num, false);
		}
		DaMoGu mushroomHitConfig = MushroomHitManager.Instance.mushroomHitConfig;
		this.defaultTime = (float)mushroomHitConfig.gameTimes;
		this.comboMissTime = (float)mushroomHitConfig.comboMiss;
		this.comboScoreTimes = mushroomHitConfig.comboTimes;
		this.comboMushroomTimes = mushroomHitConfig.comboMushroom;
		this.comboScoreNum = mushroomHitConfig.comboNum;
		this.mushroomAddTime = mushroomHitConfig.mushroomAddTime;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(false);
		this.GoReady();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		CurrenciesUIViewModel.Show(false);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			MushroomHitUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<HitMouseSettleRes>(EventNames.MushroomHitResult, new Callback<HitMouseSettleRes>(this.OnMushroonGameEnd));
		EventDispatcher.AddListener(EventNames.MushroomHitError, new Callback(this.OnMushroonGameEndError));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener<HitMouseSettleRes>(EventNames.MushroomHitResult, new Callback<HitMouseSettleRes>(this.OnMushroonGameEnd));
		EventDispatcher.RemoveListener(EventNames.MushroomHitError, new Callback(this.OnMushroonGameEndError));
		base.RemoveListeners();
	}

	protected void GoReady()
	{
		this.ResetAll();
		this.status = MushroomHitStatus.Ready;
		FXSpineManager.Instance.PlaySpine(3626, this.m_SpineRoot, "MushroomHitUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(3627, this.m_SpineRoot, "MushroomHitUI", 2000, new Action(this.GoStart), "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	protected void GoStart()
	{
		this.status = MushroomHitStatus.Start;
		this.gameCD = this.defaultTime;
		this.gameTime = 0f;
		this.refreshGameTimeCount = 0f;
		this.accelStage = 1;
		this.accelConfig = DataReader<ShuaXinPinLv>.Get(this.accelStage);
		EventDispatcher.Broadcast<ShuaXinPinLv>(EventNames.MushroomAccel, this.accelConfig);
		this.accelCD = 0f;
		this.isMaxAccel = false;
		this.newMushroomCD = 0f;
		this.SetGameTimeText();
	}

	protected void GoPause()
	{
		this.status = MushroomHitStatus.Pause;
	}

	protected void ReStart()
	{
		this.status = MushroomHitStatus.Start;
	}

	protected void GoEnd()
	{
		this.status = MushroomHitStatus.End;
		this.SetGameTimeText();
		EventDispatcher.Broadcast(EventNames.MushroomHitEnd);
		MushroomHitManager.Instance.SendHitMouseSettleReq(this.score, (int)this.gameTime);
	}

	protected void ResetAll()
	{
		this.status = MushroomHitStatus.None;
		this.gameTime = 0f;
		this.gameCD = 0f;
		this.accelStage = 0;
		this.accelCD = 0f;
		this.accelConfig = null;
		this.isMaxAccel = false;
		this.newMushroomCD = 0f;
		this.score = 0;
		this.combo = 0;
		this.comboMissCD = 0f;
		this.m_ComboControl.SetCombo(false, 0, false);
		this.plantTimeMushroomCount = 0;
		this.refreshGameTimeCount = 0f;
		this.SetGameTimeText();
		this.ClearPlantList();
		this.m_RestoreList.Clear();
		this.m_MushroomKillList.Clear();
		this.m_MushroomKillList.Add(0, 0);
		List<MoGuBiao> dataList = DataReader<MoGuBiao>.DataList;
		using (List<MoGuBiao>.Enumerator enumerator = dataList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MoGuBiao current = enumerator.get_Current();
				this.m_MushroomKillList.Add(current.id, 0);
			}
		}
	}

	private void Update()
	{
		if (this.status == MushroomHitStatus.Start)
		{
			this.gameTime += Time.get_deltaTime();
			this.gameCD -= Time.get_deltaTime();
			this.RestoreField();
			this.UpdateField(Time.get_deltaTime());
			if (this.gameCD <= 0f)
			{
				this.GoEnd();
			}
		}
	}

	protected void SetGameTimeText()
	{
		int num = Math.Max(0, Math.Min(99, (int)Math.Ceiling((double)this.gameCD)));
		if (num < 10)
		{
			this.m_SecondTenImage.get_gameObject().SetActive(false);
		}
		else
		{
			this.m_SecondTenImage.get_gameObject().SetActive(true);
			ResourceManager.SetSprite(this.m_SecondTenImage, ResourceManager.GetIconSprite("mogu_font10" + num / 10));
		}
		ResourceManager.SetSprite(this.m_SecondImage, ResourceManager.GetIconSprite("mogu_font10" + num % 10));
	}

	protected void CountScore()
	{
		int num = 0;
		using (Dictionary<int, int>.Enumerator enumerator = this.m_MushroomKillList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, int> current = enumerator.get_Current();
				if (current.get_Key() == 0)
				{
					num += current.get_Value() * this.comboScoreNum;
				}
				else
				{
					MoGuBiao moGuBiao = DataReader<MoGuBiao>.Get(current.get_Key());
					num += moGuBiao.num * current.get_Value();
				}
			}
		}
		this.score = Math.Max(num, 0);
	}

	protected void SetScoreText()
	{
		this.m_TextScore.set_text(this.score.ToString());
	}

	protected void UpdateField(float deltaTime)
	{
		this.refreshGameTimeCount += Time.get_deltaTime();
		if ((double)this.refreshGameTimeCount > 0.8)
		{
			this.refreshGameTimeCount = 0f;
			this.SetGameTimeText();
		}
		if (!this.isMaxAccel)
		{
			this.accelCD += deltaTime;
			if (this.accelCD > (float)this.accelConfig.time)
			{
				ShuaXinPinLv shuaXinPinLv = DataReader<ShuaXinPinLv>.Get(this.accelStage + 1);
				if (shuaXinPinLv != null)
				{
					this.accelStage++;
					this.accelConfig = shuaXinPinLv;
					this.accelCD = 0f;
					EventDispatcher.Broadcast<ShuaXinPinLv>(EventNames.MushroomAccel, this.accelConfig);
				}
				else
				{
					this.isMaxAccel = true;
				}
			}
		}
		this.newMushroomCD += deltaTime;
		if (this.newMushroomCD > (float)this.accelConfig.frequency / 1000f)
		{
			this.newMushroomCD = 0f;
			this.PlantMushroom();
		}
		this.CheckComboFail(Time.get_deltaTime());
		EventDispatcher.Broadcast<float>(EventNames.MushroomLifeTime, Time.get_deltaTime());
	}

	protected void PlantMushroom()
	{
		List<int> list = new List<int>();
		using (Dictionary<int, bool>.Enumerator enumerator = this.m_MushroomPlantList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, bool> current = enumerator.get_Current();
				if (!current.get_Value())
				{
					list.Add(current.get_Key());
				}
			}
		}
		int num = -1;
		if (list.get_Count() > 1)
		{
			num = list.get_Item(Random.Range(0, list.get_Count()));
		}
		else if (list.get_Count() == 1)
		{
			num = list.get_Item(0);
		}
		if (num > -1)
		{
			Transform transform = this.m_HoleList.get_Item(num);
			int id = 0;
			if (this.plantTimeMushroomCount > 0)
			{
				this.plantTimeMushroomCount--;
				id = 2;
			}
			else
			{
				int num2 = Random.Range(0, 1000);
				int[] array = new int[]
				{
					this.accelConfig.bombProbability,
					this.accelConfig.bombProbability + this.accelConfig.kingProbability,
					1000
				};
				int[] array2 = new int[]
				{
					3,
					4,
					1
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (num2 < array[i])
					{
						id = array2[i];
						break;
					}
				}
			}
			if (transform.GetComponent<MushroomHitItem>().AddMushroom(id))
			{
				this.m_MushroomPlantList.set_Item(num, true);
			}
		}
	}

	protected void RestoreField()
	{
		for (int i = 0; i < this.m_RestoreList.get_Count(); i++)
		{
			int num = this.m_RestoreList.get_Item(i);
			this.m_MushroomPlantList.set_Item(num, false);
		}
		this.m_RestoreList.Clear();
	}

	protected void ExplosionMushrooms(int index)
	{
		EventDispatcher.Broadcast<int>(EventNames.MushroomExplode, index);
		this.ClearPlantList();
		this.m_RestoreList.Clear();
	}

	protected void CheckComboFail(float deltaTime)
	{
		if (this.comboMissCD > 0f)
		{
			this.comboMissCD -= deltaTime;
			if (this.comboMissCD <= 0f)
			{
				this.ClearCombine();
			}
		}
	}

	protected void ClearCombine()
	{
		this.combo = 0;
	}

	protected void AddCombine()
	{
		this.combo++;
		if (this.combo > 0)
		{
			if (this.combo % this.comboMushroomTimes == 0)
			{
				this.plantTimeMushroomCount++;
			}
			if (this.combo % this.comboScoreTimes == 0)
			{
				Dictionary<int, int> mushroomKillList;
				Dictionary<int, int> expr_52 = mushroomKillList = this.m_MushroomKillList;
				int num;
				int expr_55 = num = 0;
				num = mushroomKillList.get_Item(num);
				expr_52.set_Item(expr_55, num + 1);
				this.CountScore();
			}
			this.comboMissCD = this.comboMissTime;
		}
	}

	protected void SetCombineControl()
	{
		bool isShow = this.combo >= 10;
		this.m_ComboControl.SetCombo(isShow, this.combo, false);
	}

	protected void OnClickBtnExit(GameObject go)
	{
		if (this.status == MushroomHitStatus.Ready || this.status == MushroomHitStatus.Start)
		{
			this.GoPause();
			UIManagerControl.Instance.OpenUI("DialogBoxUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
			DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(340022, false), null, delegate
			{
				this.ReStart();
			}, delegate
			{
				this.GoEnd();
			}, GameDataUtils.GetNoticeText(103), GameDataUtils.GetNoticeText(102), "button_orange_1", "button_orange_1", null);
		}
		else
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}
	}

	protected bool CheckMushroomCanHit(MushroomHitItem script)
	{
		if (this.status != MushroomHitStatus.Start)
		{
			return false;
		}
		int itemIndex = script.itemIndex;
		if (!this.m_MushroomPlantList.get_Item(itemIndex))
		{
			return false;
		}
		int mushroomId = script.mushroomId;
		MoGuBiao moGuBiao = DataReader<MoGuBiao>.Get(mushroomId);
		Dictionary<int, int> mushroomKillList;
		Dictionary<int, int> expr_3C = mushroomKillList = this.m_MushroomKillList;
		int num;
		int expr_3F = num = mushroomId;
		num = mushroomKillList.get_Item(num);
		expr_3C.set_Item(expr_3F, num + 1);
		this.CountScore();
		if (moGuBiao.result == 2)
		{
			this.ClearCombine();
		}
		else
		{
			this.AddCombine();
			if (moGuBiao.result == 3)
			{
				this.gameCD += (float)this.mushroomAddTime;
				this.SetGameTimeText();
			}
		}
		return true;
	}

	protected void OnMushroomHit(MushroomHitItem script)
	{
		int itemIndex = script.itemIndex;
		if (!this.m_MushroomPlantList.get_Item(itemIndex))
		{
			return;
		}
		this.m_RestoreList.Add(itemIndex);
	}

	protected void OnMushroomWilt(MushroomHitItem script)
	{
		int itemIndex = script.itemIndex;
		this.m_RestoreList.Add(itemIndex);
	}

	protected void OnMushroomBeforeExplode(MushroomHitItem script)
	{
		int itemIndex = script.itemIndex;
		if (!this.m_MushroomPlantList.get_Item(itemIndex))
		{
			return;
		}
		this.GoPause();
		this.ExplosionMushrooms(itemIndex);
	}

	protected void OnMushroomAfterExplode(MushroomHitItem script)
	{
		this.ReStart();
	}

	protected void OnMushroonGameEnd(HitMouseSettleRes msg)
	{
		MushroomHitEndUI mushroomHitEndUI = UIManagerControl.Instance.OpenUI("MushroomHitEndUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as MushroomHitEndUI;
		mushroomHitEndUI.RefreshUI(this.m_MushroomKillList, this.score, msg.dropItem, delegate
		{
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		});
		mushroomHitEndUI.PlayAnimation();
	}

	protected void OnMushroonGameEndError()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected void ClearPlantList()
	{
		List<int> list = new List<int>(this.m_MushroomPlantList.get_Keys());
		using (List<int>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				this.m_MushroomPlantList.set_Item(current, false);
			}
		}
	}
}
