using System.Collections.Generic;
using UnityEngine;
public class CharacterScript : MonoBehaviour
{
  public static CharacterScript Instance;
  [SerializeField] private Animator animator;
  [SerializeField] private float speed = 3f;
  [SerializeField] private ParticleSystem runParticle;
  public List<EnemyScript> followerEnemyScriptList;
  bool _tap2start;
  bool _leftArmSlap, _rightArmSlap;
  Vector3 mouseDownPosition, mousePosition, mouseDiff;
  public GameObject plus1TextObj;
  bool success, gameOver;
  Vector3 startPosition, endPosition;
  float timer;
  bool finishKick;
  int damageValue = 1;
  float deafultSpeed = 3f;
  private void Awake()
  {
    Instance = this;
  }
  private void Start()
  {
    animator.SetFloat("animSpeed", 1 * (speed / deafultSpeed));
  }
  private void Update()
  {
    if (finishKick)
    {
      timer += Time.deltaTime;
      transform.position = Vector3.Lerp(startPosition, endPosition, timer);
      if (timer > 1f)
      {
        finishKick = false;
      }
    }
    if (success || gameOver)
      return;
    if (!_tap2start)
    {
      if (Input.GetMouseButtonDown(0))
      {
        StartGame();
      }
      return;
    }
    transform.Translate(transform.forward * Time.deltaTime * speed);
    if (Input.GetMouseButtonDown(0))
    {
      mouseDownPosition = Input.mousePosition;
    }
    if (Input.GetMouseButton(0))
    {
      mousePosition = Input.mousePosition;
      mouseDiff = mousePosition - mouseDownPosition;
      var posX = Mathf.Clamp(mouseDiff.x, -2, 2);
      transform.position += new Vector3(posX * 0.01f, 0, 0);
      if (transform.position.x > 2f)
      {
        transform.position = new Vector3(2f, transform.position.y, transform.position.z);
      }
      if (transform.position.x < -2f)
      {
        transform.position = new Vector3(-2f, transform.position.y, transform.position.z);
      }
    }
    if (Input.GetMouseButtonUp(0))
    {
      mouseDiff = Vector3.zero;
    }
  }
  FinishScript finishScript;
  void StartGame()
  {
    _tap2start = true;
    animator.SetTrigger("run");
    UIScript.Instance.Tap2StartSetActive(false);
    runParticle.Play();
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<FinishScript>())
    {
      finishScript = other.GetComponent<FinishScript>();
      Success();
    }
    if (other.CompareTag("Obstacle"))
    {
      GameOver();
    }
    if (finishKick && other.GetComponent<EnemyScript>())
    {
      finishScript.FallBossCharacter(damageValue);
      timer = 0;
      finishKick = false;
    }
  }
  void GameOver()
  {
    runParticle.Stop();
    gameOver = true;
    UIScript.Instance.GameOver();
  }
  void Success()
  {
    runParticle.Stop();
    foreach (var item in followerEnemyScriptList)
    {
      item.Fall();
      item.transform.parent = null;
    }
    success = true;
    Camera.main.GetComponent<CameraScript>().ChangeTarget(finishScript.bossTransform);
    startPosition = transform.position;
    endPosition = finishScript.bossTransform.position;
    timer = 0;
    animator.SetTrigger("kick");
    finishKick = true;
    UIScript.Instance.GameSuccess();
  }
  public void Slap(bool left = true)
  {
    if (left)
    {
      animator.SetTrigger("leftSlap");
    }
    else
    {
      animator.SetTrigger("rightSlap");
    }
  }
  public void IncreaseDamageValue()
  {
    damageValue++;
  }
}