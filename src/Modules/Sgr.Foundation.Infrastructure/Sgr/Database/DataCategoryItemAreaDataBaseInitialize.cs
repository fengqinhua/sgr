using Microsoft.Extensions.Logging;
using Sgr.DataDictionaryAggregate;
using Sgr.EntityFrameworkCore;
using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Database
{
    public class DataCategoryItemAreaDataBaseInitialize : IDataBaseInitialize
    {
        private readonly ILogger<DataCategoryItemAreaDataBaseInitialize> _logger;
        private readonly SgrDbContext _context;
        private readonly IDataCategoryItemManage _dataCategoryItemManage;

        public DataCategoryItemAreaDataBaseInitialize(ILogger<DataCategoryItemAreaDataBaseInitialize> logger, 
            SgrDbContext context,
            IDataCategoryItemManage dataCategoryItemManage)
        {
            this._logger = logger;
            this._context = context;
            this._dataCategoryItemManage = dataCategoryItemManage;
            
        }       

        public int Order => 100;

        public async Task Initialize()
        {
            await initArea();
        }

        private async Task initArea()
        {
            if (!_context.Set<DataCategoryItem>().Any(f => f.CategoryTypeCode == FoundationCategoryTypeProvider.CategoryType_Area_Code))
            {
                //如果未设置
                string filePath = Path.Combine(LocalFileHelper.GetApplicationDirectory(), "Sead", "category_area.txt");
                if (File.Exists(filePath))
                {
                    List<Area> list = new ();
                    foreach (var line in await File.ReadAllLinesAsync(filePath))
                    {
                        string[] items = line.Split('\t');
                        if (items.Length == 4)
                        {
                            _ = long.TryParse(items[0], out long areaId);
                            _ = long.TryParse(items[1], out long areaPId);
                            list.Add(new Area()
                            {
                                Id = areaId,
                                PId = areaPId,
                                Name = items[3],
                                Value = items[2]
                            });
                        }
                    }

                    //递归的方式创建
                    await CreateSons(list, f => f.PId == 0, 0);

                    this._context.SaveChanges();

                    _logger.LogInformation("dictionary - area information initialization complete ...");
                }
                else
                    _logger.LogInformation("dictionary - area sead file not found ...");

            }
            else
                _logger.LogInformation("dictionary - area information has been set ...");
        }

        private async Task CreateSons(IEnumerable<Area> all, Func<Area, bool> predicate,long pid)
        {
            var sons = all.Where(predicate);
            foreach (var son in sons)
            {
                _ = int.TryParse(son.Value, out int orderNumber);
                var dataCategoryItem = await this._dataCategoryItemManage.CreateNewAsync(son.Name, son.Value,"", FoundationCategoryTypeProvider.CategoryType_Area_Code, orderNumber, pid);
                dataCategoryItem.CreationTime = DateTimeOffset.UtcNow;

                await _context.Set<DataCategoryItem>().AddAsync(dataCategoryItem);

                await CreateSons(all, f => f.PId == son.Id, dataCategoryItem.Id);
            }
        }

        class Area
        {
            public long Id { get; set; }
            public long PId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }
    }
}
