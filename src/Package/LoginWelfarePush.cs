using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(8834), ForSend(8834), ProtoContract(Name = "LoginWelfarePush")]
	[Serializable]
	public class LoginWelfarePush : IExtensible
	{
		public static readonly short OP = 8834;

		private readonly List<EveryDayInfo> _everyDayInfo = new List<EveryDayInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "everyDayInfo", DataFormat = DataFormat.Default)]
		public List<EveryDayInfo> everyDayInfo
		{
			get
			{
				return this._everyDayInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
