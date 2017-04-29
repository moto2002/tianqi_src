using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class RankUpPreviewManager
{
	protected const string RankUpPreviewCellName = "RankUpPreviewCell";

	protected const string NewRendererName = "temp_render_texture";

	protected const string NewCameraName = "ui_temp_camera";

	protected const string MeshRootName = "body";

	protected const float UpUnitVector = 0.9f;

	protected const float ForwardUnitVector = 2.95f;

	protected static RankUpPreviewManager instance;

	public List<GameObject> cameras = new List<GameObject>();

	public List<GameObject> models = new List<GameObject>();

	protected List<int> pool = new List<int>();

	public static RankUpPreviewManager Instance
	{
		get
		{
			if (RankUpPreviewManager.instance == null)
			{
				RankUpPreviewManager.instance = new RankUpPreviewManager();
			}
			return RankUpPreviewManager.instance;
		}
	}

	protected RankUpPreviewManager()
	{
	}

	public void Release()
	{
	}

	public RankUpPreviewCell GetPreview(Transform root)
	{
		GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("RankUpPreviewCell");
		instantiate2Prefab.get_transform().SetParent(root, false);
		instantiate2Prefab.SetActive(true);
		return instantiate2Prefab.GetComponent<RankUpPreviewCell>();
	}

	public void ReleasePreview(RankUpPreviewCell rankUpPreviewCell)
	{
		if (!rankUpPreviewCell)
		{
			return;
		}
		this.ReturnPoolID(rankUpPreviewCell.ModelIndex);
		rankUpPreviewCell.ResetModelData();
	}

	protected int BorrowModelIndex()
	{
		if (this.pool.get_Count() == 0)
		{
			this.pool.Add(0);
			return 0;
		}
		for (int i = 0; i < this.pool.get_Count(); i++)
		{
			if (this.pool.get_Item(i) == -1)
			{
				this.pool.set_Item(i, i);
				return i;
			}
		}
		int count = this.pool.get_Count();
		this.pool.Add(count);
		return count;
	}

	protected void ReturnPoolID(int index)
	{
		if (index > this.pool.get_Count() - 1 || index < 0)
		{
			Debug.LogError("Logic Error!");
			return;
		}
		this.pool.set_Item(index, -1);
		if (index == this.pool.get_Count() - 1)
		{
			int num = 0;
			for (int i = this.pool.get_Count() - 1; i > -1; i--)
			{
				if (this.pool.get_Item(i) == -1)
				{
					num++;
				}
			}
			this.pool.RemoveRange(this.pool.get_Count() - num, num);
		}
	}

	public void ReleaseAllPreview()
	{
		this.pool.Clear();
		for (int i = 0; i < this.cameras.get_Count(); i++)
		{
			if (this.cameras.get_Item(i).get_gameObject())
			{
				Object.Destroy(this.cameras.get_Item(i).get_gameObject());
			}
		}
		this.cameras.Clear();
		for (int j = 0; j < this.models.get_Count(); j++)
		{
			if (this.models.get_Item(j).get_gameObject())
			{
				Object.Destroy(this.models.get_Item(j).get_gameObject());
			}
		}
		this.models.Clear();
	}

	public List<GameObject> SetModelData(RawImage rawImage, ExteriorArithmeticUnit exteriorUnit, out int index)
	{
		List<GameObject> list = new List<GameObject>();
		index = this.BorrowModelIndex();
		RenderTexture renderTexture = null;
		RTManager.CreateRenderTexture(ref renderTexture, "temp_render_texture");
		rawImage.GetComponent<RectTransform>().set_sizeDelta(new Vector2((float)renderTexture.get_width(), (float)renderTexture.get_height()));
		RTManager.SetRT(rawImage, renderTexture);
		Camera camera = this.CreateModelCamera(renderTexture);
		list.Add(camera.get_gameObject());
		ActorModel model = this.GetModel(index, exteriorUnit);
		list.Add(model.get_gameObject());
		camera.get_transform().set_rotation(model.get_transform().get_rotation() * Quaternion.AngleAxis(180f, Vector3.get_up()));
		Vector3 vector = model.get_transform().get_up() * 0.9f;
		Vector3 vector2 = model.get_transform().get_forward() * 2.95f;
		camera.get_transform().set_position(model.get_transform().get_position() + vector + vector2);
		return list;
	}

	protected Camera CreateModelCamera(RenderTexture renderTexture)
	{
		GameObject gameObject = new GameObject("ui_temp_camera");
		Camera camera = gameObject.AddComponent<Camera>();
		camera.set_targetTexture(renderTexture);
		camera.set_cullingMask(1 << LayerMask.NameToLayer("NPC"));
		camera.set_orthographic(true);
		camera.set_orthographicSize(1.6f);
		camera.set_useOcclusionCulling(false);
		return camera;
	}

	protected ActorModel GetModel(int index, ExteriorArithmeticUnit exteriorUnit)
	{
		ActorModel actorModel = ModelPool.Instance.Get(exteriorUnit.FinalModelID);
		actorModel.ModelType = ActorModelType.UI;
		actorModel.ModelLayer = "NPC";
		actorModel.set_name(index.ToString());
		actorModel.get_transform().set_position(new Vector3((float)(-1000 * (index + 1)), 1000f, 0f));
		actorModel.EquipOn(exteriorUnit.FinalWeaponID, exteriorUnit.FinalWeaponGogok);
		actorModel.EquipOn(exteriorUnit.FinalClothesID, 0);
		actorModel.EquipWingOn(exteriorUnit.FinalWingID);
		LayerSystem.SetGameObjectLayer(actorModel.get_gameObject(), "NPC", 1);
		actorModel.PreciseSetAction("idle_city");
		return actorModel;
	}
}
