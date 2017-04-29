using System;

public class SceneManagerEvent
{
	public static string ClearSceneDependentLogic = "ClearSceneDependentLogic";

	public static string UnloadScene = "SceneManagerEvent.UnloadScene";

	public static string LoadSceneStart = "SceneManagerEvent.LoadSceneStart";

	public static string LoadSceneEnd = "SceneManagerEvent.LoadSceneEnd";

	public static string SceneRelatedUnload = "SceneManagerEvent.SceneRelatedUnload";

	public static string SceneRelatedLoad = "SceneManagerEvent.SceneRelatedLoad";

	public static string PreLoadResourceEnd = "SceneManagerEvent.PreLoadResourceEnd";

	public static string SetEnterCameraEnd = "SceneManagerEvent.SetEnterCameraEnd";

	public static string SetEnterMapObjEnd = "SceneManagerEvent.SetEnterMapObjEnd";

	public static string CloseLoadingUIEnd = "SceneManagerEvent.CloseLoadingUIEnd";
}
