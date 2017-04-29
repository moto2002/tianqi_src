using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBloodControl : MonoBehaviour
{
	public long m_id;

	private RectTransform _WaveBloodRTransform;

	private List<WaveBloodUnit> m_listWBU = new List<WaveBloodUnit>();

	public float rectWidth = 200f;

	public float rectHeight = 100f;

	private Keyframe[] kf_offsets_x;

	private Keyframe[] kf_offsets_y;

	private Keyframe[] kf_alphas;

	private Keyframe[] kf_scales;

	private float time_offset_x;

	private float time_offset_y;

	private float time_alpha;

	private float time_scale;

	private float max_time;

	public RectTransform WaveBloodRTransform
	{
		get
		{
			return this._WaveBloodRTransform;
		}
		set
		{
			this._WaveBloodRTransform = value;
		}
	}

	private void OnEnable()
	{
		if (this.WaveBloodRTransform != null)
		{
			this.WaveBloodRTransform.get_gameObject().SetActive(true);
		}
	}

	private void OnDestroy()
	{
		this.DeleteAll();
	}

	private void LateUpdate()
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (this._WaveBloodRTransform == null)
		{
			return;
		}
		float unscaledTime = Time.get_unscaledTime();
		int i = this.m_listWBU.get_Count();
		while (i > 0)
		{
			WaveBloodUnit waveBloodUnit = this.m_listWBU.get_Item(--i);
			this.CalMaxTime(waveBloodUnit);
			float num = unscaledTime - waveBloodUnit.movementStart;
			waveBloodUnit.offset_x = waveBloodUnit.offsetXCurve.Evaluate(num) + waveBloodUnit.screenPos.x;
			waveBloodUnit.offset_y = waveBloodUnit.offsetYCurve.Evaluate(num) + waveBloodUnit.screenPos.y;
			float a = waveBloodUnit.alphaCurve.Evaluate(num);
			WaveBloodPerformance wbp = waveBloodUnit.wbp;
			switch (wbp)
			{
			case WaveBloodPerformance.NumToHurt:
			case WaveBloodPerformance.NumToCritical:
			case WaveBloodPerformance.NumToTreat:
				for (int j = 0; j < waveBloodUnit.listImageText.get_Count(); j++)
				{
					Image image = waveBloodUnit.listImageText.get_Item(j);
					Color color = image.get_color();
					color.a = a;
					image.set_color(color);
				}
				break;
			default:
				switch (wbp)
				{
				case WaveBloodPerformance.MissToSelf:
				case WaveBloodPerformance.MissToOther:
				{
					Color white = Color.get_white();
					white.a = a;
					waveBloodUnit.m_spImage.set_color(white);
					break;
				}
				case WaveBloodPerformance.Break:
				{
					Color white2 = Color.get_white();
					white2.a = a;
					waveBloodUnit.m_spImage.set_color(white2);
					break;
				}
				}
				break;
			}
			float num2 = waveBloodUnit.scaleCurve.Evaluate(num);
			if (num2 < 0.001f)
			{
				num2 = 0f;
			}
			if (waveBloodUnit.thisTransform == null)
			{
				Debug.LogError("ent.thisTransform is null");
			}
			else
			{
				waveBloodUnit.thisTransform.set_localScale(Vector3.get_one() * num2);
			}
			if (num > this.max_time)
			{
				this.Delete(waveBloodUnit);
			}
			else if (waveBloodUnit.thisTransform == null)
			{
				Debug.LogError("ent.thisTransform is null");
			}
			else
			{
				waveBloodUnit.thisTransform.get_gameObject().SetActive(true);
			}
		}
		int k = this.m_listWBU.get_Count();
		while (k > 0)
		{
			WaveBloodUnit waveBloodUnit2 = this.m_listWBU.get_Item(--k);
			if (waveBloodUnit2 == null)
			{
				Debug.LogError("ent is null");
			}
			else if (waveBloodUnit2.thisTransform == null)
			{
				Debug.LogError("ent.thisTransform is null");
			}
			else
			{
				Vector2 anchoredPosition = waveBloodUnit2.thisTransform.get_anchoredPosition();
				anchoredPosition.x = waveBloodUnit2.offset_x;
				anchoredPosition.y = waveBloodUnit2.offset_y;
				waveBloodUnit2.thisTransform.set_anchoredPosition(anchoredPosition);
			}
		}
	}

	private void CalMaxTime(WaveBloodUnit ent)
	{
		this.kf_offsets_x = ent.offsetXCurve.get_keys();
		this.kf_offsets_y = ent.offsetYCurve.get_keys();
		this.kf_alphas = ent.alphaCurve.get_keys();
		this.kf_scales = ent.scaleCurve.get_keys();
		if (this.kf_offsets_x.Length > 0)
		{
			this.time_offset_x = this.kf_offsets_x[this.kf_offsets_x.Length - 1].get_time();
		}
		else
		{
			this.time_offset_x = 0f;
		}
		if (this.kf_offsets_y.Length > 0)
		{
			this.time_offset_y = this.kf_offsets_y[this.kf_offsets_y.Length - 1].get_time();
		}
		else
		{
			this.time_offset_y = 0f;
		}
		if (this.kf_alphas.Length > 0)
		{
			this.time_alpha = this.kf_alphas[this.kf_alphas.Length - 1].get_time();
		}
		else
		{
			this.time_alpha = 0f;
		}
		if (this.kf_scales.Length > 0)
		{
			this.time_scale = this.kf_scales[this.kf_scales.Length - 1].get_time();
		}
		else
		{
			this.time_scale = 0f;
		}
		this.max_time = 0f;
		if (this.time_offset_x > this.max_time)
		{
			this.max_time = this.time_offset_x;
		}
		if (this.time_offset_y > this.max_time)
		{
			this.max_time = this.time_offset_y;
		}
		if (this.time_alpha > this.max_time)
		{
			this.max_time = this.time_alpha;
		}
		if (this.time_scale > this.max_time)
		{
			this.max_time = this.time_scale;
		}
	}

	private void Delete(WaveBloodUnit ent)
	{
		this.m_listWBU.Remove(ent);
		if (ent != null && ent.thisTransform != null)
		{
			WaveBloodManager.Instance.ReuseUnit(ent.thisTransform.get_gameObject());
		}
	}

	public void DeleteAll()
	{
		for (int i = 0; i < this.m_listWBU.get_Count(); i++)
		{
			if (this.m_listWBU.get_Item(i) != null && this.m_listWBU.get_Item(i).thisTransform != null)
			{
				WaveBloodManager.Instance.ReuseUnit(this.m_listWBU.get_Item(i).thisTransform.get_gameObject());
			}
		}
		this.m_listWBU.Clear();
	}

	private static int Comparison(WaveBloodUnit a, WaveBloodUnit b)
	{
		if (a.movementStart < b.movementStart)
		{
			return -1;
		}
		if (a.movementStart > b.movementStart)
		{
			return 1;
		}
		return 0;
	}

	private WaveBloodUnit BloodCreate(Color color, int num, int element, WaveBloodPerformance wbp, int trackID, int resource, int damageType, List<int> cellSize)
	{
		WaveBloodUnit unit = WaveBloodManager.Instance.GetUnit();
		this.BloodSetting(unit, color, num, element, wbp, trackID, resource, damageType, cellSize);
		this.m_listWBU.Add(unit);
		return unit;
	}

	private void BloodSetting(WaveBloodUnit gridUI, Color color, int num, int element, WaveBloodPerformance wbp, int trackID, int resource, int damageType, List<int> cellSize)
	{
		gridUI.thisTransform.SetParent(this._WaveBloodRTransform);
		gridUI.thisTransform.set_localScale(Vector3.get_one());
		Vector3 position = Utils.WorldToUIPos(base.get_transform().get_position());
		position.z = 0f;
		RectTransform rectTransform = gridUI.get_transform() as RectTransform;
		rectTransform.set_position(position);
		Vector2 anchoredPosition = rectTransform.get_anchoredPosition();
		anchoredPosition.x += gridUI.horizontalOffset;
		anchoredPosition.y += gridUI.verticalOffset;
		rectTransform.set_anchoredPosition(anchoredPosition);
		gridUI.screenPos = rectTransform.get_anchoredPosition();
		gridUI.time = Time.get_unscaledTime();
		gridUI.thisTransform.SetAsLastSibling();
		gridUI.cellSize = cellSize;
		WaveBloodAnimCurve.GetTrackList(trackID, ref gridUI.offsetXCurve, ref gridUI.offsetYCurve, ref gridUI.alphaCurve, ref gridUI.scaleCurve);
		gridUI.wbp = wbp;
		switch (wbp)
		{
		case WaveBloodPerformance.NumToHurt:
		case WaveBloodPerformance.NumToCritical:
		case WaveBloodPerformance.NumToTreat:
			gridUI.SetNum(num, resource);
			break;
		default:
			switch (wbp)
			{
			case WaveBloodPerformance.MissToSelf:
			case WaveBloodPerformance.MissToOther:
			case WaveBloodPerformance.Break:
				gridUI.SetImage(true, wbp);
				break;
			}
			break;
		}
	}

	public void SetID(long id)
	{
		this.m_id = id;
	}

	public void ShowText(int text, Color color, int element, WaveBloodPerformance wbp, int trackID, int resource, int damageType, List<int> cellSize)
	{
		if (!base.get_enabled())
		{
			return;
		}
		if (text == 0 && WaveBloodManager.IsNum(wbp))
		{
			return;
		}
		float num = Vector3.Distance(base.get_transform().get_position(), CamerasMgr.MainCameraRoot.get_position()) * 0.01f;
		if (num > 95f)
		{
			num = 95f;
		}
		float num2 = (100f - num) * 0.01f;
		float num3 = WaveBloodManager.Instance.blood_number_deviation_width * num2;
		float num4 = WaveBloodManager.Instance.blood_number_deviation_height * num2;
		float num5 = Random.get_value();
		float value = Random.get_value();
		if ((double)value > 0.5)
		{
			num5 = 0f - num5;
		}
		float value2 = Random.get_value();
		WaveBloodUnit waveBloodUnit = this.BloodCreate(color, text, element, wbp, trackID, resource, damageType, cellSize);
		waveBloodUnit.stay = 0f;
		waveBloodUnit.val = (float)text;
		waveBloodUnit.horizontalOffset = num3 * 0.5f * num5;
		waveBloodUnit.verticalOffset = num4 * value2;
		color.a = 0f;
		this.m_listWBU.Sort(new Comparison<WaveBloodUnit>(WaveBloodControl.Comparison));
	}
}
