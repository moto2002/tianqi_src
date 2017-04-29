using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(54), ForSend(54), ProtoContract(Name = "RunedStoneLoginPush")]
	[Serializable]
	public class RunedStoneLoginPush : IExtensible
	{
		public static readonly short OP = 54;

		private readonly List<RunedStoneInfo> _stoneInfos = new List<RunedStoneInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "stoneInfos", DataFormat = DataFormat.Default)]
		public List<RunedStoneInfo> stoneInfos
		{
			get
			{
				return this._stoneInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
