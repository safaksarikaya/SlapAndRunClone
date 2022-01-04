using UnityEngine;
using System.Collections.Generic;
public class FinishScript : MonoBehaviour
{
  public Transform bossTransform;
  public Transform finishItems;
  [SerializeField] List<Color> finishItemColorList;
  Vector3 startPosition, endPosition, midPosition;
  float timer;
  bool finishKick;
  private void Start()
  {
    for (int i = 0; i < finishItems.childCount; i++)
    {
      finishItems.GetChild(i).GetComponent<Renderer>().material.color = finishItemColorList[i % finishItemColorList.Count];
    }
  }
  private void Update()
  {
    if (finishKick)
    {
      timer += Time.deltaTime;
      bossTransform.position = Vector3.Lerp(Vector3.Lerp(startPosition, midPosition, timer), Vector3.Lerp(midPosition, endPosition, timer), timer);
      if (timer >= 1f)
      {
        finishKick = false;
        timer = 0;
      }
    }
  }
  public void FallBossCharacter(int value)
  {
    startPosition = bossTransform.position;
    endPosition = GetEndPosition(value);
    midPosition = (startPosition + endPosition) / 2 + Vector3.up * 2f;
    timer = 0;
    bossTransform.GetComponent<EnemyScript>().Fall();
    finishKick = true;
  }
  Vector3 GetEndPosition(int value)
  {
    var end = Vector3.zero;
    for (int i = 0; i < finishItems.childCount; i++)
    {
      if (value == finishItems.GetChild(i).GetComponent<FinishItemScript>().value)
      {
        end = finishItems.GetChild(i).position;
        break;
      }
    }
    if (value > finishItems.childCount)
    {
      value = finishItems.childCount;
    }
    return end;
  }
}