using System;

namespace Unuse
{
	public enum UpdateState2
	{
		Init,
		CheckVersionCode,
		CleanApkAndPatch,
		GetPatchInfo,
		ValidateApk,
		CheckUpdateWay,
		InstallApk,
		DownloadApk,
		ValidatePatch,
		DownloadPatch,
		MergePatch,
		End
	}
}
