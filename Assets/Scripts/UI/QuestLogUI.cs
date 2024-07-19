using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;

    private void QuestLogTogglePressed()
    {
        if (contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        CursorManager.instance.ChangeCursorMode(CursorManager.CursorState.Menu);
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        CursorManager.instance.ChangeCursorMode(CursorManager.CursorState.FPS);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            QuestLogTogglePressed();
        }
    }
    public void SetQuestLogInfo(Quest quest)
    {
        // quest name
        questDisplayNameText.text = quest.info.displayName;

        // status
        //questStatusText.text = quest.GetFullStatusText();
    }

    public void SetEmpty()
    {
        questDisplayNameText.text = string.Empty;
        questStatusText.text = string.Empty;
    }
}
