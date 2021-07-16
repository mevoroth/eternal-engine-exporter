using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace Eternal
{
	[System.Serializable]
	public class EternalMeshType : EternalType
	{
		public EternalMeshType(string InPath, UnityEngine.Transform InMeshTransform)
		{
			m_Transforms.Add(new Transform(InMeshTransform));
			m_Path = InPath;
		}

		public string m_Path;
	}
}

public class MeshExporter
{
	public static void Export(GameObject UnityScene, Eternal.Scene Scene)
	{
		Transform[] Transforms = UnityScene.GetComponentsInChildren<Transform>();
		for (int TransformIndex = 0; TransformIndex < Transforms.Length; ++TransformIndex)
		{
			GameObject CurrentGameObject = Transforms[TransformIndex].gameObject;
			PrefabAssetType AssetType = PrefabUtility.GetPrefabAssetType(CurrentGameObject);
			if (PrefabUtility.IsOutermostPrefabInstanceRoot(CurrentGameObject))
			{
				string PrefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(CurrentGameObject);
				PrefabPath = Path.GetFileName(PrefabPath);

				bool bAdded = false;
				for (int ExistingMeshIndex = 0; ExistingMeshIndex < Scene.Meshes.Count; ++ExistingMeshIndex)
				{
					if (Scene.Meshes[ExistingMeshIndex].m_Path == PrefabPath)
					{
						Scene.Meshes[ExistingMeshIndex].m_Transforms.Add(
							new Eternal.Transform(Transforms[TransformIndex])
						);
						bAdded = true;
						break;
					}
				}

				if (!bAdded)
				{
					Eternal.EternalMeshType EngineMesh = new Eternal.EternalMeshType(PrefabPath, Transforms[TransformIndex]);
					Scene.Meshes.Add(EngineMesh);
				}
			}
		}
	}
}
