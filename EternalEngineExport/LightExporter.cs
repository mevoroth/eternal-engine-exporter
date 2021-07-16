using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEditor;
using System.IO;

namespace Eternal
{
	public enum LightType
	{
		LightType_Directional = 0,
		LightType_Point,
		LightType_Spot,
		LightType_Area
	}

	public enum LightMobility
	{
		LightMobility_Static = 0,
		LightMobility_Dynamic
	}

	[System.Serializable]
	public class EternalLightType : EternalType
	{
		static LightType[] s_LightTypes = new LightType[]
		{
			LightType.LightType_Spot,
			LightType.LightType_Directional,
			LightType.LightType_Point,
			LightType.LightType_Area
		};

		public EternalLightType(Light InLight)
		{
			HDAdditionalLightData AdditionalData = InLight.GetComponent<HDAdditionalLightData>();

			m_Transforms.Add(new Transform(InLight.transform));
			m_LightType = s_LightTypes[(int)InLight.type];
			m_LightMobility = InLight.lightmapBakeType == LightmapBakeType.Realtime ? LightMobility.LightMobility_Dynamic : LightMobility.LightMobility_Static;
			m_LightColor = InLight.color;
			m_LightIntensity = InLight.intensity;
			m_LightInnerRadius = AdditionalData.luxAtDistance;
			m_LightOuterRadius = InLight.range;
			m_LightInnerSpotAngle = InLight.spotAngle * AdditionalData.innerSpotPercent01;
			m_LightOuterSpotAngle = InLight.spotAngle;

			string PrefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(InLight.gameObject);
			PrefabPath = Path.GetFileName(PrefabPath);
			m_LightName = PrefabPath;
		}

		public LightType m_LightType;
		public LightMobility m_LightMobility;
		public Color m_LightColor;
		public float m_LightIntensity;
		public float m_LightInnerRadius;
		public float m_LightOuterRadius;
		public float m_LightInnerSpotAngle;
		public float m_LightOuterSpotAngle;
		public string m_LightName;
	}
}

public class LightExporter
{
	public static void Export(GameObject UnityScene, Eternal.Scene Scene)
	{
		Light[] Lights = UnityScene.GetComponentsInChildren<Light>();

		for (int LightIndex = 0; LightIndex < Lights.Length; ++LightIndex)
		{
			string PrefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(Lights[LightIndex].gameObject);
			PrefabPath = Path.GetFileName(PrefabPath);

			bool bAdded = false;
			for (int ExistingLightIndex = 0; ExistingLightIndex < Scene.Lights.Count; ++ExistingLightIndex)
			{
				if (Scene.Lights[ExistingLightIndex].m_LightName == PrefabPath)
				{
					Scene.Lights[ExistingLightIndex].m_Transforms.Add(
						new Eternal.Transform(Lights[LightIndex].transform)
					);
					bAdded = true;
					break;
				}
			}

			if (!bAdded)
			{
				Eternal.EternalLightType EngineLight = new Eternal.EternalLightType(Lights[LightIndex]);
				Scene.Lights.Add(EngineLight);
			}
		}
	}
}
