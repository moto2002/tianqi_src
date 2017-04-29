using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradePreviewUnit : MonoBehaviour
{
	private int m_index;

	private Text m_lblPreviewName;

	private RawImage m_texPreviewModel;

	private Image m_spBackground03;

	private GameObject m_goModel;

	private GameObject m_goCamera;

	private int m_modelId;

	private void Awake()
	{
		this.m_lblPreviewName = base.get_transform().FindChild("PreviewName").GetComponent<Text>();
		this.m_texPreviewModel = base.get_transform().FindChild("PreviewModel").GetComponent<RawImage>();
		this.m_spBackground03 = base.get_transform().FindChild("Background03").GetComponent<Image>();
		this.m_texPreviewModel.set_material(RTManager.Instance.RTNoAlphaMat);
		EventTriggerListener expr_71 = EventTriggerListener.Get(base.get_gameObject());
		expr_71.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_71.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragModel));
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

	public void Init(int index, string name, float alpha)
	{
		this.m_index = index;
		this.m_lblPreviewName.set_text(name);
		this.m_spBackground03.set_color(new Color(1f, 1f, 1f, alpha));
	}

	public void SetModel(int modelId)
	{
		this.m_modelId = modelId;
		ModelDisplayManager.SetRawImage(this.m_texPreviewModel, modelId, new Vector2((float)(1000 * this.m_index), ModelDisplayManager.OFFSET_TO_PETUI.y), ref this.m_goModel, ref this.m_goCamera);
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
		this.SetModel(this.m_modelId);
	}
}
