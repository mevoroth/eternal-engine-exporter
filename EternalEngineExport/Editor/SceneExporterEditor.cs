using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneExporter))]
public class SceneExporterEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SceneExporter SceneExporterScript = (SceneExporter)target;
		if (GUILayout.Button("Export"))
		{
			SceneExporterScript.Export();
		}
	}
}
