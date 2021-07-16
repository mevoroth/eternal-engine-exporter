using UnityEngine;

namespace Eternal
{
	[System.Serializable]
	public class Transform
	{
		public Transform(UnityEngine.Transform InTransform)
		{
			m_Position = InTransform.position;
			m_Rotation = InTransform.rotation;
			m_Scale = InTransform.lossyScale;
		}

		public Vector3 m_Position;
		public Quaternion m_Rotation;
		public Vector3 m_Scale;
	}
}
