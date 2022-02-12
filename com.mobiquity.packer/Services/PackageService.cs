using com.mobiquity.packer.Common;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
	public class PackageService : IPackageService
	{
		private IPackageFileParser _packageFileParser;

		public PackageService(IPackageFileParser packageFileParser)
		{
			_packageFileParser = packageFileParser;
		}

		public PackageFileModel ParsePackageFile(string fileContents)
		{
			return _packageFileParser.Parse(fileContents);
		}

		public string SortPackages(List<PackageModel> packages)
		{
			var result = "";
			foreach(var package in packages)
			{
				// Clean up the list so we only work with relevant items
				package.PackageItems.RemoveAll(x => x.Weight > package.WeightLimit);
				package.PackageItems.RemoveAll(x => x.Cost > Constants.PACKAGE_ITEM_COST_MAX);

				package.PackageItems = package.PackageItems.OrderBy(x => GetPackageItemWeightCostValue(x.Weight, x.Cost)).ToList();

				while (package.WeightLimit < package.PackageItems.Sum(x => x.Weight))
				{
					package.PackageItems.Remove(package.PackageItems.First());
				}

				foreach (var item in package.PackageItems)
				{
					result += $"{item.Index}";

					if (item != package.PackageItems.Last())
					{
						result += ",";
					}
					
				}

				if (!package.PackageItems.Any())
				{
					result += "-";
				}
				result += "\n";
			}

			return result;
		}

		private decimal GetPackageItemWeightCostValue(decimal weight, decimal cost)
		{
			return cost / weight;
		}
	}
}
