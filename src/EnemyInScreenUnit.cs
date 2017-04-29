using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class EnemyInScreenUnit : BaseUIBehaviour
{
	public class TargetAngle
	{
		public const int zTOP = 180;

		public const int zBOTTOM = 0;

		public const int zRIGHT = 90;

		public const int zLEFT = -90;
	}

	private const float NonTargetOffsetDistance = 17f;

	private const float TargetOffsetDistance = 21.5f;

	private float m_fScreenWidth;

	private float m_fScreenHeight;

	private Vector3 m_vScreenPosTarget;

	private Vector3 m_vScreenPosSelf;

	private bool m_isBehideCamera;

	private bool m_isOutsideScreen = true;

	public long uuid;

	private Transform TargetPosOfWorld;

	private ActorParent TargetActorParent;

	private bool IsTarget;

	public void AwakeSelf(Transform prefab)
	{
		this.m_myTransform = prefab;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.set_enabled(true);
	}

	private void OnEnable()
	{
		this.Show(true);
	}

	private void OnDisable()
	{
		this.Show(false);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this.uuid > 0L)
		{
			BillboardManager.Instance.RemoveBillboardsInfo(this.uuid, base.get_transform());
		}
	}

	public void SetUUID(long uuid)
	{
		this.uuid = uuid;
	}

	public Transform GetBillboardTransform()
	{
		return this.m_myTransform;
	}

	public void Show(bool isShow)
	{
		if (this.m_myTransform != null)
		{
			this.m_myTransform.get_gameObject().SetActive(isShow);
		}
	}

	public void SetTargetPosition(Transform targetPosOfWorld, ActorParent targetActorParent)
	{
		this.TargetPosOfWorld = targetPosOfWorld;
		this.TargetActorParent = targetActorParent;
	}

	private Vector3 GetTargetPosition()
	{
		if (this.TargetActorParent != null)
		{
			return this.TargetActorParent.GetAnimationFootPos();
		}
		if (this.TargetPosOfWorld != null)
		{
			return this.TargetPosOfWorld.get_position();
		}
		return Vector3.get_zero();
	}

	public void SetFlag(bool isTarget, bool isTeammate)
	{
		if (isTeammate)
		{
			isTarget = false;
		}
		this.IsTarget = isTarget;
		base.FindTransform("EnemyNonTarget").get_gameObject().SetActive(!isTarget);
		base.FindTransform("EnemyIsTarget").get_gameObject().SetActive(isTarget);
		if (isTeammate)
		{
			ResourceManager.SetSprite(base.FindTransform("EnemyNonTarget").GetComponent<Image>(), ResourceManager.GetIconSprite("xiaoguaizs2"));
		}
		else
		{
			ResourceManager.SetSprite(base.FindTransform("EnemyNonTarget").GetComponent<Image>(), ResourceManager.GetIconSprite("xiaoguaizs"));
		}
	}

	public void ResetAll()
	{
		base.ResetParent();
		this.uuid = 0L;
		this.m_myTransform = null;
		base.set_enabled(false);
	}

	private float GetOffset()
	{
		if (this.IsTarget)
		{
			return 21.5f;
		}
		return 17f;
	}

	public void LateUpdate()
	{
		if (!SystemConfig.IsBillboardOn)
		{
			this.ShowTargetTf(false);
			return;
		}
		if (this.m_myTransform == null || EntityWorld.Instance.EntSelf == null || !EntityWorld.Instance.ActSelf)
		{
			this.ShowTargetTf(false);
			return;
		}
		if (this.uuid > 0L && BillboardManager.Instance.IsBillboardInfoOff(this.uuid, BillboardManager.BillboardInfoOffOption.Arrow))
		{
			this.ShowTargetTf(false);
			return;
		}
		this.m_isOutsideScreen = true;
		this.m_isBehideCamera = false;
		this.m_fScreenWidth = (float)Screen.get_width();
		this.m_fScreenHeight = (float)Screen.get_height();
		this.m_vScreenPosTarget = CamerasMgr.CameraMain.WorldToScreenPoint(this.GetTargetPosition());
		this.m_vScreenPosSelf = CamerasMgr.CameraMain.WorldToScreenPoint(EntityWorld.Instance.EntSelf.Actor.GetAnimationFootPos());
		if (this.m_vScreenPosTarget.z < 0f)
		{
			this.m_isBehideCamera = true;
		}
		else
		{
			this.m_isBehideCamera = false;
		}
		if (this.m_vScreenPosTarget.x < 0f || this.m_vScreenPosTarget.x > this.m_fScreenWidth)
		{
			this.SetScreenX();
		}
		else if (this.m_vScreenPosTarget.y < 0f || this.m_vScreenPosTarget.y > this.m_fScreenHeight)
		{
			this.SetOutScreenY();
		}
		else if (this.m_isBehideCamera && this.m_vScreenPosTarget.y > 0f && this.m_vScreenPosTarget.y < this.m_fScreenHeight)
		{
			this.SetOutScreenYFarBehindCamera();
		}
		else
		{
			this.m_isOutsideScreen = false;
		}
		if (this.m_isOutsideScreen)
		{
			this.ShowTargetTf(true);
			this.SetTargetTfOffset();
		}
		else
		{
			this.ShowTargetTf(false);
		}
	}

	private void SetScreenXY_Xless0()
	{
		if (this.m_vScreenPosTarget.y > this.m_fScreenHeight)
		{
			if (!this.m_isBehideCamera)
			{
				float enemyInScreenY = this.GetEnemyInScreenY(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				if (enemyInScreenY > this.m_fScreenHeight)
				{
					float enemyInScreenX = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX, this.m_fScreenHeight));
					this.SetTargetTfLocalEulerAngles(180);
				}
				else if (enemyInScreenY < 0f)
				{
					float enemyInScreenX2 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX2, 0f));
					this.SetTargetTfLocalEulerAngles(0);
				}
				else
				{
					this.SetTargetTfPosition(new Vector2(0f, enemyInScreenY));
					this.SetTargetTfLocalEulerAngles(-90);
				}
			}
			else
			{
				float enemyInScreenY2 = this.GetEnemyInScreenY(this.m_fScreenWidth, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				if (enemyInScreenY2 > this.m_fScreenHeight)
				{
					float enemyInScreenX3 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX3, this.m_fScreenHeight));
					this.SetTargetTfLocalEulerAngles(180);
				}
				else if (enemyInScreenY2 < 0f)
				{
					float enemyInScreenX4 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX4, 0f));
					this.SetTargetTfLocalEulerAngles(0);
				}
				else
				{
					this.SetTargetTfPosition(new Vector2(this.m_fScreenWidth, enemyInScreenY2));
					this.SetTargetTfLocalEulerAngles(90);
				}
			}
		}
		else if (0f < this.m_vScreenPosTarget.y && this.m_vScreenPosTarget.y < this.m_fScreenHeight)
		{
			float enemyInScreenY3 = this.GetEnemyInScreenY(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
			this.SetTargetTfPosition(new Vector2(0f, enemyInScreenY3));
			this.SetTargetTfLocalEulerAngles(-90);
		}
		else if (!this.m_isBehideCamera)
		{
			float enemyInScreenY4 = this.GetEnemyInScreenY(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
			if (enemyInScreenY4 < 0f)
			{
				float enemyInScreenX5 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX5, 0f));
				this.SetTargetTfLocalEulerAngles(0);
			}
			else if (enemyInScreenY4 > this.m_fScreenHeight)
			{
				float enemyInScreenX6 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX6, this.m_fScreenHeight));
				this.SetTargetTfLocalEulerAngles(180);
			}
			else
			{
				this.SetTargetTfPosition(new Vector2(0f, enemyInScreenY4));
				this.SetTargetTfLocalEulerAngles(-90);
			}
		}
		else
		{
			float enemyInScreenY5 = this.GetEnemyInScreenY(this.m_fScreenWidth, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
			if (enemyInScreenY5 > this.m_fScreenHeight)
			{
				float enemyInScreenX7 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX7, this.m_fScreenHeight));
				this.SetTargetTfLocalEulerAngles(180);
			}
			else if (enemyInScreenY5 < 0f)
			{
				float enemyInScreenX8 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX8, 0f));
				this.SetTargetTfLocalEulerAngles(0);
			}
			else
			{
				this.SetTargetTfPosition(new Vector2(this.m_fScreenWidth, enemyInScreenY5));
				this.SetTargetTfLocalEulerAngles(90);
			}
		}
	}

	private void SetScreenXY_XBigWidth()
	{
		if (this.m_vScreenPosTarget.y > this.m_fScreenHeight)
		{
			if (!this.m_isBehideCamera)
			{
				float enemyInScreenY = this.GetEnemyInScreenY(this.m_fScreenWidth, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				if (enemyInScreenY > this.m_fScreenHeight)
				{
					float enemyInScreenX = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX, this.m_fScreenHeight));
					this.SetTargetTfLocalEulerAngles(180);
				}
				else if (enemyInScreenY < 0f)
				{
					float enemyInScreenX2 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX2, 0f));
					this.SetTargetTfLocalEulerAngles(0);
				}
				else
				{
					this.SetTargetTfPosition(new Vector2(this.m_fScreenWidth, enemyInScreenY));
					this.SetTargetTfLocalEulerAngles(90);
				}
			}
			else
			{
				float enemyInScreenY2 = this.GetEnemyInScreenY(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				if (enemyInScreenY2 < 0f)
				{
					float enemyInScreenX3 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX3, 0f));
					this.SetTargetTfLocalEulerAngles(0);
				}
				else if (enemyInScreenY2 > this.m_fScreenHeight)
				{
					float enemyInScreenX4 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
					this.SetTargetTfPosition(new Vector2(enemyInScreenX4, this.m_fScreenHeight));
					this.SetTargetTfLocalEulerAngles(180);
				}
				else
				{
					this.SetTargetTfPosition(new Vector2(0f, enemyInScreenY2));
					this.SetTargetTfLocalEulerAngles(-90);
				}
			}
		}
		else if (0f < this.m_vScreenPosTarget.y && this.m_vScreenPosTarget.y < this.m_fScreenHeight)
		{
			float enemyInScreenY3 = this.GetEnemyInScreenY(this.m_fScreenWidth, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
			this.SetTargetTfPosition(new Vector2(this.m_fScreenWidth, enemyInScreenY3));
			this.SetTargetTfLocalEulerAngles(90);
		}
		else if (!this.m_isBehideCamera)
		{
			float enemyInScreenY4 = this.GetEnemyInScreenY(this.m_fScreenWidth, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
			if (enemyInScreenY4 < 0f)
			{
				float enemyInScreenX5 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX5, 0f));
				this.SetTargetTfLocalEulerAngles(0);
			}
			else if (enemyInScreenY4 > this.m_fScreenHeight)
			{
				float enemyInScreenX6 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX6, this.m_fScreenHeight));
				this.SetTargetTfLocalEulerAngles(180);
			}
			else
			{
				this.SetTargetTfPosition(new Vector2(this.m_fScreenWidth, enemyInScreenY4));
				this.SetTargetTfLocalEulerAngles(90);
			}
		}
		else
		{
			float enemyInScreenY5 = this.GetEnemyInScreenY(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
			if (enemyInScreenY5 < 0f)
			{
				float enemyInScreenX7 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX7, 0f));
				this.SetTargetTfLocalEulerAngles(0);
			}
			else if (enemyInScreenY5 > this.m_fScreenHeight)
			{
				float enemyInScreenX8 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX8, this.m_fScreenHeight));
				this.SetTargetTfLocalEulerAngles(180);
			}
			else
			{
				this.SetTargetTfPosition(new Vector2(0f, enemyInScreenY5));
				this.SetTargetTfLocalEulerAngles(-90);
			}
		}
	}

	private void SetScreenX()
	{
		if (this.m_vScreenPosTarget.x < 0f)
		{
			this.SetScreenXY_Xless0();
		}
		else if (this.m_vScreenPosTarget.x > this.m_fScreenWidth)
		{
			this.SetScreenXY_XBigWidth();
		}
	}

	private void SetOutScreenY()
	{
		if (this.m_vScreenPosTarget.y > this.m_fScreenHeight)
		{
			if (!this.m_isBehideCamera)
			{
				float enemyInScreenX = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX, this.m_fScreenHeight));
				this.SetTargetTfLocalEulerAngles(180);
			}
			else
			{
				float enemyInScreenX2 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX2, 0f));
				this.SetTargetTfLocalEulerAngles(0);
			}
		}
		else if (this.m_vScreenPosTarget.y < 0f)
		{
			if (!this.m_isBehideCamera)
			{
				float enemyInScreenX3 = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX3, 0f));
				this.SetTargetTfLocalEulerAngles(0);
			}
			else
			{
				float enemyInScreenX4 = this.GetEnemyInScreenX(this.m_fScreenHeight, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
				this.SetTargetTfPosition(new Vector2(enemyInScreenX4, this.m_fScreenHeight));
				this.SetTargetTfLocalEulerAngles(180);
			}
		}
	}

	private void SetOutScreenYFarBehindCamera()
	{
		float enemyInScreenX = this.GetEnemyInScreenX(0f, this.m_vScreenPosTarget, this.m_vScreenPosSelf);
		this.SetTargetTfPosition(new Vector2(enemyInScreenX, 0f));
		this.SetTargetTfLocalEulerAngles(0);
	}

	private void ShowTargetTf(bool isShow)
	{
		if (this.GetBillboardTransform() == null)
		{
			return;
		}
		if (isShow)
		{
			this.GetBillboardTransform().set_localScale(Vector3.get_one());
		}
		else
		{
			this.GetBillboardTransform().set_localScale(Vector3.get_zero());
		}
	}

	private void SetTargetTfPosition(Vector2 screenPostion)
	{
		this.GetBillboardTransform().set_position(this.GetBillboardCamera().ScreenToWorldPoint(new Vector3(screenPostion.x, screenPostion.y, this.m_vScreenPosTarget.z)));
		this.GetBillboardTransform().set_localPosition(new Vector3(this.GetBillboardTransform().get_localPosition().x, this.GetBillboardTransform().get_localPosition().y, 0f));
	}

	private void SetTargetTfLocalEulerAngles(int zAngle)
	{
		this.GetBillboardTransform().set_localEulerAngles(new Vector3(0f, 0f, (float)zAngle));
		if (this.IsTarget)
		{
			base.FindTransform("EnemyIsTarget2").set_localEulerAngles(-new Vector3(0f, 0f, (float)zAngle));
		}
	}

	private void SetTargetTfOffset()
	{
		float z = this.GetBillboardTransform().get_localEulerAngles().z;
		int num = (int)z;
		if (num != -90)
		{
			if (num == 0)
			{
				Transform expr_4E = this.GetBillboardTransform();
				expr_4E.set_localPosition(expr_4E.get_localPosition() + new Vector3(0f, this.GetOffset(), 0f));
				return;
			}
			if (num == 90)
			{
				Transform expr_DF = this.GetBillboardTransform();
				expr_DF.set_localPosition(expr_DF.get_localPosition() + new Vector3(-this.GetOffset(), 0f, 0f));
				return;
			}
			if (num == 180)
			{
				Transform expr_7E = this.GetBillboardTransform();
				expr_7E.set_localPosition(expr_7E.get_localPosition() + new Vector3(0f, -this.GetOffset(), 0f));
				return;
			}
			if (num != 270)
			{
				return;
			}
		}
		Transform expr_AF = this.GetBillboardTransform();
		expr_AF.set_localPosition(expr_AF.get_localPosition() + new Vector3(this.GetOffset(), 0f, 0f));
	}

	private Camera GetBillboardCamera()
	{
		return CamerasMgr.CameraUI;
	}

	private float GetEnemyInScreenY(float x, Vector3 posTarget, Vector3 posSelf)
	{
		float num = posTarget.x - posSelf.x;
		if (num == 0f)
		{
			return 0f;
		}
		float num2 = posTarget.y - posSelf.y;
		float num3 = (x - posSelf.x) * num2 / num;
		return num3 + posSelf.y;
	}

	private float GetEnemyInScreenX(float y, Vector3 posTarget, Vector3 posSelf)
	{
		float num = posTarget.y - posSelf.y;
		if (num == 0f)
		{
			return 0f;
		}
		float num2 = posTarget.x - posSelf.x;
		if (num2 == 0f)
		{
			return posSelf.x;
		}
		float num3 = num2 * (y - posSelf.y) / num;
		return num3 + posSelf.x;
	}
}
