using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyStore.Code; 

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        private const string ValidConnectionStringName = "MyDB"; 
        private const string InvalidConnectionStringName = "InvalidDB";
        [TestMethod]
        public void VerifyConnection_ValidConnectionString_ReturnsTrue()
        {
            SqlConnectionVerifier verifier = new SqlConnectionVerifier(ValidConnectionStringName);
            bool result = verifier.VerifyConnection();
            Assert.IsTrue(result, "Expected connection verification to succeed.");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidConnectionString_ThrowsArgumentException()
        {
            SqlConnectionVerifier verifier = new SqlConnectionVerifier(InvalidConnectionStringName);
        }
    }
}