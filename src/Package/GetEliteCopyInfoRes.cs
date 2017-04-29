using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2763), ForSend(2763), ProtoContract(Name = "GetEliteCopyInfoRes")]
	[Serializable]
	public class GetEliteCopyInfoRes : IExtensible
	{
		public static readonly short OP = 2763;

		private readonly List<EliteCopyInfo> _info = new List<EliteCopyInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<EliteCopyInfo> info
		{
			get
			{
				return this._info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
