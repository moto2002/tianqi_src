using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(807), ForSend(807), ProtoContract(Name = "RoleLayerChangedNty")]
	[Serializable]
	public class RoleLayerChangedNty : IExtensible
	{
		public static readonly short OP = 807;

		private long _roleId;

		private int _layer;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "layer", DataFormat = DataFormat.TwosComplement)]
		public int layer
		{
			get
			{
				return this._layer;
			}
			set
			{
				this._layer = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
