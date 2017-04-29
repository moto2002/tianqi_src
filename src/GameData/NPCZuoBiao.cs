using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "NPCZuoBiao")]
	[Serializable]
	public class NPCZuoBiao : IExtensible
	{
		private int _id;

		private readonly List<int> _coordinate = new List<int>();

		private int _model;

		private int _scene;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, Name = "coordinate", DataFormat = DataFormat.TwosComplement)]
		public List<int> coordinate
		{
			get
			{
				return this._coordinate;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				this._scene = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
