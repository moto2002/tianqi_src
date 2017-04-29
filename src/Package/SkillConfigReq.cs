using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1722), ForSend(1722), ProtoContract(Name = "SkillConfigReq")]
	[Serializable]
	public class SkillConfigReq : IExtensible
	{
		public static readonly short OP = 1722;

		private int _sourceNum;

		private int _index;

		private int _skillConfigNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sourceNum", DataFormat = DataFormat.TwosComplement)]
		public int sourceNum
		{
			get
			{
				return this._sourceNum;
			}
			set
			{
				this._sourceNum = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "index", DataFormat = DataFormat.TwosComplement)]
		public int index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "skillConfigNum", DataFormat = DataFormat.TwosComplement)]
		public int skillConfigNum
		{
			get
			{
				return this._skillConfigNum;
			}
			set
			{
				this._skillConfigNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
