using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPathControlPoint : MonoBehaviour
{
	public string givenName = string.Empty;

	public string customName = string.Empty;

	public string fullName = string.Empty;

	[SerializeField]
	private Vector3 _position;

	[SerializeField]
	private bool _splitControlPoints;

	[SerializeField]
	private Vector3 _forwardControlPoint;

	[SerializeField]
	private Vector3 _backwardControlPoint;

	[SerializeField]
	private Vector3 _pathDirection = Vector3.get_forward();

	public int index;

	public float percentage;

	public float normalisedPercentage;

	public Vector3 localPosition
	{
		get
		{
			return base.get_transform().get_rotation() * this._position;
		}
		set
		{
			this._position = Quaternion.Inverse(base.get_transform().get_rotation()) * value;
		}
	}

	public Vector3 worldPosition
	{
		get
		{
			return this.LocalToWorldPosition(this._position);
		}
		set
		{
			this._position = this.WorldToLocalPosition(value);
		}
	}

	public Vector3 forwardControlPointWorld
	{
		get
		{
			return this.forwardControlPoint + base.get_transform().get_position();
		}
		set
		{
			this.forwardControlPoint = value - base.get_transform().get_position();
		}
	}

	public Vector3 forwardControlPoint
	{
		get
		{
			return base.get_transform().get_rotation() * (this._forwardControlPoint + this._position);
		}
		set
		{
			Vector3 vector = Quaternion.Inverse(base.get_transform().get_rotation()) * value;
			vector += -this._position;
			this._forwardControlPoint = vector;
		}
	}

	public Vector3 forwardControlPointLocal
	{
		get
		{
			return base.get_transform().get_rotation() * this._forwardControlPoint;
		}
		set
		{
			Vector3 forwardControlPoint = Quaternion.Inverse(base.get_transform().get_rotation()) * value;
			this._forwardControlPoint = forwardControlPoint;
		}
	}

	public Vector3 backwardControlPointWorld
	{
		get
		{
			return this.backwardControlPoint + base.get_transform().get_position();
		}
		set
		{
			this.backwardControlPoint = value - base.get_transform().get_position();
		}
	}

	public Vector3 backwardControlPoint
	{
		get
		{
			Vector3 vector = (!this._splitControlPoints) ? (-this._forwardControlPoint) : this._backwardControlPoint;
			return base.get_transform().get_rotation() * (vector + this._position);
		}
		set
		{
			Vector3 vector = Quaternion.Inverse(base.get_transform().get_rotation()) * value;
			vector += -this._position;
			if (this._splitControlPoints)
			{
				this._backwardControlPoint = vector;
			}
			else
			{
				this._forwardControlPoint = -vector;
			}
		}
	}

	public bool splitControlPoints
	{
		get
		{
			return this._splitControlPoints;
		}
		set
		{
			if (value != this._splitControlPoints)
			{
				this._backwardControlPoint = -this._forwardControlPoint;
			}
			this._splitControlPoints = value;
		}
	}

	public Vector3 trackDirection
	{
		get
		{
			return this._pathDirection;
		}
		set
		{
			if (value == Vector3.get_zero())
			{
				return;
			}
			this._pathDirection = value.get_normalized();
		}
	}

	public string displayName
	{
		get
		{
			if (this.customName != string.Empty)
			{
				return this.customName;
			}
			return this.givenName;
		}
	}

	private void OnEnable()
	{
		base.set_hideFlags(2);
	}

	public Vector3 WorldToLocalPosition(Vector3 _worldPosition)
	{
		Vector3 vector = _worldPosition - base.get_transform().get_position();
		return Quaternion.Inverse(base.get_transform().get_rotation()) * vector;
	}

	public Vector3 LocalToWorldPosition(Vector3 _localPosition)
	{
		return base.get_transform().get_rotation() * _localPosition + base.get_transform().get_position();
	}

	public void CopyData(CameraPathControlPoint to)
	{
		to.customName = this.customName;
		to.index = this.index;
		to.percentage = this.percentage;
		to.normalisedPercentage = this.normalisedPercentage;
		to.worldPosition = this.worldPosition;
		to.splitControlPoints = this._splitControlPoints;
		to.forwardControlPoint = this._forwardControlPoint;
		to.backwardControlPoint = this._backwardControlPoint;
	}
}
