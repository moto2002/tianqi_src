using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "HeadInfo")]
	[Serializable]
	public class HeadInfo : IExtensible
	{
		private int _id;

		private readonly List<int> _MainScene = new List<int>();

		private readonly List<int> _InstanceType1 = new List<int>();

		private readonly List<int> _InstanceType2 = new List<int>();

		private readonly List<int> _InstanceType3 = new List<int>();

		private readonly List<int> _InstanceType4 = new List<int>();

		private readonly List<int> _InstanceType5 = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, Name = "MainScene", DataFormat = DataFormat.TwosComplement)]
		public List<int> MainScene
		{
			get
			{
				return this._MainScene;
			}
		}

		[ProtoMember(5, Name = "InstanceType1", DataFormat = DataFormat.TwosComplement)]
		public List<int> InstanceType1
		{
			get
			{
				return this._InstanceType1;
			}
		}

		[ProtoMember(6, Name = "InstanceType2", DataFormat = DataFormat.TwosComplement)]
		public List<int> InstanceType2
		{
			get
			{
				return this._InstanceType2;
			}
		}

		[ProtoMember(7, Name = "InstanceType3", DataFormat = DataFormat.TwosComplement)]
		public List<int> InstanceType3
		{
			get
			{
				return this._InstanceType3;
			}
		}

		[ProtoMember(8, Name = "InstanceType4", DataFormat = DataFormat.TwosComplement)]
		public List<int> InstanceType4
		{
			get
			{
				return this._InstanceType4;
			}
		}

		[ProtoMember(9, Name = "InstanceType5", DataFormat = DataFormat.TwosComplement)]
		public List<int> InstanceType5
		{
			get
			{
				return this._InstanceType5;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
