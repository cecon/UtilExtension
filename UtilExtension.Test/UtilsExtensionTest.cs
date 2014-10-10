using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilExtension.Test {

    [TestClass]
    public class UtilsExtensionTest
    {
        [TestMethod]
        public void ToMd5Test()
        {
            Assert.AreEqual("e10adc3949ba59abbe56e057f20f883e", "123456".ToMd5());
            Assert.AreEqual("dbf59bd164fe55695cc7d960da4f1516", "Eu".ToMd5());
            Assert.AreEqual("4a7bbad7ca3cc8872244ec4993d2d4c4", "Teste Chato para cachorro".ToMd5());
            Assert.AreEqual("7978024ab84086f6b97d03d56c536dee", "Vai dar certo".ToMd5());
            Assert.AreEqual("cb42e130d1471239a27fca6228094f0e", "kkk".ToMd5());
        }
        [TestMethod]
        public void ToTileCaseTest()
        {
            Assert.AreNotEqual("Teste Teste", "teste teste");
            Assert.AreEqual("James Douglas Morrison", "james douglas morrison".ToTitleCase());
            Assert.AreEqual("Sid Vicious", "sid vicious".ToTitleCase());
            Assert.AreEqual("Ozzy Osbourne", "ozzy osbourne".ToTitleCase());
            Assert.AreEqual("Freddie Mercury", "freddie mercury".ToTitleCase());
        }

        [TestMethod]
        public void ToBase64StringTest()
        {
            Assert.AreEqual("MTIzNDU2", "123456".ToBase64());
            Assert.AreEqual("aHR0cDovL3NldWRvbWluaW8uY29t", "http://seudominio.com".ToBase64());
            Assert.AreEqual("VGVzdGUgQ2hhdG8gcGFyYSBjYWNob3Jybw==", "Teste Chato para cachorro".ToBase64());
            Assert.AreEqual("VmFpIGRhciBjZXJ0bw==", "Vai dar certo".ToBase64());
            Assert.AreEqual("a2tr", "kkk".ToBase64());
        }
        [TestMethod]
        public void ToBase64ByteTest()
        {
            Assert.AreEqual("MTIzNDU2", "123456".ToCharArray().Select(b => (byte)b).ToArray().ToBase64());
            Assert.AreEqual("aHR0cDovL3NldWRvbWluaW8uY29t", "http://seudominio.com".ToCharArray().Select(b => (byte)b).ToArray().ToBase64());
            Assert.AreEqual("VGVzdGUgQ2hhdG8gcGFyYSBjYWNob3Jybw==", "Teste Chato para cachorro".ToCharArray().Select(b => (byte)b).ToArray().ToBase64());
            Assert.AreEqual("VmFpIGRhciBjZXJ0bw==", "Vai dar certo".ToCharArray().Select(b => (byte)b).ToArray().ToBase64());
            Assert.AreEqual("a2tr", "kkk".ToCharArray().Select(b => (byte)b).ToArray().ToBase64());
        }

        [TestMethod]
        public void FromBase64Test()
        {
            Assert.AreEqual("123456", "MTIzNDU2".FromBase64());
            Assert.AreEqual("http://seudominio.com", "aHR0cDovL3NldWRvbWluaW8uY29t".FromBase64());
            Assert.AreEqual("Teste Chato para cachorro", "VGVzdGUgQ2hhdG8gcGFyYSBjYWNob3Jybw==".FromBase64());
            Assert.AreEqual("Vai dar certo", "VmFpIGRhciBjZXJ0bw==".FromBase64());
            Assert.AreEqual("kkk", "a2tr".FromBase64());
        }

        [TestMethod]
        public void ToDigitTest()
        {
            Assert.AreEqual("", "Edu".ToDigit());
            Assert.AreEqual("123", "Edu123".ToDigit());
            Assert.AreEqual("123456", "ABC123ACF456".ToDigit());
            Assert.AreEqual("111111222", "AA111AAAAAAA111ÇÇÇ222".ToDigit());
            Assert.AreEqual("0", "Freddie Mercury 0".ToDigit());
            Assert.AreEqual("0000001", "Freddie Mercury 0000001".ToDigit());
        }

        [TestMethod]
        public void ToLetterTest()
        {
            Assert.AreEqual("Edu", "Edu123".ToLetter());
            Assert.AreEqual("ABCACF", "ABC123ACF456".ToLetter());
            Assert.AreEqual("AAAAAAAAAÇÇÇ", "AA111AAAAAAA111ÇÇÇ222".ToLetter());
            Assert.AreEqual("FreddieMercury", "Freddie Mercury 0".ToLetter());
            Assert.AreEqual("FreddieMercury", "Freddie Mercury 0000001".ToLetter());
        }

        [TestMethod]
        public void ToSymbleTest()
        {
            Assert.AreEqual("", "Edu123".ToSymbol());
            Assert.AreEqual("%", "ABC123ACF456%".ToSymbol());
            Assert.AreEqual("", "ºAA111AAAAAAA111ÇÇÇ222".ToSymbol());
            Assert.AreEqual("**  ", "**Freddie Mercury 0".ToSymbol());
            Assert.AreEqual("#%$  ", "#%$Freddie Mercury 0000001".ToSymbol());
        }

        [TestMethod]
        public void ToMoneyTest()
        {
            Assert.AreEqual(0M, "RRR".ToMoney(3));
            Assert.AreEqual(0M, "".ToMoney(3));
            Assert.AreEqual(-2.5M, "(R$ 2.50)".ToMoney());
            Assert.AreEqual(111111.222M, "ºAA111AAAAAAA111ÇÇÇ222".ToMoney(3));
            Assert.AreEqual(0M, "-**Freddie Mercury 0".ToMoney());
            Assert.AreEqual(.01M, "#%$Freddie Mercury 0000001".ToMoney());
        }

        [TestMethod]
        public void ToMaskTest()
        {
            Assert.AreEqual("111.111.111-11", "11111111111".ToMask("000.000.000-00"));
            Assert.AreEqual("09.009.000/0001-55", "09009000000155".ToMask("00.000.000/0000-00"));
            Assert.AreEqual("01/01/2000", "01012000".ToMask("99/99/9999"));            
        }


        [TestMethod]
        public void ToTypeTest()
        {
            var foo = new FooTest {Endereco = "Rua 1", Nome = "Foo1", Telefone = "11999999999"};
            var node = new NodeTest { Tipo = "Admin", Nome = "Foo1", Telefone = "11999999999" };

            var fooToNode = foo.ToType<NodeTest>();
            Assert.AreEqual(foo.Nome, fooToNode.Nome);
            Assert.AreEqual(foo.Telefone, fooToNode.Telefone);
            Assert.AreEqual(null, fooToNode.Tipo);

            var nodeToFoo = node.ToType<FooTest>();
            Assert.AreEqual(node.Nome, nodeToFoo.Nome);
            Assert.AreEqual(node.Telefone, nodeToFoo.Telefone);
            Assert.AreEqual(null, nodeToFoo.Endereco);           
        }

        [TestMethod]
        public void MapFromToTest()
        {
            var foo = new FooTest { Endereco = "Rua 1", Nome = "Foo1", Telefone = "11999999999" };
            var node = new NodeTest { Tipo = "Admin", Nome = "Foo1", Telefone = "11999999999" };

            var fooFromNode = new FooTest();
            fooFromNode.MapFromTo(node);
            var nodeFromFoo = new NodeTest();
            nodeFromFoo.MapFromTo(foo);



            Assert.AreEqual(node.Nome, fooFromNode.Nome);
            Assert.AreEqual(node.Telefone, fooFromNode.Telefone);
            Assert.AreEqual(null, fooFromNode.Endereco);

            Assert.AreEqual(foo.Nome, nodeFromFoo.Nome);
            Assert.AreEqual(foo.Telefone, nodeFromFoo.Telefone);
            Assert.AreEqual(null, nodeFromFoo.Tipo);

            nodeFromFoo.MapFromTo(null);

            Assert.AreEqual(foo.Nome, nodeFromFoo.Nome);
            Assert.AreEqual(foo.Telefone, nodeFromFoo.Telefone);
            Assert.AreEqual(null, nodeFromFoo.Tipo);

        }

        [TestMethod]
        public void FromJsonTest()
        {
            const string json1 = "{\"Nome\":\"Ozzi1\"}";
            const string json2 = "{\"Nome\":\"Ozzi2\", \"Endereco\":\"Rua 2\"}";
            const string json3 = "{\"Nome\":\"Ozzi3\", \"Tipo\":\"Admin\"}";
            const string json4 = "{\"Nome\":\"Ozzi4\", \"Telefone\":\"11999999999\"}";

            var foo1 = json1.FromJson<FooTest>();
            var foo2 = json2.FromJson<FooTest>();
            var foo3 = json3.FromJson<FooTest>();
            var foo4 = json4.FromJson<FooTest>();
            var node1 = json1.FromJson<NodeTest>();
            var node2 = json2.FromJson<NodeTest>();
            var node3 = json3.FromJson<NodeTest>();
            var node4 = json4.FromJson<NodeTest>();

            Assert.AreEqual("Ozzi1", foo1.Nome);
            Assert.AreEqual(null, foo1.Telefone);
            Assert.AreEqual(null, foo1.Endereco);
            
            Assert.AreEqual("Ozzi2", foo2.Nome);
            Assert.AreEqual(null, foo2.Telefone);
            Assert.AreEqual("Rua 2", foo2.Endereco);
            
            Assert.AreEqual("Ozzi3", foo3.Nome);
            Assert.AreEqual(null, foo3.Telefone);
            Assert.AreEqual(null, foo3.Endereco);
            
            Assert.AreEqual("Ozzi4", foo4.Nome);
            Assert.AreEqual("11999999999", foo4.Telefone);
            Assert.AreEqual(null, foo4.Endereco);

            
            Assert.AreEqual("Ozzi1", node1.Nome);
            Assert.AreEqual(null, node1.Telefone);
            Assert.AreEqual(null, node1.Tipo);
            
            Assert.AreEqual("Ozzi2", node2.Nome);
            Assert.AreEqual(null, node2.Telefone);
            Assert.AreEqual(null, node2.Tipo);
            
            Assert.AreEqual("Ozzi3", node3.Nome);
            Assert.AreEqual(null, node3.Telefone);
            Assert.AreEqual("Admin", node3.Tipo);
            
            Assert.AreEqual("Ozzi4", node4.Nome);
            Assert.AreEqual("11999999999", node4.Telefone);
            Assert.AreEqual(null, node4.Tipo);
        }

        [TestMethod]
        public void FromTypeTest()
        {
            var foo = new FooTest { Endereco = "Rua 1", Nome = "Foo1", Telefone = "11999999999" };
            var node = new NodeTest { Tipo = "Admin", Nome = "Foo1", Telefone = "11999999999" };

            var fooFromNode = foo.FromType(node);
            Assert.AreEqual(node.Nome, fooFromNode.Nome);
            Assert.AreEqual(node.Telefone, fooFromNode.Telefone);
            Assert.AreEqual(null, fooFromNode.Endereco);

            var nodeFromFoo = node.FromType(foo);
            Assert.AreEqual(foo.Nome, nodeFromFoo.Nome);
            Assert.AreEqual(foo.Telefone, nodeFromFoo.Telefone);
            Assert.AreEqual(null, nodeFromFoo.Tipo);
        }

        class FooTest
        {
            public string Nome { get; set; }
            public string Endereco { get; set; }
            public string Telefone { get; set; }
        }
        class NodeTest
        {
            public string Nome { get; set; }
            public string Tipo { get; set; }
            public string Telefone { get; set; }
        }
    }   
}