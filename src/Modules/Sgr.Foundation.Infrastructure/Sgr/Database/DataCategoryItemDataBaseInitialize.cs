using Microsoft.Extensions.Logging;
using Sgr.DataCategoryAggregate;
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
    public class DataCategoryItemDataBaseInitialize : IDataBaseInitialize
    {
        private readonly ILogger<DataCategoryItemAreaDataBaseInitialize> _logger;
        private readonly SgrDbContext _context;
        private readonly IDataCategoryItemManage _dataCategoryItemManage;

        public DataCategoryItemDataBaseInitialize(ILogger<DataCategoryItemAreaDataBaseInitialize> logger, 
            SgrDbContext context,
            IDataCategoryItemManage dataCategoryItemManage)
        {
            this._logger = logger;
            this._context = context;
            this._dataCategoryItemManage = dataCategoryItemManage;
            
        }       

        public int Order => 101;

        public async Task Initialize()
        {
            await initOnCategoryType(FoundationCategoryTypeProvider.CategoryType_SoftType_Code, "category_soft_type.txt");
            await initOnCategoryType(FoundationCategoryTypeProvider.CategoryType_SoftClassification_Code, "category_soft_classification.txt");
        }

        private async Task initOnCategoryType(string categoryTypeCode, string fileName)
        {
            if (!_context.Set<DataCategoryItem>().Any(f => f.CategoryTypeCode == categoryTypeCode))
            {
                //如果未设置
                string filePath = Path.Combine(LocalFileHelper.GetApplicationDirectory(), "Sead", fileName);
                if (File.Exists(filePath))
                {
                    List<NameValue> list = new();
                    foreach (var line in await File.ReadAllLinesAsync(filePath))
                    {
                        string[] items = line.Split(',');
                        if (items.Length >= 3)
                            list.Add(new NameValue(items[1], items[0]) { Description = items[2] });
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        var item = list[i];
                        var dataCategoryItem = await this._dataCategoryItemManage.CreateNewAsync(item.Name, item.Value, item.Description, categoryTypeCode, i, 0);
                        dataCategoryItem.CreationTime = DateTimeOffset.UtcNow;
                        await _context.Set<DataCategoryItem>().AddAsync(dataCategoryItem);
                    }

                    _context.SaveChanges();

                    _logger.LogInformation($"{categoryTypeCode} information initialization complete ...");
                }
                else
                    _logger.LogInformation($"{categoryTypeCode} sead file not found ...");

            }
            else
                _logger.LogInformation($"{categoryTypeCode} information has been set ...");
        }
    }
}
