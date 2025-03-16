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
    public GameObject UIPanel
    {
        get
        {
            if(mUIPanel==null)
            {
                CreateUIpanel();
            }
            return mUIPanel;
        }
    }

    public UIBase()
    {
        
    }

    protected virtual void CreateUIpanel(){}
    public virtual void Init() { }

    public virtual void Update(float deltaTime) { }

    public virtual void LateUpdate(float deltaTime) { }
}
