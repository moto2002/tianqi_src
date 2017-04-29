using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ArtifactSkill_ARRAY")]
	[Serializable]
	public class ArtifactSkill_ARRAY : IExtensible
	{
		private readonly List<ArtifactSkill> _items = new List<ArtifactSkill>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<ArtifactSkill> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
