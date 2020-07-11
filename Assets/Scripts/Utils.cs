using UnityEngine;

namespace Utils
{
	public class Utils : MonoBehaviour
	{
		public static void SetRenderLayer(GameObject obj, int initialLayer)
		{
			int layerOffset = - (int) (obj.transform.position.y * 10); // Gives a value between -10, 10 ish

			obj.GetComponent<SpriteRenderer>().sortingOrder = initialLayer + layerOffset;
		}
	}
}