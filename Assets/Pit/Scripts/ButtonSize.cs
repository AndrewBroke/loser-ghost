using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Button btn;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Target", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Target", false);
    }

}
