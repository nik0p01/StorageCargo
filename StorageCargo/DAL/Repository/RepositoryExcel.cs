using Microsoft.Office.Interop.Excel;
using StorageCargo.DAL.Entities;
using StorageCargo.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace StorageCargo.DAL.Repository
{
    /// <summary>
    /// Получения данных из таблицы Excel 
    /// </summary>
    public class RepositoryExcel : IRepository
    {
        private readonly string _excelFilePatch;
        /// <summary>
        /// Конструктор репозитория 
        /// </summary>
        /// <param name="excelFilePatch">Путь к файлу Excel</param>
        public RepositoryExcel(string excelFilePatch)
        {
            _excelFilePatch = excelFilePatch;
        }
        /// <summary>
        /// Метод получения данных из Excel файла
        /// </summary>
        /// <returns>Таблица грузов</returns>
        public ICollection<Cargo> GetCargos()
        {
            CollectionCargos cargos = new CollectionCargos();
            int sheet = 1;
            GetDateFromExcel(cargos, sheet);
            List<Cargo> cargosResult = cargos.Entities.ConvertAll(e => e as Cargo);
            return cargosResult;
        }

        /// <summary>
        /// Метод получения данных из Excel файла
        /// </summary>
        /// <returns>Таблица тарифов</returns>
        public ICollection<Rate> GetRates()
        {
            CollectionRates rates = new CollectionRates();
            int sheet = 2;
            GetDateFromExcel(rates, sheet);
            List<Rate> ratesResult = rates.Entities.ConvertAll(e => e as Rate);
            return ratesResult;
        }
        /// <summary>
        /// Метод заполнения таблицы из файла Excel 
        /// </summary>
        /// <param name="Entities">Коллекция строк таблицы</param>
        /// <param name="sheet">Лист, с которого необходимо производить чтение в файле Excel</param>
        private void GetDateFromExcel(ICollectionEntities Entities, int sheet)
        {
            if (!File.Exists(_excelFilePatch))
            {
                throw new FileNotFoundException($"EXcel file {_excelFilePatch} not found");
            }
            Application ObjExcel = new Application();
            try
            {
                Workbook ObjWorkBook = ObjExcel.Workbooks.Open(_excelFilePatch, 0, true, 5, "", "", false,
                XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                Worksheet ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[sheet];
                var lastCell = ObjWorkSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell);
                int numberRows = lastCell.Row;
                int numberColumn = lastCell.Column;
                string[] row = new string[numberColumn];
                for (int i = 1; i < numberRows; i++)
                {
                    for (int j = 0; j < numberColumn; j++)
                    {
                        row[j] = (ObjWorkSheet.Cells[i + 1, j + 1]).Text.ToString();
                    }
                    Entities.AddEntity(row); //Добавление строки, метод спецефический для каждой реализации интерфейса ICollectionEntities 
                }
            }
            catch (Exception ex)
            {
                throw new ExcelProcessingException("Excel file processing error",ex );
            }
            finally
            {
                ObjExcel.Quit();
            }
        }
    }
}
