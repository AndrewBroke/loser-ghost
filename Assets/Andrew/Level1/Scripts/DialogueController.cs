using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

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

    private bool _inTrigger = false;

    private bool _canCancel = false;
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && _inTrigger)
        {
            EndCutScene();
            _canCancel = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inTrigger = true;
            StartCutScene();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inTrigger = false;
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
        StartCoroutine("MakeCameraCloser");
        Invoke("EnableCancel", 3);
    }

    private void EndCutScene()
    {
        dialogueWindow.alpha = 0f;
        StopCoroutine("MakeCameraCloser");
        StartCoroutine("MakeCameraAway");
        endEvents.Invoke();
        Collider2D collider;
        if(TryGetComponent<Collider2D>(out collider))
        {
            collider.enabled = false;
        }
        
        Destroy(gameObject, 1.5f);
    }

    IEnumerator MakeCameraAway()
    {
        Movement movement = _player.GetComponent<Movement>();
        movement.canMove = true;

        PostProcessVolume ppVolume = Camera.main.GetComponent<PostProcessVolume>();

        float delta = (initialCameraSize - newCameraSize) / 60.0f;
        for (int i = 0; i < 60; i++)
        {
            virtualCamera.m_Lens.OrthographicSize += delta;
            ppVolume.weight -= 1 / 60.0f;
            if (virtualCamera.m_Lens.OrthographicSize >= initialCameraSize) break;
            yield return new WaitForSeconds(1 / 60.0f);
        }

        ppVolume.weight = 0;
        virtualCamera.m_Lens.OrthographicSize = initialCameraSize;
    }

    IEnumerator MakeCameraCloser()
    {
        Movement movement = _player.GetComponent<Movement>();
        movement.canMove = false;

        PostProcessVolume ppVolume = Camera.main.GetComponent<PostProcessVolume>();
        float delta = (initialCameraSize - newCameraSize) / 60.0f;

        for (int i = 0; i < 60; i++)
        {
            virtualCamera.m_Lens.OrthographicSize -= delta;
            ppVolume.weight += 1 / 60.0f;
            if (virtualCamera.m_Lens.OrthographicSize <= newCameraSize) break;
            yield return new WaitForSeconds(1 / 60.0f);
        }
        ppVolume.weight = 1;
        virtualCamera.m_Lens.OrthographicSize = newCameraSize;
        movement.canMove = !movement.canMove;
    }

    IEnumerator ChangeCameraSize()
    {
        Movement movement = _player.GetComponent<Movement>();
        movement.canMove = !movement.canMove;

        PostProcessVolume ppVolume = Camera.main.GetComponent<PostProcessVolume>();

        float delta = (initialCameraSize - newCameraSize) / 60.0f;

        if (virtualCamera.m_Lens.OrthographicSize == newCameraSize)
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize += delta;
                ppVolume.weight -= 1 / 60.0f;
                yield return new WaitForSeconds(1 / 60.0f);
            }
            
            ppVolume.weight = 0;
            virtualCamera.m_Lens.OrthographicSize = initialCameraSize;
        }
        else
        {
            for (int i = 0; i < 60; i++)
            {
                virtualCamera.m_Lens.OrthographicSize -= delta;
                ppVolume.weight += 1 / 60.0f;
                yield return new WaitForSeconds(1 / 60.0f);
            }
            ppVolume.weight = 1;
            virtualCamera.m_Lens.OrthographicSize = newCameraSize;
        }
    }

    private void EnableCancel()
    {
        _canCancel = true;
    }
}
