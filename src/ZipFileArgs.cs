using System;
using System.Collections.Generic;

public class ZipFileArgs
{
	public string zipFile;

	public string TarDir = string.Empty;

	public bool OverWrite;

	public Action unZipFinish;

	public List<string> FilterDir;
}
