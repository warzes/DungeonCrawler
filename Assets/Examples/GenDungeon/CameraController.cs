using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private int cameraSpeed = 10;
	[SerializeField] private int minSize = 5;
	[SerializeField] private int maxSize = 40;

	private Camera camera;

	void Start()
	{
		camera = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		camera.orthographicSize -= camera.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
		camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minSize, maxSize);

		if (Input.GetKey(KeyCode.A))
			transform.position += cameraSpeed * Time.deltaTime * Vector3.left;
		if (Input.GetKey(KeyCode.D))
			transform.position += cameraSpeed * Time.deltaTime * Vector3.right;
		if (Input.GetKey(KeyCode.W))
			transform.position += cameraSpeed * Time.deltaTime * Vector3.up;
		if (Input.GetKey(KeyCode.S))
			transform.position += cameraSpeed * Time.deltaTime * Vector3.down;
	}
}
