using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ZhuoYueShuXingKu")]
	[Serializable]
	public class ZhuoYueShuXingKu : IExtensible
	{
		private int _libraryId;

		private readonly List<int> _level = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "libraryId", DataFormat = DataFormat.TwosComplement)]
		public int libraryId
		{
			get
			{
				return this._libraryId;
			}
			set
			{
				this._libraryId = value;
			}
		}

		[ProtoMember(2, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public List<int> level
		{
			get
			{
				return this._level;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
