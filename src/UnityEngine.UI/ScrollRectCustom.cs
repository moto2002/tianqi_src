using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Scroll Rect Custom", 33), RequireComponent(typeof(RectTransform)), SelectionBase]
	public class ScrollRectCustom : UIBehaviour, ICanvasElement, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IScrollHandler
	{
		public enum MovementType
		{
			Unrestricted,
			Elastic,
			Clamped
		}

		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}

		public class Direction
		{
			public const int None = 0;

			public const int Previous = 1;

			public const int Next = 2;
		}

		private const float ERROR_RANGE_2_CONTENT_BOUND_CHANGE = 10f;

		[SerializeField]
		private RectTransform m_Content;

		[SerializeField]
		private bool m_Horizontal = true;

		[SerializeField]
		private bool m_Vertical = true;

		[SerializeField]
		private ScrollRectCustom.MovementType m_MovementType = ScrollRectCustom.MovementType.Elastic;

		[SerializeField]
		private float m_Elasticity = 0.1f;

		[SerializeField]
		private bool m_MovePage;

		[SerializeField]
		private bool m_childCenter;

		[SerializeField]
		private bool m_Inertia = true;

		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		[SerializeField]
		private ScrollRectCustom.ScrollRectEvent m_OnValueChanged = new ScrollRectCustom.ScrollRectEvent();

		private Vector2 m_PointerStartLocalCursor = Vector2.get_zero();

		private Vector2 m_ContentStartPosition = Vector2.get_zero();

		private RectTransform m_ViewRect;

		private Bounds m_ContentBounds;

		private float _contentBoundSize;

		private Bounds m_ViewBounds;

		private Vector2 m_Velocity;

		private bool m_IsDragging;

		private Vector2 m_PrevPosition = Vector2.get_zero();

		private Bounds m_PrevContentBounds;

		private Bounds m_PrevViewBounds;

		[NonSerialized]
		private bool m_HasRebuiltLayout;

		public bool GuideLock = true;

		private bool HasBeginDrag;

		[HideInInspector]
		public Action<int> OnPageChanged;

		[HideInInspector]
		public Action<int> OnMoveStopped;

		[HideInInspector]
		public Action OnHasBuilt;

		private bool NeedPageChangedCallBack;

		private Transform _Arrow2First;

		private Transform _Arrow2Last;

		private int _CurrentPageIndex;

		private float MinMovePercent = 0.1f;

		private float ErrorRange = 1f;

		private LayoutGroup m_layout;

		private Vector2 srcPoint;

		private bool m_IsShifting;

		private float _ShiftAmount = 0.1f;

		private Vector2 MoveSrcPoint = Vector2.get_zero();

		private Vector2 m_MoveDstPoint = Vector2.get_zero();

		public float PageMINError = 50f;

		private readonly Vector3[] m_Corners = new Vector3[4];

		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		public ScrollRectCustom.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		public bool movePage
		{
			get
			{
				return this.m_MovePage;
			}
			set
			{
				this.m_MovePage = value;
			}
		}

		public bool childCenter
		{
			get
			{
				return this.m_childCenter;
			}
			set
			{
				this.m_childCenter = value;
			}
		}

		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.get_onValueChanged().RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.get_onValueChanged().AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
			}
		}

		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.get_onValueChanged().RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.get_onValueChanged().AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
			}
		}

		public ScrollRectCustom.ScrollRectEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		private Vector2 ContentStartPosition
		{
			get
			{
				return this.m_ContentStartPosition;
			}
			set
			{
				this.m_ContentStartPosition = value;
			}
		}

		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.get_transform();
				}
				return this.m_ViewRect;
			}
		}

		private Bounds ContentBounds
		{
			get
			{
				return this.m_ContentBounds;
			}
			set
			{
				this.m_ContentBounds = value;
			}
		}

		private Vector3 ContentBoundsSize
		{
			get
			{
				return this.ContentBounds.get_size();
			}
		}

		private Vector3 ContentBoundsCenter
		{
			get
			{
				return this.ContentBounds.get_center();
			}
		}

		private Vector2 ContentBoundsMin
		{
			get
			{
				return this.ContentBounds.get_min();
			}
		}

		private Vector2 ContentBoundsMax
		{
			get
			{
				return this.ContentBounds.get_max();
			}
		}

		public Vector2 Velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		protected bool IsDragging
		{
			get
			{
				return this.m_IsDragging;
			}
			set
			{
				this.m_IsDragging = value;
				if (this.m_IsDragging)
				{
					this.IsShifting = false;
				}
			}
		}

		private bool HasRebuiltLayout
		{
			get
			{
				return this.m_HasRebuiltLayout;
			}
			set
			{
				this.m_HasRebuiltLayout = value;
			}
		}

		public Action<PointerEventData> onBeginDrag
		{
			private get;
			set;
		}

		public Action<PointerEventData> onEndDrag
		{
			private get;
			set;
		}

		public Action<PointerEventData> onDrag
		{
			private get;
			set;
		}

		public Action<Vector2> onScroll
		{
			private get;
			set;
		}

		[HideInInspector]
		public Transform Arrow2First
		{
			get
			{
				return this._Arrow2First;
			}
			set
			{
				this._Arrow2First = value;
				EventTriggerListener expr_17 = EventTriggerListener.Get(this._Arrow2First.get_gameObject());
				expr_17.onClick = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_17.onClick, new EventTriggerListener.VoidDelegateGameObject(this.OnClickArrow));
			}
		}

		[HideInInspector]
		public Transform Arrow2Last
		{
			get
			{
				return this._Arrow2Last;
			}
			set
			{
				this._Arrow2Last = value;
				EventTriggerListener expr_17 = EventTriggerListener.Get(this._Arrow2Last.get_gameObject());
				expr_17.onClick = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_17.onClick, new EventTriggerListener.VoidDelegateGameObject(this.OnClickArrow));
			}
		}

		public int CurrentPageIndex
		{
			get
			{
				return this._CurrentPageIndex;
			}
			set
			{
				this._CurrentPageIndex = Mathf.Clamp(value, 0, Mathf.Max(this.GetPageNum() - 1, 0));
				this.NeedPageChangedCallBack = true;
			}
		}

		private bool IsShifting
		{
			get
			{
				return this.m_IsShifting;
			}
			set
			{
				this.m_IsShifting = value;
				if (this.m_IsShifting)
				{
					this.StopMovement();
				}
				else if (this.OnMoveStopped != null)
				{
					this.OnMoveStopped.Invoke(this.CurrentPageIndex);
				}
			}
		}

		private float ShiftAmount
		{
			get
			{
				return this._ShiftAmount;
			}
			set
			{
				this._ShiftAmount = value;
			}
		}

		private Vector2 MoveDstPoint
		{
			get
			{
				return this.m_MoveDstPoint;
			}
			set
			{
				this.m_MoveDstPoint = value;
				this.MoveSrcPoint = this.content.get_anchoredPosition();
			}
		}

		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.ContentBoundsSize.x <= this.m_ViewBounds.get_size().x)
				{
					return (float)((this.m_ViewBounds.get_min().x <= this.ContentBoundsMin.x) ? 0 : 1);
				}
				return (this.m_ViewBounds.get_min().x - this.ContentBoundsMin.x) / (this.ContentBoundsSize.x - this.m_ViewBounds.get_size().x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.ContentBoundsSize.y <= this.m_ViewBounds.get_size().y)
				{
					return (float)((this.m_ViewBounds.get_min().y <= this.ContentBoundsMin.y) ? 0 : 1);
				}
				return (this.m_ViewBounds.get_min().y - this.ContentBoundsMin.y) / (this.ContentBoundsSize.y - this.m_ViewBounds.get_size().y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		private Vector3 GetContentBoundsSize()
		{
			if (!this.movePage)
			{
				return this.ContentBounds.get_size();
			}
			float minSpacing = this.GetMinSpacing();
			int pageNum = this.GetPageNum();
			if (this.horizontal)
			{
				return new Vector3(minSpacing * (float)pageNum, this.ContentBounds.get_size().y, this.ContentBounds.get_size().z);
			}
			return new Vector3(this.ContentBounds.get_size().x, minSpacing * (float)pageNum, this.ContentBounds.get_size().z);
		}

		private Vector2 GetContentBoundsMax()
		{
			if (!this.movePage)
			{
				return this.ContentBounds.get_max();
			}
			if (this.horizontal)
			{
				float num = (float)this.GetPageNum() * this.GetMinSpacing() + this.ContentBoundsMin.x;
				return new Vector2(num, this.ContentBounds.get_max().y);
			}
			return this.ContentBounds.get_max();
		}

		private bool IsContentBoundSizeChange()
		{
			if (this.horizontal)
			{
				if (Mathf.Abs(this._contentBoundSize - this.ContentBounds.get_size().x) >= 10f)
				{
					this._contentBoundSize = this.ContentBounds.get_size().x;
					return true;
				}
			}
			else if (this.vertical && Mathf.Abs(this._contentBoundSize - this.ContentBounds.get_size().y) >= 10f)
			{
				this._contentBoundSize = this.ContentBounds.get_size().y;
				return true;
			}
			return false;
		}

		protected override void Awake()
		{
			this.srcPoint = new Vector2(this.content.get_anchoredPosition().x, this.content.get_anchoredPosition().y);
			this.m_layout = this.content.GetComponent<LayoutGroup>();
		}

		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing != 2)
			{
				return;
			}
			this.UpdateBounds();
			this.UpdateScrollbars(Vector2.get_zero());
			this.UpdatePrevData();
			this.HasRebuiltLayout = true;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.get_onValueChanged().AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.get_onValueChanged().AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.get_onValueChanged().RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.get_onValueChanged().RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.HasRebuiltLayout = false;
			this.IsDragging = false;
			this.IsShifting = false;
		}

		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Velocity = Vector2.get_zero();
		}

		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (this.GuideLock && GuideManager.Instance.uidrag_lock)
			{
				return;
			}
			if (this.IsLessOnePage())
			{
				return;
			}
			if (this.onBeginDrag != null)
			{
				this.onBeginDrag.Invoke(eventData);
			}
			if (eventData.get_button() != null)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.NeedPageChangedCallBack = true;
			this.HasBeginDrag = true;
			this.IsShifting = false;
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.get_zero();
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.get_position(), eventData.get_pressEventCamera(), ref this.m_PointerStartLocalCursor);
			this.ContentStartPosition = this.m_Content.get_anchoredPosition();
			this.IsDragging = true;
		}

		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (this.GuideLock && GuideManager.Instance.uidrag_lock)
			{
				return;
			}
			if (this.IsLessOnePage())
			{
				return;
			}
			if (this.onEndDrag != null)
			{
				this.onEndDrag.Invoke(eventData);
			}
			if (eventData.get_button() != null)
			{
				return;
			}
			if (this.HasBeginDrag)
			{
				this.HasBeginDrag = false;
				this.IsDragging = false;
				this.AdjustPosition4MovePage();
				this.AdjustPostion4ChildCenter();
			}
		}

		public virtual void OnDrag(PointerEventData eventData)
		{
			if (this.GuideLock && GuideManager.Instance.uidrag_lock)
			{
				return;
			}
			if (this.IsLessOnePage())
			{
				return;
			}
			if (this.onDrag != null)
			{
				this.onDrag.Invoke(eventData);
			}
			if (eventData.get_button() != null)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.NeedPageChangedCallBack = true;
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.get_position(), eventData.get_pressEventCamera(), ref vector))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 vector2 = vector - this.m_PointerStartLocalCursor;
			Vector2 vector3 = this.ContentStartPosition + vector2;
			Vector2 vector4 = this.CalculateOffset(vector3 - this.m_Content.get_anchoredPosition());
			vector3 += vector4;
			if (this.m_MovementType == ScrollRectCustom.MovementType.Elastic)
			{
				if (vector4.x != 0f)
				{
					vector3.x -= ScrollRectCustom.RubberDelta(vector4.x, this.m_ViewBounds.get_size().x);
				}
				if (vector4.y != 0f)
				{
					vector3.y -= ScrollRectCustom.RubberDelta(vector4.y, this.m_ViewBounds.get_size().y);
				}
			}
			this.SetContentAnchoredPosition(vector3);
		}

		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			if (this.IsLessOnePage())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.get_scrollDelta();
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			Vector2 vector = this.m_Content.get_anchoredPosition();
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRectCustom.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.get_anchoredPosition());
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		public void Move2Next()
		{
			this.Velocity = Vector2.get_zero();
			float distance = 0f;
			if (this.horizontal)
			{
				distance = -this.GetMinSpacing();
			}
			else if (this.vertical)
			{
				distance = this.GetMinSpacing();
			}
			this.Move2Offset(distance);
		}

		public void Move2Previous()
		{
			this.Velocity = Vector2.get_zero();
			float distance = 0f;
			if (this.horizontal)
			{
				distance = this.GetMinSpacing();
			}
			else if (this.vertical)
			{
				distance = -this.GetMinSpacing();
			}
			this.Move2Offset(distance);
		}

		public void Move2Last(bool bRightNow = false)
		{
			this.Velocity = Vector2.get_zero();
			if (this.horizontal)
			{
				this.Move2Point(this.srcPoint - new Vector2(this.content.get_sizeDelta().x - this.viewRect.get_sizeDelta().x, 0f), bRightNow);
			}
			else if (this.vertical)
			{
				this.Move2Point(this.srcPoint + new Vector2(0f, this.content.get_sizeDelta().y - this.viewRect.get_sizeDelta().y), bRightNow);
			}
		}

		public void Move2First(bool bRightNow = false)
		{
			this.Velocity = Vector2.get_zero();
			this.Move2Point(this.srcPoint, bRightNow);
		}

		public void Move2Index(int index, bool bRightNow = false)
		{
			this.Velocity = Vector2.get_zero();
			float minSpacing = this.GetMinSpacing();
			if (this.horizontal)
			{
				this.Move2Point(this.srcPoint + new Vector2(-minSpacing * (float)index, 0f), bRightNow);
			}
			else if (this.vertical)
			{
				this.Move2Point(this.srcPoint + new Vector2(0f, minSpacing * (float)index), bRightNow);
			}
		}

		public void Move2Page(int pageIndex, bool bRightNow = false)
		{
			if (!this.movePage)
			{
				return;
			}
			this.Velocity = Vector2.get_zero();
			this.CurrentPageIndex = pageIndex;
			int num = Mathf.Clamp(pageIndex, 0, Math.Max(0, this.GetPageNum() - 1));
			if (this.horizontal)
			{
				this.Move2Point(this.srcPoint - new Vector2(this.viewRect.get_sizeDelta().x * (float)num, 0f), bRightNow);
			}
			else if (this.vertical)
			{
				this.Move2Point(this.srcPoint - new Vector2(0f, -this.viewRect.get_sizeDelta().y * (float)num), bRightNow);
			}
		}

		private void DoPageChangedCallback()
		{
			if (this.NeedPageChangedCallBack)
			{
				if (this.Arrow2First != null)
				{
					this.Arrow2First.get_gameObject().SetActive(!this.IsOnPositionFirst());
				}
				if (this.Arrow2Last != null)
				{
					this.Arrow2Last.get_gameObject().SetActive(!this.IsOnPositionLast());
				}
				if (this.OnPageChanged != null)
				{
					this.OnPageChanged.Invoke(this.CurrentPageIndex);
					this.NeedPageChangedCallBack = false;
				}
			}
		}

		private void OnClickArrow(GameObject go)
		{
			if (go == this.Arrow2First.get_gameObject() || go == this.Arrow2Last.get_gameObject())
			{
				this.NeedPageChangedCallBack = true;
			}
		}

		public int GetPageNum()
		{
			if (!this.movePage)
			{
				return 0;
			}
			float minSpacing = this.GetMinSpacing();
			int num;
			if (this.horizontal)
			{
				num = (int)(this.ContentBounds.get_size().x / minSpacing);
				if ((float)((int)this.ContentBounds.get_size().x % (int)minSpacing) > this.PageMINError)
				{
					num++;
				}
			}
			else
			{
				num = (int)(this.ContentBounds.get_size().y / minSpacing);
				if ((float)((int)this.ContentBounds.get_size().y % (int)minSpacing) > this.PageMINError)
				{
					num++;
				}
			}
			return num;
		}

		public bool IsOnPositionFirst()
		{
			if (this.horizontal)
			{
				return this.m_Content.get_anchoredPosition().x > this.PointAxisWithBoundMax() || Mathf.Abs(this.m_Content.get_anchoredPosition().x - this.PointAxisWithBoundMax()) < this.ErrorRange;
			}
			return this.m_Content.get_anchoredPosition().y < this.PointAxisWithBoundMin() || Mathf.Abs(this.m_Content.get_anchoredPosition().y - this.PointAxisWithBoundMin()) < this.ErrorRange;
		}

		public bool IsOnPositionLast()
		{
			if (this.horizontal)
			{
				return this.m_Content.get_anchoredPosition().x < this.PointAxisWithBoundMin() || Mathf.Abs(this.m_Content.get_anchoredPosition().x - this.PointAxisWithBoundMin()) < 10f;
			}
			return this.m_Content.get_anchoredPosition().y > this.PointAxisWithBoundMax() || Mathf.Abs(this.m_Content.get_anchoredPosition().y - this.PointAxisWithBoundMax()) < 10f;
		}

		private bool IsLessOnePage()
		{
			if (this.horizontal)
			{
				return this.ContentBounds.get_size().x <= this.viewRect.get_sizeDelta().x + this.PageMINError;
			}
			return !this.vertical || this.ContentBounds.get_size().y <= this.viewRect.get_sizeDelta().y + this.PageMINError;
		}

		private void UpdateShift()
		{
			if (this.IsShifting)
			{
				if (this.horizontal)
				{
					float num = this.content.get_anchoredPosition().x + this.ShiftAmount * Time.get_deltaTime();
					if ((this.MoveSrcPoint.x <= this.MoveDstPoint.x && num >= this.MoveDstPoint.x) || (this.MoveSrcPoint.x >= this.MoveDstPoint.x && num <= this.MoveDstPoint.x))
					{
						num = this.MoveDstPoint.x;
					}
					this.SetContentPosition(new Vector2(num, this.content.get_anchoredPosition().y));
					if (Mathf.Abs(num - this.MoveDstPoint.x) < Mathf.Abs(this.ErrorRange))
					{
						this.IsShifting = false;
					}
					if (this.content.get_anchoredPosition().x > this.srcPoint.x || this.content.get_anchoredPosition().x < this.srcPoint.x - (this.GetContentBoundsSize().x - this.viewRect.get_sizeDelta().x))
					{
						this.IsShifting = false;
					}
				}
				if (this.vertical)
				{
					float num2 = this.content.get_anchoredPosition().y + this.ShiftAmount * Time.get_deltaTime();
					if ((this.MoveSrcPoint.y <= this.MoveDstPoint.y && num2 >= this.MoveDstPoint.y) || (this.MoveSrcPoint.y >= this.MoveDstPoint.y && num2 <= this.MoveDstPoint.y))
					{
						num2 = this.MoveDstPoint.y;
					}
					this.SetContentPosition(new Vector2(this.content.get_anchoredPosition().x, num2));
					if (Mathf.Abs(num2 - this.MoveDstPoint.y) < Mathf.Abs(this.ErrorRange))
					{
						this.IsShifting = false;
					}
					if (this.content.get_anchoredPosition().y < this.srcPoint.y || this.content.get_anchoredPosition().y > this.srcPoint.y + (this.GetContentBoundsSize().y - this.viewRect.get_sizeDelta().y))
					{
						this.IsShifting = false;
					}
				}
			}
		}

		private void AdjustPosition4MovePage()
		{
			if (this.movePage)
			{
				int num = this.CalMoveDirection();
				float num2 = this.GetMinSpacing() - Mathf.Abs(this.GetResidue(this.m_Content.get_anchoredPosition()));
				if (num == 0)
				{
					this.Move2Offset(0f);
				}
				else if (num == 2)
				{
					if (this.horizontal)
					{
						this.Move2Offset(-num2);
					}
					else
					{
						this.Move2Offset(num2);
					}
				}
				else if (num == 1)
				{
					if (this.horizontal)
					{
						this.Move2Offset(num2);
					}
					else
					{
						this.Move2Offset(-num2);
					}
				}
			}
		}

		private void AdjustPostion4ChildCenter()
		{
			if (this.childCenter)
			{
				int num = this.CalMoveDirection();
				float num2 = this.GetMinSpacing() - Mathf.Abs(this.GetResidue(this.m_Content.get_anchoredPosition()));
				if (num == 0)
				{
					this.Move2Offset(0f);
				}
				else if (num == 2)
				{
					if (this.horizontal)
					{
						this.Move2Offset(-num2);
					}
					else
					{
						this.Move2Offset(num2);
					}
				}
				else if (num == 1)
				{
					if (this.horizontal)
					{
						this.Move2Offset(num2);
					}
					else
					{
						this.Move2Offset(-num2);
					}
				}
			}
		}

		private void Move2Offset(float distance)
		{
			if (this.horizontal)
			{
				this.MoveDstPoint = new Vector2(this.content.get_anchoredPosition().x + distance, this.content.get_anchoredPosition().y);
				this.Move2Point(this.MoveDstPoint, false);
			}
			else if (this.vertical)
			{
				this.MoveDstPoint = new Vector2(this.content.get_anchoredPosition().x, this.content.get_anchoredPosition().y + distance);
				this.Move2Point(this.MoveDstPoint, false);
			}
		}

		private void Move2Point(Vector2 point, bool bRightNow = false)
		{
			point = this.MoveToPointWithRound(point);
			if (bRightNow)
			{
				if (this.horizontal)
				{
					this.SetContentPosition(new Vector2(point.x, this.content.get_anchoredPosition().y));
				}
				else if (this.vertical)
				{
					this.SetContentPosition(new Vector2(this.content.get_anchoredPosition().x, point.y));
				}
				return;
			}
			if (this.horizontal)
			{
				this.MoveDstPoint = point;
				float num = this.MoveDstPoint.x - this.content.get_anchoredPosition().x;
				this.ShiftAmount = num / 0.2f;
				this.IsShifting = true;
			}
			else if (this.vertical)
			{
				this.MoveDstPoint = point;
				float num2 = this.MoveDstPoint.y - this.content.get_anchoredPosition().y;
				this.ShiftAmount = num2 / 0.2f;
				this.IsShifting = true;
			}
		}

		private Vector2 MoveToPointWithRound(Vector2 dstPoint)
		{
			float finalPointAxis = this.GetFinalPointAxis(dstPoint);
			if (this.horizontal)
			{
				this.SetCurrentPage(finalPointAxis);
				return new Vector2(finalPointAxis, dstPoint.y);
			}
			if (this.vertical)
			{
				this.SetCurrentPage(finalPointAxis);
				return new Vector2(dstPoint.x, finalPointAxis);
			}
			return Vector2.get_zero();
		}

		private float GetFinalPointAxis(Vector2 dstPoint)
		{
			int division = this.GetDivision(dstPoint);
			float minSpacing = this.GetMinSpacing();
			float finalPointAxisWithoutBound = this.GetFinalPointAxisWithoutBound(division, minSpacing);
			return this.GetFinalPointAxisWithBound(finalPointAxisWithoutBound);
		}

		private float GetFinalPointAxisWithoutBound(int division, float minSpacing)
		{
			if (this.horizontal)
			{
				return (float)division * minSpacing + this.srcPoint.x;
			}
			if (this.vertical)
			{
				return (float)division * minSpacing + this.srcPoint.y;
			}
			return 0f;
		}

		private float GetFinalPointAxisWithBound(float dst)
		{
			if (this.movePage)
			{
				float num = this.PointAxisWithBoundMin();
				float num2 = this.PointAxisWithBoundMax();
				return Mathf.Clamp(dst, num, num2);
			}
			return dst;
		}

		private float PointAxisWithBoundMin()
		{
			float result = 0f;
			if (this.horizontal)
			{
				float num = this.GetContentBoundsSize().x - this.viewRect.get_sizeDelta().x;
				if (num > 0f)
				{
					result = -num;
				}
			}
			else if (this.vertical)
			{
			}
			return result;
		}

		private float PointAxisWithBoundMax()
		{
			float result = 0f;
			if (!this.horizontal)
			{
				if (this.vertical)
				{
					float num = this.GetContentBoundsSize().y - this.viewRect.get_sizeDelta().y;
					if (num > 0f)
					{
						result = num;
					}
				}
			}
			return result;
		}

		private void SetCurrentPage(float dstValue)
		{
			float minSpacing = this.GetMinSpacing();
			if (minSpacing <= 0f)
			{
				return;
			}
			float num = 0f;
			int num2 = 0;
			if (this.horizontal)
			{
				num2 = Mathf.Abs((int)((dstValue - this.srcPoint.x) / minSpacing));
				num = (dstValue - this.srcPoint.x) % minSpacing;
			}
			else if (this.vertical)
			{
				num2 = Mathf.Abs((int)((dstValue - this.srcPoint.y) / minSpacing));
				num = (dstValue - this.srcPoint.y) % minSpacing;
			}
			if (Mathf.Abs(num) >= this.PageMINError)
			{
				num2++;
			}
			this.CurrentPageIndex = num2;
		}

		private void SetContentPosition(Vector2 point)
		{
			this.content.set_anchoredPosition(point);
		}

		private float GetResidue(Vector2 dstPoint)
		{
			float minSpacing = this.GetMinSpacing();
			float result = 0f;
			if (this.horizontal)
			{
				result = (dstPoint.x - this.srcPoint.x) % minSpacing;
			}
			else if (this.vertical)
			{
				result = (dstPoint.y - this.srcPoint.y) % minSpacing;
			}
			return result;
		}

		private int GetDivision(Vector2 dstPoint)
		{
			int num = 0;
			float minSpacing = this.GetMinSpacing();
			if (this.horizontal)
			{
				num = (int)((dstPoint.x - this.srcPoint.x) / minSpacing);
				float num2 = (dstPoint.x - this.srcPoint.x) % minSpacing;
				if (num2 < 0f)
				{
					if (Mathf.Abs(num2) >= minSpacing * (1f - this.MinMovePercent))
					{
						num--;
					}
					else
					{
						num = num;
					}
				}
				else if (num2 > 0f)
				{
					if (Mathf.Abs(num2) >= minSpacing * this.MinMovePercent)
					{
						num = num;
					}
					else
					{
						num++;
					}
				}
			}
			else if (this.vertical)
			{
				num = (int)((dstPoint.y - this.srcPoint.y) / minSpacing);
				float num2 = (dstPoint.y - this.srcPoint.y) % minSpacing;
				if (num2 > 0f && Mathf.Abs(num2) >= minSpacing * this.MinMovePercent)
				{
					num++;
				}
				else if (num2 < 0f && Mathf.Abs(num2) >= minSpacing * this.MinMovePercent)
				{
					num--;
				}
			}
			return num;
		}

		private float GetMinSpacing()
		{
			float result = 0f;
			if (this.horizontal)
			{
				if (!this.movePage)
				{
					if (this.m_layout != null)
					{
						if (this.m_layout is HorizontalOrVerticalLayoutGroup)
						{
							result = (this.m_layout as HorizontalOrVerticalLayoutGroup).get_spacing();
						}
						else if (this.m_layout is GridLayoutGroup)
						{
							result = (this.m_layout as GridLayoutGroup).get_spacing().x + (this.m_layout as GridLayoutGroup).get_cellSize().x;
						}
					}
				}
				else if (this.viewRect != null)
				{
					result = this.viewRect.get_sizeDelta().x;
				}
			}
			else if (this.vertical)
			{
				if (!this.movePage)
				{
					if (this.m_layout is HorizontalOrVerticalLayoutGroup)
					{
						result = (this.m_layout as HorizontalOrVerticalLayoutGroup).get_spacing();
					}
					else if (this.m_layout is GridLayoutGroup)
					{
						result = (this.m_layout as GridLayoutGroup).get_spacing().y + (this.m_layout as GridLayoutGroup).get_cellSize().y;
					}
				}
				else if (this.viewRect != null)
				{
					result = this.viewRect.get_sizeDelta().y;
				}
			}
			return result;
		}

		private int CalMoveDirection()
		{
			if (this.horizontal)
			{
				if (Mathf.Abs(this.m_Content.get_anchoredPosition().x - this.ContentStartPosition.x) < this.GetMinSpacing() * this.MinMovePercent)
				{
					return 0;
				}
				if (this.m_Content.get_anchoredPosition().x < this.ContentStartPosition.x)
				{
					return 2;
				}
				return 1;
			}
			else
			{
				if (!this.vertical)
				{
					return 0;
				}
				if (Mathf.Abs(this.m_Content.get_anchoredPosition().y - this.ContentStartPosition.y) < this.GetMinSpacing() * this.MinMovePercent)
				{
					return 0;
				}
				if (this.m_Content.get_anchoredPosition().y > this.ContentStartPosition.y)
				{
					return 2;
				}
				return 1;
			}
		}

		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		private bool EnsureLayoutHasRebuilt()
		{
			if (!this.HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
			else if (this.IsContentBoundSizeChange())
			{
				this.NeedPageChangedCallBack = true;
				if (this.OnHasBuilt != null)
				{
					this.OnHasBuilt.Invoke();
					return true;
				}
			}
			return false;
		}

		public virtual void StopMovement()
		{
			this.Velocity = Vector2.get_zero();
		}

		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.get_anchoredPosition().x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.get_anchoredPosition().y;
			}
			if (position != this.m_Content.get_anchoredPosition())
			{
				this.SetContentPosition(position);
				this.UpdateBounds();
			}
		}

		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			if (this.EnsureLayoutHasRebuilt())
			{
				return;
			}
			this.UpdateBounds();
			this.UpdateShift();
			if (this.IsShifting)
			{
				return;
			}
			float unscaledDeltaTime = Time.get_unscaledDeltaTime();
			Vector2 vector = this.CalculateOffset(Vector2.get_zero());
			if (!this.IsDragging && (vector != Vector2.get_zero() || this.Velocity != Vector2.get_zero()))
			{
				Vector2 vector2 = this.m_Content.get_anchoredPosition();
				for (int i = 0; i < 2; i++)
				{
					if (this.m_MovementType == ScrollRectCustom.MovementType.Elastic && vector.get_Item(i) != 0f)
					{
						float num = this.Velocity.get_Item(i);
						vector2.set_Item(i, Mathf.SmoothDamp(this.m_Content.get_anchoredPosition().get_Item(i), this.m_Content.get_anchoredPosition().get_Item(i) + vector.get_Item(i), ref num, this.m_Elasticity, float.PositiveInfinity, unscaledDeltaTime));
						this.m_Velocity.set_Item(i, num);
					}
					else if (this.m_Inertia)
					{
						int num2;
						int expr_130 = num2 = i;
						float num3 = this.m_Velocity.get_Item(num2);
						this.m_Velocity.set_Item(expr_130, num3 * Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime));
						if (Mathf.Abs(this.Velocity.get_Item(i)) < 1f)
						{
							this.m_Velocity.set_Item(i, 0f);
						}
						int expr_188 = num2 = i;
						num3 = vector2.get_Item(num2);
						vector2.set_Item(expr_188, num3 + this.Velocity.get_Item(i) * unscaledDeltaTime);
					}
					else
					{
						this.m_Velocity.set_Item(i, 0f);
					}
				}
				if (Mathf.Abs(this.Velocity.get_magnitude()) > 0.01f)
				{
					if (this.m_MovementType == ScrollRectCustom.MovementType.Clamped)
					{
						vector = this.CalculateOffset(vector2 - this.m_Content.get_anchoredPosition());
						vector2 += vector;
					}
					this.SetContentAnchoredPosition(vector2);
					if (this.onScroll != null)
					{
						this.onScroll.Invoke(this.Velocity);
					}
				}
			}
			if (this.IsDragging && this.m_Inertia)
			{
				Vector3 vector3 = (this.m_Content.get_anchoredPosition() - this.m_PrevPosition) / unscaledDeltaTime;
				this.Velocity = Vector3.Lerp(this.Velocity, vector3, unscaledDeltaTime * 10f);
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.ContentBounds != this.m_PrevContentBounds || this.m_Content.get_anchoredPosition() != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
			this.DoPageChangedCallback();
		}

		private void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.get_zero();
			}
			else
			{
				this.m_PrevPosition = this.m_Content.get_anchoredPosition();
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.ContentBounds;
		}

		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.set_size(Mathf.Clamp01((this.m_ViewBounds.get_size().x - Mathf.Abs(offset.x)) / this.ContentBoundsSize.x));
				this.m_HorizontalScrollbar.set_value(this.horizontalNormalizedPosition);
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.set_size(Mathf.Clamp01((this.m_ViewBounds.get_size().y - Mathf.Abs(offset.y)) / this.ContentBoundsSize.y));
				this.m_VerticalScrollbar.set_value(this.verticalNormalizedPosition);
			}
		}

		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		private void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.ContentBoundsSize.get_Item(axis) - this.m_ViewBounds.get_size().get_Item(axis);
			float num2 = this.m_ViewBounds.get_min().get_Item(axis) - value * num;
			float num3 = this.m_Content.get_localPosition().get_Item(axis) + num2 - this.ContentBounds.get_min().get_Item(axis);
			Vector3 localPosition = this.m_Content.get_localPosition();
			if (Mathf.Abs(localPosition.get_Item(axis) - num3) > 0.01f)
			{
				localPosition.set_Item(axis, num3);
				this.m_Content.set_localPosition(localPosition);
				this.m_Velocity.set_Item(axis, 0f);
				this.UpdateBounds();
			}
		}

		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		private void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.get_rect().get_center(), this.viewRect.get_rect().get_size());
			this.ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 contentBoundsSize = this.ContentBoundsSize;
			Vector3 contentBoundsCenter = this.ContentBoundsCenter;
			Vector3 vector = this.m_ViewBounds.get_size() - contentBoundsSize;
			if (vector.x > 0f)
			{
				contentBoundsCenter.x -= vector.x * (this.m_Content.get_pivot().x - 0.5f);
				contentBoundsSize.x = this.m_ViewBounds.get_size().x;
			}
			if (vector.y > 0f)
			{
				contentBoundsCenter.y -= vector.y * (this.m_Content.get_pivot().y - 0.5f);
				contentBoundsSize.y = this.m_ViewBounds.get_size().y;
			}
			this.m_ContentBounds.set_size(contentBoundsSize);
			this.m_ContentBounds.set_center(contentBoundsCenter);
		}

		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			Vector3 vector = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
			Vector3 vector2 = new Vector3(-3.40282347E+38f, -3.40282347E+38f, -3.40282347E+38f);
			Matrix4x4 worldToLocalMatrix = this.viewRect.get_worldToLocalMatrix();
			this.m_Content.GetWorldCorners(this.m_Corners);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector3 = worldToLocalMatrix.MultiplyPoint3x4(this.m_Corners[i]);
				vector = Vector3.Min(vector3, vector);
				vector2 = Vector3.Max(vector3, vector2);
			}
			Bounds result = new Bounds(vector, Vector3.get_zero());
			result.Encapsulate(vector2);
			return result;
		}

		private Vector2 CalculateOffset(Vector2 delta)
		{
			Vector2 zero = Vector2.get_zero();
			if (this.m_MovementType == ScrollRectCustom.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 contentBoundsMin = this.ContentBoundsMin;
			Vector2 contentBoundsMax = this.GetContentBoundsMax();
			if (this.m_Horizontal)
			{
				contentBoundsMin.x += delta.x;
				contentBoundsMax.x += delta.x;
				if (contentBoundsMin.x > this.m_ViewBounds.get_min().x)
				{
					zero.x = this.m_ViewBounds.get_min().x - contentBoundsMin.x;
				}
				else if (contentBoundsMax.x < this.m_ViewBounds.get_max().x)
				{
					zero.x = this.m_ViewBounds.get_max().x - contentBoundsMax.x;
				}
			}
			if (this.m_Vertical)
			{
				contentBoundsMin.y += delta.y;
				contentBoundsMax.y += delta.y;
				if (contentBoundsMax.y < this.m_ViewBounds.get_max().y)
				{
					zero.y = this.m_ViewBounds.get_max().y - contentBoundsMax.y;
				}
				else if (contentBoundsMin.y > this.m_ViewBounds.get_min().y)
				{
					zero.y = this.m_ViewBounds.get_min().y - contentBoundsMin.y;
				}
			}
			return zero;
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		virtual Transform get_transform()
		{
			return base.get_transform();
		}

		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}
	}
}
