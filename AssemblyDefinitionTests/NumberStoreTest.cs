using NUnit.Framework;

namespace AssemblyGraph.AssemblyDefinitionTests
{
    public class NumberStoreTest
    {
        [Test]
        public void CreateNumberStore()
        {
            Assert.NotNull(ConnectorIDs.Instance);
        }

        [Test]
        public void GetFirstNumber()
        {
            ConnectorIDs.Instance.Reset();
            Assert.AreEqual(0, ConnectorIDs.Instance.GetNextAvailable());
        }

        [Test]
        public void TwoInARow()
        {
            ConnectorIDs.Instance.Reset();
            Assert.AreEqual(0, ConnectorIDs.Instance.GetNextAvailable());
            Assert.AreEqual(1, ConnectorIDs.Instance.GetNextAvailable());
        }
    }
}