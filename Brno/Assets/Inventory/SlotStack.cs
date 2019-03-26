using System.Collections.Generic;
public delegate void SlotStackHandler();
public class SlotStack<T> : Stack<T>
{
    public event SlotStackHandler OnPush, OnPop, OnClear;

    public new void Push(T item)
    {
        base.Push(item);
        if (OnPush != null)
        {
            OnPush();
        }
    }
    public new void Pop()
    {
        base.Pop();
        if (OnPop != null)
        {
            OnPop();
        }
    }
    public new void Clear()
    {
        base.Clear();
        if (OnClear != null)
        {
            OnClear();
        }
    }
}
