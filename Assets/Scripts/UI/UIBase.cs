using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UI_LAYER
{
    TOP,
    MID,
    DOWN,
}

public class UIBase
{
    protected CanvasGroup mCanvasGroup;
    public CanvasGroup canvasGroup=>mCanvasGroup;

    protected UI_LAYER mLayer;
    public UI_LAYER layer;

    protected GameObject mUIPanel;
    public GameObject UIPanel=>mUIPanel;

    public UIBase(GameObject gameObject)
    {
        mUIPanel=gameObject;
    }
    public virtual void Init() { }

    public virtual void Update(float deltaTime) { }

    public virtual void LateUpdate(float deltaTime) { }
}
