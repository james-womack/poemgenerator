using System;
using System.Collections.Generic;
using System.Text;

namespace PoetryEngine {
    class HistoryKeeper<T> {
        private List<T> history;
        private int traverser;
        private int limit;

        public HistoryKeeper() {
            history = new List<T>();
            traverser = 0;
            limit = -1;
        }

        public HistoryKeeper(int limit) {
            history = new List<T>();
            traverser = 0;
            this.limit = limit;
        }

        public T GetCurrentElement() {
            if(history.Count > traverser)
                return history[traverser];
            return default(T);
        }

        public T GetPreviousElement() {
            if(traverser > 0)
                return history[traverser - 1];
            return default(T);
        }

        public T TraverceCurrentElement() {
            if(history.Count > traverser)
                return history[traverser++];
            return default(T);
        }

        public T TraverceBackElement() {
            if(traverser > 0 && history.Count >= traverser)
                return history[--traverser];
            return default(T);
        }

        public T TraverceForwardElement() {
            if(history.Count > traverser)
                return history[++traverser];
            return default(T);
        }

        public T GetFirstElement() {
            if(history.Count > 0)
                return history[0];
            return default(T);
        }
        public T GetLastElement() {
            if(history.Count > 0)
                return history[history.Count - 1];
            return default(T);
        }

        public bool HasHistoryEnded() {
            return history.Count <= traverser;
        }

        public T GotoBeginingOfHistory() {
            traverser = 0;
            return GetCurrentElement();
        }

        public T GotoEndOfHistory() {
            traverser = history.Count - 1;
            return GetCurrentElement();
        }

        public void ClearHistory() {
            traverser = 0;
            history.Clear();
        }

        public bool Any() {
            return history.Count > 0;
        }

        public bool AddHistory(T element) {
            history.Add(element);
            if(limit > 0 && history.Count > limit) {
                history.RemoveAt(0);

                if(traverser > 0) {
                    traverser--;
                    return true;
                }
            }
            return false;
        }
    }
}
