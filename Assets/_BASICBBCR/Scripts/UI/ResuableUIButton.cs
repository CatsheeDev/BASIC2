using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum ButtonType
{
    Text,
    Image
}

public class ResuableUIButton : MonoBehaviour
{
    [Header("Universal Settings")]
    [SerializeField] private UnityEvent OnClick;
    [SerializeField] private bool TransitionOnClick;

    [ShowIf("TransitionOnClick")]
    [SerializeField] private TransitionType transType;

    [SerializeField] private ButtonType type;

    [ShowIf("IsTextType")]
    [SerializeField] private bool underlineOnHover;
    private TextMeshProUGUI Text;
    private FontStyles originalStyle; 

    [ShowIf("IsImageType")]
    [SerializeField] private Sprite highlighedSprite;

    private bool IsTextType() => type == ButtonType.Text;
    private bool IsImageType() => type == ButtonType.Image;

    private void Start()
    {
        if (IsTextType())
        {
            if (!TryGetComponent(out Text))
            {
                Text = GetComponentInChildren<TextMeshProUGUI>();
            }

            originalStyle = Text.fontStyle; 
        }
    }

    public void OnPointerExit()
    {
        if (IsTextType())
        {
            Text.fontStyle = originalStyle; 
        }
    }

    public void OnPointerEnter()
    {
        Debug.Log("yessssssssssssssss");
        if (IsTextType())
        {
            Debug.Log("yes.");
            Text.fontStyle = FontStyles.Underline;
        }
    }
}
