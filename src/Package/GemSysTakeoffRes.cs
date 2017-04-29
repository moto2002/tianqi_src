using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(239), ForSend(239), ProtoContract(Name = "GemSysTakeoffRes")]
	[Serializable]
	public class GemSysTakeoffRes : IExtensible
	{
		public static readonly short OP = 239;

		private EquipLibType.ELT _type;

		private int _hole;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public EquipLibType.ELT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "hole", DataFormat = DataFormat.TwosComplement)]
		public int hole
		{
			get
			{
				return this._hole;
			}
			set
			{
				this._hole = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
