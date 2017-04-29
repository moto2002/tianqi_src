using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2297), ForSend(2297), ProtoContract(Name = "DataCompositeReq")]
	[Serializable]
	public class DataCompositeReq : IExtensible
	{
		[ProtoContract(Name = "OpType")]
		public enum OpType
		{
			[ProtoEnum(Name = "Enchanting", Value = 1)]
			Enchanting = 1,
			[ProtoEnum(Name = "RisingStar", Value = 2)]
			RisingStar
		}

		public static readonly short OP = 2297;

		private DataCompositeReq.OpType _tType;

		private int _cfgId;

		private int _method = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "tType", DataFormat = DataFormat.TwosComplement)]
		public DataCompositeReq.OpType tType
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

		[ProtoMember(2, IsRequired = true, Name = "cfgId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "method", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
