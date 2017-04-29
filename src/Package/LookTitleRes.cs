using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(8671), ForSend(8671), ProtoContract(Name = "LookTitleRes")]
	[Serializable]
	public class LookTitleRes : IExtensible
	{
		public static readonly short OP = 8671;

		private readonly List<TitleId> _ids = new List<TitleId>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "ids", DataFormat = DataFormat.Default)]
		public List<TitleId> ids
		{
			get
			{
				return this._ids;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
