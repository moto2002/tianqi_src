using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(430), ForSend(430), ProtoContract(Name = "EnchantEquipRes")]
	[Serializable]
	public class EnchantEquipRes : IExtensible
	{
		public static readonly short OP = 430;

		private long _equipId;

		private ExcellentAttr _oldEnchantAttr;

		private ExcellentAttr _newEnchantAttr;

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

		[ProtoMember(2, IsRequired = true, Name = "oldEnchantAttr", DataFormat = DataFormat.Default)]
		public ExcellentAttr oldEnchantAttr
		{
			get
			{
				return this._oldEnchantAttr;
			}
			set
			{
				this._oldEnchantAttr = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "newEnchantAttr", DataFormat = DataFormat.Default)]
		public ExcellentAttr newEnchantAttr
		{
			get
			{
				return this._newEnchantAttr;
			}
			set
			{
				this._newEnchantAttr = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
