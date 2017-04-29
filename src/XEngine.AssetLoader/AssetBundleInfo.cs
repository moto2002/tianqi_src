using System;

namespace XEngine.AssetLoader
{
	[Serializable]
	public class AssetBundleInfo
	{
		public string filename;

		public string hash;

		public int package;

		public int offset;

		public int length;
	}
}
