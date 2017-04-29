using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(595), ForSend(595), ProtoContract(Name = "RefineEquipRes")]
	[Serializable]
	public class RefineEquipRes : IExtensible
	{
		public static readonly short OP = 595;

		private long _equipId;

		private ExcellentAttr _excellentAttrs;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "excellentAttrs", DataFormat = DataFormat.Default)]
		public ExcellentAttr excellentAttrs
		{
			get
			{
				return this._excellentAttrs;
			}
			set
			{
				this._excellentAttrs = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
