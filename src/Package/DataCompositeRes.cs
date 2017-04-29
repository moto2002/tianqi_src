using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7762), ForSend(7762), ProtoContract(Name = "DataCompositeRes")]
	[Serializable]
	public class DataCompositeRes : IExtensible
	{
		public static readonly short OP = 7762;

		private int _cfgId;

		private int _method;

		private int _count;

		private int _tType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "cfgId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cfgId
		{
			get
			{
				return this._cfgId;
			}
			set
			{
				this._cfgId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "method", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "tType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int tType
		{
			get
			{
				return this._tType;
			}
			set
			{
				this._tType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
