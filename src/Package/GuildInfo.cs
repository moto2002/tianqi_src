using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "GuildInfo")]
	[Serializable]
	public class GuildInfo : IExtensible
	{
		private string _guildName;

		private readonly List<int> _titles = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildName", DataFormat = DataFormat.Default)]
		public string guildName
		{
			get
			{
				return this._guildName;
			}
			set
			{
				this._guildName = value;
			}
		}

		[ProtoMember(2, Name = "titles", DataFormat = DataFormat.TwosComplement)]
		public List<int> titles
		{
			get
			{
				return this._titles;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
