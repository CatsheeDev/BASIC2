using UnityEngine.Events;

namespace BasicAPI
{
    public static class Events
    {
        public static UnityEvent OnNotebookCollect = new();

        public static UnityEvent OnMathGameEnd = new();
    }
}