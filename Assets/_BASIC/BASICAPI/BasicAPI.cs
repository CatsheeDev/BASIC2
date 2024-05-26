using UnityEngine.Events;

namespace BasicAPI
{
    public static class Events
    {
        /// <summary>
        /// Fired upon player collecting a notebook, aka directly after interacting with it.
        /// Please use "OnMathGameEnd" for upon YCTP completion.
        /// </summary>
        public static UnityEvent OnNotebookCollect = new();

        /// <summary>
        /// Fired upon YCTP completion
        /// Please use "OnNotebookCollect" for upon player collecting a notebook.
        /// </summary>
        public static UnityEvent OnMathGameEnd = new();
    }
}