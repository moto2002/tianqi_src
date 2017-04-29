using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2793), ForSend(2793), ProtoContract(Name = "RoleTalentResetRes")]
	[Serializable]
	public class RoleTalentResetRes : IExtensible
	{
		public static readonly short OP = 2793;

		private readonly List<RoleTalentInfo> _talent = new List<RoleTalentInfo>();

		private int _resetTalentCount;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "talent", DataFormat = DataFormat.Default)]
		public List<RoleTalentInfo> talent
		{
			get
			{
				return this._talent;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "resetTalentCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resetTalentCount
		{
			get
			{
				return this._resetTalentCount;
			}
			set
			{
				this._resetTalentCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
