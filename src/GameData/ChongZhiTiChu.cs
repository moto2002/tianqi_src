using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "ChongZhiTiChu")]
	[Serializable]
	public class ChongZhiTiChu : IExtensible
	{
		private string _name;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
