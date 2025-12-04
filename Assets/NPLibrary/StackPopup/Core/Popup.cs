//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//#region Editor
//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEditorInternal;

//[CustomEditor(typeof(Popup))]
//public class PopupEditor : Editor
//{
//    private Popup popup;
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        popup = (Popup)target;

//        if (GUILayout.Button("Get Popups"))
//        {
//            popup.GetFrames();
//        }
//    }
//}
//#endif
//#endregion

//public class Popup : StackUI {

//    private static Popup instance;

//    public static Popup Instance
//    {
//        get {
//            if (instance == null) {
//                instance = FindObjectOfType<Popup>();
//            }
//            return instance;
//        }
//    }

//    public List<MonoFrame> frames = new List<MonoFrame>();

//    public void GetFrames()
//    {
//        MonoFrame[] monoFrames = this.GetComponentsInChildren<MonoFrame>(true);
//        frames = new List<MonoFrame>();
//        frames.AddRange(monoFrames);
//    }

//    public void Show<T>(object data = null, bool dismissCurrent = false, bool pauseCurrent = false)
//    {
//        MonoFrame monoFrame = frames.Find(a => a is T);
//        this.Show(monoFrame, data, dismissCurrent, pauseCurrent);
//    }

//    public void Show<T>(out T frame, bool animated = false, object data = null, bool dismissCurrent = false, bool pauseCurrent = false)
//    {
//        MonoFrame monoFrame = frames.Find(a => a is T);
//        this.Show(monoFrame, animated, data, dismissCurrent, pauseCurrent);
//        frame = (T)Convert.ChangeType(monoFrame, typeof(T));
//    }
//}
