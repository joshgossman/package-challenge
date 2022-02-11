using com.mobiquity.packer.Common;
using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace com.mobiquity.packer.Services
{
	public class PackageFileParser : IPackageFileParser
	{
		public PackageFileModel Parse(string filePath)
		{
			try
			{
                var result = new PackageFileModel();

                var file = new System.IO.StreamReader(filePath);
                var line = "";

                while ((line = file.ReadLine()) != null)
                {
                    var packageResult = new PackageModel();

                    var lineItems = line.Split(new string[] { " : " }, StringSplitOptions.None);
                    var packageItems = lineItems[1].Split(' ');

                    packageResult.WeightLimitInKg = int.Parse(lineItems[0]);
                    foreach (var packageItem in packageItems)
					{
                        var packageItemClean = packageItem.Split('(', ')')[1];
                        var packageItemDetails = packageItemClean.Split(',');

                        packageResult.PackageItems.Add(new PackageItemModel {
                            Index = int.Parse(packageItemDetails[0]),
                            WeightInKg = decimal.Parse(packageItemDetails[1], CultureInfo.InvariantCulture),
                            Cost = int.Parse(Regex.Match(packageItemDetails[2], @"\d+").Value) //Remove non-numeric data and parse to int
                        });
					}

                    result.PackageModels.Add(packageResult);
                }

                file.Close();

                return result;
            }
            catch (Exception e)
			{
				throw new Exception("Unable to parse file", e);
			}
        }
	}
}
