using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1038), ForSend(1038), ProtoContract(Name = "EnchantEquipResultAckRes")]
	[Serializable]
	public class EnchantEquipResultAckRes : IExtensible
	{
		public static readonly short OP = 1038;

		private long _equipId;

		private int _position;

		private ExcellentAttr _excellentAttr;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "excellentAttr", DataFormat = DataFormat.Default)]
		public ExcellentAttr excellentAttr
		{
			get
			{
				return this._excellentAttr;
			}
			set
			{
				this._excellentAttr = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
