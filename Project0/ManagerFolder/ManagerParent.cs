using System;

namespace Managers
{
    abstract class ManagerParent
    {
        abstract public int GetTotal();

        abstract public bool CheckFor(string x);

        abstract public Object Get(String x);
    }
}