using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleSender : MonoBehaviour
{
    [SerializeField] private EventSender sender;
    [SerializeField] private SubtitleDisplayer.SubtitleList subtitle;
    // Start is called before the first frame update
    public void OnEnable()
    {
        if(sender) sender.OnActivate += SendSubtitle;
    }
    public void OnDisable()
    {
        if (sender) sender.OnActivate -= SendSubtitle;
    }

    private void SendSubtitle(EventSender obj)
    {
        SubtitleDisplayer.Instance.SetLineGroup(subtitle);
    }

}
