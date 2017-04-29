using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2783), ForSend(2783), ProtoContract(Name = "ExtraRewardPush")]
	[Serializable]
	public class ExtraRewardPush : IExtensible
	{
		public static readonly short OP = 2783;

		private readonly List<ExtraRewardInfo> _record = new List<ExtraRewardInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "record", DataFormat = DataFormat.Default)]
		public List<ExtraRewardInfo> record
		{
			get
			{
				return this._record;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
