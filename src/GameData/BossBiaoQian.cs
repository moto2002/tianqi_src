using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BossBiaoQian")]
	[Serializable]
	public class BossBiaoQian : IExtensible
	{
		private int _key;

		private int _page;

		private int _nameId;

		private int _modelId;

		private readonly List<float> _modelOffset = new List<float>();

		private float _modelAngle;

		private int _scene;

		private int _icon;

		private int _step;

		private int _vipLevel;

		private readonly List<int> _dropItem = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
		public int key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "page", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int page
		{
			get
			{
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "nameId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameId
		{
			get
			{
				return this._nameId;
			}
			set
			{
				this._nameId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "modelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(6, Name = "modelOffset", DataFormat = DataFormat.FixedSize)]
		public List<float> modelOffset
		{
			get
			{
				return this._modelOffset;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "modelAngle", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float modelAngle
		{
			get
			{
				return this._modelAngle;
			}
			set
			{
				this._modelAngle = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "vipLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLevel
		{
			get
			{
				return this._vipLevel;
			}
			set
			{
				this._vipLevel = value;
			}
		}

		[ProtoMember(12, Name = "dropItem", DataFormat = DataFormat.TwosComplement)]
		public List<int> dropItem
		{
			get
			{
				return this._dropItem;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
