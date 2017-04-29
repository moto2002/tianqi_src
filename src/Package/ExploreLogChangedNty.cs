using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1777), ForSend(1777), ProtoContract(Name = "ExploreLogChangedNty")]
	[Serializable]
	public class ExploreLogChangedNty : IExtensible
	{
		public static readonly short OP = 1777;

		private readonly List<ExploreLog> _exploreLogs = new List<ExploreLog>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "exploreLogs", DataFormat = DataFormat.Default)]
		public List<ExploreLog> exploreLogs
		{
			get
			{
				return this._exploreLogs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
