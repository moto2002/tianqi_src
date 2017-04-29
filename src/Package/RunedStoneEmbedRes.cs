using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(764), ForSend(764), ProtoContract(Name = "RunedStoneEmbedRes")]
	[Serializable]
	public class RunedStoneEmbedRes : IExtensible
	{
		public static readonly short OP = 764;

		private int _skillId;

		private int _groupId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
