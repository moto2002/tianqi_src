using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YeWaiBOSSMoXing")]
	[Serializable]
	public class YeWaiBOSSMoXing : IExtensible
	{
		[ProtoContract(Name = "PointbossPair")]
		[Serializable]
		public class PointbossPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
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

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _key;

		private int _scene;

		private readonly List<int> _set = new List<int>();

		private readonly List<int> _point = new List<int>();

		private readonly List<YeWaiBOSSMoXing.PointbossPair> _pointboss = new List<YeWaiBOSSMoXing.PointbossPair>();

		private int _limit;

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

		[ProtoMember(3, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, Name = "set", DataFormat = DataFormat.TwosComplement)]
		public List<int> set
		{
			get
			{
				return this._set;
			}
		}

		[ProtoMember(5, Name = "point", DataFormat = DataFormat.TwosComplement)]
		public List<int> point
		{
			get
			{
				return this._point;
			}
		}

		[ProtoMember(6, Name = "pointboss", DataFormat = DataFormat.Default)]
		public List<YeWaiBOSSMoXing.PointbossPair> pointboss
		{
			get
			{
				return this._pointboss;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "limit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int limit
		{
			get
			{
				return this._limit;
			}
			set
			{
				this._limit = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
