using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;

public class BallElement : MonoBehaviour
{
	public static string BallItemInfos = "BallItemInfos";

	public static BallElement Instance;

	public Transform ballLight;

	public Transform angleZeroItem;

	public float touchRate = 0.1f;

	public float aRotation = 2f;

	public float rotationSpeed;

	private List<YBanKuaiSuoYin> listPentagon = new List<YBanKuaiSuoYin>();

	public string lastBlockChche = string.Empty;

	public bool shouldChangePosImmediately = true;

	public bool havaAddItemEffect;

	public ElementInstanceUI elementInstanceUI;

	public float distanceVisable;

	private Transform transformActor;

	private float actorMovePercent;

	private Vector3 actorPosFrom;

	private Vector3 actorPosTo;

	private List<string> listFXBlockID = new List<string>();

	private uint timeCalNear;

	private void Awake()
	{
		BallElement.Instance = this;
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
		this.ballLight = base.get_transform().get_parent().FindChild("light");
		for (int i = 0; i < base.get_transform().get_childCount(); i++)
		{
			Transform child = base.get_transform().GetChild(i);
			if (!child.get_name().Contains("FLAG"))
			{
				child.get_gameObject().AddComponent<BallElementItem>();
			}
		}
		List<YBanKuaiSuoYin> dataList = DataReader<YBanKuaiSuoYin>.DataList;
		for (int j = 0; j < dataList.get_Count(); j++)
		{
			YBanKuaiSuoYin yBanKuaiSuoYin = dataList.get_Item(j);
			if (yBanKuaiSuoYin.pentagon == 1)
			{
				this.listPentagon.Add(yBanKuaiSuoYin);
			}
		}
		string name = string.Empty;
		if (EntityWorld.Instance.EntSelf.TypeID == 1)
		{
			name = DataReader<YWanFaSheZhi>.Get("boyModel").value;
		}
		else
		{
			name = DataReader<YWanFaSheZhi>.Get("girlModel").value;
		}
		GameObject gameObject = Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject;
		this.transformActor = gameObject.get_transform();
	}

	private void Update()
	{
		if (ElementInstanceManager.Instance.m_isActorMoving)
		{
			this.actorMovePercent += Time.get_deltaTime() * 4f;
			this.transformActor.set_position(Vector3.Lerp(this.actorPosFrom, this.actorPosTo, this.actorMovePercent));
			if (this.actorMovePercent > 1f)
			{
				ElementInstanceManager.Instance.m_isActorMoving = false;
				this.actorMovePercent = 0f;
			}
		}
	}

	private void OnEnable()
	{
		Utils.EnableRoleLight(false);
		base.get_transform().get_parent().FindChild("BallLight").GetComponent<Light>().set_enabled(true);
		ElementInstanceManager.Instance.m_isActorMoving = false;
	}

	private void OnDisable()
	{
		Utils.EnableRoleLight(true);
	}

	private void LoadSceneEnd(int sceneID)
	{
		if (MySceneManager.IsMainScene(sceneID))
		{
			Utils.EnableRoleLight(false);
		}
	}

	public void CalDistance()
	{
		Transform transform = base.get_transform().FindChild("AA");
		float num = Vector3.Distance(transform.get_position(), base.get_transform().get_position());
		float num2 = Vector3.Distance(base.get_transform().get_position(), CamerasMgr.Camera2RTCommon.get_transform().get_position());
		this.distanceVisable = Mathf.Sqrt(num * num + num2 * num2);
		this.distanceVisable *= 0.98f;
	}

	private void SetActorPlace()
	{
	}

