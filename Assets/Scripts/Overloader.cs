using UnityEngine;

public class Overloader : MonoBehaviour
{
	[SerializeField] private int cunt;

	private void FixedUpdate()
	{
		for (var i = 0; i < cunt; i++)
			GetComponent<Transform>();
	}
}
