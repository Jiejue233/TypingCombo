using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingCombo.src.Helpers
{
    public class KeyBuffer
    {
        private Queue<int> keyQueue;
        private Dictionary<int, int> keyCount;
        public KeyBuffer()
        {
            keyQueue = new Queue<int>(100); ;
            keyCount = new Dictionary<int, int>();
        }

        public float Add(int key)
        {
            int countForehand;
            if (keyQueue.Count <= 100)
            {
                keyQueue.Enqueue(key);
            }
            else
            {
                int tempVk = keyQueue.Dequeue();
                keyQueue.Enqueue(key);
                keyCount[tempVk] -= 1;
            }
            if (keyCount.ContainsKey(key))
            {
                countForehand = keyCount[key];
                keyCount[key] += 1;
            }
            else
            {
                countForehand = 0;
                keyCount.Add(key, 1);
            }
            return countForehand / 100.0f;
        }

        public void Clear()
        {
            keyQueue.Clear();
            foreach (int key in keyCount.Keys)
            {
                keyCount[key] = 0;
            }
        }
    }
}
