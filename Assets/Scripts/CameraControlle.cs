using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float verticalOffset = 2f;
    [SerializeField] private bool followX = true;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 camPos = transform.position;

        if (followX)
        {
            camPos.x = Mathf.Lerp(camPos.x, target.position.x, followSpeed * Time.deltaTime);
        }

        float desiredY = target.position.y + verticalOffset;
        if (desiredY < camPos.y)
        {
            camPos.y = Mathf.Lerp(camPos.y, desiredY, followSpeed * Time.deltaTime);
        }

        camPos.z = -10f;
        transform.position = camPos;
    }
}
