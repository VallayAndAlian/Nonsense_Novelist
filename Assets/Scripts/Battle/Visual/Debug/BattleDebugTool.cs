using System;
using UnityEngine;

public class BattleDebugTool : MonoBehaviour
{
    private bool isWindowOpen = false;
    private Rect windowRect = new Rect (20, 20, 520, 650);
    private Vector2 dragOffset;
    
    protected bool mStartBattle = false;
    protected BattleBase mBattle = null;
    protected BattleRunner mRunner = null;

    private void Start()
    {
        mRunner = BattleRunner.Instance;
    }

    void OnGUI()
    {
        // 打开窗口的按钮
        if (GUI.Button(new Rect(10, 10, 200, 20), "Open DebugTool"))
        {
            isWindowOpen = true;
        }
 
        // 当窗口打开时显示窗口
        if (isWindowOpen)
        {
            DrawWindow();
        }
    }
 
    void DrawWindow()
    {
        windowRect = GUILayout.Window(0, windowRect, OnWindowFunction, "DebugTool");
    }
 
    void OnWindowFunction(int windowID)
    {
        // 关闭按钮
        if (GUILayout.Button("Close"))
        {
            isWindowOpen = false;
        }
        
        if (!mStartBattle)
        {
            if (GUILayout.Button("Start Battle"))
            {
                mRunner.StartBattle();
                
                mBattle = mRunner.Battle;
                mStartBattle = true;
            }
        }
        else
        {
            GUILayout.Label("Battle State {}", mBattle.State.ToString());
        }
        
        DragWindow(windowID);
    }

    void DragWindow(int windowID)
    {
        // 处理窗口拖动
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        
        // 处理窗口拖动事件
        Event e = Event.current;
        Vector2 mousePos = e.mousePosition;

        if (windowRect.Contains(mousePos))
        {
            switch (e.type)
            {
                case UnityEngine.EventType.MouseDown:
                    dragOffset = windowRect.position - mousePos;
                    e.Use();
                    break;
                case UnityEngine.EventType.MouseDrag:
                    windowRect.position = mousePos + dragOffset;
                    e.Use();
                    break;
            }
        }
    }
}