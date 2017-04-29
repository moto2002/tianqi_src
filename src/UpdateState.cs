using System;

public enum UpdateState
{
	Init,
	CheckVersion,
	CleanPatch,
	GetUpdateInfo,
	ShouldDownload,
	DownloadPatch,
	ValidatePatch,
	AskForDownloadAgain,
	InstallPatch,
	Restart,
	End
}
