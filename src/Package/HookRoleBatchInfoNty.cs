using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(137), ForSend(137), ProtoContract(Name = "HookRoleBatchInfoNty")]
	[Serializable]
	public class HookRoleBatchInfoNty : IExtensible
	{
		public static readonly short OP = 137;

		private int _batch = 1;

		private int _bossRefreshRate = 2;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "batch", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int batch
		{
			get
			{
				return this._batch;
			}
			set
			{
				this._batch = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "bossRefreshRate", DataFormat = DataFormat.TwosComplement), DefaultValue(2)]
		public int bossRefreshRate
		{
			get
			{
				return this._bossRefreshRate;
			}
			set
			{
				this._bossRefreshRate = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
