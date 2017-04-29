using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1829), ForSend(1829), ProtoContract(Name = "SkillUpReq")]
	[Serializable]
	public class SkillUpReq : IExtensible
	{
		public static readonly short OP = 1829;

		private int _sortNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sortNum", DataFormat = DataFormat.TwosComplement)]
		public int sortNum
		{
			get
			{
				return this._sortNum;
			}
			set
			{
				this._sortNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
