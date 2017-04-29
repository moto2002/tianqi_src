using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ExploreLog")]
	[Serializable]
	public class ExploreLog : IExtensible
	{
		private int _logType;

		private string _content;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "logType", DataFormat = DataFormat.TwosComplement)]
		public int logType
		{
			get
			{
				return this._logType;
			}
			set
			{
				this._logType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "content", DataFormat = DataFormat.Default)]
		public string content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
