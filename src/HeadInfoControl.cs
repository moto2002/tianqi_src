using System;
using UnityEngine;

public class HeadInfoControl : MonoBehaviour
{
	private const float far_distance = 5f;

	private const float near_distance = 4f;

	private bool m_isOcclusionOn = true;

	public bool m_isInAVC;

	public Transform m_headInfoUI;

	public int m_actorType;

	private long _uuid;

	public long uuid
	{
		get
		{
			return this._uuid;
		}
		set
		{
			this._uuid = value;
			if (EntityWorld.Instance == null || EntityWorld.Instance.EntSelf == null)
			{
				return;
			}
			if (EntityWorld.Instance.EntSelf.ID == this._uuid)
			{
				this.m_isOcclusionOn = false;
			}
			else
			{
				this.m_isOcclusionOn = true;
			}
		}
	}

	private void OnDisable()
	{
		HeadInfoManager.Instance.show_control_normal(this.uuid, false);
	}

	private void OnDestroy()
	{
		if (this.uuid > 0L)
		{
			this.m_headInfoUI = null;
			BillboardManager.Instance.RemoveBillboardsInfo(this.uuid, null);
		}
	}

	public void ResetAll()
	{
		this.uuid = 0L;
		this.m_actorType = 0;
		this.m_headInfoUI = null;
		base.set_enabled(false);
	}

	private void LateUpdate()
	{
		Vector3 vector;
		Vector3 vector2;
		if (this.UpdatePos(out vector, out vector2))
		{
			this.DealOcclusion(ref vector, ref vector2);
		}
	}

	public void UpdatePos()
	{
		if (!ActorVisibleManager.Instance.IsShow(this.uuid))
		{
			return;
		}
		Vector3 vector;
		Vector3 vector2;
		this.UpdatePos(out vector, out vector2);
	}

	private bool UpdatePos(out Vector3 target_screen, out Vector3 actor_screen)
	{
		target_screen = Vector3.get_zero();
		actor_screen = Vector3.get_zero();
		if (!SystemConfig.IsBillboardOn)
		{
			return false;
		}
		if (HeadInfoManager.Instance.SelfHeadInfoPosition == null || this.m_headInfoUI == null)
		{
			return false;
		}
		if (CamerasMgr.CameraMain == null)
		{
			return false;
		}
		if (CamerasMgr.CameraUI == null)
		{
			return false;
		}
		if (!HeadInfoManager.Instance.IsControlOn(this.uuid))
		{
			return false;
		}
		if (!SystemConfig.IsHeadInfoOn && !EntityWorld.Instance.EntSelf.IsInBattle && this.m_actorType != 1 && this.m_actorType != 31)
		{
			HeadInfoManager.Instance.show_control_normal(this.uuid, false);
			return false;
		}
		if (this.uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(this.uuid, BillboardManager.BillboardInfoOffOption.HeadInfo))
		{
			HeadInfoManager.Instance.show_control_normal(this.uuid, false);
			return false;
		}
		Vector3 position = base.get_transform().get_position();
		Vector3 position2 = HeadInfoManager.Instance.SelfHeadInfoPosition.get_position();
		target_screen = CamerasMgr.CameraMain.WorldToScreenPoint(position);
		actor_screen = CamerasMgr.CameraMain.WorldToScreenPoint(position2);
		Vector3 position3 = CamerasMgr.CameraUI.ScreenToWorldPoint(target_screen);
		this.m_headInfoUI.set_position(position3);
		if (Mathf.Abs(this.m_headInfoUI.get_localPosition().z) > UIConst.UI_CAMERA_CLIP)
		{
			this.m_headInfoUI.set_localPosition(new Vector3(this.m_headInfoUI.get_localPosition().x, this.m_headInfoUI.get_localPosition().y, 0f));
		}
		return true;
	}

	private void DealOcclusion(ref Vector3 target_screen, ref Vector3 actor_screen)
	{
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		if (HeadInfoManager.Instance.SelfHeadInfoPosition == null)
		{
			return;
		}
		if (base.get_transform() == null)
		{
			return;
		}
		if (this.m_isOcclusionOn)
		{
			float num = Vector3.Distance(base.get_transform().get_position(), HeadInfoManager.Instance.SelfHeadInfoPosition.get_position());
			if (Mathf.Abs(num) > BillboardManager.GetDistanceOfAVC(this.m_actorType))
			{
				HeadInfoManager.Instance.show_control_normal(this.uuid, false);
				return;
			}
			if (EntityWorld.Instance.EntSelf.IsInBattle)
			{
				if (actor_screen.z - target_screen.z >= 5f)
				{
					HeadInfoManager.Instance.show_control_normal(this.uuid, false);
				}
				else if (actor_screen.z - target_screen.z <= 4f)
				{
					HeadInfoManager.Instance.show_control_normal(this.uuid, true);
				}
			}
			else if (actor_screen.z - target_screen.z >= 5f)
			{
				HeadInfoManager.Instance.show_control_normal(this.uuid, false);
			}
			else if (actor_screen.z - target_screen.z <= 4f)
			{
				HeadInfoManager.Instance.show_control_normal(this.uuid, true);
			}
		}
		else
		{
			HeadInfoManager.Instance.show_control_normal(this.uuid, true);
		}
	}
}
