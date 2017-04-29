using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2283), ForSend(2283), ProtoContract(Name = "PushOtherData")]
	[Serializable]
	public class PushOtherData : IExtensible
	{
		public static readonly short OP = 2283;

		private readonly List<OtherData> _otherData = new List<OtherData>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "otherData", DataFormat = DataFormat.Default)]
		public List<OtherData> otherData
		{
			get
			{
				return this._otherData;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
