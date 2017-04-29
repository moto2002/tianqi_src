using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(632), ForSend(632), ProtoContract(Name = "SendFashionInfo")]
	[Serializable]
	public class SendFashionInfo : IExtensible
	{
		public static readonly short OP = 632;

		private readonly List<FashionInfo> _Info = new List<FashionInfo>();

		private readonly List<string> _wearFashion = new List<string>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "Info", DataFormat = DataFormat.Default)]
		public List<FashionInfo> Info
		{
			get
			{
				return this._Info;
			}
		}

		[ProtoMember(2, Name = "wearFashion", DataFormat = DataFormat.Default)]
		public List<string> wearFashion
		{
			get
			{
				return this._wearFashion;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
