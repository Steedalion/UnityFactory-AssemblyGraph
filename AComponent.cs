using GraphDomain;

namespace AssemblyGraph
{
    public class AComponent : GNode
    {
        public string name;
        public AComponent(AssemblyComponent current,int index)
        {
            Index = index;
            name = current.name;
        }

    }
}