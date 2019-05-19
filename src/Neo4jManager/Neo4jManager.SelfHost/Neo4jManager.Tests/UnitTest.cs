using Neo4jManager.ServiceInterface;
using Neo4jManager.ServiceModel;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;

namespace Neo4jManager.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<DeploymentService>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

//        [Test]
//        public void Can_call_MyServices()
//        {
//            var service = appHost.Container.Resolve<MyServices>();
//
//            var response = (HelloResponse)service.Any(new Hello { Name = "World" });
//
//            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
//        }
    }
}