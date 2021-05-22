using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private CameraData cameraData;

    private void Start()
    {
        SetCamRotation();
        SetCamPosition(player.position);
    }

    private void Update()
    {
        FollowByPosition();
    }

    private void FollowByPosition()
    {
        float posY = transform.position.y;
        if (cameraData.CanFollow && posY > 7f)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + cameraData.CamOffset, cameraData.CameraFollowSpeed * Time.deltaTime);
        }
    }

    public void SetCamPosition(Vector3 position)
    {
        transform.position = position + cameraData.CamOffset;
    }

    private void SetCamRotation()
    {
        transform.eulerAngles = new Vector3(cameraData.CamAngle, 0f, 0f);
    }
}
