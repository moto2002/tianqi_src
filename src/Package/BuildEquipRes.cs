using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4839), ForSend(4839), ProtoContract(Name = "BuildEquipRes")]
	[Serializable]
	public class BuildEquipRes : IExtensible
	{
		public static readonly short OP = 4839;

		private long _equipId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "equipId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
