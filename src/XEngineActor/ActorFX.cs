using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineCommand;

namespace XEngineActor
{
	public class ActorFX : Actor
	{
		private const float SEQ_FRAME_LOOP_TIME = -1f;

		public int uid;

		public bool AutoDestroy = true;

		public Transform Host;

		public Transform HostRoot;

		public bool IsSyncNode;

		public bool IsFollowTarget;

		private bool _IsLodLow;

		[SerializeField]
		public bool IsAnimLoop;

		private Collider[] m_colliders;

		private float[] m_particleSpeedInitRates;

		public Action finishCallback;

		private float[] m_startSpeed;

		private float[] m_startSize;

		private float[] m_gravityModifier;

		private float m_speed = 1f;

		private float m_scale;

		private Vector3 vecterScale = Vector3.get_one();

		private List<Animator> m_animators = new List<Animator>();

		private List<Animation> m_animations = new List<Animation>();

		private List<ParticleSystem> m_particleSystems = new List<ParticleSystem>();

		private List<FXSequenceFrames> m_sequenceFrames = new List<FXSequenceFrames>();

		private bool m_animatorsOver;

		private bool m_particleOver;

		private bool m_seqFrameOver;

		private int m_bulletLife;

		private int m_countStat;

		private bool IsInit;

		public float m_currentSpeed;

		private XPoint xPoint = new XPoint();

		private uint FinishedTimerID;

		private float m_frameRate = 1f;

		private bool IsFXFinished;

		private bool m_onlyKilledBySuicide;

		private int animatorDoneNum;

		private float m_maxAnimatorLiveTime;

		private float m_maxAnimationLiveTime;

		private float m_maxSequenceFramesLiveTime = -1f;

		private float m_maxDelayTimeOnFX;

		private static List<Delay> allDelays = new List<Delay>();

		private static Delay[] tempDelays = null;

		private List<int> m_particelHadOff = new List<int>();

		private float m_thisAliveTime;

		private List<GameObject> Nodes = new List<GameObject>();

		private List<bool> NodeState = new List<bool>();

		private Dictionary<Renderer, bool> mNodesStateOfRender = new Dictionary<Renderer, bool>();

		private Dictionary<Collider, bool> mColliderEnable = new Dictionary<Collider, bool>();

		public bool IsLodLow
		{
			get
			{
				return this._IsLodLow;
			}
			set
			{
				if (this.IsInit && this._IsLodLow != value)
				{
					this._IsLodLow = value;
					this.InitFXLOD();
				}
				else
				{
					this._IsLodLow = value;
				}
			}
		}

		public Action<Actor, XPoint, ActorParent> bulletCallback
		{
			get;
			set;
		}

		public float Speed
		{
			get
			{
				return this.m_speed;
			}
			set
			{
				this.m_speed = value;
				this.SetFrameRate(1f, false);
			}
		}

		public float Scale
		{
			get
			{
				return this.m_scale;
			}
			set
			{
				this.m_scale = value;
				if (this == null || base.get_transform() == null)
				{
					return;
				}
				base.get_transform().set_localScale(Vector3.get_one() * this.m_scale);
				this.vecterScale = Vector3.get_one() * this.m_scale;
				if (this.m_particleSystems != null)
				{
					for (int i = 0; i < this.m_particleSystems.get_Count(); i++)
					{
						ParticleSystem particleSystem = this.m_particleSystems.get_Item(i);
						if (!(particleSystem == null))
						{
							if (this.m_startSpeed != null && i < this.m_startSpeed.Length)
							{
								particleSystem.set_startSpeed(this.m_scale * this.m_startSpeed[i]);
							}
							if (this.m_startSize != null && i < this.m_startSize.Length)
							{
								particleSystem.set_startSize(this.m_scale * this.m_startSize[i]);
							}
							if (this.m_gravityModifier != null && i < this.m_gravityModifier.Length)
							{
								particleSystem.set_gravityModifier(this.m_scale * this.m_gravityModifier[i]);
							}
						}
					}
				}
			}
		}

		public Vector3 VecterScale
		{
			get
			{
				return this.vecterScale;
			}
			set
			{
				if (this.m_particleSystems.get_Count() > 0)
				{
					return;
				}
				this.vecterScale = value;
				if (this == null || base.get_transform() == null)
				{
					return;
				}
				base.get_transform().set_localScale(value);
			}
		}

