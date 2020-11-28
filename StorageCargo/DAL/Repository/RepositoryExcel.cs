using Microsoft.Office.Interop.Excel;
using StorageCargo.DAL.Entities;
using StorageCargo.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace StorageCargo.DAL.Repository
{
    public class RepositoryExcel : IRepository
    {
        private readonly string _excelFilePatch;

        public RepositoryExcel(string excelFilePatch)
        {
            _excelFilePatch = excelFilePatch;
        }

        public ICollection<Cargo> GetCargos()
        {
            CollectionCargos cargos = new CollectionCargos();
            int sheet = 1;
            GetDateFromExcel(cargos, sheet);
            List<Cargo> cargosResult = cargos.Entities.ConvertAll(e => e as Cargo);
            return cargosResult;
        }

        public ICollection<Rate> GetRates()
        {
            CollectionRates rates = new CollectionRates();
            int sheet = 2;
            GetDateFromExcel(rates, sheet);
            List<Rate> ratesResult = rates.Entities.ConvertAll(e => e as Rate);
            return ratesResult;
        }

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
                    Entities.AddEntity(row);
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
