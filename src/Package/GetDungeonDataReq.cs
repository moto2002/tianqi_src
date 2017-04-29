using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(699), ForSend(699), ProtoContract(Name = "GetDungeonDataReq")]
	[Serializable]
	public class GetDungeonDataReq : IExtensible
	{
		public static readonly short OP = 699;

		private ChapterResume _chaterInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "chaterInfo", DataFormat = DataFormat.Default)]
		public ChapterResume chaterInfo
		{
			get
			{
				return this._chaterInfo;
			}
			set
			{
				this._chaterInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
