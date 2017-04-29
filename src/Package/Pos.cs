using ProtoBuf;
using System;
using UnityEngine;

namespace Package
{
	[ProtoContract(Name = "Pos")]
	[Serializable]
	public class Pos : IExtensible
	{
		private float _x;

		private float _y;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "x", DataFormat = DataFormat.FixedSize)]
		public float x
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "y", DataFormat = DataFormat.FixedSize)]
		public float y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}

		public static Pos FromScenePos(Vector3 pos)
		{
			return new Pos
			{
				x = pos.x * 100f,
				y = pos.z * 100f
			};
		}

		public static Vector3 ToScenePos(Pos pos)
		{
			return PosDirUtility.ToTerrainPoint(pos, 0f);
		}

		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.x,
				", ",
				this.y,
				") "
			});
		}
	}
}
