using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementInstanceUI : UIBase, ListViewInterface
{
	private enum ElementInstanceUIState
	{
		Ball,
		Mine
	}

	private enum DragState
	{
		Begin,
		Dragging,
		End,
		ContinueDrag
	}

	public static ElementInstanceUI Instance;

	public string mineDetailBlockID = "None";

	private ElementInstanceUI.ElementInstanceUIState currentUIState;

	private bool continueRotate;

	private Quaternion lastQuaterion;

	private Quaternion currentQuaterion;

	private Quaternion quaternionDir = Quaternion.get_identity();

	private float percent = 1f;

	private float slowSpeed = 0.05f;

	private float speedDelta = 0.8f;

	private bool isScrollTo;

	private Quaternion scrollOriginDir = Quaternion.get_identity();

	private Quaternion scrollTargetDir = Quaternion.get_identity();

	private float scrollPercent;

	private float scrollSpeed = 0.05f;

	private Vector2 lastDragPos = Vector2.get_zero();

	private float revetEnergyTime;

	private float timeCalDelta;

	private float timeCache;

	private Vector2 onPointerDownPlace;

	private ElementInstanceUI.DragState dragState = ElementInstanceUI.DragState.End;

	private Quaternion OriginalQuaternion;

	private Quaternion OffsetQuaternion;

	private RawImage RawImageElementBall;

	private Image TouchPlace;

	private RectTransform ImageLocate;

	private Text TextEnergyNum;

	private ButtonCustom BtnRevert;

	public RectTransform ImageArrow;

	private ButtonCustom BtnShowActor;

	private ButtonCustom BtnMines;

	private Text TextTime;

	private Image ImageProgress;

	private Transform TimeObj;

	private Text TextBuffActor1;

	private Text TextBuffActor2;

	private Text TextBuffActor3;

	private Text TextBuffPet1;

	private Text TextBuffPet2;

	private Text TextBuffPet3;

	private ListView ListViewMine;

	private Transform Ball;

	private Transform Mines;

	private Transform TextNoMines;

	protected override void Preprocessing()
	{
		this.isMask = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		ElementInstanceUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.hideMainCamera = true;
		this.TouchPlace = base.FindTransform("TouchPlace").GetComponent<Image>();
		this.RawImageElementBall = base.FindTransform("RawImageElementBall").GetComponent<RawImage>();
		this.ImageLocate = base.FindTransform("ImageLocate").GetComponent<RectTransform>();
		this.TextEnergyNum = base.FindTransform("TextEnergyNum").GetComponent<Text>();
		this.BtnRevert = base.FindTransform("BtnRevert").GetComponent<ButtonCustom>();
		this.ImageArrow = base.FindTransform("ImageArrow").GetComponent<RectTransform>();
		this.BtnShowActor = base.FindTransform("BtnShowActor").GetComponent<ButtonCustom>();
		this.BtnMines = base.FindTransform("BtnMines").GetComponent<ButtonCustom>();
		this.TextTime = base.FindTransform("TextTime").GetComponent<Text>();
		this.ImageProgress = base.FindTransform("ImageProgress").GetComponent<Image>();
		this.TimeObj = base.FindTransform("TimeObj");
		this.TextBuffActor1 = base.FindTransform("TextBuffActor1").GetComponent<Text>();
		this.TextBuffActor2 = base.FindTransform("TextBuffActor2").GetComponent<Text>();
		this.TextBuffActor3 = base.FindTransform("TextBuffActor3").GetComponent<Text>();
		this.TextBuffPet1 = base.FindTransform("TextBuffPet1").GetComponent<Text>();
		this.TextBuffPet2 = base.FindTransform("TextBuffPet2").GetComponent<Text>();
		this.TextBuffPet3 = base.FindTransform("TextBuffPet3").GetComponent<Text>();
		this.ListViewMine = base.FindTransform("ListViewMine").GetComponent<ListView>();
		this.Mines = base.FindTransform("Mines");
		this.Ball = base.FindTransform("Ball");
		this.TextNoMines = base.FindTransform("TextNoMines");
		this.BtnRevert.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnRevert);
		this.BtnShowActor.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnShowActor);
		this.BtnMines.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnMines);
		this.ListViewMine.manager = this;
		this.ListViewMine.Init(ListView.ListViewScrollStyle.Up);
		if (ElementInstanceManager.Instance.m_elementCopyLoginPush != null)
		{
			this.revetEnergyTime = (float)ElementInstanceManager.Instance.m_elementCopyLoginPush.residueRecoverTime;
		}
		RTManager.Instance.SetModelRawImage1(this.RawImageElementBall, false);
		EventTriggerListener expr_272 = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_272.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_272.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragImageTouchPlace));
		EventTriggerListener expr_2A3 = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_2A3.onBeginDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_2A3.onBeginDrag, new EventTriggerListener.VoidDelegateData(this.OnBeginDrag));
		EventTriggerListener expr_2D4 = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_2D4.onEndDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_2D4.onEndDrag, new EventTriggerListener.VoidDelegateData(this.OnEndDragTouchPlace));
		EventTriggerListener expr_305 = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_305.onPointerDown = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_305.onPointerDown, new EventTriggerListener.VoidDelegateData(this.OnPointerDown));
		EventTriggerListener expr_336 = EventTriggerListener.Get(this.TouchPlace.get_gameObject());
		expr_336.onPointerUp = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_336.onPointerUp, new EventTriggerListener.VoidDelegateData(this.OnPointerUp));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (this.timeCache != 0f)
		{
			this.revetEnergyTime -= Time.get_time() - this.timeCache;
		}
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ElementBall);
		ModelDisplayManager.Instance.ShowElementBall(true);
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110019), string.Empty, delegate
		{
			base.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.RefreshUI();
		this.ShowActorPostion(true);
		if (!this.mineDetailBlockID.Equals("None"))
		{
			ElementInstanceMineDetailUI elementInstanceMineDetailUI = UIManagerControl.Instance.OpenUI("ElementInstanceMineDetailUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as ElementInstanceMineDetailUI;
			elementInstanceMineDetailUI.blockID = this.mineDetailBlockID;
			elementInstanceMineDetailUI.RefreshUI();
			this.mineDetailBlockID = "None";
		}
		else if (ElementInstanceManager.Instance.m_shouldShow)
		{
			ElementInstanceManager.Instance.m_shouldShow = false;
			ElementInstanceMineDetailUI elementInstanceMineDetailUI2 = UIManagerControl.Instance.OpenUI("ElementInstanceMineDetailUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as ElementInstanceMineDetailUI;
			elementInstanceMineDetailUI2.RefreshUI();
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.timeCache = Time.get_time();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ElementBall);
	}

	private void Update()
	{
		if (this.isScrollTo)
		{
			this.scrollPercent += this.scrollSpeed;
			BallElement.Instance.get_transform().set_rotation(Quaternion.Lerp(this.scrollOriginDir, this.scrollTargetDir, this.scrollPercent));
			BallElement.Instance.ballLight.set_rotation(BallElement.Instance.get_transform().get_rotation());
			if (this.scrollPercent > 1f)
			{
				this.isScrollTo = false;
			}
		}
		else if (this.continueRotate && this.percent > 0f)
		{
			this.percent -= this.slowSpeed;
			BallElement.Instance.get_transform().set_rotation(Quaternion.Lerp(BallElement.Instance.get_transform().get_rotation(), BallElement.Instance.get_transform().get_rotation() * this.quaternionDir, this.percent));
			BallElement.Instance.ballLight.set_rotation(BallElement.Instance.get_transform().get_rotation());
		}
		this.timeCalDelta += Time.get_deltaTime();
		this.revetEnergyTime -= Time.get_deltaTime();
		if (this.timeCalDelta > 1f)
		{
			this.timeCalDelta = 0f;
			if (this.revetEnergyTime >= 0f)
			{
				this.TextTime.set_text(TimeConverter.ChangeSecsToString((int)this.revetEnergyTime));
			}
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetExploreEnergyChangedNty, new Callback(this.OnGetExploreEnergyChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetExploreBlockRes, new Callback(this.OnGetExploreBlockRes));
		EventDispatcher.AddListener(EventNames.OnGetBlockStatusChangedNty, new Callback(this.OnGetBlockStatusChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetExploreLogChangedNty, new Callback(this.OnGetExploreLogChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetMineInfoChangedNty, new Callback(this.OnGetMineInfoChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetLastBlockChangedNty, new Callback(this.OnGetLastBlockChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetStartToFightRes, new Callback(this.OnGetStartToFightRes));
		EventDispatcher.AddListener(EventNames.OnGetAccepttDebrisRes, new Callback(this.OnGetAccepttDebrisRes));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetExploreEnergyChangedNty, new Callback(this.OnGetExploreEnergyChangedNty));
		EventDispatcher.RemoveListener(EventNames.OnGetExploreBlockRes, new Callback(this.OnGetExploreBlockRes));
		EventDispatcher.RemoveListener(EventNames.OnGetBlockStatusChangedNty, new Callback(this.OnGetBlockStatusChangedNty));
		EventDispatcher.RemoveListener(EventNames.OnGetExploreLogChangedNty, new Callback(this.OnGetExploreLogChangedNty));
		EventDispatcher.RemoveListener(EventNames.OnGetMineInfoChangedNty, new Callback(this.OnGetMineInfoChangedNty));
		EventDispatcher.RemoveListener(EventNames.OnGetLastBlockChangedNty, new Callback(this.OnGetLastBlockChangedNty));
		EventDispatcher.RemoveListener(EventNames.OnGetStartToFightRes, new Callback(this.OnGetStartToFightRes));
		EventDispatcher.RemoveListener(EventNames.OnGetAccepttDebrisRes, new Callback(this.OnGetAccepttDebrisRes));
	}

	public void RefreshLogs()
	{
	}

	public void RefreshDetail()
	{
		int exploreEnergy = ElementInstanceManager.Instance.m_elementCopyLoginPush.exploreEnergy;
		int num = DataReader<YWanFaSheZhi>.Get("powerTopLine").num;
		if (exploreEnergy >= num)
		{
			this.TimeObj.get_gameObject().SetActive(false);
		}
		else
		{
			this.TimeObj.get_gameObject().SetActive(true);
		}
		this.TextEnergyNum.set_text(exploreEnergy + "/" + num);
		float num2 = (float)exploreEnergy / (float)num;
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		this.ImageProgress.set_fillAmount(num2);
	}

	public void RefreshProperty()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < DataReader<YJiaoSeJiaChengKu>.DataList.get_Count(); i++)
		{
			dictionary.Add(DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(i).propertyId, 0);
		}
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		for (int j = 0; j < DataReader<YChongWuJiaChengKu>.DataList.get_Count(); j++)
		{
			dictionary2.Add(DataReader<YChongWuJiaChengKu>.DataList.get_Item(j).propertyId, 0);
		}
		List<PropertyInfo> playerProperty = ElementInstanceManager.Instance.m_elementCopyLoginPush.playerProperty;
		for (int k = 0; k < ElementInstanceManager.Instance.m_elementCopyLoginPush.playerProperty.get_Count(); k++)
		{
			Dictionary<int, int> dictionary3;
			Dictionary<int, int> expr_8A = dictionary3 = dictionary;
			int num;
			int expr_9B = num = playerProperty.get_Item(k).propertyId;
			num = dictionary3.get_Item(num);
			expr_8A.set_Item(expr_9B, num + (playerProperty.get_Item(k).propertyValue - 100));
		}
		List<PropertyInfo> petProperty = ElementInstanceManager.Instance.m_elementCopyLoginPush.petProperty;
		for (int l = 0; l < ElementInstanceManager.Instance.m_elementCopyLoginPush.petProperty.get_Count(); l++)
		{
			Dictionary<int, int> dictionary4;
			Dictionary<int, int> expr_FD = dictionary4 = dictionary2;
			int num;
			int expr_10E = num = petProperty.get_Item(l).propertyId;
			num = dictionary4.get_Item(num);
			expr_FD.set_Item(expr_10E, num + (petProperty.get_Item(l).propertyValue - 100));
		}
		string depictCount = DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(0).depictCount;
		this.TextBuffActor1.set_text(depictCount.Replace("{s1}", dictionary.get_Item(DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(0).propertyId).ToString()));
		depictCount = DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(1).depictCount;
		this.TextBuffActor2.set_text(depictCount.Replace("{s1}", dictionary.get_Item(DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(1).propertyId).ToString()));
		depictCount = DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(2).depictCount;
		this.TextBuffActor3.set_text(depictCount.Replace("{s1}", dictionary.get_Item(DataReader<YJiaoSeJiaChengKu>.DataList.get_Item(2).propertyId).ToString()));
		depictCount = DataReader<YChongWuJiaChengKu>.DataList.get_Item(0).depictCount;
		this.TextBuffPet1.set_text(depictCount.Replace("{s1}", dictionary2.get_Item(DataReader<YChongWuJiaChengKu>.DataList.get_Item(0).propertyId).ToString()));
		depictCount = DataReader<YChongWuJiaChengKu>.DataList.get_Item(1).depictCount;
		this.TextBuffPet2.set_text(depictCount.Replace("{s1}", dictionary2.get_Item(DataReader<YChongWuJiaChengKu>.DataList.get_Item(1).propertyId).ToString()));
		depictCount = DataReader<YChongWuJiaChengKu>.DataList.get_Item(2).depictCount;
		this.TextBuffPet3.set_text(depictCount.Replace("{s1}", dictionary2.get_Item(DataReader<YChongWuJiaChengKu>.DataList.get_Item(2).propertyId).ToString()));
	}

	public void RefreshBall()
	{
		BallElement.Instance.RefreshBallIcons();
	}

	public void RefreshBtnMines()
	{
		this.BtnMines.get_transform().FindChild("TextNum").GetComponent<Text>().set_text(ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.get_Count().ToString());
	}

	private void SetUIState(ElementInstanceUI.ElementInstanceUIState state)
	{
		this.currentUIState = state;
		if (state == ElementInstanceUI.ElementInstanceUIState.Ball)
		{
			this.Ball.get_gameObject().SetActive(true);
			this.Mines.get_gameObject().SetActive(false);
			this.BtnMines.get_transform().FindChild("Image3").get_gameObject().SetActive(true);
			this.BtnMines.get_transform().FindChild("Image4").get_gameObject().SetActive(false);
		}
		else
		{
			this.Ball.get_gameObject().SetActive(false);
			this.Mines.get_gameObject().SetActive(true);
			this.BtnMines.get_transform().FindChild("Image3").get_gameObject().SetActive(false);
			this.BtnMines.get_transform().FindChild("Image4").get_gameObject().SetActive(true);
		}
	}

	public void RefreshUI()
	{
		this.SetUIState(ElementInstanceUI.ElementInstanceUIState.Ball);
		this.RefreshDetail();
		this.RefreshBall();
		this.RefreshLogs();
		this.RefreshBtnMines();
		this.RefreshProperty();
	}

	public void SetInfoUnit(BallElementItem ballitem, string text)
	{
	}

	private void ShowActorPostion(bool isQuick)
	{
		this.continueRotate = false;
		Vector3 vector = new Vector3(275f, 0f, 0f);
		Vector3 zero = Vector3.get_zero();
		float num = (float)CamerasMgr.Camera2RTCommon.get_pixelWidth();
		float num2 = (float)CamerasMgr.Camera2RTCommon.get_pixelHeight();
		zero.x = (vector.x + num / 2f) / num * num;
		zero.y = (vector.y + num2 / 2f) / num2 * num2;
		zero.z = 0f;
		Ray ray = CamerasMgr.Camera2RTCommon.ScreenPointToRay(zero);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit))
		{
			Vector3 vector2 = raycastHit.get_point() - BallElement.Instance.get_transform().get_position();
			Quaternion quaternion = Quaternion.LookRotation(vector2);
			Quaternion rotation = BallElement.Instance.get_transform().get_rotation();
			Transform transform = BallElement.Instance.get_transform().FindChild(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock);
			Quaternion quaternion2 = Quaternion.Inverse(Quaternion.LookRotation(transform.get_position() - BallElement.Instance.get_transform().get_position()));
			if (isQuick)
			{
				BallElement.Instance.get_transform().set_rotation(quaternion * quaternion2 * rotation);
				BallElement.Instance.ballLight.set_rotation(BallElement.Instance.get_transform().get_rotation());
			}
			else
			{
				this.scrollPercent = 0f;
				this.isScrollTo = true;
				this.scrollOriginDir = BallElement.Instance.get_transform().get_rotation();
				this.scrollTargetDir = quaternion * quaternion2 * rotation;
			}
		}
	}

	private void OnClickBtnGet(GameObject sender)
	{
		ElementInstanceMineObj component = sender.get_transform().get_parent().get_parent().GetComponent<ElementInstanceMineObj>();
		ElementInstanceManager.Instance.SendAcceptDebrisReq(component.blockID);
	}

	private void OnClickBtnShowProperty(GameObject sender)
	{
	}

	private void OnClickBtnMines(GameObject sender)
	{
		if (this.currentUIState == ElementInstanceUI.ElementInstanceUIState.Ball)
		{
			this.currentUIState = ElementInstanceUI.ElementInstanceUIState.Mine;
		}
		else
		{
			this.currentUIState = ElementInstanceUI.ElementInstanceUIState.Ball;
		}
		if (this.currentUIState == ElementInstanceUI.ElementInstanceUIState.Mine)
		{
			ElementInstanceManager.Instance.SendMineInfoReq(delegate
			{
				if (ElementInstanceManager.Instance.m_mineInfoRes.mineInfos.get_Count() == 0)
				{
					this.TextNoMines.get_gameObject().SetActive(true);
				}
				else
				{
					this.TextNoMines.get_gameObject().SetActive(false);
				}
				this.ListViewMine.Refresh();
			});
		}
		this.SetUIState(this.currentUIState);
	}

	private void OnClickBtnRevert(GameObject sender)
	{
		ElementInstanceManager.Instance.BuyRecovery();
	}

	private void OnClickImageTouchPlaceData(PointerEventData eventData)
	{
		if (Vector2.Distance(this.onPointerDownPlace, eventData.get_position()) > 10f)
		{
			return;
		}
	}

	private void OnClickBtnShowActor(GameObject sender)
	{
		this.ShowActorPostion(false);
	}

	private void OnPointerUp(PointerEventData eventData)
	{
		if (ElementInstanceManager.Instance.m_isActorMoving)
		{
			return;
		}
		if (Vector2.Distance(this.onPointerDownPlace, eventData.get_position()) > 10f)
		{
			return;
		}
		Vector3 position = CamerasMgr.CameraUI.ScreenToWorldPoint(new Vector3(eventData.get_position().x, eventData.get_position().y, 0f));
		position.z = 0f;
		this.ImageLocate.set_position(position);
		position = this.ImageLocate.get_localPosition();
		Vector3 zero = Vector3.get_zero();
		float num = (float)CamerasMgr.Camera2RTCommon.get_pixelWidth();
		float num2 = (float)CamerasMgr.Camera2RTCommon.get_pixelHeight();
		zero.x = (position.x + num / 2f) / num * num;
		zero.y = (position.y + num2 / 2f) / num2 * num2;
		zero.z = 0f;
		Ray ray = CamerasMgr.Camera2RTCommon.ScreenPointToRay(zero);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit, 1000f, -1))
		{
			BallElementItem bei = raycastHit.get_collider().get_gameObject().GetComponent<BallElementItem>();
			bei.DoOnClickAnimation();
			if (bei.blockInfo == null)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502319, false));
				return;
			}
			List<string> around = DataReader<YBanKuaiSuoYin>.Get(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock).around;
			if (!around.Contains(bei.blockInfo.blockId))
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502267, false));
				return;
			}
			if (bei.isActor)
			{
				if (bei.blockInfo.incidentType == RandomIncidentType.IncidentType.MONSTER && !bei.blockInfo.isChallenge)
				{
					TimerHeap.AddTimer(500u, 0, delegate
					{
						DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(502304, false), GameDataUtils.GetChineseContent(502303, false), delegate
						{
						}, delegate
						{
							ElementInstanceManager.Instance.SendStartToFightReq(bei.blockInfo.blockId);
						}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
					});
				}
				else if (bei.blockInfo.incidentType == RandomIncidentType.IncidentType.MINE && !bei.blockInfo.isChallenge)
				{
					TimerHeap.AddTimer(500u, 0, delegate
					{
						DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(502304, false), GameDataUtils.GetChineseContent(502303, false), delegate
						{
						}, delegate
						{
							ElementInstanceManager.Instance.SendStartToFightReq(bei.blockInfo.blockId);
						}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
					});
				}
			}
			else if (bei.blockInfo.incidentType == RandomIncidentType.IncidentType.ROADBLOCK)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502300, false));
			}
			else if ((bei.blockInfo.incidentType == RandomIncidentType.IncidentType.MONSTER || bei.blockInfo.incidentType == RandomIncidentType.IncidentType.MINE) && !bei.blockInfo.isChallenge)
			{
				if (ElementInstanceManager.Instance.CheckIsAround(bei.blockInfo.blockId))
				{
					if (bei.blockInfo.incidentType == RandomIncidentType.IncidentType.MONSTER)
					{
						ElementInstanceMonsterMeet elementInstanceMonsterMeet = InstanceManagerUI.OpenElementInstanceMonsterMeet();
						elementInstanceMonsterMeet.blockID = bei.blockInfo.blockId;
						elementInstanceMonsterMeet.RefreshUI(bei.blockInfo.incidentTypeId);
					}
					else
					{
						ElementInstanceMineDetailUI elementInstanceMineDetailUI = UIManagerControl.Instance.OpenUI("ElementInstanceMineDetailUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as ElementInstanceMineDetailUI;
						elementInstanceMineDetailUI.blockID = bei.blockInfo.blockId;
						elementInstanceMineDetailUI.RefreshUI();
					}
				}
				else
				{
					UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(502307, false));
				}
			}
			else if (bei.blockInfo.incidentType == RandomIncidentType.IncidentType.MINE && bei.blockInfo.isChallenge && ElementInstanceManager.Instance.CheckIsAround(bei.blockInfo.blockId))
			{
				ElementInstanceMineDetailUI elementInstanceMineDetailUI2 = UIManagerControl.Instance.OpenUI("ElementInstanceMineDetailUI", UINodesManager.NormalUIRoot, false, UIType.NonPush) as ElementInstanceMineDetailUI;
				elementInstanceMineDetailUI2.blockID = bei.blockInfo.blockId;
				elementInstanceMineDetailUI2.RefreshUI();
			}
			else
			{
				ElementInstanceManager.Instance.SendExploreBlockReq(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock, raycastHit.get_collider().get_gameObject().get_name());
			}
		}
	}

	private void OnPointerDown(PointerEventData eventData)
	{
		if (ElementInstanceManager.Instance.m_isActorMoving)
		{
			return;
		}
		this.onPointerDownPlace = eventData.get_position();
	}

	private void BeginDrag(PointerEventData eventData)
	{
		this.lastDragPos = eventData.get_position();
		this.continueRotate = false;
		this.percent = 1f;
		Vector3 position = CamerasMgr.CameraUI.ScreenToWorldPoint(new Vector3(eventData.get_position().x, eventData.get_position().y, 0f));
		position.z = 0f;
		this.ImageLocate.set_position(position);
		position = this.ImageLocate.get_localPosition();
		Vector3 zero = Vector3.get_zero();
		float num = (float)CamerasMgr.Camera2RTCommon.get_pixelWidth();
		float num2 = (float)CamerasMgr.Camera2RTCommon.get_pixelHeight();
		zero.x = (position.x + num / 2f) / num * num;
		zero.y = (position.y + num2 / 2f) / num2 * num2;
		zero.z = 0f;
		Ray ray = CamerasMgr.Camera2RTCommon.ScreenPointToRay(zero);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit))
		{
			this.lastQuaterion = BallElement.Instance.get_transform().get_rotation();
			this.currentQuaterion = BallElement.Instance.get_transform().get_rotation();
			this.dragState = ElementInstanceUI.DragState.Begin;
			this.OriginalQuaternion = BallElement.Instance.get_transform().get_rotation();
			Vector3 vector = raycastHit.get_point() - BallElement.Instance.get_transform().get_position();
			this.OffsetQuaternion = Quaternion.Inverse(Quaternion.LookRotation(vector));
		}
	}

	private void OnBeginDrag(PointerEventData eventData)
	{
		if (ElementInstanceManager.Instance.m_isActorMoving)
		{
			return;
		}
		this.BeginDrag(eventData);
	}

	private void OnDragImageTouchPlace(PointerEventData eventData)
	{
		if (ElementInstanceManager.Instance.m_isActorMoving)
		{
			return;
		}
		if (this.dragState == ElementInstanceUI.DragState.End)
		{
			return;
		}
		if (this.dragState == ElementInstanceUI.DragState.ContinueDrag)
		{
			this.BeginDrag(eventData);
			return;
		}
		this.dragState = ElementInstanceUI.DragState.Dragging;
		Vector3 position = CamerasMgr.CameraUI.ScreenToWorldPoint(new Vector3(eventData.get_position().x, eventData.get_position().y, 0f));
		position.z = 0f;
		this.ImageLocate.set_position(position);
		position = this.ImageLocate.get_localPosition();
		Vector3 zero = Vector3.get_zero();
		float num = (float)CamerasMgr.Camera2RTCommon.get_pixelWidth();
		float num2 = (float)CamerasMgr.Camera2RTCommon.get_pixelHeight();
		zero.x = (position.x + num / 2f) / num * num;
		zero.y = (position.y + num2 / 2f) / num2 * num2;
		zero.z = 0f;
		Ray ray = CamerasMgr.Camera2RTCommon.ScreenPointToRay(zero);
		Vector3 vector = Vector3.get_zero();
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit))
		{
			vector = raycastHit.get_point() - BallElement.Instance.get_transform().get_position();
			Debug.DrawLine(BallElement.Instance.get_transform().get_position(), raycastHit.get_point(), Color.get_green());
			this.lastQuaterion = BallElement.Instance.get_transform().get_rotation();
			BallElement.Instance.get_transform().set_rotation(Quaternion.LookRotation(vector) * this.OffsetQuaternion * this.OriginalQuaternion);
			BallElement.Instance.ballLight.set_rotation(BallElement.Instance.get_transform().get_rotation());
			this.currentQuaterion = BallElement.Instance.get_transform().get_rotation();
		}
		else
		{
			this.dragState = ElementInstanceUI.DragState.ContinueDrag;
		}
		this.lastDragPos = eventData.get_position();
	}

	private void OnEndDragTouchPlace(PointerEventData eventData)
	{
		if (ElementInstanceManager.Instance.m_isActorMoving)
		{
			return;
		}
		if (this.dragState == ElementInstanceUI.DragState.End)
		{
			return;
		}
		this.ContinueRotate(eventData.get_position());
		this.dragState = ElementInstanceUI.DragState.End;
	}

	private void ContinueRotate(Vector2 position)
	{
		this.slowSpeed = 1f / Vector2.Distance(this.lastDragPos, position) * this.speedDelta;
		if ((double)this.slowSpeed > 0.8)
		{
			this.slowSpeed = 0.8f;
		}
		this.continueRotate = true;
		this.quaternionDir = Quaternion.Inverse(this.lastQuaterion) * this.currentQuaterion;
	}

	private void OnGetAccepttDebrisRes()
	{
		this.ListViewMine.Refresh();
	}

	private void OnGetExploreEnergyChangedNty()
	{
		this.RefreshDetail();
		this.revetEnergyTime = (float)ElementInstanceManager.Instance.m_elementCopyLoginPush.residueRecoverTime;
	}

	private void OnGetExploreBlockRes()
	{
		this.RefreshBall();
		BlockInfo bi = ElementInstanceManager.Instance.GetBlockInfo(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock);
		if ((bi.incidentType == RandomIncidentType.IncidentType.MONSTER || bi.incidentType == RandomIncidentType.IncidentType.MINE) && !bi.isChallenge)
		{
			TimerHeap.AddTimer(500u, 0, delegate
			{
				DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(502304, false), GameDataUtils.GetChineseContent(502303, false), delegate
				{
				}, delegate
				{
					ElementInstanceManager.Instance.SendStartToFightReq(bi.blockId);
				}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
			});
		}
	}

	private void OnGetBlockStatusChangedNty()
	{
		this.RefreshBall();
		this.RefreshProperty();
	}

	private void OnGetExploreLogChangedNty()
	{
		this.RefreshLogs();
	}

	private void OnGetMineInfoChangedNty()
	{
		this.RefreshBtnMines();
		BallElement.Instance.RefreshMineFX();
	}

	private void OnGetLastBlockChangedNty()
	{
		this.RefreshBall();
	}

	private void OnGetStartToFightRes()
	{
	}

	public Cell CellForRow(ListView listView, int row)
	{
		Cell cell = listView.CellForReuseIndentify("cell");
		if (cell == null)
		{
			cell = new Cell(listView);
			cell.identify = "cell";
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ElementInstanceMineObj");
			cell.content = instantiate2Prefab;
			ElementInstanceMineObj component = instantiate2Prefab.GetComponent<ElementInstanceMineObj>();
			component.BtnGet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGet);
		}
		MineInfo mineInfo = ElementInstanceManager.Instance.m_mineInfoRes.mineInfos.get_Item(row);
		ElementInstanceMineObj component2 = cell.content.GetComponent<ElementInstanceMineObj>();
		component2.SetUI(mineInfo, row);
		return cell;
	}

	public float SpacingForRow(ListView listView, int row)
	{
		return 205f;
	}

	public uint CountOfRows(ListView listView)
	{
		return (uint)ElementInstanceManager.Instance.m_mineInfoRes.mineInfos.get_Count();
	}
}