		public bool AnimatorsOver
		{
			get
			{
				return this.m_animatorsOver;
			}
			set
			{
				this.m_animatorsOver = value;
			}
		}

		public bool ParticleOver
		{
			get
			{
				return this.m_particleOver;
			}
			set
			{
				this.m_particleOver = value;
			}
		}

		public bool SeqFrameOver
		{
			get
			{
				return this.m_seqFrameOver;
			}
			set
			{
				this.m_seqFrameOver = value;
			}
		}

		public int bulletLife
		{
			get
			{
				return this.m_bulletLife;
			}
			set
			{
				this.m_bulletLife = value;
				if (this.m_bulletLife == 0)
				{
					this.FXFinished();
				}
			}
		}

		public bool OnlyKilledBySuicide
		{
			get
			{
				return this.m_onlyKilledBySuicide;
			}
			set
			{
				this.m_onlyKilledBySuicide = value;
			}
		}

		private bool CheckFXFinished()
		{
			if (this.AutoDestroy)
			{
				this.FXFinished();
				return true;
			}
			return false;
		}

		public void AwakeSelf()
		{
			this.IsFXFinished = false;
			this.Awake();
		}

		protected override void Awake()
		{
			if (this.IsInit)
			{
				return;
			}
			this.IsInit = true;
			base.Awake();
			this.InitFXLOD();
			this.InitColliders();
			this.InitAnimators();
			this.InitAnimations();
			this.InitParticles();
			this.InitSequenceFrames();
			this.InitTransformNodesStateOfActive(base.get_transform());
			this.InitTransformNodesStateOfRender(base.get_transform());
			this.InitColliderEnable(base.get_transform());
			this.CalDelayTimeByRoot();
			this.CalAnimationLiveTime();
			this.CalAnimatorLiveTime();
			if (base.get_gameObject().get_layer() != LayerSystem.NameToLayer("Gear"))
			{
				LayerSystem.SetGameObjectLayer(base.get_gameObject(), "FX", 1);
			}
			this.InitFXEventReceivers();
		}

		protected void OnEnable()
		{
			if (this.resGUID == 900)
			{
				Debug.Log("ActorFX OnEnable: " + DateTime.get_Now());
			}
			this.m_thisAliveTime = 0f;
			this.animatorDoneNum = 0;
			this.ResetParticeRenderer();
			this.AnimatorsOver = false;
			this.ParticleOver = false;
			this.SeqFrameOver = false;
			this.AnimatorsOver = (this.m_animators.get_Count() == 0 && this.m_animations.get_Count() == 0);
			this.ParticleOver = (this.m_particleSystems.get_Count() == 0);
			this.SeqFrameOver = (this.m_sequenceFrames.get_Count() == 0);
		}

		protected void OnDisable()
		{
		}

		protected override void OnDestroy()
		{
			this.DealWhenFXFinish();
			base.OnDestroy();
		}

		private void DealWhenFXFinish()
		{
			this.ResetTransformNodesStateOfActive();
			this.ResetTransformNodesStateOfRender();
			this.ResetColliderEnable();
			this.SetAllNoloopParticleOff();
			this.SetFrameRate(1f, true);
			this.Scale = 1f;
			this.OnlyKilledBySuicide = false;
			TimerHeap.DelTimer(this.FinishedTimerID);
			if (this != null && base.get_gameObject() != null)
			{
				base.get_gameObject().SetActive(false);
			}
			FXManager.Instance.DeleteFX(this.uid);
		}

