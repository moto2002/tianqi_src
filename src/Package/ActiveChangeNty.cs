using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8830), ForSend(8830), ProtoContract(Name = "ActiveChangeNty")]
	[Serializable]
	public class ActiveChangeNty : IExtensible
	{
		public static readonly short OP = 8830;

		private readonly List<ActiveCenterInfo> _activeInfos = new List<ActiveCenterInfo>();

		private long _serverTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "activeInfos", DataFormat = DataFormat.Default)]
		public List<ActiveCenterInfo> activeInfos
		{
			get
			{
				return this._activeInfos;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "serverTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long serverTime
		{
			get
			{
				return this._serverTime;
			}
			set
			{
				this._serverTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
