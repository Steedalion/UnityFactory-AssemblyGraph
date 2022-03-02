using System.Collections.Generic;

namespace AssemblyGraph
{
    public class ConnectorIDs
    {
        private static HashSet<int> store = new HashSet<int>();
        private int Limit = 9000;

        private static ConnectorIDs instance;

        public static ConnectorIDs Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectorIDs();
                }

                return instance;
            }
        }

        public int GetNextAvailable()
        {
            for (int i = 0; i < Limit; i++)
            {
                if (!Contains(i))
                {
                    store.Add(i);
                    return i;
                }
            }

            throw new NumberStoreExceededException();
        }

        private bool Contains(int i)
        {
            return store.Contains(i);
        }

        public void Reset()
        {
            store = new HashSet<int>();
        }
    }
}