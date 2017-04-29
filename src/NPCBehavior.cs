using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.AssetLoader;
using XEngineCommand;

public abstract class NPCBehavior
{
	protected int id;

	protected Transform transform;

	protected Animator animator;

	protected List<int> asyncModelIDs = new List<int>();

	protected float frameActionSpeed = 1f;

	protected float realActionSpeed = 1f;

	protected string curActionStatus = string.Empty;

	protected string curOutPutAction = string.Empty;

	protected string defaultIdleActionStatus = "idle";

	public abstract bool EnableUpdate
	{
		get;
	}

	public float FrameActionSpeed
	{
		get
		{
			return this.frameActionSpeed;
		}
		set
		{
			if (this.frameActionSpeed != value)
			{
				this.frameActionSpeed = value;
				this.UpdateActionSpeed();
			}
		}
	}

	public float RealActionSpeed
	{
		get
		{
			return this.realActionSpeed;
		}
		set
		{
			this.realActionSpeed = value;
			this.ChangeAnimationSpeed();
		}
	}

	protected string CurActionStatus
	{
		get
		{
			return this.curActionStatus;
		}
		set
		{
			this.curActionStatus = value;
		}
	}

	public virtual string CurOutPutAction
	{
		get
		{
			return this.curOutPutAction;
		}
		set
		{
			this.curOutPutAction = value;
		}
	}

	public string DefaultIdleActionStatus
	{
		get
		{
			return this.defaultIdleActionStatus;
		}
		set
		{
			this.defaultIdleActionStatus = value;
		}
	}

	public abstract void Init(int theID, int modelID, Transform root);

	public abstract void ApplyDefaultState();

	protected void GetAsyncModel(Transform root, int modelID, Action callback)
	{
		AvatarModel avatarModelData = DataReader<AvatarModel>.Get(modelID);
		if (avatarModelData == null)
		{
			return;
		}
		this.asyncModelIDs.Add(GameObjectLoader.Instance.Get(avatarModelData.path, delegate(GameObject obj)
		{
			if (obj == null)
			{
				Debug.LogError(string.Format("马上联系左总，GameObjectLoader.Instance.Get拿到空值，路径是{0}", avatarModelData.path));
				return;
			}
			obj.SetActive(true);
			obj.set_name(modelID.ToString());
			root.set_localScale(Vector3.get_one() * avatarModelData.scale);
			UGUITools.ResetTransform(obj.get_transform(), root);
			Animator[] componentsInChildren = obj.GetComponentsInChildren<Animator>(true);
			if (componentsInChildren.Length > 0)
			{
				componentsInChildren[0].set_cullingMode((avatarModelData.alwaysVisible <= 0) ? 1 : 0);
				AssetManager.AssetOfControllerManager.SetController(componentsInChildren[0], modelID, false);
			}
			if (callback != null)
			{
				callback.Invoke();
			}
		}));
	}

	protected Collider CreateCollider(GameObject go, List<int> args)
	{
		if (args.get_Count() < 1)
		{
			return null;
		}
		int num = args.get_Item(0);
		if (num != 1)
		{
			if (num != 2)
			{
				return null;
			}
			if (args.get_Count() < 5)
			{
				return null;
			}
			SphereCollider sphereCollider = go.AddMissingComponent<SphereCollider>();
			sphereCollider.set_center(new Vector3((float)args.get_Item(1) * 0.01f, (float)args.get_Item(2) * 0.01f, (float)args.get_Item(3) * 0.01f));
			sphereCollider.set_radius((float)args.get_Item(4) * 0.01f);
			return sphereCollider;
		}
		else
		{
			if (args.get_Count() < 7)
			{
				return null;
			}
			BoxCollider boxCollider = go.AddMissingComponent<BoxCollider>();
			boxCollider.set_center(new Vector3((float)args.get_Item(1) * 0.01f, (float)args.get_Item(2) * 0.01f, (float)args.get_Item(3) * 0.01f));
			boxCollider.set_size(new Vector3((float)args.get_Item(4) * 0.01f, (float)args.get_Item(5) * 0.01f, (float)args.get_Item(6) * 0.01f));
			return boxCollider;
		}
	}

