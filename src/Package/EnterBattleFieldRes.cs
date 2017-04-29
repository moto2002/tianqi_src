using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(795), ForSend(795), ProtoContract(Name = "EnterBattleFieldRes")]
	[Serializable]
	public class EnterBattleFieldRes : IExtensible
	{
		public static readonly short OP = 795;

		private string _fieldId;

		private long _echoData;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fieldId", DataFormat = DataFormat.Default)]
		public string fieldId
		{
			get
			{
				return this._fieldId;
			}
			set
			{
				this._fieldId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "echoData", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long echoData
		{
			get
			{
				return this._echoData;
			}
			set
			{
				this._echoData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
