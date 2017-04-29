using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameData
{
	[AttributeUsage]
	internal class CfgIndicesAttribute : Attribute
	{
		private List<string> _fieldNames;

		public List<string> FieldNames
		{
			get
			{
				return this._fieldNames;
			}
		}

		public CfgIndicesAttribute(params string[] fieldNames)
		{
			this._fieldNames = new List<string>(fieldNames);
		}

		public bool HasField(MemberInfo info)
		{
			return info.get_MemberType() == 16 && this.HasField(info.get_Name());
		}

		public bool HasField(string fieldName)
		{
			return this._fieldNames.Contains(fieldName);
		}
	}
}
