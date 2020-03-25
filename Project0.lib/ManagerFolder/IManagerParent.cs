using System;

namespace Managers
{
    /// <summary>
    ///  This interface is the framework for managers. All logic handlers must do these things
    /// </summary>
    public abstract class IManagerParent
    {
        abstract public int GetTotal();

        abstract public bool CheckFor(string x);

        abstract public Object Get(String x);
        abstract public void SetCurrent(string x);
    }
}