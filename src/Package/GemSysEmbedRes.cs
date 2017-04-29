using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2134), ForSend(2134), ProtoContract(Name = "GemSysEmbedRes")]
	[Serializable]
	public class GemSysEmbedRes : IExtensible
	{
		public static readonly short OP = 2134;

		private long _newGemId;

		private int _newGemTypeId;

		private EquipLibType.ELT _type;

		private int _hole;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "newGemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long newGemId
		{
			get
			{
				return this._newGemId;
			}
			set
			{
				this._newGemId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "newGemTypeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newGemTypeId
		{
			get
			{
				return this._newGemTypeId;
			}
			set
			{
				this._newGemTypeId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "hole", DataFormat = DataFormat.TwosComplement)]
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
