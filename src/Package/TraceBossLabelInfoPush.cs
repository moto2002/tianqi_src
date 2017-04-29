using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2839), ForSend(2839), ProtoContract(Name = "TraceBossLabelInfoPush")]
	[Serializable]
	public class TraceBossLabelInfoPush : IExtensible
	{
		public static readonly short OP = 2839;

		private readonly List<TraceBossInfo> _info = new List<TraceBossInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<TraceBossInfo> info
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