	public void RefreshBallIcons()
	{
		if (!this.lastBlockChche.Equals(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock))
		{
			if (this.lastBlockChche.get_Length() != 0)
			{
				Transform transform = base.get_transform().FindChild(this.lastBlockChche);
				BallElementItem component = transform.GetComponent<BallElementItem>();
				component.isActor = false;
			}
			Transform transform2 = base.get_transform().FindChild(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock);
			BallElementItem component2 = transform2.GetComponent<BallElementItem>();
			component2.isActor = true;
			this.transformActor.set_parent(transform2);
			this.transformActor.set_localRotation(Quaternion.Euler(90f, 0f, 0f));
			this.transformActor.set_localScale(new Vector3(0.1f, 0.1f, 0.1f));
			Transform transform3 = this.transformActor.FindChild("Actor").FindChild("root");
			Transform transform4 = this.transformActor.FindChild("Actor").FindChild("front");
			Vector3 vector = transform2.get_position() - transform3.get_position();
			Vector3 vector2 = transform4.get_position() - transform3.get_position();
			float num = Vector3.Dot(vector.get_normalized(), vector2.get_normalized());
			float num2 = Mathf.Acos(num);
			num2 *= 57.2957764f;
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			float num3 = Vector3.Distance(base.get_transform().get_position(), transform3.get_position() + vector3);
			float num4 = Vector3.Distance(base.get_transform().get_position(), transform3.get_position());
			if (num3 > num4)
			{
				num2 = -num2;
			}
			Transform expr_1AD = this.transformActor.FindChild("Actor");
			expr_1AD.set_localRotation(expr_1AD.get_localRotation() * Quaternion.Euler(0f, 0f, num2));
			if (this.shouldChangePosImmediately)
			{
				this.shouldChangePosImmediately = false;
				ElementInstanceManager.Instance.m_isActorMoving = false;
				this.transformActor.set_position(transform2.get_position());
			}
			else
			{
				ElementInstanceManager.Instance.m_isActorMoving = true;
				this.actorPosFrom = transform3.get_position();
				this.actorPosTo = transform2.get_position();
			}
			this.lastBlockChche = ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock;
		}
		TimerHeap.DelTimer(this.timeCalNear);
		uint start = 0u;
		if (this.timeCalNear != 0u)
		{
			start = 200u;
		}
		this.timeCalNear = TimerHeap.AddTimer(start, 0, delegate
		{
			List<BlockInfo> activateBlocks = ElementInstanceManager.Instance.m_elementCopyLoginPush.activateBlocks;
			Material material = BallElement.GetAssetByName("MaterialBallItemPassBy", typeof(Material)) as Material;
			Material material2 = BallElement.GetAssetByName("MaterialBallItem", typeof(Material)) as Material;
			Material material3 = BallElement.GetAssetByName("MaterialBallItemPassByPentagon", typeof(Material)) as Material;
			Material material4 = BallElement.GetAssetByName("MaterialBallItem_Pentagon", typeof(Material)) as Material;
			Material material5 = BallElement.GetAssetByName("MaterialBallItemNearPentagon", typeof(Material)) as Material;
			Material material6 = BallElement.GetAssetByName("MaterialBallItemNear", typeof(Material)) as Material;
			Material material7 = BallElement.GetAssetByName("MaterialBallItemNearPassBy", typeof(Material)) as Material;
			Material material8 = BallElement.GetAssetByName("MaterialBallItemNearPentagonPassBy", typeof(Material)) as Material;
			Material material9 = BallElement.GetAssetByName("MaterialBallItemMineNear", typeof(Material)) as Material;
			Material material10 = BallElement.GetAssetByName("MaterialBallItemMine", typeof(Material)) as Material;
			for (int i = 0; i < this.ballLight.get_childCount(); i++)
			{
				if (this.ballLight.GetChild(i).get_gameObject().get_activeSelf())
				{
					this.ballLight.GetChild(i).get_gameObject().SetActive(false);
				}
			}
			List<string> around = DataReader<YBanKuaiSuoYin>.Get(ElementInstanceManager.Instance.m_elementCopyLoginPush.lastBlock).around;
			for (int j = 0; j < activateBlocks.get_Count(); j++)
			{
				BlockInfo bi = activateBlocks.get_Item(j);
				Transform transform5 = base.get_transform().FindChild(bi.blockId);
				BallElementItem component3 = transform5.GetComponent<BallElementItem>();
				Material mat;
				if (this.listPentagon.Find((YBanKuaiSuoYin a) => a.ballId.Equals(bi.blockId)) != null)
				{
					if ((around.Contains(bi.blockId) && bi.incidentType != RandomIncidentType.IncidentType.ROADBLOCK) || component3.isActor)
					{
						if (bi.isChallenge)
						{
							mat = material8;
						}
						else
						{
							mat = material5;
						}
					}
					else if (bi.isChallenge)
					{
						mat = material3;
					}
					else
					{
						mat = material4;
					}
				}
				else if ((around.Contains(bi.blockId) && bi.incidentType != RandomIncidentType.IncidentType.ROADBLOCK) || component3.isActor)
				{
					if (bi.isChallenge)
					{
						mat = material7;
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.MINE)
					{
						mat = material9;
					}
					else
					{
						mat = material6;
					}
				}
				else if (bi.isChallenge)
				{
					mat = material;
				}
				else if (bi.incidentType == RandomIncidentType.IncidentType.MINE)
				{
					mat = material10;
				}
				else
				{
					mat = material2;
				}
				Utils.SetShareMaterial(transform5.GetComponent<MeshRenderer>(), mat);
				if (component3.blockInfo != bi)
				{
					component3.blockInfo = bi;
					if (component3.gameObjectBlockInfo != null)
					{
						if (bi.incidentType == RandomIncidentType.IncidentType.PETPROPERTY || bi.incidentType == RandomIncidentType.IncidentType.PLAYERPROPERTY || bi.incidentType == RandomIncidentType.IncidentType.TOOL || bi.incidentType == RandomIncidentType.IncidentType.RECOVRYENERGY)
						{
							GameObject goInstantiate = Object.Instantiate<GameObject>(component3.gameObjectBlockInfo);
							ResourceManager.SetInstantiateUIRef(goInstantiate, null);
							goInstantiate.get_transform().set_parent(component3.gameObjectBlockInfo.get_transform().get_parent());
							goInstantiate.get_transform().set_position(component3.gameObjectBlockInfo.get_transform().get_position());
							goInstantiate.get_transform().set_localScale(component3.gameObjectBlockInfo.get_transform().get_localScale());
							goInstantiate.get_transform().set_localRotation(component3.gameObjectBlockInfo.get_transform().get_localRotation());
							TimerHeap.AddTimer(50u, 0, delegate
							{
								Object.Destroy(goInstantiate);
							});
						}
						Object.Destroy(component3.gameObjectBlockInfo);
					}
					GameObject gameObject = null;
					string name = string.Empty;
					string text = string.Empty;
					if (bi.incidentType == RandomIncidentType.IncidentType.MINE)
					{
						name = DataReader<YKuangJingKu>.Get(bi.incidentTypeId).Model;
						gameObject = (Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject);
						text = DataReader<YKuangJingKu>.Get(bi.incidentTypeId).holdName;
						ElementInstanceUI.Instance.SetInfoUnit(component3, text);
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.MONSTER)
					{
						name = DataReader<YGuaiWuKu>.Get(bi.incidentTypeId).Model;
						if (bi.isChallenge)
						{
							gameObject = new GameObject("ElementEMPTY");
						}
						else
						{
							text = DataReader<YGuaiWuKu>.Get(bi.incidentTypeId).Name;
							gameObject = (Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject);
						}
						ElementInstanceUI.Instance.SetInfoUnit(component3, text);
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.PETPROPERTY)
					{
						name = DataReader<YChongWuJiaChengKu>.Get(bi.incidentTypeId).Model;
						if (bi.isChallenge)
						{
							gameObject = new GameObject("ElementEMPTY");
						}
						else
						{
							gameObject = (Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject);
							text = DataReader<YChongWuJiaChengKu>.Get(bi.incidentTypeId).eventName;
						}
						ElementInstanceUI.Instance.SetInfoUnit(component3, text);
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.PLAYERPROPERTY)
					{
						name = DataReader<YJiaoSeJiaChengKu>.Get(bi.incidentTypeId).Model;
						if (bi.isChallenge)
						{
							gameObject = new GameObject("ElementEMPTY");
						}
						else
						{
							gameObject = (Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject);
							text = DataReader<YJiaoSeJiaChengKu>.Get(bi.incidentTypeId).eventName;
						}
						ElementInstanceUI.Instance.SetInfoUnit(component3, text);
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.ROADBLOCK)
					{
						name = DataReader<YWanFaSheZhi>.Get("roadBlockModel").value;
						gameObject = (Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject);
						ElementInstanceUI.Instance.SetInfoUnit(component3, GameDataUtils.GetChineseContent(502317, false));
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.TOOL || bi.incidentType == RandomIncidentType.IncidentType.RECOVRYENERGY)
					{
						name = DataReader<YDaoJuKu>.Get(bi.incidentTypeId).Model;
						if (bi.isChallenge)
						{
							gameObject = new GameObject("ElementEMPTY");
						}
						else
						{
							gameObject = (Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(name), typeof(Object))) as GameObject);
							text = DataReader<YDaoJuKu>.Get(bi.incidentTypeId).holdName;
						}
						ElementInstanceUI.Instance.SetInfoUnit(component3, text);
					}
					else if (bi.incidentType == RandomIncidentType.IncidentType.EMPTY)
					{
						gameObject = new GameObject("ElementEMPTY");
						ElementInstanceUI.Instance.SetInfoUnit(component3, string.Empty);
					}
					gameObject.get_transform().set_parent(transform5);
					gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
					gameObject.get_transform().set_localRotation(Quaternion.Euler(90f, 0f, 0f));
					component3.gameObjectBlockInfo = gameObject;
				}
			}
			for (int k = 0; k < ElementInstanceManager.Instance.m_elementCopyLoginPush.mineBlockId.get_Count(); k++)
			{
				string text2 = ElementInstanceManager.Instance.m_elementCopyLoginPush.mineBlockId.get_Item(k);
				Transform transform6 = base.get_transform().FindChild(text2);
				BallElementItem component4 = transform6.GetComponent<BallElementItem>();
				if (!(component4.gameObjectBlockInfo != null))
				{
					string value = DataReader<YWanFaSheZhi>.Get("lockMineModel").value;
					GameObject gameObject2 = Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab(value), typeof(Object))) as GameObject;
					ElementInstanceUI.Instance.SetInfoUnit(component4, GameDataUtils.GetChineseContent(502316, false));
					gameObject2.get_transform().set_parent(transform6);
					gameObject2.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
					gameObject2.get_transform().set_localRotation(Quaternion.Euler(90f, 0f, 0f));
					component4.gameObjectBlockInfo = gameObject2;
				}
			}
			for (int l = 0; l < this.listPentagon.get_Count(); l++)
			{
				YBanKuaiSuoYin yBanKuaiSuoYin = this.listPentagon.get_Item(l);
				if (base.get_transform().FindChild(yBanKuaiSuoYin.ballId).GetComponent<BallElementItem>().blockInfo == null)
				{
					Utils.SetShareMaterial(base.get_transform().FindChild(yBanKuaiSuoYin.ballId).GetComponent<MeshRenderer>(), material4);
				}
			}
			this.RefreshMineFX();
		});
	}

	public void RefreshMineFX()
	{
		int i;
		for (i = 0; i < this.listFXBlockID.get_Count(); i++)
		{
			if (ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.Find((MinePetInfo a) => a.blockId.Equals(this.listFXBlockID.get_Item(i))) == null)
			{
				Object.Destroy(base.get_transform().FindChild(this.listFXBlockID.get_Item(i)).FindChild("FX").get_gameObject());
				this.listFXBlockID.RemoveAt(i);
				i--;
			}
		}
		for (int j = 0; j < ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.get_Count(); j++)
		{
			MinePetInfo minePetInfo = ElementInstanceManager.Instance.m_elementCopyLoginPush.minePetInfos.get_Item(j);
			if (this.listFXBlockID.Find((string a) => a.Equals(minePetInfo.blockId)) == null)
			{
				Transform parent = base.get_transform().FindChild(minePetInfo.blockId);
				GameObject gameObject = Object.Instantiate<GameObject>(AssetManager.AssetOfNoPool.LoadAssetNowNoAB("GameEffect/Prefabs/tongyong/f00131", typeof(Object)) as GameObject);
				gameObject.SetActive(true);
				gameObject.get_transform().set_parent(parent);
				gameObject.get_transform().set_localScale(new Vector3(0.08f, 0.08f, 0.08f));
				gameObject.get_transform().set_localPosition(Vector3.get_zero());
				gameObject.get_transform().set_localRotation(Quaternion.Euler(90f, 0f, 0f));
				gameObject.set_name("FX");
				this.listFXBlockID.Add(minePetInfo.blockId);
			}
		}
	}

	public static Object GetAssetByName(string name, Type type)
	{
		return AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPath(name, string.Empty), type);
	}
}
