using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController m_Instance;

    void Awake()
    {
        if(SceneController.m_Instance==null) 
        {
            m_Instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        
    }

    public void LoadScene(string SceneName) 
    {
        SceneManager.LoadScene(SceneName);
    }
}
