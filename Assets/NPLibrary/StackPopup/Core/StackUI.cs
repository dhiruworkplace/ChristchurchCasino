using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class StackUI : MonoBehaviour, IFrameListener
{
    private static StackUI instance;

    public static StackUI Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<StackUI>();
            }
            return instance;
        }
    }

    [Space()]
	protected Stack<IFrame> frames = new Stack<IFrame>();

	public Stack<IFrame> GetFrames() {
		return frames;
	}

    #region Show

    public void Show<T>(object data = null, bool dismissCurrent = false, bool pauseCurrent = true) where T : MonoFrame 
    {
        GameObject prefab = Resources.Load(string.Format("{0}/{1}", typeof(T).Name, typeof(T).Name)) as GameObject;
        if (prefab == null) {
            Debug.LogError(string.Format("{0}/{1}", typeof(T).Name, typeof(T).Name) + " not found!");
            return;
        }

        MonoFrame frame = Instantiate(prefab, transform).GetComponent<MonoFrame>();
        Show(frame, data, dismissCurrent, pauseCurrent);
    }

	public void Show<T>(out T frame, object data = null, bool dismissCurrent = false, bool pauseCurrent = true) where T : MonoFrame
	{
        GameObject prefab = Resources.Load(string.Format("{0}/{1}", typeof(T).Name, typeof(T).Name)) as GameObject;
        if (prefab == null) {
            Debug.LogError(string.Format("{0}/{1}", typeof(T).Name, typeof(T).Name) + " not found!");
            frame = null;
            return;
        }

        frame = Instantiate(prefab, transform).GetComponent<T>();
        Show(frame,data,dismissCurrent,pauseCurrent);
	}

    private void Show(MonoFrame frame, object data = null, bool dismissCurrent = true, bool pauseCurrent = true) {
        if (dismissCurrent && Current != null) {
            var current = frames.Pop();
            current.Listener = null;
            current.Dismiss();
        } else {
            var current = Current;
            if (current != null && pauseCurrent) {
                current.Pause();
            }
        }

        frames.Push(frame);

        frame.Listener = this;
        frame.Show(data);
    }
	#endregion

	public void Pop()
	{
		if (Current != null)
		{
			Current.Dismiss();
		}
	}

	public void DismissAllFrames()
	{
		while (frames.Count > 0)
		{
			var frame = frames.Pop();
			frame.Listener = null;
			frame.Dismiss();
		}
	}

	public IFrame Current
	{
		get { return frames.Count > 0 ? frames.Peek() : null; }
	}

	#region IFrameListener

	public void OnShown(MonoFrame frame)
	{
		
	}

	public void OnDismissed(MonoFrame frame)
	{

		frame.Listener = null;
		if (Current == frame)
		{
			frames.Pop();
		}

		if (Current != null)
		{
			Current.Resume();
		}
	}

	public void OnPaused(MonoFrame frame)
	{

	}

	public void OnResumed(MonoFrame frane)
	{
		
	}

	#endregion
}

public class MonoFrame : MonoBehaviour, IFrame 
{
    private float TimeShowPopup = 0.5f;

	#region IFrame
    public virtual void Show(object data) {
        gameObject.SetActive(true);
        if (Listener != null)
            Listener.OnShown(this);
    }

    public virtual void Dismiss() {
        Destroy(gameObject);
        if (Listener != null)
            Listener.OnDismissed(this);
    }

    public virtual void Pause() {
        gameObject.SetActive(false);
        if (Listener != null)
            Listener.OnPaused(this);

    }

    public virtual void Resume() {
        if (gameObject.activeSelf)
            return;
        gameObject.SetActive(true);
        if (Listener != null)
            Listener.OnResumed(this);
    }

	public IFrameListener Listener {
		get;
		set;
	}

	#endregion

	#region Button Callback

    [ContextMenu("OnClose")]
	public virtual void OnCloseButtonClicked ()
	{
		Dismiss ();
	}

	#endregion
}

public interface IFrame
{

	void Show(object data);

	void Dismiss();

	void Pause();

	void Resume();

	IFrameListener Listener
	{
		get;
		set;
	}
}

public interface IFrameListener
{
	void OnShown(MonoFrame frame);

	void OnDismissed(MonoFrame frame);

	void OnPaused(MonoFrame frame);

	void OnResumed(MonoFrame frane);

}
















