using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public float rateOfIncrease;
    
    private float m_ScrollValue;
    private Scrollbar m_Scrollbar;

    private void OnEnable()
    {
        m_Scrollbar = GetComponent<Scrollbar>();
        m_ScrollValue = m_Scrollbar.value;
    }

    private void Start()
    {
        StartCoroutine(AutoScrollToLatest());
    }

    private IEnumerator AutoScrollToLatest()
    {
        while (m_ScrollValue < 1)
        {
            m_ScrollValue += rateOfIncrease * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            m_Scrollbar.value = m_ScrollValue;
        }
        
    }
}
