using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7892), ForSend(7892), ProtoContract(Name = "SetSkillFormationReq")]
	[Serializable]
	public class SetSkillFormationReq : IExtensible
	{
		public static readonly short OP = 7892;

		private int _skillConfigNum;

		private int _tType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillConfigNum", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "tType", DataFormat = DataFormat.TwosComplement)]
		public int tType
		{
			get
			{
				return this._tType;
			}
			set
			{
				this._tType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
