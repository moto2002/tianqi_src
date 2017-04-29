using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(951), ForSend(951), ProtoContract(Name = "RoleLayerChangedReport")]
	[Serializable]
	public class RoleLayerChangedReport : IExtensible
	{
		public static readonly short OP = 951;

		private int _layer;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "layer", DataFormat = DataFormat.TwosComplement)]
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
