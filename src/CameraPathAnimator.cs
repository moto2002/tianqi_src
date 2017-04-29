using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraPathAnimator : MonoBehaviour
{
	public enum animationModes
	{
		once,
		loop,
		reverse,
		reverseLoop,
		pingPong,
		still
	}

	public enum orientationModes
	{
		custom,
		target,
		mouselook,
		followpath,
		reverseFollowpath,
		followTransform,
		twoDimentions,
		fixedOrientation,
		none
	}

	public delegate void AnimationStartedEventHandler();

	public delegate void AnimationPausedEventHandler();

	public delegate void AnimationStoppedEventHandler();

	public delegate void AnimationFinishedEventHandler();

	public delegate void AnimationLoopedEventHandler();

	public delegate void AnimationPingPongEventHandler();

	public delegate void AnimationPointReachedEventHandler();

	public delegate void AnimationCustomEventHandler(string eventName);

	public delegate void AnimationPointReachedWithNumberEventHandler(int pointNumber);

	public float minimumCameraSpeed = 0.01f;

	public Transform orientationTarget;

	[SerializeField]
	private CameraPath _cameraPath;

	public bool playOnStart = true;

	public Transform animationObject;

	private Camera animationObjectCamera;

	private bool _isCamera = true;

	private bool _playing;

	public CameraPathAnimator.animationModes animationMode;

	[SerializeField]
	private CameraPathAnimator.orientationModes _orientationMode;

	public bool smoothOrientationModeChanges;

	public float orientationModeLerpTime = 0.3f;

	private float _orientationModeLerpTimer;

	private CameraPathAnimator.orientationModes _previousOrientationMode;

	private float pingPongDirection = 1f;

	public Vector3 fixedOrientaion = Vector3.get_forward();

	public Vector3 fixedPosition;

	public bool normalised = true;

	[SerializeField]
	private bool _dynamicNormalisation;

	public float editorPercentage;

	[SerializeField]
	private float _pathTime = 10f;

	[SerializeField]
	private float _pathSpeed = 10f;

	private float _percentage;

	private float _lastPercentage;

	public float nearestOffset;

	private float _delayTime;

	public float startPercent;

	public bool animateFOV = true;

	public Vector3 targetModeUp = Vector3.get_up();

	public float sensitivity = 5f;

	public float minX = -90f;

	public float maxX = 90f;

	private bool _animateSceneObjectInEditor;

	public Vector3 animatedObjectStartPosition;

	public Quaternion animatedObjectStartRotation;

	public event CameraPathAnimator.AnimationStartedEventHandler AnimationStartedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationStartedEvent = (CameraPathAnimator.AnimationStartedEventHandler)Delegate.Combine(this.AnimationStartedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationStartedEvent = (CameraPathAnimator.AnimationStartedEventHandler)Delegate.Remove(this.AnimationStartedEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationPausedEventHandler AnimationPausedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationPausedEvent = (CameraPathAnimator.AnimationPausedEventHandler)Delegate.Combine(this.AnimationPausedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationPausedEvent = (CameraPathAnimator.AnimationPausedEventHandler)Delegate.Remove(this.AnimationPausedEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationStoppedEventHandler AnimationStoppedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationStoppedEvent = (CameraPathAnimator.AnimationStoppedEventHandler)Delegate.Combine(this.AnimationStoppedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationStoppedEvent = (CameraPathAnimator.AnimationStoppedEventHandler)Delegate.Remove(this.AnimationStoppedEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationFinishedEventHandler AnimationFinishedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationFinishedEvent = (CameraPathAnimator.AnimationFinishedEventHandler)Delegate.Combine(this.AnimationFinishedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationFinishedEvent = (CameraPathAnimator.AnimationFinishedEventHandler)Delegate.Remove(this.AnimationFinishedEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationLoopedEventHandler AnimationLoopedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationLoopedEvent = (CameraPathAnimator.AnimationLoopedEventHandler)Delegate.Combine(this.AnimationLoopedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationLoopedEvent = (CameraPathAnimator.AnimationLoopedEventHandler)Delegate.Remove(this.AnimationLoopedEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationPingPongEventHandler AnimationPingPongEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationPingPongEvent = (CameraPathAnimator.AnimationPingPongEventHandler)Delegate.Combine(this.AnimationPingPongEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationPingPongEvent = (CameraPathAnimator.AnimationPingPongEventHandler)Delegate.Remove(this.AnimationPingPongEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationPointReachedEventHandler AnimationPointReachedEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationPointReachedEvent = (CameraPathAnimator.AnimationPointReachedEventHandler)Delegate.Combine(this.AnimationPointReachedEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationPointReachedEvent = (CameraPathAnimator.AnimationPointReachedEventHandler)Delegate.Remove(this.AnimationPointReachedEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationPointReachedWithNumberEventHandler AnimationPointReachedWithNumberEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationPointReachedWithNumberEvent = (CameraPathAnimator.AnimationPointReachedWithNumberEventHandler)Delegate.Combine(this.AnimationPointReachedWithNumberEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationPointReachedWithNumberEvent = (CameraPathAnimator.AnimationPointReachedWithNumberEventHandler)Delegate.Remove(this.AnimationPointReachedWithNumberEvent, value);
		}
	}

	public event CameraPathAnimator.AnimationCustomEventHandler AnimationCustomEvent
	{
		[MethodImpl(32)]
		add
		{
			this.AnimationCustomEvent = (CameraPathAnimator.AnimationCustomEventHandler)Delegate.Combine(this.AnimationCustomEvent, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.AnimationCustomEvent = (CameraPathAnimator.AnimationCustomEventHandler)Delegate.Remove(this.AnimationCustomEvent, value);
		}
	}

	public float pathSpeed
	{
		get
		{
			return this._pathSpeed;
		}
		set
		{
			if (this._cameraPath.speedList.listEnabled)
			{
				Debug.LogWarning("Path Speed in Animator component is ignored and overridden by Camera Path speed points.");
			}
			this._pathSpeed = Mathf.Max(value, this.minimumCameraSpeed);
		}
	}

	public float animationTime
	{
		get
		{
			return this._pathTime;
		}
		set
		{
			if (this.animationMode != CameraPathAnimator.animationModes.still)
			{
				Debug.LogWarning("Path time is ignored and overridden during animation when not in Animation Mode Still.");
			}
			this._pathTime = Mathf.Max(value, 0f);
		}
	}

	public float currentTime
	{
		get
		{
			return this._pathTime * this._percentage;
		}
	}

	public bool isPlaying
	{
		get
		{
			return this._playing;
		}
	}

	public float percentage
	{
		get
		{
			return this._percentage;
		}
	}

	public bool pingPongGoingForward
	{
		get
		{
			return this.pingPongDirection == 1f;
		}
	}

	public CameraPath cameraPath
	{
		get
		{
			if (!this._cameraPath)
			{
				this._cameraPath = base.GetComponent<CameraPath>();
			}
			return this._cameraPath;
		}
	}

	public bool dynamicNormalisation
	{
		get
		{
			return this._dynamicNormalisation;
		}
		set
		{
			if (value)
			{
				this._dynamicNormalisation = true;
				this._cameraPath.normalised = false;
			}
			else
			{
				this._dynamicNormalisation = false;
			}
		}
	}

	public CameraPathAnimator.orientationModes orientationMode
	{
		get
		{
			return this._orientationMode;
		}
		set
		{
			if (this._orientationMode != value)
			{
				this._orientationModeLerpTimer = 0f;
				this._previousOrientationMode = this._orientationMode;
				this._orientationMode = value;
			}
		}
	}

	private bool isReversed
	{
		get
		{
			return this.animationMode == CameraPathAnimator.animationModes.reverse || this.animationMode == CameraPathAnimator.animationModes.reverseLoop || this.pingPongDirection < 0f;
		}
	}

	public bool isCamera
	{
		get
		{
			if (this.animationObject == null)
			{
				this._isCamera = false;
			}
			else
			{
				this._isCamera = (this.animationObjectCamera != null);
			}
			return this._isCamera;
		}
	}

	public bool animateSceneObjectInEditor
	{
		get
		{
			return this._animateSceneObjectInEditor;
		}
		set
		{
			if (value != this._animateSceneObjectInEditor)
			{
				this._animateSceneObjectInEditor = value;
				if (this.animationObject != null && this.animationMode != CameraPathAnimator.animationModes.still)
				{
					if (this._animateSceneObjectInEditor)
					{
						this.animatedObjectStartPosition = this.animationObject.get_transform().get_position();
						this.animatedObjectStartRotation = this.animationObject.get_transform().get_rotation();
					}
					else
					{
						this.animationObject.get_transform().set_position(this.animatedObjectStartPosition);
						this.animationObject.get_transform().set_rotation(this.animatedObjectStartRotation);
					}
				}
			}
			this._animateSceneObjectInEditor = value;
		}
	}

	public void Play()
	{
		this._playing = true;
		if (!this.isReversed)
		{
			if (this._percentage == 0f)
			{
				if (this.AnimationStartedEvent != null)
				{
					this.AnimationStartedEvent();
				}
				this.cameraPath.eventList.OnAnimationStart(0f);
			}
		}
		else if (this._percentage == 1f)
		{
			if (this.AnimationStartedEvent != null)
			{
				this.AnimationStartedEvent();
			}
			this.cameraPath.eventList.OnAnimationStart(1f);
		}
		this._lastPercentage = this._percentage;
	}

	public void Stop()
	{
		this._playing = false;
		this._percentage = 0f;
		if (this.AnimationStoppedEvent != null)
		{
			this.AnimationStoppedEvent();
		}
	}

	public void Pause()
	{
		this._playing = false;
		if (this.AnimationPausedEvent != null)
		{
			this.AnimationPausedEvent();
		}
	}

	public void Seek(float value)
	{
		this._percentage = Mathf.Clamp01(value);
		this._lastPercentage = this._percentage;
		this.UpdateAnimationTime(false);
		this.UpdatePointReached();
		bool playing = this._playing;
		this._playing = true;
		this.UpdateAnimation();
		this._playing = playing;
	}

	public void Reverse()
	{
		switch (this.animationMode)
		{
		case CameraPathAnimator.animationModes.once:
			this.animationMode = CameraPathAnimator.animationModes.reverse;
			break;
		case CameraPathAnimator.animationModes.loop:
			this.animationMode = CameraPathAnimator.animationModes.reverseLoop;
			break;
		case CameraPathAnimator.animationModes.reverse:
			this.animationMode = CameraPathAnimator.animationModes.once;
			break;
		case CameraPathAnimator.animationModes.reverseLoop:
			this.animationMode = CameraPathAnimator.animationModes.loop;
			break;
		case CameraPathAnimator.animationModes.pingPong:
			this.pingPongDirection = (float)((this.pingPongDirection != -1f) ? -1 : 1);
			break;
		}
	}

	public Quaternion GetOrientation(CameraPathAnimator.orientationModes mode, float percent, bool ignoreNormalisation)
	{
		Quaternion quaternion = Quaternion.get_identity();
		switch (mode)
		{
		case CameraPathAnimator.orientationModes.custom:
			quaternion = this.cameraPath.GetPathRotation(percent, ignoreNormalisation);
			break;
		case CameraPathAnimator.orientationModes.target:
		{
			Vector3 pathPosition = this.cameraPath.GetPathPosition(percent);
			Vector3 vector;
			if (this.orientationTarget != null)
			{
				vector = this.orientationTarget.get_transform().get_position() - pathPosition;
			}
			else
			{
				vector = Vector3.get_forward();
			}
			quaternion = Quaternion.LookRotation(vector, this.targetModeUp);
			break;
		}
		case CameraPathAnimator.orientationModes.mouselook:
			if (!Application.get_isPlaying())
			{
				quaternion = Quaternion.LookRotation(this.cameraPath.GetPathDirection(percent));
				quaternion *= Quaternion.Euler(base.get_transform().get_forward() * -this.cameraPath.GetPathTilt(percent));
			}
			else
			{
				quaternion = Quaternion.LookRotation(this.cameraPath.GetPathDirection(percent));
				quaternion *= this.GetMouseLook();
			}
			break;
		case CameraPathAnimator.orientationModes.followpath:
			quaternion = Quaternion.LookRotation(this.cameraPath.GetPathDirection(percent));
			quaternion *= Quaternion.Euler(base.get_transform().get_forward() * -this.cameraPath.GetPathTilt(percent));
			break;
		case CameraPathAnimator.orientationModes.reverseFollowpath:
			quaternion = Quaternion.LookRotation(-this.cameraPath.GetPathDirection(percent));
			quaternion *= Quaternion.Euler(base.get_transform().get_forward() * -this.cameraPath.GetPathTilt(percent));
			break;
		case CameraPathAnimator.orientationModes.followTransform:
		{
			if (this.orientationTarget == null)
			{
				return Quaternion.get_identity();
			}
			float num = this.cameraPath.GetNearestPoint(this.orientationTarget.get_position());
			num = Mathf.Clamp01(num + this.nearestOffset);
			Vector3 pathPosition = this.cameraPath.GetPathPosition(num);
			Vector3 vector = this.orientationTarget.get_transform().get_position() - pathPosition;
			quaternion = Quaternion.LookRotation(vector);
			break;
		}
		case CameraPathAnimator.orientationModes.twoDimentions:
			quaternion = Quaternion.LookRotation(Vector3.get_forward());
			break;
		case CameraPathAnimator.orientationModes.fixedOrientation:
			quaternion = Quaternion.LookRotation(this.fixedOrientaion);
			break;
		case CameraPathAnimator.orientationModes.none:
			quaternion = this.animationObject.get_rotation();
			break;
		}
		return quaternion;
	}

	public Quaternion GetAnimatedOrientation(float percent, bool ignoreNormalisation)
	{
		Quaternion quaternion = this.GetOrientation(this._orientationMode, percent, ignoreNormalisation);
		if (this.smoothOrientationModeChanges && this._orientationModeLerpTimer < this.orientationModeLerpTime)
		{
			Quaternion orientation = this.GetOrientation(this._previousOrientationMode, percent, ignoreNormalisation);
			float num = this._orientationModeLerpTimer / this.orientationModeLerpTime;
			float num2 = Mathf.SmoothStep(0f, 1f, num);
			quaternion = Quaternion.Slerp(orientation, quaternion, num2);
		}
		return quaternion * base.get_transform().get_rotation();
	}

	private void Awake()
	{
		if (this.animationObject == null)
		{
			this._isCamera = false;
		}
		else
		{
			this.animationObjectCamera = this.animationObject.GetComponentInChildren<Camera>();
			this._isCamera = (this.animationObjectCamera != null);
		}
		Camera[] allCameras = Camera.get_allCameras();
		if (allCameras.Length == 0)
		{
			Debug.LogWarning("Warning: There are no cameras in the scene");
			this._isCamera = false;
		}
		if (!this.isReversed)
		{
			this._percentage = 0f + this.startPercent;
		}
		else
		{
			this._percentage = 1f - this.startPercent;
		}
	}

	private void OnEnable()
	{
		this.cameraPath.eventList.CameraPathEventPoint += new CameraPathEventList.CameraPathEventPointHandler(this.OnCustomEvent);
		this.cameraPath.delayList.CameraPathDelayEvent += new CameraPathDelayList.CameraPathDelayEventHandler(this.OnDelayEvent);
		if (this.animationObject != null)
		{
			this.animationObjectCamera = this.animationObject.GetComponentInChildren<Camera>();
		}
	}

	private void Start()
	{
		if (this.playOnStart)
		{
			this.Play();
		}
		if (Application.get_isPlaying() && this.orientationTarget == null && (this._orientationMode == CameraPathAnimator.orientationModes.followTransform || this._orientationMode == CameraPathAnimator.orientationModes.target))
		{
			Debug.LogWarning("There has not been an orientation target specified in the Animation component of Camera Path.", base.get_transform());
		}
	}

	private void Update()
	{
		if (!this.isCamera)
		{
			if (this._playing)
			{
				this.UpdateAnimation();
				this.UpdatePointReached();
				this.UpdateAnimationTime();
			}
			else if (this._cameraPath.nextPath != null && this._percentage >= 1f)
			{
				this.PlayNextAnimation();
			}
		}
	}

	private void LateUpdate()
	{
		if (this.isCamera)
		{
			if (this._playing)
			{
				this.UpdateAnimation();
				this.UpdatePointReached();
				this.UpdateAnimationTime();
			}
			else if (this._cameraPath.nextPath != null && this._percentage >= 1f)
			{
				this.PlayNextAnimation();
			}
		}
	}

	private void OnDisable()
	{
		this.CleanUp();
	}

	private void OnDestroy()
	{
		this.CleanUp();
	}

	private void PlayNextAnimation()
	{
		if (this._cameraPath.nextPath != null)
		{
			CameraPathAnimator component = this._cameraPath.nextPath.GetComponent<CameraPathAnimator>();
			float value = (!this._cameraPath.interpolateNextPath) ? 0f : (this._percentage % 1f);
			component.Seek(value);
			component.Play();
			this.Stop();
		}
	}

	private void UpdateAnimation()
	{
		if (this.animationObject == null)
		{
			Debug.LogError("There is no animation object specified in the Camera Path Animator component. Nothing to animate.\nYou can find this component in the main camera path game object called " + base.get_gameObject().get_name() + ".");
			this.Stop();
			return;
		}
		if (!this._playing)
		{
			return;
		}
		if (this.animationMode != CameraPathAnimator.animationModes.still)
		{
			if (this.cameraPath.speedList.listEnabled)
			{
				this._pathTime = this._cameraPath.pathLength / Mathf.Max(this.cameraPath.GetPathSpeed(this._percentage), this.minimumCameraSpeed);
			}
			else
			{
				this._pathTime = this._cameraPath.pathLength / Mathf.Max(this._pathSpeed * this.cameraPath.GetPathEase(this._percentage), this.minimumCameraSpeed);
			}
			this.animationObject.set_position(this.cameraPath.GetPathPosition(this._percentage));
		}
		if (this._orientationMode != CameraPathAnimator.orientationModes.none)
		{
			this.animationObject.set_rotation(this.GetAnimatedOrientation(this._percentage, false));
		}
		if (this.isCamera && this._cameraPath.fovList.listEnabled)
		{
			if (!this.animationObjectCamera.get_orthographic())
			{
				this.animationObjectCamera.set_fieldOfView(this._cameraPath.GetPathFOV(this._percentage));
			}
			else
			{
				this.animationObjectCamera.set_orthographicSize(this._cameraPath.GetPathOrthographicSize(this._percentage));
			}
		}
		this.CheckEvents();
	}

	private void UpdatePointReached()
	{
		if (this._percentage == this._lastPercentage)
		{
			return;
		}
		if (Mathf.Abs(this.percentage - this._lastPercentage) > 0.999f)
		{
			this._lastPercentage = this.percentage;
			return;
		}
		for (int i = 0; i < this.cameraPath.realNumberOfPoints; i++)
		{
			CameraPathControlPoint cameraPathControlPoint = this.cameraPath[i];
			bool flag = (cameraPathControlPoint.percentage >= this._lastPercentage && cameraPathControlPoint.percentage <= this.percentage) || (cameraPathControlPoint.percentage >= this.percentage && cameraPathControlPoint.percentage <= this._lastPercentage);
			if (flag)
			{
				if (this.AnimationPointReachedEvent != null)
				{
					this.AnimationPointReachedEvent();
				}
				if (this.AnimationPointReachedWithNumberEvent != null)
				{
					this.AnimationPointReachedWithNumberEvent(i);
				}
			}
		}
		this._lastPercentage = this.percentage;
	}

	private void UpdateAnimationTime()
	{
		this.UpdateAnimationTime(true);
	}

	private void UpdateAnimationTime(bool advance)
	{
		if (this._orientationMode == CameraPathAnimator.orientationModes.followTransform)
		{
			return;
		}
		if (this._delayTime > 0f)
		{
			this._delayTime += -Time.get_deltaTime();
			return;
		}
		if (advance)
		{
			switch (this.animationMode)
			{
			case CameraPathAnimator.animationModes.once:
				if (this._percentage >= 1f)
				{
					this._playing = false;
					if (this.AnimationFinishedEvent != null)
					{
						this.AnimationFinishedEvent();
					}
				}
				else
				{
					this._percentage += Time.get_deltaTime() * (1f / this._pathTime);
				}
				break;
			case CameraPathAnimator.animationModes.loop:
				if (this._percentage >= 1f)
				{
					this._percentage = 0f;
					this._lastPercentage = 0f;
					if (this.AnimationLoopedEvent != null)
					{
						this.AnimationLoopedEvent();
					}
				}
				this._percentage += Time.get_deltaTime() * (1f / this._pathTime);
				break;
			case CameraPathAnimator.animationModes.reverse:
				if (this._percentage <= 0f)
				{
					this._percentage = 0f;
					this._playing = false;
					if (this.AnimationFinishedEvent != null)
					{
						this.AnimationFinishedEvent();
					}
				}
				else
				{
					this._percentage += -Time.get_deltaTime() * (1f / this._pathTime);
				}
				break;
			case CameraPathAnimator.animationModes.reverseLoop:
				if (this._percentage <= 0f)
				{
					this._percentage = 1f;
					this._lastPercentage = 1f;
					if (this.AnimationLoopedEvent != null)
					{
						this.AnimationLoopedEvent();
					}
				}
				this._percentage += -Time.get_deltaTime() * (1f / this._pathTime);
				break;
			case CameraPathAnimator.animationModes.pingPong:
			{
				float num = Time.get_deltaTime() * (1f / this._pathTime);
				this._percentage += num * this.pingPongDirection;
				if (this._percentage >= 1f)
				{
					this._percentage = 1f - num;
					this._lastPercentage = 1f;
					this.pingPongDirection = -1f;
					if (this.AnimationPingPongEvent != null)
					{
						this.AnimationPingPongEvent();
					}
				}
				if (this._percentage <= 0f)
				{
					this._percentage = num;
					this._lastPercentage = 0f;
					this.pingPongDirection = 1f;
					if (this.AnimationPingPongEvent != null)
					{
						this.AnimationPingPongEvent();
					}
				}
				break;
			}
			case CameraPathAnimator.animationModes.still:
				if (this._percentage >= 1f)
				{
					this._playing = false;
					if (this.AnimationFinishedEvent != null)
					{
						this.AnimationFinishedEvent();
					}
				}
				else
				{
					this._percentage += Time.get_deltaTime() * (1f / this._pathTime);
				}
				break;
			}
			if (this.smoothOrientationModeChanges)
			{
				if (this._orientationModeLerpTimer < this.orientationModeLerpTime)
				{
					this._orientationModeLerpTimer += Time.get_deltaTime();
				}
				else
				{
					this._orientationModeLerpTimer = this.orientationModeLerpTime;
				}
			}
		}
		this._percentage = Mathf.Clamp01(this._percentage);
	}

	private Quaternion GetMouseLook()
	{
		if (this.animationObject == null)
		{
			return Quaternion.get_identity();
		}
		float num = (float)Screen.get_width() / 2f;
		float num2 = (float)Screen.get_height() / 2f;
		float num3 = (Input.get_mousePosition().x - num) / (float)Screen.get_width() * 180f;
		float num4 = ((float)Screen.get_height() - Input.get_mousePosition().y - num2) / (float)Screen.get_height() * 180f;
		num4 = Mathf.Clamp(num4, this.minX, this.maxX);
		return Quaternion.Euler(new Vector3(num4, num3, 0f));
	}

	private void CheckEvents()
	{
		this.cameraPath.CheckEvents(this._percentage);
	}

	private void CleanUp()
	{
		this.cameraPath.eventList.CameraPathEventPoint += new CameraPathEventList.CameraPathEventPointHandler(this.OnCustomEvent);
		this.cameraPath.delayList.CameraPathDelayEvent += new CameraPathDelayList.CameraPathDelayEventHandler(this.OnDelayEvent);
	}

	private void OnDelayEvent(float time)
	{
		if (time > 0f)
		{
			this._delayTime = time;
		}
		else
		{
			this.Pause();
		}
	}

	private void OnCustomEvent(string eventName)
	{
		if (this.AnimationCustomEvent != null)
		{
			this.AnimationCustomEvent(eventName);
		}
	}
}
