using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RankUpPreviewCell : BaseUIBehaviour
{
	protected RawImage RankUpPreviewCellModelProjection;

	protected UIBase bindUI;

	protected int modelID;

	protected ExteriorArithmeticUnit exteriorUnit;

	protected int modelIndex = -1;

	protected GameObject previewCamera;

	protected GameObject previewModel;

	public ExteriorArithmeticUnit ExteriorUnit
	{
		get
		{
			if (this.exteriorUnit == null)
			{
				this.exteriorUnit = new ExteriorArithmeticUnit(null, null, null, null);
			}
			return this.exteriorUnit;
		}
	}

	public int ModelIndex
	{
		get
		{
			return this.modelIndex;
		}
		set
		{
			this.modelIndex = value;
		}
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.RankUpPreviewCellModelProjection = base.FindTransform("RankUpPreviewCellModelProjection").GetComponent<RawImage>();
		EventTriggerListener expr_29 = EventTriggerListener.Get(base.get_gameObject());
		expr_29.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_29.onDrag, new EventTriggerListener.VoidDelegateData(this.OnDragModel));
	}

	private void OnDestroy()
	{
		RankUpPreviewManager.Instance.ReleasePreview(this);
	}

	public void Bind(UIBase theBindUI)
	{
		this.bindUI = theBindUI;
	}

	public void SetData(int theModelID)
	{
		this.modelID = theModelID;
		this.SetModelData(this.modelID);
	}

	protected void SetModelData(int modelID)
	{
		this.ExteriorUnit.WrapSetData(delegate
		{
			this.ExteriorUnit.Clone(EntityWorld.Instance.EntSelf.ExteriorUnit, false);
			this.ExteriorUnit.ClientModelID = 0;
			this.ExteriorUnit.FashionIDs = null;
			this.ExteriorUnit.ServerModelID = modelID;
		});
		List<GameObject> list = RankUpPreviewManager.Instance.SetModelData(this.RankUpPreviewCellModelProjection, this.ExteriorUnit, out this.modelIndex);
		if (list.get_Count() > 0)
		{
			this.previewCamera = list.get_Item(0);
		}
		if (list.get_Count() > 1)
		{
			this.previewModel = list.get_Item(1);
		}
	}

	public void ResetModelData()
	{
		if (this.previewCamera)
		{
			Object.Destroy(this.previewCamera);
		}
		if (this.previewModel)
		{
			Object.Destroy(this.previewModel);
		}
	}

	protected void OnDragModel(PointerEventData eventData)
	{
		if (!this.previewModel)
		{
			return;
		}
		Transform expr_1C = this.previewModel.get_transform();
		expr_1C.set_rotation(expr_1C.get_rotation() * Quaternion.AngleAxis(-eventData.get_delta().x, Vector3.get_up()));
	}

	public void DoOnApplicationPause()
	{
		this.SetModelData(this.modelID);
	}
}
