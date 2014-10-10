using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilExtension.Test
{
    [TestClass]
    public class CheckerExtensionTest
    {
        [TestMethod]
        public void IsMatchTestTrue()
        {            
            var trueTests  = new[]
            {
                new{Value=  "Eduardo Mendonça", charactersAllowed = "CEMacdemnuroç123 "},
                new{Value=  "Edu 123", charactersAllowed = "CEMacdemnuroç123 "},
                new{Value=  "Cecon", charactersAllowed = "CEMacdemnuroç123 "},
                new{Value=  "edu1", charactersAllowed = "CEMacdemnuroç123 "},
                new{Value=  "çççççççç123", charactersAllowed = "CEMacdemnuroç123 "}
            };

            foreach (var trueTest in trueTests)
            {
                Assert.IsTrue(trueTest.Value.IsMatch(trueTest.charactersAllowed));                
            }
        }
        [TestMethod]
        public void IsMatchTestFalse()
        {
            var trueTests = new[]
            {
                new{Value=  "Eduardo Mendonça", charactersAllowed = "CEMacdemnuro123 "},
                new{Value=  "Edu 123", charactersAllowed = "CEMacdemnuro "},
                new{Value=  "Cecon", charactersAllowed = "EMacdemnuro123 "},
                new{Value=  "edu1", charactersAllowed = "CEMacdemnuro23 "},
                new{Value=  "çççççççç123", charactersAllowed = "CEMacdemnuro123 "}
            };

            foreach (var trueTest in trueTests)
            {
                Assert.IsFalse(trueTest.Value.IsMatch(trueTest.charactersAllowed));
            }
        }

        [TestMethod]
        public void AreDigitsTestTrue()
        {
            var trueTests = new[]
            {
                new{Value=  "12345"},
                new{Value=  "87890"},
                new{Value=  "0000000009"},
                new{Value=  "1234567890"},
                new{Value=  "252524"}
            };

            foreach (var trueTest in trueTests)
            {
                Assert.IsTrue(trueTest.Value.AreDigits());
            }
        }
        [TestMethod]
        public void AreDigitsTestFalse()
        {
            var falseTests = new[]
            {
                new{Value=  "12345Q"},
                new{Value=  "878A90"},
                new{Value=  "00Q00000009"},
                new{Value=  "123SS4567890"},
                new{Value=  "2525,24"}
            };

            foreach (var falseTest in falseTests)
            {
                Assert.IsFalse(falseTest.Value.AreDigits());
            }
        }

        [TestMethod]
        public void IsLetterOrDigitsTestTrue()
        {
            var trueTests1 = new[]
            {
                new {Value = "Edu123", Add = ""},
                new {Value = "EDU123456789", Add = ""},
                new {Value = "Devefuncionar123", Add = ""},
                new {Value = "Yuup", Add = ""},
                new {Value = "kkk0123456789", Add = ""}
            };
            var trueTests2 = new[]
            {
                new{Value=  "Edu123?", Add = "?"},
                new{Value=  "EDU123456789.", Add = "."},
                new{Value=  "Deve funcionar 123 !!!", Add = "! "},
                new{Value=  "Yuup ³", Add = "³ "},
                new{Value=  "kkk0123456789º", Add = "º"}
            };

            foreach (var trueTest in trueTests1)
            {
                Assert.IsTrue(trueTest.Value.IsLetterOrDigit());
            }
            foreach (var trueTest in trueTests2)
            {
                Assert.IsTrue(trueTest.Value.IsLetterOrDigit(trueTest.Add));
            }
        }
        [TestMethod]
        public void IsLetterOrDigitsTestFalse()
        {
            var falseTests1 = new[]
            {
                new {Value = "Edu123$", Add = ""},
                new {Value = "EDU1234.56789", Add = ""},
                new {Value = "Deve func-ionar 123", Add = ""},
                new {Value = "Yuupç", Add = ""},
                new {Value = "kkk01<>23456789", Add = ""}
            };
            var falseTests2 = new[]
            {
                new{Value=  "Edu123?ç", Add = "?"},
                new{Value=  "EDU123456789,.", Add = "."},
                new{Value=  "Deve funcionar 123ç !!!", Add = "!"},
                new{Value=  "Yuup ³²", Add = "³"},
                new{Value=  "kkk0123456789º#", Add = "º"}
            };

            foreach (var falseTest in falseTests1)
            {
                Assert.IsFalse(falseTest.Value.IsLetterOrDigit());
            }
            foreach (var falseTest in falseTests2)
            {
                Assert.IsFalse(falseTest.Value.IsLetterOrDigit(falseTest.Add));
            }
        }

        [TestMethod]
        public void CheckCnpjTestTrue()
        {
            var trueTests = new[]
            {
                new{Value=  "52.658.548/0001-27"},
                new{Value=  "51773.188/0001-41"},
                new{Value=  "88116285000171"},
                new{Value=  "72.273.816/0001-08"},
                new{Value=  "55.288.832000147"}
            };

            foreach (var trueTest in trueTests)
            {
                Assert.IsTrue(trueTest.Value.CheckCnpj());
            }
        }
        [TestMethod]
        public void CheckCnpjTestFalse()
        {
            var falseTests = new[]
            {
                new{Value=  "31.979.768/0001-81"},
                new{Value=  "08.328.274/0001-14"},
                new{Value=  "96444600001-88"},
                new{Value=  "81.647.809/000187"},
                new{Value=  "79222652000100"}
            };

            foreach (var falseTest in falseTests)
            {
                Assert.IsFalse(falseTest.Value.CheckCnpj());
            }
        }

        [TestMethod]
        public void CheckCpfTestTrue()
        {
            var trueTests = new[]
            {
                new{Value=  "128.120.696-27"},
                new{Value=  "863.443.753-10"},
                new{Value=  "592.787.176-30"},
                new{Value=  "185215987-18"},
                new{Value=  "38415699603"}
            };

            foreach (var trueTest in trueTests)
            {
                Assert.IsTrue(trueTest.Value.CheckCpf());
            }
        }
        [TestMethod]
        public void CheckCpfTestFalse()
        {
            var falseTests = new[]
            {
                new{Value=  "42451.481-52"},
                new{Value=  "528.670.118-14"},
                new{Value=  "46738244828"},
                new{Value=  "279.046.328-00"},
                new{Value=  "228.551.298-86"}
            };

            foreach (var falseTest in falseTests)
            {
                Assert.IsFalse(falseTest.Value.CheckCpf());
            }
        }

        [TestMethod]
        public void CheckEmailTestTrue()
        {
            var trueTests = new[]
            {
                new{Value=  "edu@gmail.com"},
                new{Value=  "tes@teste.com.br"},
                new{Value=  "kkk@pqp.com.br"},
                new{Value=  "kid.le@yahoo.com"},
                new{Value=  "teste@teste.net"}
            };

            foreach (var trueTest in trueTests)
            {
                Assert.IsTrue(trueTest.Value.CheckEmail());
            }
        }
        [TestMethod]
        public void CheckEmailTestFalse()
        {
            var falseTests = new[]
            {
                new{Value=  "edu@gmail."},
                new{Value=  "tes@teste.br"},
                new{Value=  "kkk@pqp..br"},
                new{Value=  "kid.le@.com"},
                new{Value=  "teste@testenet.tt"}
            };

            foreach (var falseTest in falseTests)
            {
                Assert.IsFalse(falseTest.Value.CheckEmail());
            }
        }
    }
}
