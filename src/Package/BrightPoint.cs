using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BrightPoint")]
	[Serializable]
	public class BrightPoint : IExtensible
	{
		private int _pointId;

		private int _index;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pointId", DataFormat = DataFormat.ZigZag)]
		public int pointId
		{
			get
			{
				return this._pointId;
			}
			set
			{
				this._pointId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "index", DataFormat = DataFormat.ZigZag)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
