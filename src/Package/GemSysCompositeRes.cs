using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(803), ForSend(803), ProtoContract(Name = "GemSysCompositeRes")]
	[Serializable]
	public class GemSysCompositeRes : IExtensible
	{
		public static readonly short OP = 803;

		private int _typeId;

		private int _method = 1;

		private int _count;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "typeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "method", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int method
		{
			get
			{
				return this._method;
			}
			set
			{
				this._method = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
