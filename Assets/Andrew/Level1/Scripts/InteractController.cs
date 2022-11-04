using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class InteractController : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private GameObject miniGame;

    [Header("Events")]
    [SerializeField] private UnityEvent enterEvents;
    [SerializeField] private UnityEvent exitEvents;
    
    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform target;
    [SerializeField] float newCameraSize;

    private bool _inCollider = false;
    private float _initialCameraSize;
    private GameObject _player;
    private bool _isMiniGameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        _initialCameraSize = virtualCamera.m_Lens.OrthographicSize;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey) && _inCollider && !_isMiniGameStarted)
        {
            StartMiniGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inCollider = true;
            enterEvents.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inCollider = false;
            exitEvents.Invoke();
        }
    }

    private void StartMiniGame()
    {
        _isMiniGameStarted = true;
        miniGame.SetActive(true);
        NewFollowTarget();
    }

    public void NewFollowTarget()
    {
        if(virtualCamera.m_Follow != target)
        {
            virtualCamera.m_Follow = target;
        }
        else
        {
            virtualCamera.m_Follow = _player.transform;
        }
        
        StartCoroutine("ChangeCameraSize");
    }

    IEnumerator ChangeCameraSize()
    {
        float delta = (_initialCameraSize - newCameraSize) / 60.0f;

        if (virtualCamera.m_Lens.OrthographicSize == newCameraSize)
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize += delta;
                yield return new WaitForSeconds(1/60.0f);
            }
            virtualCamera.m_Lens.OrthographicSize = _initialCameraSize;
        }
        else
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize -= delta;
                yield return new WaitForSeconds(1 / 60.0f);
            }
            virtualCamera.m_Lens.OrthographicSize = newCameraSize;
        }
    }
}
