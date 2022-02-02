using System;
using AssemblyGetInfoLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyGetInfoTest
{
    [TestClass]
    public class Tests
    {
        private Node Setup()
        {
            IGetInfo getter = new AssemblyGetInfo();
            return getter.GetInfoFromFile("D:\\assembly\\TesterDll\\bin\\Debug\\TesterDll.dll");
        }

        [TestMethod]
        public void LoadingAssemblyTest()
        {
            Node actual = Setup();
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void AssemblyNameTest()
        {
            Node actual = Setup();
            string str = actual.Content;
            Assert.IsTrue(actual.Content.StartsWith("TesterDll"));
        }

        [TestMethod]
        public void TypeNameTest()
        {
            Node actual = Setup();
            Assert.AreEqual("Tester", actual.Nodes[0].Nodes[0].Content);
        }

        [TestMethod]
        public void FieldsIdentifyTest()
        {
            Node actual = Setup();
            Assert.AreEqual("Fields", actual.Nodes[0].Nodes[0].Nodes[0].Content);
        }

        [TestMethod]
        public void PropertiesIdentifyTest()
        {
            Node actual = Setup();
            Assert.AreEqual("Properties", actual.Nodes[0].Nodes[0].Nodes[1].Content);
        }

        [TestMethod]
        public void MethodsIdentifyTest()
        {
            Node actual = Setup();
            Assert.AreEqual("Methods", actual.Nodes[0].Nodes[0].Nodes[2].Content);
        }

    }
}
