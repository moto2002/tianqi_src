using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "Vector2")]
	[Serializable]
	public class Vector2 : IExtensible
	{
		public static readonly short OP = 281;

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
