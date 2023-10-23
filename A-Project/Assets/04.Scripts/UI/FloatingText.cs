using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Text))]
public class FloatingText : MonoBehaviour
{
    [SerializeField] private string m_text;
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private float m_destroyTime = 2f;
    [SerializeField] private Transform m_target;
    private Vector3 m_moveDir;
    private Vector3 m_startTargetPos;
    private Vector3 m_updateTargetPos;

    private float m_time;
    private Text theText;

    // Start is called before the first frame update
    void Start()
    {
        //m_time = 0f;
        //m_startTargetPos = Camera.main.WorldToScreenPoint(m_target.position);
        //m_moveDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1).normalized;
    }

    void Update()
    {
        //m_time += Time.deltaTime;
        //m_updateTargetPos = Camera.main.WorldToScreenPoint(m_target.position);
        //transform.position = m_updateTargetPos + m_moveDir * m_speed * m_time;
        //if (m_time < m_destroyTime)
        //    theText.color = new Color(theText.color.r, theText.color.g, theText.color.b, 1f - m_time / m_destroyTime);
        //else
        //    Destroy(gameObject);

        transform.position = Input.mousePosition;
    }

    public void ResetText(string p_text, Transform p_target, bool p_isCritical, bool p_isHeal = false)
    {
        theText = GetComponent<Text>();
        m_text = p_text;
        m_target = p_target;
        theText.text = m_text;
        if (p_isCritical)
        {
            m_text = p_text + "!";
            theText.fontSize += 6;
            theText.color = new Color(1, 0, 0, theText.color.a);
        }
        if (p_isHeal)
        {
            theText.color = new Color(0, 1, 0, theText.color.a);
        }
    }
}