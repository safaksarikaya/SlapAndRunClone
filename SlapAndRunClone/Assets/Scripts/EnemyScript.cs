using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  [SerializeField] Animator animator;
  public bool fall;
  [SerializeField] ParticleSystem hitParticle;
  bool run;
  private void Update()
  {
    if (run)
    {
      transform.LookAt(CharacterScript.Instance.transform);
    }
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Obstacle"))
    {
      transform.parent = null;
      Fall();
      Invoke("Run", 2.1f);
    }
  }
  public void Fall()
  {
    hitParticle.Play();
    animator.SetTrigger("fallDown");
  }
  void Run()
  {
    transform.parent = CharacterScript.Instance.transform;
    run = true;
    animator.SetTrigger("run");
  }
}