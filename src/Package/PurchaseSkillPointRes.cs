using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(950), ForSend(950), ProtoContract(Name = "PurchaseSkillPointRes")]
	[Serializable]
	public class PurchaseSkillPointRes : IExtensible
	{
		public static readonly short OP = 950;

		private int _skillPoint;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "skillPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillPoint
		{
			get
			{
				return this._skillPoint;
			}
			set
			{
				this._skillPoint = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
