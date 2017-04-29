using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(565), ForSend(565), ProtoContract(Name = "GemBaseInfoNty")]
	[Serializable]
	public class GemBaseInfoNty : IExtensible
	{
		public static readonly short OP = 565;

		private readonly List<GemPartInfo> _gemPartInfos = new List<GemPartInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "gemPartInfos", DataFormat = DataFormat.Default)]
		public List<GemPartInfo> gemPartInfos
		{
			get
			{
				return this._gemPartInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
