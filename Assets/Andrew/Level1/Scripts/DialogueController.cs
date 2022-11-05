using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue Text")]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CanvasGroup dialogueWindow;
    [Space]
    [SerializeField][TextAreaAttribute] string dialogueText;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float newCameraSize;
    [SerializeField] float initialCameraSize;

    [Space]
    [SerializeField] UnityEvent endEvents;

    
    private GameObject _player;

    private bool _canCancel = false;
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && _canCancel)
        {
            EndCutScene();
            _canCancel = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCutScene();
        }
    }

    public void StartCutSceneInTime(float time)
    {
        Invoke("StartCutScene", time);
    }

    private void StartCutScene()
    {
        dialogueWindow.alpha = 1.0f;
        text.text = dialogueText;
        StartCoroutine("ChangeCameraSize");
        Invoke("EnableCancel", 3);
    }

    private void EndCutScene()
    {
        dialogueWindow.alpha = 0f;

        StartCoroutine("ChangeCameraSize");
        endEvents.Invoke();
        Destroy(gameObject, 2);
    }

    IEnumerator ChangeCameraSize()
    {
        Movement movement = _player.GetComponent<Movement>();
        movement.canMove = !movement.canMove;

        float delta = (initialCameraSize - newCameraSize) / 60.0f;

        if (virtualCamera.m_Lens.OrthographicSize == newCameraSize)
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize += delta;
                yield return new WaitForSeconds(1 / 60.0f);
            }
            virtualCamera.m_Lens.OrthographicSize = initialCameraSize;
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

    private void EnableCancel()
    {
        _canCancel = true;
    }
}
