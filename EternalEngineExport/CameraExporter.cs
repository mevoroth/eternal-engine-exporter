using System.Collections.Generic;
using UnityEngine;

namespace Eternal
{
	public enum Projection
	{
		Projection_Orthographic = 0,
		Projection_Perspective
	}

	[System.Serializable]
	public class EternalCameraType : EternalType
	{
		public EternalCameraType(UnityEngine.Camera InCamera)
		{
			m_Transforms.Add(new Transform(InCamera.transform));
			m_Projection = InCamera.orthographic ? Projection.Projection_Orthographic : Projection.Projection_Perspective;
			m_FOV = InCamera.fieldOfView;
			m_Near = InCamera.nearClipPlane;
			m_Far = InCamera.farClipPlane;
		}

		public Projection m_Projection;
		public float m_FOV;
		public float m_Near;
		public float m_Far;
	}
}

public class CameraExporter
{
	public static void Export(GameObject UnityScene, Eternal.Scene Scene)
	{
		Camera[] Cameras = UnityScene.GetComponentsInChildren<Camera>();
		for (int CameraIndex = 0; CameraIndex < Cameras.Length; ++CameraIndex)
		{
			Eternal.EternalCameraType EngineCamera = new Eternal.EternalCameraType(Cameras[CameraIndex]);
			Scene.Cameras.Add(EngineCamera);
		}
	}
}
