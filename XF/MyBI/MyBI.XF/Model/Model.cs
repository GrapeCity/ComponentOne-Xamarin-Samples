using C1.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xamarin.Forms;

namespace MyBI
{
    public class Model
    {
        public C1DataCollection<Product> ProductList { get; set; }
        public C1DataCollection<Region> RegionList { get; set; }
        public C1DataCollection<DetailDataItem> DetailItemList { get; set; }
        public C1DataCollection<DataItem> ItemList { get; set; }

        public event EventHandler<EventArgs> DataUpdated;

        public double CurrentUnitSales { get; set; }
        public double CurrentRevenue { get; set; }

        private Assembly _assembly;

        private Region _filterRegion;
        private Product _filterProduct;

        private static Model _instance;

        public static Model Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Model();
                return _instance;
            }
        }

        public bool Install()
        {
            _filterRegion = new Region() { iD = 0, Name = MyBI.Resources.AppResources.AllRegionsItem };
            _filterProduct = new Product() { iD = 0, Name = MyBI.Resources.AppResources.AllProductsItem };
            _assembly = Assembly.Load(new AssemblyName("MyBI.XF"));
            bool regionsResult = InstallRegions();
            bool productsResult = InstallProducts();
            bool itemsResult = InstallItems();
            return regionsResult && productsResult && itemsResult;
        }

        private bool InstallRegions()
        {
            System.IO.Stream stream;
            try
            {
                stream = _assembly.GetManifestResourceStream("MyBI.Data.Region.csv");
            }
            catch
            {
                stream = null;
            }
            if (stream == null)
                return false;
            List<Region> regionList = new List<Region>();
            using (CsvFileReader reader = new CsvFileReader(stream))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    int id = 0;
                    if (int.TryParse(row[0], out id))
                    {
                        Region region = new Region();
                        region.iD = id;
                        region.Name = row[1];
                        regionList.Add(region);
                    }
                }
            }
            regionList.Insert(0, new Region() { iD = 0, Name = MyBI.Resources.AppResources.AllRegionsItem });
            RegionList = new C1DataCollection<Region>(regionList);
            return true;
        }
        private bool InstallProducts()
        {
            System.IO.Stream stream;
            try
            {
                stream = _assembly.GetManifestResourceStream("MyBI.Data.Product.csv");

            }
            catch
            {
                stream = null;
            }

            if (stream == null)
                return false;
            List<Product> productList = new List<Product>();
            using (CsvFileReader reader = new CsvFileReader(stream))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    int id = 0;
                    if (int.TryParse(row[0], out id))
                    {
                        Product product = new Product();
                        product.iD = id;
                        product.Name = row[1];
                        productList.Add(product);
                    }
                }
            }
            productList.Insert(0, new Product() { iD = 0, Name = MyBI.Resources.AppResources.AllProductsItem });
            ProductList = new C1DataCollection<Product>(productList);
            return true;
        }
        private bool InstallItems()
        {
            //var stream = _assembly.GetManifestResourceStream("MyBI.Data.Data.csv");
            System.IO.Stream stream;
            try
            {
                stream = _assembly.GetManifestResourceStream("MyBI.Data.Data.csv");

            }
            catch
            {
                stream = null;
            }

            if (stream == null)
                return false;
            List<DataItem> itemsList = new List<DataItem>();
            using (CsvFileReader reader = new CsvFileReader(stream))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    int id = 0;
                    if (int.TryParse(row[0], out id))
                    {
                        DataItem item = new DataItem();
                        item.iD = id;
                        item.Month = DateTime.Parse(row[1]);
                        int regionID = int.Parse(row[2]);
                        item.Region = (Region)RegionList[regionID];
                        int productID = int.Parse(row[4]);
                        item.Product = (Product)ProductList[productID];
                        item.Units = double.Parse(row[6]);
                        item.Revenue = double.Parse(row[7]);
                        itemsList.Add(item);
                    }
                }
            }
            ItemList = new C1DataCollection<DataItem>(itemsList);
            UpdateData();
            return true;
        }

        public async void FilterByRegion(Region region)
        { 
            if (_filterRegion.Equals(region))
                return;
            else
                _filterRegion = region;
            await ItemList.FilterAsync(Filter);
            UpdateData();
            DataUpdated?.Invoke(null, new EventArgs());
        }
        public async void FilterByProdcut(Product product)
        {
            if (_filterProduct.Equals(product))
                return;
            else
                _filterProduct = product;
            await ItemList.FilterAsync(Filter);
            UpdateData();
            DataUpdated?.Invoke(null, new EventArgs());
        }

        private bool Filter(object item)
        {
            DataItem dataItem = (DataItem)item;
            if(dataItem != null)
            {
                bool regionResult = _filterRegion.iD == 0 ? true : _filterRegion.Equals(dataItem.Region);
                bool productResult = _filterProduct.iD == 0 ? true : _filterProduct.Equals(dataItem.Product);
                return regionResult && productResult;
            }
            return false;
        }

        private void UpdateData()
        {
            var detailItems = from DataItem data in ItemList
                              group data by data.Month into groupItems
                              orderby groupItems.Key
                              select new DetailDataItem
                              {
                                  Month = groupItems.Key,
                                  SumRevenue = groupItems.Sum(i => i.Revenue),
                                  SumUnits = groupItems.Sum(i => i.Units),
                              };
            DetailItemList = new C1DataCollection<DetailDataItem>(detailItems);
            double unitStartValue = 0;
            double revenueStartValue = 0;            
            double maxUnitValue = MathHelper.Instance.GetMaxValue();
            double maxRevenueValue = MathHelper.Instance.GetMaxValue(false);
            double deltaUnit = FlexChartHelper.Instance.CalculateTrendLine(out unitStartValue);
            double deltaRevenue = FlexChartHelper.Instance.CalculateTrendLine(out revenueStartValue, false);
            int index = 0;
            foreach (DetailDataItem item in DetailItemList)
            {
                item.MaxUnits = maxUnitValue;
                item.MaxRevenue = maxRevenueValue;
                item.TrendUnitValue = unitStartValue + deltaUnit * index;
                item.TrendRevenueValue = revenueStartValue + deltaRevenue * index;
                index++;
            }
            CurrentUnitSales = ItemList.Sum(i => ((DataItem)i).Units) / DetailItemList.Count;
            CurrentRevenue = ItemList.Sum(i => ((DataItem)i).Revenue) / DetailItemList.Count;
        }
    }
}