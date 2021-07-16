using System.Collections.Generic;

namespace Eternal
{
	public class EternalType
	{
		public List<Transform> m_Transforms = new List<Transform>();
	}

	[System.Serializable]
	public class Scene
	{
		public Scene()
		{

		}

		public List<EternalCameraType> Cameras = new List<EternalCameraType>();
		public List<EternalMeshType> Meshes = new List<EternalMeshType>();
		public List<EternalLightType> Lights = new List<EternalLightType>();
	}
}
