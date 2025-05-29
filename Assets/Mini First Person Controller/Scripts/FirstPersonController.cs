using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour, ITakeDamage
{
    [Header("Animation")]
    public AnimationClip m_ShootAnim;
    public AnimationClip m_ReloadAnim;
    Animation m_Animation;

    [Header("UI")]
    public Slider m_HealthBar;
    public TextMeshProUGUI m_BulletCounterUI;
    public PauseMenuController m_PauseMenu;

    [Header("Stats")]
    public float speed;
    public float runSpeed = 9.0f;
    public float m_MaxHealth;
    public float m_FireRate;
    float m_FireRateTimer;
    public int m_MaxBullets;
    int m_Bullets;
    float m_CurrentHealth;
    public float m_GunDamage;
    public LayerMask m_CameraLayerMask;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        m_Animation=GetComponent<Animation>();
        m_HealthBar.maxValue=m_MaxHealth;
        m_HealthBar.value=m_MaxHealth;
        m_CurrentHealth=m_MaxHealth;
        Reload();
    }

    private void Update()
    {
        if(Time.timeScale>0.0f) 
        {
            Vector3 l_CenterPosition=new Vector3(Screen.width/2.0f, Screen.height/2.0f, 10.0f);
            Vector3 l_ShootDirection=Camera.main.ScreenToWorldPoint(l_CenterPosition)-Camera.main.transform.position;
            if(Input.GetMouseButtonDown(0) && !m_Animation.isPlaying && m_FireRateTimer<=0.0f && m_Bullets>0) 
            {
                RaycastHit l_CameraRaycastHit;
                if(Physics.Raycast(Camera.main.transform.position, l_ShootDirection, out l_CameraRaycastHit, 1000.0f, m_CameraLayerMask))
                {
                    if(l_CameraRaycastHit.transform.TryGetComponent(out ITakeDamage Enemy))
                        Enemy.TakeDamage(m_GunDamage);
                }
                m_Animation.Play(m_ShootAnim.name);
                m_FireRateTimer=m_FireRate;
                m_Bullets--;
                if(m_Bullets<=0) 
                    m_Animation.PlayQueued(m_ReloadAnim.name);
            }
            m_BulletCounterUI.text=m_Bullets+"/"+m_MaxBullets;
            Debug.DrawRay(Camera.main.transform.position, l_ShootDirection, Color.green);
            m_FireRateTimer-=Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.P)) 
            {
                m_PauseMenu.SetNormalPause();
                m_PauseMenu.OpenMenu();
            }
            if(Input.GetKeyDown(KeyCode.X))
                TakeDamage(10.0f);
        }
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    public void TakeDamage(float Damage)
    {
        m_CurrentHealth-=Damage;
        m_HealthBar.value=m_CurrentHealth;
        if(m_CurrentHealth<=0.0f) 
        {
            m_PauseMenu.SetGameOver();
            m_PauseMenu.OpenMenu();
        }
    }

    public void Reload() 
    {
        m_Bullets=m_MaxBullets;
    }
}