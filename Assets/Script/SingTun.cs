using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingTun<T> : MonoBehaviour where T : MonoBehaviour
{
    static protected T m_instance = null;
    static public T getInstance()
    {
        if (null == m_instance)
        {
            m_instance = GameObject.FindObjectOfType<T>();
            if (null == m_instance)
            {
                GameObject go = new GameObject(typeof(T).ToString());
                m_instance = go.AddComponent<T>();
            }
            m_bAlive = true;
        }
        return m_instance;
    }


    [SerializeField] protected bool m_DontDestroy = false;

    static private bool m_bAlive = false;

    static public bool isAlive
    {
        get { return m_bAlive; }
    }

    protected virtual void OnDestroy()
    {
        if (null != m_instance)
        {
            if (m_instance == this)
            {
                m_instance = null;
                m_bAlive = false;
            }
        }
    }

    protected virtual void Awake()
    {
        if (isAlive)
        {
            if (m_instance != this)
                Object.Destroy(gameObject);
            return;
        }

        if (null == m_instance)
        {
            m_instance = this as T;
            m_bAlive = true;
        }
        if (m_DontDestroy)
            Object.DontDestroyOnLoad(gameObject);
    }
}