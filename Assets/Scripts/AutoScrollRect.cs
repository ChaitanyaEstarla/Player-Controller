using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScrollRect : MonoBehaviour
{
    private ScrollRect m_ScrollRect;
    private float m_ScrollValue;

    public float rateOfIncrease;
    
    private void Start()
    {
        m_ScrollRect = GetComponent<ScrollRect>();
        
        m_ScrollValue = m_ScrollRect.verticalNormalizedPosition;

        StartCoroutine(ScrollToLast());
    }

    private IEnumerator ApplyScrollPosition()
    {
        while (m_ScrollValue > 0)
        {
            m_ScrollValue -= rateOfIncrease * Time.deltaTime;
            Debug.Log(m_ScrollValue);
            m_ScrollRect.verticalNormalizedPosition = m_ScrollValue;
            yield return new WaitForFixedUpdate();
        }
    }
    
    private IEnumerator ScrollToLast ()
    {
        m_ScrollRect.gameObject.SetActive(false);
        m_ScrollRect.gameObject.SetActive(true);
        float i = 0;
        var rate = 1f / 0.5f;
        var normalValue = m_ScrollRect.verticalNormalizedPosition;
        while (i < 1f) {
            i += Time.deltaTime * rate;
            m_ScrollRect.verticalNormalizedPosition = Mathf.Lerp (normalValue, 0, i);
            yield return 0;
        }
    }
}
