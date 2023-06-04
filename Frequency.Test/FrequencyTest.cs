using System.Text;

namespace Frequency.Test
{

    public class FrequencyTests
    {
        StringBuilder _Output;
        string _testDirectory = TestContext.CurrentContext.TestDirectory;
        string _testFileDir;

        [SetUp]
        public void Setup()
        {
            _Output = new StringBuilder();
            StringWriter outputWriter = new StringWriter(_Output);
            Console.SetOut(outputWriter);
            _testFileDir = $"""{_testDirectory}\..\..\..\..\TestFiles\""";
        }

        public string[] runFrequencyCaptureConsole(string[] args)
        {
            FrequencyCounter.Frequency.Main(args);
            return _Output.ToString().Split("\r\n");
        }


        string[] expectedGoodFileOutput = new string[]
        {
            "Case sensitivity set to: True",
            "Total characters: 58",
            "e (12)",
            "d (6)",
            "h (5)",
            "r (4)",
            "o (4)",
            "t (3)",
            "T (2)",
            "i (2)",
            "f (2)",
            "u (2)",
            string.Empty
        };

        string[] expectedMissingFileOutput = new string[]
        {
            "Case sensitivity set to: True",
            "File not found.",
            string.Empty
        };

        string[] expectedUsageOutput = new string[]
        {
            "Usage: frequency.exe filename [-u] [-i]",
            """Usage: Optional arguments "-u" - display usage""",
            """Usage: Optional arguments "-i" - ignore letter case [-i]""",
            string.Empty
        };

        string[] expectedCaseSensitiveFalseOutput = new string[]
        {
            "Case sensitivity set to: False",
            "Total characters: 58",
            "E (12)",
            "D (6)",
            "T (5)",
            "H (5)",
            "R (4)",
            "O (4)",
            "I (2)",
            "F (2)",
            "U (2)",
            "Q (1)",
            string.Empty
        };

        string[] expectedNoFileInArgumentsOutput = new string[]
{
            "Please supply a file to process.",
            "Usage: frequency.exe filename [-u] [-i]",
            """Usage: Optional arguments "-u" - display usage""",
            """Usage: Optional arguments "-i" - ignore letter case [-i]""",
            string.Empty
};


        [Test]
        public void TestMainWithValidFile()
        {
            string[] goodFileLocation = new string[] { $"""{_testFileDir}test.txt""" };
            string[] actualOutput = runFrequencyCaptureConsole(goodFileLocation);
            Assert.That(actualOutput, Is.EqualTo(expectedGoodFileOutput));
        }

        [Test]
        public void TestMainWithMissingFile()
        {
            string[] badFileLocation = new string[] { $"""{_testFileDir}iDontExist.txt""" };
            string[] actualOutput = runFrequencyCaptureConsole(badFileLocation);
            Assert.That(actualOutput, Is.EqualTo(expectedMissingFileOutput));
        }

        [Test]
        public void TestDisplayUsage()
        {
            string[] withUsageArgument = new string[] { $"""{_testFileDir}test.txt""", "-u" };
            string[] actualOutput = runFrequencyCaptureConsole(withUsageArgument);
            Assert.That(actualOutput, Is.EqualTo(expectedUsageOutput));
        }

        [Test]
        public void TestDisplayUsageOnly()
        {
            string[] withUsageArgumentOnly = new string[] { "-u" };
            string[] actualOutput = runFrequencyCaptureConsole(withUsageArgumentOnly);
            Assert.That(actualOutput, Is.EqualTo(expectedUsageOutput));
        }

        [Test]
        public void TestCaseInsensitiveUsage()
        {
            string[] withCaseInsensitiveArgument = new string[] { $"""{_testFileDir}test.txt""", "-i" };
            string[] actualOutput = runFrequencyCaptureConsole(withCaseInsensitiveArgument);
            Assert.That(actualOutput, Is.EqualTo(expectedCaseSensitiveFalseOutput));
        }

        [Test]
        public void TestCaseInsensitiveFlagOnly()
        {
            string[] withOnlyInsensitiveArgument = new string[] { "-i" };
            string[] actualOutput = runFrequencyCaptureConsole(withOnlyInsensitiveArgument);
            Assert.That(actualOutput, Is.EqualTo(expectedNoFileInArgumentsOutput));
        }

        [Test]
        public void TestInvalidSwitch()
        {
            string[] withBadSwitchArgument = new string[] { $"""{_testFileDir}test.txt""", "-b" };
            string[] actualOutput = runFrequencyCaptureConsole(withBadSwitchArgument);
            Assert.That(actualOutput, Is.EqualTo(expectedGoodFileOutput));
        }

        [Test]
        public void TestNoArguments()
        {
            string[] withNoArgument = new string[0];
            string[] actualOutput = runFrequencyCaptureConsole(withNoArgument);
            Assert.That(actualOutput, Is.EqualTo(expectedNoFileInArgumentsOutput));
        }
    }
}