	public virtual void Release()
	{
		if (this.asyncModelIDs.get_Count() == 0)
		{
			return;
		}
		for (int i = 0; i < this.asyncModelIDs.get_Count(); i++)
		{
			GameObjectLoader.Instance.PoolRecycle(this.asyncModelIDs.get_Item(i));
		}
	}

	public abstract void Born();

	public abstract void Die();

	public abstract void OnEnter();

	public abstract void OnExit();

	public abstract int GetState();

	public abstract void UpdateState(object state);

	public abstract void Update();

	public abstract void UpdateHeadInfoState();

	public virtual void OnNotifyPropChanged(NotifyPropChangedCmd cmd)
	{
		string propName = cmd.propName;
		if (propName != null)
		{
			if (NPCBehavior.<>f__switch$map1A == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(1);
				dictionary.Add("AnimFactor", 0);
				NPCBehavior.<>f__switch$map1A = dictionary;
			}
			int num;
			if (NPCBehavior.<>f__switch$map1A.TryGetValue(propName, ref num))
			{
				if (num == 0)
				{
					this.FrameActionSpeed = cmd.propValue;
				}
			}
		}
	}

	protected virtual void UpdateActionSpeed()
	{
		this.RealActionSpeed = this.FrameActionSpeed;
	}

	protected void ChangeAnimationSpeed()
	{
		if (this.animator)
		{
			this.animator.set_speed(this.RealActionSpeed);
		}
	}

	public void CastAction(string actionName)
	{
		if (!this.animator)
		{
			return;
		}
		if (!this.animator.get_runtimeAnimatorController())
		{
			return;
		}
		this.OnActionStatusExit(new ActionStatusExitCmd
		{
			actName = this.CurOutPutAction,
			isBreak = true
		});
		this.CurActionStatus = actionName;
		this.CurOutPutAction = this.GetOutPutAction(actionName);
		this.animator.Play(this.CurOutPutAction);
		this.OnActionStatusEnter(new ActionStatusEnterCmd
		{
			actName = this.CurOutPutAction
		});
	}

	public virtual void OnAnimationEnd(AnimationEndCmd cmd)
	{
		if (this.CurOutPutAction != string.Empty && this.CurOutPutAction != cmd.actName)
		{
			return;
		}
		if (ActionStatusName.IsDieAction(this.CurActionStatus))
		{
			this.DeadAnimationEnd();
		}
		else if (DataReader<Action>.Contains(this.CurActionStatus))
		{
			if (DataReader<Action>.Get(this.CurActionStatus).loop == 0)
			{
				this.EndAnimationResetToIdle();
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.CurActionStatus))
			{
				Debug.LogError("Action表不存在 " + this.CurActionStatus);
			}
			this.EndAnimationResetToIdle();
		}
	}

	public virtual void OnActionStatusEnter(ActionStatusEnterCmd cmd)
	{
		this.UpdateActionSpeed();
	}

	public virtual void OnActionStatusExit(ActionStatusExitCmd cmd)
	{
		if (XUtility.StartsWith(cmd.actName, string.Empty))
		{
			return;
		}
		this.UpdateActionSpeed();
	}

	protected string GetOutPutAction(string actionName)
	{
		string text = actionName + "_city";
		if (this.animator.HasAction(text))
		{
			return text;
		}
		return actionName;
	}

	protected void EndAnimationResetToIdle()
	{
		this.CastAction(this.DefaultIdleActionStatus);
	}

	public virtual void OnAnimatorBecameVisiable()
	{
		if (DataReader<Action>.Contains(this.CurActionStatus))
		{
			if (DataReader<Action>.Get(this.CurActionStatus).loop != 0)
			{
				this.CastAction(this.CurActionStatus);
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.CurActionStatus))
			{
				Debug.LogError("Action表不存在 " + this.CurActionStatus);
			}
			this.EndAnimationResetToIdle();
		}
	}

	protected virtual void DeadAnimationEnd()
	{
	}

	public virtual void OnSeleted()
	{
	}
}
