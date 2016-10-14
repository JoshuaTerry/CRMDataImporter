using System;
using System.Collections.Generic;

namespace CRMDataImport
{
    public interface IActionList : IEnumerable<Action<ActionContext>> { }

    public class ActionList : List<Action<ActionContext>>, IActionList { }
}
