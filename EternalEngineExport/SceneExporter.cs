using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneExporter : MonoBehaviour
{
	public string m_ExportPath;
	public bool m_JsonDebug;

	public void Export()
	{
		Eternal.Scene EternalScene = new Eternal.Scene();
		CameraExporter.Export(gameObject, EternalScene);
		MeshExporter.Export(gameObject, EternalScene);
		LightExporter.Export(gameObject, EternalScene);

		if (m_ExportPath.Length == 0)
			return;

		if (!Directory.Exists(m_ExportPath))
			Directory.CreateDirectory(m_ExportPath);

		{
			StreamWriter OutputLevelFile = new StreamWriter(m_ExportPath + "/" + SceneManager.GetActiveScene().name + ".json");
			OutputLevelFile.Write(JsonUtility.ToJson(EternalScene, m_JsonDebug));
			OutputLevelFile.Close();
		}
	}
}
