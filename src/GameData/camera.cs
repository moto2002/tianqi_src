using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"type"
	}), ProtoContract(Name = "camera")]
	[Serializable]
	public class camera : IExtensible
	{
		private string _type;

		private float _moveSmallCorrection;

		private float _rotateSmallCorrection;

		private float _moveMediumCorrection;

		private float _moveLargeCorrection;

		private float _moveSmallAngle;

		private float _moveMediumAngle;

		private float _moveLargeAngle;

		private float _rotateMediumCorrection;

		private float _rotateLargeCorrection;

		private float _rotateSmallAngle;

		private float _rotateMediumAngle;

		private float _rotateLargeAngle;

		private float _cameraPosYMax;

		private float _cameraPosYMin;

		private float _cameraPosYCorrection;

		private float _cameraPosZPointAPointC;

		private float _cameraPosZCoefficient;

		private float _cameraAngleMax;

		private float _cameraAngleMin;

		private float _pointApointBDistance;

		private float _pointBPosY;

		private int _canDrag;

		private float _dragSpeed;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.Default)]
		public string type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "moveSmallCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveSmallCorrection
		{
			get
			{
				return this._moveSmallCorrection;
			}
			set
			{
				this._moveSmallCorrection = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "rotateSmallCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotateSmallCorrection
		{
			get
			{
				return this._rotateSmallCorrection;
			}
			set
			{
				this._rotateSmallCorrection = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "moveMediumCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveMediumCorrection
		{
			get
			{
				return this._moveMediumCorrection;
			}
			set
			{
				this._moveMediumCorrection = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "moveLargeCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveLargeCorrection
		{
			get
			{
				return this._moveLargeCorrection;
			}
			set
			{
				this._moveLargeCorrection = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "moveSmallAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveSmallAngle
		{
			get
			{
				return this._moveSmallAngle;
			}
			set
			{
				this._moveSmallAngle = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "moveMediumAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveMediumAngle
		{
			get
			{
				return this._moveMediumAngle;
			}
			set
			{
				this._moveMediumAngle = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "moveLargeAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float moveLargeAngle
		{
			get
			{
				return this._moveLargeAngle;
			}
			set
			{
				this._moveLargeAngle = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "rotateMediumCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotateMediumCorrection
		{
			get
			{
				return this._rotateMediumCorrection;
			}
			set
			{
				this._rotateMediumCorrection = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "rotateLargeCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotateLargeCorrection
		{
			get
			{
				return this._rotateLargeCorrection;
			}
			set
			{
				this._rotateLargeCorrection = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "rotateSmallAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotateSmallAngle
		{
			get
			{
				return this._rotateSmallAngle;
			}
			set
			{
				this._rotateSmallAngle = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "rotateMediumAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotateMediumAngle
		{
			get
			{
				return this._rotateMediumAngle;
			}
			set
			{
				this._rotateMediumAngle = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "rotateLargeAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float rotateLargeAngle
		{
			get
			{
				return this._rotateLargeAngle;
			}
			set
			{
				this._rotateLargeAngle = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "cameraPosYMax", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraPosYMax
		{
			get
			{
				return this._cameraPosYMax;
			}
			set
			{
				this._cameraPosYMax = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "cameraPosYMin", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraPosYMin
		{
			get
			{
				return this._cameraPosYMin;
			}
			set
			{
				this._cameraPosYMin = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "cameraPosYCorrection", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraPosYCorrection
		{
			get
			{
				return this._cameraPosYCorrection;
			}
			set
			{
				this._cameraPosYCorrection = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "cameraPosZPointAPointC", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraPosZPointAPointC
		{
			get
			{
				return this._cameraPosZPointAPointC;
			}
			set
			{
				this._cameraPosZPointAPointC = value;
			}
		}

		[ProtoMember(19, IsRequired = false, Name = "cameraPosZCoefficient", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraPosZCoefficient
		{
			get
			{
				return this._cameraPosZCoefficient;
			}
			set
			{
				this._cameraPosZCoefficient = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "cameraAngleMax", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraAngleMax
		{
			get
			{
				return this._cameraAngleMax;
			}
			set
			{
				this._cameraAngleMax = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "cameraAngleMin", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float cameraAngleMin
		{
			get
			{
				return this._cameraAngleMin;
			}
			set
			{
				this._cameraAngleMin = value;
			}
		}

		[ProtoMember(22, IsRequired = false, Name = "pointApointBDistance", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float pointApointBDistance
		{
			get
			{
				return this._pointApointBDistance;
			}
			set
			{
				this._pointApointBDistance = value;
			}
		}

		[ProtoMember(23, IsRequired = false, Name = "pointBPosY", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float pointBPosY
		{
			get
			{
				return this._pointBPosY;
			}
			set
			{
				this._pointBPosY = value;
			}
		}

		[ProtoMember(24, IsRequired = false, Name = "canDrag", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int canDrag
		{
			get
			{
				return this._canDrag;
			}
			set
			{
				this._canDrag = value;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "dragSpeed", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float dragSpeed
		{
			get
			{
				return this._dragSpeed;
			}
			set
			{
				this._dragSpeed = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
