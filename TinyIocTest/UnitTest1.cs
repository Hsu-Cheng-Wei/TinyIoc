using NUnit.Framework;
using TinyIoc;

namespace Tests
{
    public interface IDemo { }

    public class Demo : IDemo { }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var container = new TinyIocContainer();

            container.Register<>()
        }
    }
}