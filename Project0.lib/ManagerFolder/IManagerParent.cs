using System;

namespace Managers
{
    public abstract class IManagerParent
    {
        abstract public int GetTotal();

        abstract public bool CheckFor(string x);

        abstract public Object Get(String x);
        abstract public void SetCurrent(string x);
    }
}