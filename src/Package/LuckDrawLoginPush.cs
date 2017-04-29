using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(735), ForSend(735), ProtoContract(Name = "LuckDrawLoginPush")]
	[Serializable]
	public class LuckDrawLoginPush : IExtensible
	{
		public static readonly short OP = 735;

		private readonly List<LuckDrawInfo> _luckDrawInfos = new List<LuckDrawInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "luckDrawInfos", DataFormat = DataFormat.Default)]
		public List<LuckDrawInfo> luckDrawInfos
		{
			get
			{
				return this._luckDrawInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
