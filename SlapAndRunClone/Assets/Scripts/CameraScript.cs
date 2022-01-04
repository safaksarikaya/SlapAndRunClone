using UnityEngine;
public class CameraScript : MonoBehaviour
{
  Transform targetTransform;
  Vector3 distance;
  [SerializeField] float minX, maxX;
  private void Start()
  {
    targetTransform = CharacterScript.Instance.transform;
    distance = transform.position - targetTransform.position;
  }
  private void Update()
  {
    var position = Vector3.Slerp(transform.position, targetTransform.position + distance, 3 * Time.deltaTime);
    transform.position = new Vector3(Mathf.Clamp(position.x, minX, maxX), position.y, position.z);
  }
  public void ChangeTarget(Transform target)
  {
    targetTransform = target;
  }
}