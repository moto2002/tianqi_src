using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3348), ForSend(3348), ProtoContract(Name = "GetGuildLogRes")]
	[Serializable]
	public class GetGuildLogRes : IExtensible
	{
		public static readonly short OP = 3348;

		private readonly List<GuildLogTrace> _logTraces = new List<GuildLogTrace>();

		private int _nPage;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "logTraces", DataFormat = DataFormat.Default)]
		public List<GuildLogTrace> logTraces
		{
			get
			{
				return this._logTraces;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nPage
		{
			get
			{
				return this._nPage;
			}
			set
			{
				this._nPage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
