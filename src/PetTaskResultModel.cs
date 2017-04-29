using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XEngineActor;

public class PetTaskResultModel : MonoBehaviour
{
	private int m_index;

	private RawImage m_texPreviewModel;

	private GameObject m_goModel;

	private GameObject m_goCamera;

	private int m_modelId;

	private ActorModel m_actorModel;

	private void Awake()
	{
		this.m_texPreviewModel = base.get_transform().FindChild("PreviewModel").GetComponent<RawImage>();
		this.m_texPreviewModel.set_material(RTManager.Instance.RTNoAlphaMat);
		EventTriggerListener expr_3B = EventTriggerListener.Get(base.get_gameObject());
		expr_3B.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_3B.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragModel));
	}

	private void OnDisable()
	{
		Object.Destroy(this.m_goModel);
		Object.Destroy(this.m_goCamera);
	}

	private void OnDragModel(PointerEventData eventData)
	{
		if (this.m_goModel != null)
		{
			Transform expr_1C = this.m_goModel.get_transform();
			expr_1C.set_rotation(expr_1C.get_rotation() * Quaternion.AngleAxis(-eventData.get_delta().x, Vector3.get_up()));
		}
	}

	public void Init(int index)
	{
		this.m_index = index;
	}

	public void SetModelWithSkill(int modelId, int skillId, Action actionEnd)
	{
		this.m_modelId = modelId;
		if (this.m_actorModel == null)
		{
			this.m_actorModel = this.GetAM();
		}
		if (skillId > 0)
		{
			ModelDisplayManager.ShowSkill(this.m_actorModel, skillId, actionEnd);
		}
	}

	public void SetModelWithAction(int modelId, string actionName, Action actionEnd = null)
	{
		this.m_modelId = modelId;
		if (this.m_actorModel == null)
		{
			this.m_actorModel = this.GetAM();
		}
		ModelDisplayManager.ShowAction(this.m_actorModel, actionName, actionEnd);
	}

	private ActorModel GetAM()
	{
		return ModelDisplayManager.SetRawImage(this.m_texPreviewModel, this.m_modelId, new Vector2((float)(1000 * this.m_index), ModelDisplayManager.OFFSET_TO_PETUI.y), ref this.m_goModel, ref this.m_goCamera);
	}

	private void OnApplicationPause(bool bPause)
	{
		this.FixedRT();
		TimerHeap.AddTimer(2000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && base.get_gameObject().get_activeInHierarchy())
			{
				this.FixedRT();
			}
		});
	}

	private void FixedRT()
	{
		if (this.m_goCamera == null)
		{
			return;
		}
		Camera component = this.m_goCamera.GetComponent<Camera>();
		if (component == null || component.get_targetTexture() != null)
		{
			return;
		}
		Object.Destroy(this.m_goCamera);
		this.m_goCamera = null;
		this.SetModelWithSkill(this.m_modelId, 0, null);
	}
}