		private void InitFXLOD()
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			FXLOD[] componentsInChildren = base.get_transform().GetComponentsInChildren<FXLOD>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Init(this.IsLodLow);
			}
		}

		private void InitColliders()
		{
			this.m_colliders = base.get_transform().GetComponentsInChildren<Collider>(true);
		}

		private void InitFXEventReceivers()
		{
			for (int i = 0; i < this.m_animators.get_Count(); i++)
			{
				this.m_animators.get_Item(i).get_gameObject().AddMissingComponent<FXEventReceiver>();
			}
			for (int j = 0; j < this.m_animations.get_Count(); j++)
			{
				this.m_animations.get_Item(j).get_gameObject().AddMissingComponent<FXEventReceiver>();
			}
			for (int k = 0; k < this.m_colliders.Length; k++)
			{
				this.m_colliders[k].get_gameObject().AddMissingComponent<FXEventReceiver>();
			}
		}

		private void InitAnimators()
		{
			Animator[] componentsInChildren = base.get_transform().GetComponentsInChildren<Animator>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Animator animator = componentsInChildren[i];
				if (animator.get_enabled())
				{
					FXLOD[] componentsInParent = animator.GetComponentsInParent<FXLOD>(true);
					if (!this.IsShield(componentsInParent))
					{
						this.m_animators.Add(animator);
					}
				}
			}
		}

		private void InitAnimations()
		{
			Animation[] componentsInChildren = base.get_transform().GetComponentsInChildren<Animation>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Animation animation = componentsInChildren[i];
				if (animation.get_enabled())
				{
					FXLOD[] componentsInParent = animation.GetComponentsInParent<FXLOD>(true);
					if (animation.get_clip() != null && !this.IsShield(componentsInParent))
					{
						this.m_animations.Add(animation);
					}
				}
			}
		}

		private void InitParticles()
		{
			ParticleSystem[] componentsInChildren = base.get_transform().GetComponentsInChildren<ParticleSystem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				FXLOD[] componentsInParent = componentsInChildren[i].GetComponentsInParent<FXLOD>(true);
				if (!this.IsShield(componentsInParent))
				{
					this.m_particleSystems.Add(componentsInChildren[i]);
				}
			}
			this.m_particleSpeedInitRates = new float[this.m_particleSystems.get_Count()];
			this.m_startSpeed = new float[this.m_particleSystems.get_Count()];
			this.m_startSize = new float[this.m_particleSystems.get_Count()];
			this.m_gravityModifier = new float[this.m_particleSystems.get_Count()];
			for (int j = 0; j < this.m_particleSystems.get_Count(); j++)
			{
				ParticleSystem particleSystem = this.m_particleSystems.get_Item(j);
				ParticleSystemRenderer component = particleSystem.GetComponent<ParticleSystemRenderer>();
				if (component.get_maxParticleSize() > 3f)
				{
					component.set_maxParticleSize(3f);
				}
				if (particleSystem.get_maxParticles() > GameLevelManager.GameLevelVariable.FXMaxParticles)
				{
					particleSystem.set_maxParticles(GameLevelManager.GameLevelVariable.FXMaxParticles);
				}
				this.m_particleSpeedInitRates[j] = particleSystem.get_playbackSpeed();
				this.m_startSpeed[j] = particleSystem.get_startSpeed();
				this.m_startSize[j] = particleSystem.get_startSize();
				this.m_gravityModifier[j] = particleSystem.get_gravityModifier();
			}
		}

		private void InitSequenceFrames()
		{
			FXSequenceFrames[] componentsInChildren = base.get_transform().GetComponentsInChildren<FXSequenceFrames>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				FXLOD[] componentsInParent = componentsInChildren[i].GetComponentsInParent<FXLOD>(true);
				if (!this.IsShield(componentsInParent))
				{
					this.m_sequenceFrames.Add(componentsInChildren[i]);
				}
			}
			this.InitSequenceFramesLiveTime();
		}

		private void Update()
		{
			if (this.IsFXFinished)
			{
				return;
			}
			this.m_thisAliveTime += Time.get_deltaTime() * this.m_frameRate;
			if (this.CheckOver() && this.CheckFXFinished())
			{
				return;
			}
			this.SyncNode();
		}

		private bool CheckOver()
		{
			return this.CheckAnimatorsOver() && this.CheckParticleOver() && this.CheckSequenceFramesOver();
		}

		public void SetTimer(int time)
		{
			TimerHeap.DelTimer(this.FinishedTimerID);
			TimerHeap.AddTimer((uint)time, 0, delegate
			{
				this.FXFinished();
			});
		}

		public void SetFrameRate(float rate, bool ignoreOnlyKilledBySuicide = false)
		{
			if (!ignoreOnlyKilledBySuicide && this.OnlyKilledBySuicide)
			{
				return;
			}
			this.m_frameRate = rate * this.m_speed;
			for (int i = 0; i < this.m_animators.get_Count(); i++)
			{
				if (this.m_animators.get_Item(i) != null)
				{
					this.m_animators.get_Item(i).set_speed(this.m_frameRate);
				}
			}
			for (int j = 0; j < this.m_animations.get_Count(); j++)
			{
				Animation animation = this.m_animations.get_Item(j);
				if (animation != null && animation.get_clip() != null)
				{
					animation.get_Item(animation.get_clip().get_name()).set_speed(this.m_frameRate);
				}
			}
			for (int k = 0; k < this.m_particleSystems.get_Count(); k++)
			{
				if (this.m_particleSystems.get_Item(k) != null)
				{
					this.m_particleSystems.get_Item(k).set_playbackSpeed(this.m_frameRate * this.m_particleSpeedInitRates[k]);
				}
			}
		}

		public void FXFinished()
		{
			if (this.IsFXFinished)
			{
				return;
			}
			this.IsFXFinished = true;
			this.DealWhenFXFinish();
			if (ClientApp.Instance != null)
			{
				base.Reuse();
			}
			if (this.finishCallback != null)
			{
				this.finishCallback.Invoke();
				this.finishCallback = null;
			}
		}

		private void OnAllActionStart(AnimationEvent e)
		{
		}

		private void OnAllImmune(AnimationEvent e)
		{
			this.SetFrameRate(1f, true);
			this.OnlyKilledBySuicide = true;
			base.get_transform().set_parent(FXPool.Instance.root.get_transform());
		}

		private void OnAllActionEnd(AnimationEvent e)
		{
			this.animatorDoneNum++;
		}

		private void OnAllTriggerEnter(ActorCollider other)
		{
			if (this.bulletCallback != null && other && other.Actor)
			{
				this.bulletCallback.Invoke(this, new XPoint
				{
					position = other.Actor.FixTransform.get_position(),
					rotation = other.Actor.FixTransform.get_rotation()
				}, other.Actor);
			}
		}

		private void OnAllTriggerExit(ActorCollider other)
		{
		}

		private bool CheckAnimatorsOver()
		{
			if (this.AnimatorsOver)
			{
				return true;
			}
			if (this.IsAnimLoop)
			{
				return false;
			}
			if (this.IsDelayOver() && this.IsAnimatorDone() && this.IsAnimationDone())
			{
				this.AnimatorsOver = true;
				return true;
			}
			return false;
		}

		private bool IsAnimatorDone()
		{
			return this.animatorDoneNum == this.m_animators.get_Count() || this.m_thisAliveTime >= this.m_maxAnimatorLiveTime;
		}

		private void CalAnimatorLiveTime()
		{
			if (this.m_animators != null && this.m_animators.get_Count() > 0)
			{
				for (int i = 0; i < this.m_animators.get_Count(); i++)
				{
					Animator animator = this.m_animators.get_Item(i);
					RuntimeAnimatorController runtimeAnimatorController = animator.get_runtimeAnimatorController();
					if (!(runtimeAnimatorController == null) && runtimeAnimatorController.get_animationClips() != null && runtimeAnimatorController.get_animationClips().Length != 0 && !(runtimeAnimatorController.get_animationClips()[0] == null))
					{
						float num = runtimeAnimatorController.get_animationClips()[0].get_length() + this.CalDelayTimeByChild(animator.get_transform());
						if (num > this.m_maxAnimatorLiveTime)
						{
							this.m_maxAnimatorLiveTime = num;
						}
					}
				}
			}
		}

		private bool IsAnimationDone()
		{
			return this.m_thisAliveTime >= this.m_maxAnimationLiveTime;
		}

		private void CalAnimationLiveTime()
		{
			if (this.m_animations != null && this.m_animations.get_Count() > 0)
			{
				for (int i = 0; i < this.m_animations.get_Count(); i++)
				{
					Animation animation = this.m_animations.get_Item(i);
					if (!(animation.get_clip() == null))
					{
						float num = animation.get_Item(animation.get_clip().get_name()).get_length() + this.CalDelayTimeByChild(animation.get_transform());
						if (num > this.m_maxAnimationLiveTime)
						{
							this.m_maxAnimationLiveTime = num;
						}
					}
				}
			}
		}

		private bool CheckParticleOver()
		{
			if (this.ParticleOver)
			{
				return true;
			}
			bool flag = false;
			for (int i = 0; i < this.m_particleSystems.get_Count(); i++)
			{
				if (this.m_particleSystems.get_Item(i) != null)
				{
					if (!this.IsParticleAlive(this.m_particleSystems.get_Item(i)))
					{
						this.SetNoloopParticleOff(this.m_particleSystems.get_Item(i));
					}
					else
					{
						flag = true;
					}
				}
			}
			if (this.IsDelayOver() && !flag)
			{
				this.ParticleOver = true;
				return true;
			}
			return false;
		}

		private void InitSequenceFramesLiveTime()
		{
			this.m_maxSequenceFramesLiveTime = -1f;
			for (int i = 0; i < this.m_sequenceFrames.get_Count(); i++)
			{
				FXSequenceFrames fXSequenceFrames = this.m_sequenceFrames.get_Item(i);
				if (fXSequenceFrames.IsLoop)
				{
					this.m_maxSequenceFramesLiveTime = -1f;
					return;
				}
				float nodeDelayTimes = this.GetNodeDelayTimes(fXSequenceFrames.get_transform());
				float num = nodeDelayTimes + fXSequenceFrames.LoopTime;
				if (num > this.m_maxSequenceFramesLiveTime)
				{
					this.m_maxSequenceFramesLiveTime = num;
				}
			}
		}

		private bool CheckSequenceFramesOver()
		{
			if (this.SeqFrameOver)
			{
				return true;
			}
			if (this.m_maxSequenceFramesLiveTime == -1f)
			{
				this.SeqFrameOver = false;
			}
			else if (this.m_thisAliveTime > this.m_maxSequenceFramesLiveTime)
			{
				this.SeqFrameOver = true;
			}
			else
			{
				this.SeqFrameOver = false;
			}
			return this.SeqFrameOver;
		}

		private bool IsDelayOver()
		{
			return this.m_maxDelayTimeOnFX <= 0f || this.m_thisAliveTime > this.m_maxDelayTimeOnFX;
		}

		private void CalDelayTimeByRoot()
		{
			ActorFX.allDelays.Clear();
			ActorFX.tempDelays = base.GetComponentsInChildren<Delay>(true);
			for (int i = 0; i < ActorFX.tempDelays.Length; i++)
			{
				FXLOD[] componentsInParent = ActorFX.tempDelays[i].GetComponentsInParent<FXLOD>(true);
				if (!this.IsShield(componentsInParent))
				{
					ActorFX.allDelays.Add(ActorFX.tempDelays[i]);
				}
			}
			for (int j = 0; j < ActorFX.allDelays.get_Count(); j++)
			{
				float num = this.CalDelayTimeByChild(ActorFX.allDelays.get_Item(j).get_transform());
				if (this.m_maxDelayTimeOnFX < num)
				{
					this.m_maxDelayTimeOnFX = num;
				}
			}
		}

		private float CalDelayTimeByChild(Transform target)
		{
			float num = 0f;
			ActorFX.tempDelays = target.GetComponentsInParent<Delay>(true);
			for (int i = 0; i < ActorFX.tempDelays.Length; i++)
			{
				if (ActorFX.tempDelays[i].GetComponent<ActorFX>() == null)
				{
					num += ActorFX.tempDelays[i].delayTime;
				}
			}
			return num;
		}

		private bool IsParticleAlive(ParticleSystem item)
		{
			return item.IsAlive();
		}

		private void SetAllNoloopParticleOff()
		{
			for (int i = 0; i < this.m_particleSystems.get_Count(); i++)
			{
				ParticleSystem particleSystem = this.m_particleSystems.get_Item(i);
				if (particleSystem != null && !particleSystem.get_loop())
				{
					this.EnableParticeRenderer(particleSystem, false);
				}
			}
		}

		private void SetNoloopParticleOff(ParticleSystem ps)
		{
			if (!this.m_particelHadOff.Contains(ps.GetInstanceID()))
			{
				this.m_particelHadOff.Add(ps.GetInstanceID());
				this.EnableParticeRenderer(ps, false);
			}
		}

		private void EnableParticeRenderer(ParticleSystem ps, bool enable)
		{
		}

		private void ResetParticeRenderer()
		{
			this.m_particelHadOff.Clear();
		}

		private void SyncNode()
		{
			if (this.IsSyncNode)
			{
				if (this.IsFollowTarget)
				{
					this.m_currentSpeed += 2f * Time.get_deltaTime();
					if (Vector3.Distance(base.get_transform().get_position(), this.Host.get_position()) <= this.m_currentSpeed * Time.get_deltaTime())
					{
						base.get_transform().set_position(this.Host.get_position());
						if (this.bulletCallback != null)
						{
							this.xPoint.position = base.get_transform().get_position();
							this.xPoint.rotation = base.get_transform().get_rotation();
							this.bulletCallback.Invoke(this, this.xPoint, this.HostRoot.GetComponent<ActorParent>());
						}
					}
					else
					{
						Transform expr_CF = base.get_transform();
						expr_CF.set_position(expr_CF.get_position() + (this.Host.get_position() - base.get_transform().get_position()).get_normalized() * this.m_currentSpeed * Time.get_deltaTime());
					}
				}
				else if (this.Host != null)
				{
					base.get_transform().set_position(this.Host.get_position());
				}
			}
		}

		private bool IsShield(FXLOD[] fxLods)
		{
			if (fxLods == null)
			{
				return false;
			}
			for (int i = 0; i < fxLods.Length; i++)
			{
				if (fxLods[i].IsShield(this.IsLodLow))
				{
					return true;
				}
			}
			return false;
		}

		public void ChangeLayer(string layer)
		{
			LayerSystem.SetGameObjectLayer(base.get_gameObject(), layer, 1);
		}

		private float GetNodeDelayTimes(Transform item)
		{
			float num = 0f;
			Delay[] componentsInParent = item.GetComponentsInParent<Delay>(true);
			for (int i = 0; i < componentsInParent.Length; i++)
			{
				if (componentsInParent[i].GetComponent<ActorFX>() == null)
				{
					num += componentsInParent[i].delayTime;
				}
			}
			return num;
		}

		private void InitTransformNodesStateOfActive(Transform rootTransform)
		{
			this.Nodes.Clear();
			this.NodeState.Clear();
			Transform[] componentsInChildren = rootTransform.GetComponentsInChildren<Transform>(true);
			for (int i = 1; i < componentsInChildren.Length; i++)
			{
				GameObject gameObject = componentsInChildren[i].get_gameObject();
				this.Nodes.Add(gameObject);
				this.NodeState.Add(gameObject.get_activeSelf());
			}
		}

		private void ResetTransformNodesStateOfActive()
		{
			if (this.Nodes == null)
			{
				return;
			}
			int num = 0;
			while (num < this.Nodes.get_Count() && num < this.NodeState.get_Count())
			{
				GameObject gameObject = this.Nodes.get_Item(num);
				if (gameObject != null)
				{
					gameObject.SetActive(this.NodeState.get_Item(num));
				}
				num++;
			}
		}

		private void InitTransformNodesStateOfRender(Transform rootTransform)
		{
			Renderer[] componentsInChildren = rootTransform.GetComponentsInChildren<Renderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Renderer renderer = componentsInChildren[i];
				if (renderer != null)
				{
					this.mNodesStateOfRender.set_Item(renderer, renderer.get_enabled());
				}
			}
		}

		private void ResetTransformNodesStateOfRender()
		{
			using (Dictionary<Renderer, bool>.Enumerator enumerator = this.mNodesStateOfRender.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Renderer, bool> current = enumerator.get_Current();
					if (current.get_Key() != null)
					{
						current.get_Key().set_enabled(current.get_Value());
					}
				}
			}
		}

		private void InitColliderEnable(Transform rootTransform)
		{
			Collider[] componentsInChildren = rootTransform.GetComponentsInChildren<Collider>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.mColliderEnable.set_Item(componentsInChildren[i], componentsInChildren[i].get_enabled());
			}
		}

		private void ResetColliderEnable()
		{
			using (Dictionary<Collider, bool>.Enumerator enumerator = this.mColliderEnable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Collider, bool> current = enumerator.get_Current();
					if (current.get_Key() != null)
					{
						current.get_Key().set_enabled(current.get_Value());
					}
				}
			}
		}

		public override void OnAudioEvent(AudioEventCmd cmd)
		{
			AudioPlayer audioPlayer = base.get_gameObject().AddUniqueComponent<AudioPlayer>();
			audioPlayer.RoleId = (long)this.InstanceID;
			if (SoundManager.Instance != null)
			{
				SoundManager.Instance.PlayPlayer(audioPlayer, cmd.args.get_intParameter());
			}
		}
	}
}
