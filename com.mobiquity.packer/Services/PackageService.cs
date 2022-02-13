using com.mobiquity.packer.Common;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public List<PackageModel> SortPackages(List<PackageModel> packages)
		{
			var result = new List<PackageModel>();
			foreach(var package in packages)
			{
				// Clean up the list so we only work with relevant items
				package.PackageItems.RemoveAll(x => x.Weight > package.WeightLimit);

				// Order list to ensure items that weigh less will take precedence if others cost the same
				package.PackageItems = package.PackageItems.OrderBy(x => x.Weight).ToList();

				var knapsackResult = KnapSackCalculatorOnPackageItems(package.PackageItems, package.WeightLimit);

				result.Add(new PackageModel { 
					WeightLimit = package.WeightLimit, 
					PackageItems = knapsackResult.OrderBy(x => x.Index).ToList()
				});;
			}

			return result;
		}

		public string ParseSortedPackagesToResult(List<PackageModel> packages)
		{
			var sb = new StringBuilder();
			foreach (var package in packages)
			{
				foreach (var item in package.PackageItems)
				{
					sb.Append(item.Index);
					if (item != package.PackageItems.Last())
					{
						sb.Append(",");
					}
				}

				if (!package.PackageItems.Any())
				{
					sb.Append("-");
				}

				if (package != packages.Last())
				{
					sb.AppendLine();
				}
			}

			return sb.ToString();
		}

		private List<PackageItemModel> KnapSackCalculatorOnPackageItems(List<PackageItemModel> packageItems, int weightLimit)
		{
			var result = new List<PackageItemModel>();

			// Get decimal weights as factor to turn decimal into integers for use as indexes
			var decimalWeightFactor = (int)Math.Pow(10, Helpers.FetchLargestDecimalPlace(packageItems.Select(x => x.Weight).ToList()));
			var factoredWeightLimit = weightLimit * decimalWeightFactor;

			// Following solution derived from https://dotnetcoretutorials.com/2020/04/22/knapsack-algorithm-in-c/
			var itemCount = packageItems.Count;

			decimal[,] matrix = new decimal[itemCount + 1, factoredWeightLimit + 1];

			//Go through each item. 
			for (int i = 0; i <= itemCount; i++)
			{
				//This loop basically starts at 0, and slowly gets bigger. 
				//Think of it like working out the best way to fit into smaller bags and then keep building on that. 
				for (int w = 0; w <= factoredWeightLimit; w++)
				{
					//If we are on the first loop, then set our starting matrix value to 0. 
					if (i == 0 || w == 0)
					{
						matrix[i, w] = 0;
						continue;
					}

					//Because indexes start at 0, 
					//it's easier to read if we do this here so we don't think that we are reading the "previous" element etc. 
					var currentItemIndex = i - 1;
					var currentItem = packageItems[currentItemIndex];
					//Is the weight of the current item less than W 
					//(e.g. We could find a place to put it in the bag if we had to, even if we emptied something else?)
					if (currentItem.Weight * decimalWeightFactor <= w)
					{
						//If I took this item right now, and combined it with other items
						//Would that be bigger than what you currently think is the best effort now? 
						//In other words, if W is 50, and I weigh 30. If I joined up with another item that was 20 (Or multiple that weigh 20, or none)
						//Would I be better off with that combination than what you have right now?
						//If not, then just set the value to be whatever happened with the last item 
						//(may have fit, may have done the same thing and not fit and got the previous etc). 
						matrix[i, w] = Math.Max(currentItem.Cost + matrix[i - 1, w - (int)(currentItem.Weight * decimalWeightFactor)]
												, matrix[i - 1, w]);
					}
					//This item can't fit, so bring forward what the last value was because that's still the "best" fit we have. 
					else
					{
						matrix[i, w] = matrix[i - 1, w];
					}
				}
			}

			// Determine which package items are in the results
			var res = matrix[itemCount, factoredWeightLimit];

			var weight = factoredWeightLimit;
			for (var i = itemCount; i > 0 && res > 0; i--)
			{
				if (res == matrix[i - 1, weight])
					continue;
				else
				{
					result.Add(packageItems[i - 1]);

					res = res - packageItems[i - 1].Cost;
					weight = weight - (int)(packageItems[i - 1].Weight * decimalWeightFactor);
				}
			}
			return result;
		}
	}
}
