using System;
using System.Collections.Generic;
using UnityEngine;

namespace XEngineActor
{
	public class ActorCityPlayer : ActorParentContainer<EntityCityPlayer>
	{
		protected uint stopTimer;

		protected override void Update()
		{
			if (this.GetEntity() == null)
			{
				return;
			}
			this.MoveProcess();
		}

		protected override void OnDestroy()
		{
			TimerHeap.DelTimer(this.stopTimer);
			ActorVisibleManager.Instance.Remove(base.FixTransform);
			base.OnDestroy();
		}

		public override void ClearData()
		{
			TimerHeap.DelTimer(this.stopTimer);
			base.ClearData();
		}

		public override void DestroyScript()
		{
			if (this.GetEntity() != null)
			{
				(this.GetEntity() as EntityCityPlayer).EquipCustomizationer.Release();
			}
			base.DestroyScript();
		}

		public override void FadeDestroyScript(List<AdjustTransparency> alphaControls)
		{
			if (this.GetEntity() != null)
			{
				(this.GetEntity() as EntityCityPlayer).EquipCustomizationer.Release();
			}
			base.FadeDestroyScript(alphaControls);
		}

		public override void ResetController()
		{
			AssetManager.AssetOfControllerManager.SetController(base.FixAnimator, this.GetEntity().FixModelID, false);
		}

		protected override void UpdateMoveByPointState(float deltaTime)
		{
			if (base.IsMovePausing)
			{
				if (!this.IsClearTargetPosition && !this.GetEntity().IsStatic && !this.GetEntity().IsDizzy && !this.GetEntity().IsWeak && (XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle") || this.GetEntity().IsMoveCast) && base.ToCorner < base.NavMeshPath.get_corners().Length)
				{
					base.IsMovePausing = false;
					base.FixTransform.LookAt(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
					base.LeftLength = XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPath.get_corners()[base.ToCorner]);
					if (this.CanChangeActionTo("run", true, 0, false))
					{
						this.ChangeAction("run", false, true, 1f, 0, 0, string.Empty);
					}
				}
			}
			else
			{
				base.CalculateNextPace(deltaTime);
				if ((!XUtility.StartsWith(this.CurActionStatus, "run") && !XUtility.StartsWith(this.CurActionStatus, "idle") && !this.GetEntity().IsMoveCast) || this.GetEntity().IsStatic || this.GetEntity().IsDizzy || this.GetEntity().IsWeak)
				{
					base.IsMovePausing = true;
					base.MovingDirection = Vector3.get_zero();
				}
				else if (XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPosition) <= 0.05f || base.ToCorner == base.NavMeshPath.get_corners().Length || (base.NextPace >= base.LeftLength && base.ToCorner == base.NavMeshPath.get_corners().Length - 1))
				{
					if (!this.IsClearTargetPosition)
					{
						this.IsClearTargetPosition = true;
						base.MovingDirection = Vector3.get_zero();
						base.FixTransform.set_position(new Vector3(base.NavMeshPosition.x, base.FixTransform.get_position().y, base.NavMeshPosition.z));
						TimerHeap.DelTimer(this.stopTimer);
						this.stopTimer = TimerHeap.AddTimer(350u, 0, delegate
						{
							if (this.IsClearTargetPosition && XUtility.StartsWith(this.CurActionStatus, "run"))
							{
								this.ChangeAction("idle", false, true, 1f, 0, 0, string.Empty);
							}
						});
					}
				}
				else if (base.NavMeshPath.get_corners().Length > 0 && (XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPath.get_corners()[base.ToCorner]) <= 0.05f || base.NextPace > base.LeftLength))
				{
					if (!this.IsClearTargetPosition)
					{
						base.FixTransform.set_position(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
						base.ToCorner++;
						base.MovingDirection = Vector3.get_zero();
						if (base.ToCorner < base.NavMeshPath.get_corners().Length)
						{
							base.FixTransform.LookAt(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
							base.LeftLength = XUtility.DistanceNoY(base.FixTransform.get_position(), base.NavMeshPath.get_corners()[base.ToCorner]);
						}
					}
				}
				else if (!this.IsClearTargetPosition && (XUtility.StartsWith(this.CurActionStatus, "run") || XUtility.StartsWith(this.CurActionStatus, "idle") || this.GetEntity().IsMoveCast) && base.ToCorner < base.NavMeshPath.get_corners().Length)
				{
					Vector3 vector = new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x - base.FixTransform.get_position().x, 0f, base.NavMeshPath.get_corners()[base.ToCorner].z - base.FixTransform.get_position().z);
					base.MovingDirection = vector.get_normalized();
					base.FixTransform.LookAt(new Vector3(base.NavMeshPath.get_corners()[base.ToCorner].x, base.FixTransform.get_position().y, base.NavMeshPath.get_corners()[base.ToCorner].z));
				}
			}
		}

		public override void UpdateLayer()
		{
		}
	}
}
