using UnityEngine;
public class SlapTriggerScript : MonoBehaviour
{
  [SerializeField] private bool isLeft;
  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<EnemyScript>() && !other.GetComponent<EnemyScript>().fall)
    {
      var enemyScript = other.GetComponent<EnemyScript>();
      CharacterScript.Instance.Slap(isLeft);
      enemyScript.fall = true;
      enemyScript.Fall();
      Destroy(Instantiate(CharacterScript.Instance.plus1TextObj, new Vector3(enemyScript.transform.position.x, 2.5f, enemyScript.transform.position.z), Quaternion.identity), .5f);
      UIScript.Instance.AddCoin();
      CharacterScript.Instance.IncreaseDamageValue();
    }
  }
}