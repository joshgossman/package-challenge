using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class PackageServiceTests
    {
        [Test]
        public void Parse_Success()
		{
            var contents = new StringBuilder();
            contents.AppendLine("81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)");
            contents.AppendLine("8 : (1,15.3,€34)");

            var result = new PackageService(new PackageFileParser()).ParsePackageFile(contents.ToString());

            Assert.That(result.PackageModels.Count, Is.EqualTo(2));
            Assert.That(result.PackageModels[0].WeightLimitInKg, Is.EqualTo(81));
            Assert.That(result.PackageModels[0].PackageItems.Count, Is.EqualTo(6));
            Assert.That(result.PackageModels[0].PackageItems[0].Index, Is.EqualTo(1));
            Assert.That(result.PackageModels[0].PackageItems[0].WeightInKg, Is.EqualTo(53.38));
            Assert.That(result.PackageModels[0].PackageItems[0].Cost, Is.EqualTo(45));
        }

        [Test]
        public void Parse_IncorrectFormat()
        {
            var contents = new StringBuilder();
            contents.AppendLine("(81) (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)");
            contents.AppendLine("8 : (1,15.3,€34)");

            ActualValueDelegate<object> result = () => new PackageService(new PackageFileParser()).ParsePackageFile(contents.ToString());

            Assert.That(result, Throws.TypeOf<Exception>());
        }

        private void CreateTestFile(string fileName, string fileContent)
		{

		}
    }
}
