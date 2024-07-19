using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _questTitle;
    [SerializeField] private Image image;

    [SerializeField] private Color successColor;
    [SerializeField] private Color defaultColor;

    [SerializeField] private Outline _highlight;

    private Action<QuestItemUI> OnSelectAction;
    public string questId { get; private set; }
    public void Initialize(QuestInfoSO questInfo, Action<QuestItemUI> selectAction, bool isCompleted=false)
    {
        questId = questInfo.id;
        OnSelectAction = selectAction;
        Render(questInfo.displayName, isCompleted);
        UnHighlight();
    }
    private void Render(string text, bool isCompleted)
    {
        _questTitle.text = text;
        image.color = isCompleted? successColor: defaultColor;
    }

    public void Highlight() => _highlight.enabled = true;
    public void UnHighlight() => _highlight.enabled = false;

    public void OnPointerClick(PointerEventData eventData)
    {
		OnSelectAction?.Invoke(this);
    }
}