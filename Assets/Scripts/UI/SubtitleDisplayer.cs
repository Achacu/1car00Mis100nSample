using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleDisplayer : Singleton<SubtitleDisplayer>
{
    public enum SubtitleList
    {
        Segundos100,
        PurgarPurticulas,
        CuerpoDetectado,
        CatAlizadorApagado,
        CambiandoRumbo,
        MiaulfuncionaPulsores,
        SobrecargaRefrigeracion,
        RefrigeracionEstable,
        JugarFuera,
        Papa,
        Blobbyfu,
        Abaco,
        Fotos,
        Xilofono,
        Cohete,
        DefensaOmnidireccional,
        TanquesOxigeno,
        MatrizGravitacional,
        ConexionPoliestacional,
        Encapsulamiauntico
    }
    
    [SerializeField, ContextMenuItem("TestLineGroup", "TestLineGroup")] private int testIndex = 0;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueGroup[] dialogueGroups;
    private void TestLineGroup() => ShowLineGroup(dialogueGroups[testIndex].group);

    [System.Serializable]
    public class DialogueGroup
    {
        public SubtitleList listIndex;
        public LineGroupSO group;

        public DialogueGroup(SubtitleList list, LineGroupSO group)
        {
            this.listIndex = list;
            this.group = group;
        }
    }


    private Coroutine lineGroupCorot = null;
    private void ShowLineGroup(LineGroupSO lineGroup)
    {
        if (lineGroupCorot != null)
        {
            StopCoroutine(lineGroupCorot);
            lineGroupCorot = null;
        }
        lineGroupCorot = StartCoroutine(ReadLineGroup(lineGroup));
    }

    private IEnumerator ReadLineGroup(LineGroupSO lineGroup)
    {
        dialogueText.enabled = true;
        dialogueText.font = lineGroup.font;
        for (int i = 0; i < lineGroup.lines.Length; i++)
        {
            dialogueText.text = lineGroup.lines[i].line;
            yield return new WaitForSeconds(lineGroup.lines[i].duration);
        }
        dialogueText.enabled = false;
    }

    public void SetLineGroup(SubtitleList listIndex)
    {
        for (int i = 0; i < dialogueGroups.Length; i++)
        {
            if (dialogueGroups[i].listIndex == listIndex)
            {
                ShowLineGroup(dialogueGroups[i].group);
            }
        }
    }
}
