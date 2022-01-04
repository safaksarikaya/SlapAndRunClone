using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishItemScript : MonoBehaviour
{
  public int value;
  [SerializeField] TextMesh valueText;
  private void Start()
  {
    valueText.text = (value * 10).ToString();
  }
}
