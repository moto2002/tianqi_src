using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(701), ForSend(701), ProtoContract(Name = "GetDungeonDataRes")]
	[Serializable]
	public class GetDungeonDataRes : IExtensible
	{
		public static readonly short OP = 701;

		private TypeInfo _chapterInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "chapterInfo", DataFormat = DataFormat.Default)]
		public TypeInfo chapterInfo
		{
			get
			{
				return this._chapterInfo;
			}
			set
			{
				this._chapterInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
