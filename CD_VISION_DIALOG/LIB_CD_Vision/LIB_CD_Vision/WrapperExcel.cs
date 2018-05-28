        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

using NPOI.HSSF.Model; // InternalWorkbook
using NPOI.HSSF.UserModel; // HSSFWorkbook, HSSFSheet

namespace CD_Measure
{
    public class WrapperExcel
    {
        //private int row = 0;
        //private int col = 0;

        public List<string[]> data = new List<string[]>();

        public int ROW {get { return data.Count; }}
        public int COL
        {
            get 
            {
                if (data.Count == 0) return 0;
                else 
                    return data.ElementAt(0).Count(); 
            }
        }

        public void Add(string [] columData){data.Add(columData);}
        public string GetCol(int row, int col){return data.ElementAt(row).ElementAt(col);}
        public string [] GetRow(int row ){return data.ElementAt(row);}
        public void Clear(){data.Clear();}

        public bool LoadFromFile_XLS(string strFilePath)
        {
            HSSFWorkbook wb;
            HSSFSheet sh;

            if( File.Exists( strFilePath))
            {
                try
                {
                    using (var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
                    {
                        wb = new HSSFWorkbook(fs);

                        string sheetname = wb.GetSheetAt(0).SheetName;

                        sh = (HSSFSheet)wb.GetSheet(sheetname);

                        int i = 0;

                        List<string[]> listData = new List<string[]>();

                        int COL = sh.GetRow(0).Cells.Count;
                        while (sh.GetRow(i) != null)
                        {
                            List<string> arr = new List<string>();

                            for (int col = 0; col < COL; col++)
                            {
                                var cell = sh.GetRow(i).GetCell(col);
                                arr.Add(cell.ToString());

                            }

                            listData.Add(arr.ToArray());
                            i++;
                        }

                        data = listData;
                    }
                }
                catch( Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool LoadFromFile(string strFilePath)
        {
            Clear();

            try
            {
                using (ExcelPackage excelPkg = new ExcelPackage())
                using (FileStream stream = new FileStream(strFilePath, FileMode.Open))
                {
                    excelPkg.Load(stream);
                    ExcelWorksheet oSheet = excelPkg.Workbook.Worksheets[1];

                    int totalRows = oSheet.Dimension.End.Row;
                    int totalCols = oSheet.Dimension.End.Column;


                    for (int y = 1; y <= totalRows; y++)
                    {
                        string[] arrCols = new string[totalCols];

                        for (int x = 1; x <= totalCols; x++)
                        {
                            if (oSheet.Cells[y, x].Value != null)
                            {
                                string strValue = oSheet.Cells[y, x].Value.ToString();
                                arrCols[x - 1] = strValue;

                            }// if (oSheet.Cells[y, x].Value != null)
                            else
                            {
                                arrCols[x - 1] = "";
                            }

                        }// for (int x = 1; x <= totalCols; x++)
                        Add(arrCols);

                    }// for (int y = 1; y <= totalRows; y++)
                }//using (FileStream stream = new FileStream(strFilePath, FileMode.Open))

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
            return true;
        }
    
        public static void ExcuteExcel(string strFilePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = strFilePath;
            process.Start();
        }

        public void DeleteItem(string[] item)
        {
            
            List<string[]> listNew = new List<string[]>();

            foreach( string [] single in data)
            {
                int nLengthDst = item.Length;
                int nLengthSrc = single.Length;
               
                // both have to has same length
                if( nLengthDst == nLengthSrc)
                {
                    int nMathchCount = 0;

                    // check the all column one by one
                    for( int i = 0; i < nLengthSrc; i++)
                    {
                        if (single.ElementAt(i) == item.ElementAt(i))
                        {
                            nMathchCount++;
                        }
                    }
                    // if checked count is full length means totally same
                    if( nMathchCount != nLengthSrc)
                    {
                        //make different list
                        listNew.Add(single);
                    }
                }
            }
            // swap list
            data.Clear();
            foreach( string [] single in listNew)
            {
                data.Add(single);
            }
            
        }
        public void DeleteRow(List<int> listIndex)
        {
            List<string[]> listToDie = new List<string[]>();


            for (int i = 0; i < listIndex.Count; i++)
            {
                listToDie.Add(data.ElementAt( listIndex.ElementAt(i) ));
            }

            for (int i = 0; i < listToDie.Count; i++)
            {
                string[] itemToDelete = listToDie.ElementAt(i);
                data.Remove(itemToDelete);
            }
        }
        public void AddColByValue(string value)
        {
            List<string[]> newList = new List<string[]>();

            int nColCount = 0;
            if (data.Count != 0)
            {
               nColCount = data.ElementAt(0).Count();
            }

            for ( int i = 0; i < data.Count; i++)
            {
                string[] temp = data.ElementAt(i);

                List<string> list = new List<string>();
                list = temp.ToList();
                list.Add(value);

                newList.Add( list.ToArray());
            }
            data.Clear();
            data = newList.ToList();
        }

        public void AddColByCopy(int nIndex)
        {
            nIndex -= 1;
            List<string[]> newList = new List<string[]>();

            int nColCount = 0;
            if (data.Count != 0)
            {
                nColCount = data.ElementAt(0).Count();
            }

            for (int i = 0; i < data.Count; i++)
            {
                string[] temp = data.ElementAt(i);

                List<string> list = new List<string>();
                list = temp.ToList();
                list.Add(temp[nIndex]);

                newList.Add(list.ToArray());
            }
            data.Clear();
            data = newList.ToList();
        }
        public void MoveCol(int nSrc, int nDest)
        {
            nSrc -= 1;
            nDest -= 1;

            List<string[]> newList = new List<string[]>();

            int nColCount = 0;
            if (data.Count != 0)
            {
                nColCount = data.ElementAt(0).Count();
            }

            for (int i = 0; i < data.Count; i++)
            {
                string[] temp = data.ElementAt(i);

                List<string> list = new List<string>();

                string value = temp[nSrc];

                for (int j = 0; j < nDest; j++ )
                {
                    list.Add(temp[j]);
                }
                
                list.Add(value);
                
                for (int j = nDest; j < temp.Length; j++ )
                {
                    list.Add(temp[j]);
                }
                newList.Add(list.ToArray());
            }
            data.Clear();
            data = newList.ToList();
            DeleteCol(nSrc+2);

        }
        public void DeleteCol(int nIndex)
        {
            nIndex -= 1;

            List<string[]> newList = new List<string[]>();

            if (data.Count == 0) return;
            int nCount = data.ElementAt(0).Count();

            for (int i = 0; i < data.Count; i++  )
            {
                string[] temp = data.ElementAt(i);
                List<string> listBuffer = new List<string>();

                for( int j = 0; j < nCount; j++ )
                {
                    if( j != nIndex)
                    {
                        listBuffer.Add(temp[j]);
                    }
                }
                string[] newArr = listBuffer.ToArray();
                newList.Add(newArr);
            }

            data.Clear();
            data = newList.ToList();
        }
        public void func_CutLeft(string stringA, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;
            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                int nStart = temp[nIndex].IndexOf(stringA);
                
                if (nStart != -1 )
                {
                    temp[nIndex] = temp[nIndex].Remove(0, nStart);
                }
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        public void func_CutRight(string stringA, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;
            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                int nStart = temp[nIndex].IndexOf(stringA);

                int nLength = temp[nIndex].Count();
                if (nStart != -1)
                {
                    temp[nIndex] = temp[nIndex].Remove(nStart, nLength - nStart);
                }
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        /// <summary>
        /// 좌측 빈여백을 트리밍 한다. 
        /// </summary>
        /// <param name="nIndex">좌측 트리밍을 적용할 컬럼위치</param>
        public void func_Trim(int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;
            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                temp[nIndex] = temp[nIndex].Trim();
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        public void func_CropCol(string stringA, string stringB, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;
            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                int nStart = temp[nIndex].IndexOf(stringA);
                int nEnddd = temp[nIndex].IndexOf(stringB);
                int nCrop = 1 + nEnddd - nStart;
                if (nStart != -1 && nEnddd != -1)
                {
                    temp[nIndex] = temp[nIndex].Remove(nStart, nCrop);
                }
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        /// <summary>
        /// 선택된 컬럼의 기존 문자열을 신규문자열로 대체한다.
        /// </summary>
        /// <param name="stringA">기존 문자열</param>
        /// <param name="stringB">새 문자열 </param>
        /// <param name="nIndex">선택할 column</param>
        public void func_ReplaceString( string stringA, string stringB, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;

            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                temp[nIndex] = temp[nIndex].Replace(stringA, stringB);
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        /// <summary>
        /// 선택된 컬럼의 기존 문자열을 신규문자열로 대체한다.
        /// </summary>
        /// <param name="stringA">기존 문자열</param>
        /// <param name="stringB">새 문자열 </param>
        /// <param name="nIndex">선택할 column</param>
        public void func_ReplaceString(List<int> list, string stringA, string stringB, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;

            for (int row = 0; row < ROW; row++)
            {
                int findPos = list.IndexOf(row);

                if (findPos != -1)
                {
                    string[] temp = data.ElementAt(row);

                    temp[nIndex] = temp[nIndex].Replace(stringA, stringB);
                    newList.Add(temp);
                }
                else
                {
                    newList.Add(data.ElementAt(row));
                }
            }

            data.Clear();
            data = newList.ToList();
        }
        /// <summary>
        /// 선택된 Row들의 특정 column의 시작을 길이에 따라 삭제한다.
        /// 첫번째 인자 : 다수의 선택된 row data index
        /// 두번째 인자 : 해당 row의 특정 column
        /// 세번째 인자 : 삭제할 공백수
        /// </summary>
        /// <param name="nSelectedList"></param>
        /// <param name="nPos"></param>
        /// <param name="nLength"></param>
        public void func_RemoveCharacter(List<int> nSelectedList, int nPos, int nLength)
        {
            List<string[]> newList = new List<string[]>();

            nPos -= 1;

            newList.Add(GetRow(0));
            for (int row = 1; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                int nFind = nSelectedList.IndexOf(row);

                if (nFind != -1)
                {
                    if (temp[nPos].Length > nLength)
                    {
                        temp[nPos] = temp[nPos].Remove(0, nLength);
                    }
                }
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        public void func_RemoveCharacter( int nPos, int nLength)
        {
            List<string[]> newList = new List<string[]>();

            nPos -= 1;

            newList.Add(GetRow(0));
            for (int row = 1; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);

                if (temp[nPos].Length > nLength)
                {
                    temp[nPos] = temp[nPos].Remove(0, nLength);
                }
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        public void func_RemoveFrom(List<int> nSelectedList, int from, int to)
        {
            List<string[]> newList = new List<string[]>();

            from -= 1;
            to -= 1;

            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);
                string src = temp[from];
                string dest = temp[to];

                if( dest.Contains(src) == true)
                {
                    temp[from] = "";
                }

                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        /// <summary>
        /// 특정 문자열을 선택 column 에서 일괄 삭제한다.
        /// </summary>
        /// <param name="stringA">삭제할 문자열</param>
        /// <param name="nIndex">대상 column</param>
        public void func_RemoveString( string stringA, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;

            for (int row = 0; row < ROW; row++)
            {
                string[] temp = data.ElementAt(row);
                string newTemp = "";
                if (temp[nIndex].Contains(stringA))
                {
                    newTemp = temp[nIndex].Replace(stringA, "");
                    temp[nIndex] = newTemp;
                }
                
                newList.Add(temp);
            }

            data.Clear();
            data = newList.ToList();
        }
        /// <summary>
        /// 선택된 colum 의 특정 문자열을 삭제한다. 선택리스트에 기반하여
        /// </summary>
        /// <param name="list"> 선택된 rows </param>
        /// <param name="stringA">삭제할 문자열</param>
        /// <param name="nIndex">삭제대항 column</param>
        public void func_RemoveString(List<int> list, string stringA, int nIndex)
        {
            List<string[]> newList = new List<string[]>();

            nIndex -= 1;

            for (int row = 0; row < ROW; row++)
            {
                int findPos = list.IndexOf(row);

                if (findPos != -1)
                {
                    string[] temp = data.ElementAt(row);

                    temp[nIndex] = temp[nIndex].Replace(stringA, "");
                    newList.Add(temp);
                }
                else
                {
                    newList.Add(data.ElementAt(row));
                }
            }

            data.Clear();
            data = newList.ToList();
        }
        public void func_MergeColumn(int col1, int col2)
        {
            col1 -= 1;
            col2 -= 1;

            List<string[]> newList = new List<string[]>();

            for( int row = 0; row < ROW; row++ )
            {
                string column1 = data.ElementAt(row).ElementAt(col1);
                string column2 = data.ElementAt(row).ElementAt(col2);

                string[] temp = GetRow(row);
                temp[col1] = column1 + " :: " + column2;
                newList.Add(temp);
            }
            data.Clear();
            data = newList.ToList();

            DeleteCol( 1 + col2);
        }
        public void func_PositionChange(int col1, int col2)
        {
            col1 -= 1;
            col2 -= 1;

            List<string[]> newList = new List<string[]>();

            for (int row = 0; row < ROW; row++)
            {
                string column1 = data.ElementAt(row).ElementAt(col1);
                string column2 = data.ElementAt(row).ElementAt(col2);

                string[] temp = data.ElementAt(row);

                temp[col1] = column2;
                temp[col2] = column1;
                
                newList.Add(temp);
            }
            data.Clear();
            data = newList.ToList();
        }
        public bool Dump_Data(string strPath)
        {
            bool bRes = false;

            try
            {
                string strDumpFilePath = @strPath;

                FileInfo dumpFile = new FileInfo(strDumpFilePath);

                if (dumpFile.Exists)
                {
                    dumpFile.Delete(); // ensures we create a new workbook
                    dumpFile = new FileInfo(strDumpFilePath);
                }


                int nRowCount = data.Count;
                int nColCount = data.ElementAt(0).Count();


                // If any file exists in this directory having name 'Sample1.xlsx', then delete it
                using (ExcelPackage package = new ExcelPackage(dumpFile))
                {
                    // Add a worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventry");

                    // Add the headers
                    // Note: Accessing cells by row index and column index

                    for (int row = 0; row < nRowCount; row++)
                    {
                        nColCount = data.ElementAt(row).Count();
                        for (int col = 0; col < nColCount; col++)
                        {
                            string strTemp = "";

                            if (data.ElementAt(row).ElementAt(col) != null)
                            {
                                strTemp = data.ElementAt(row).ElementAt(col);
                            }

                            worksheet.Cells[1 + row, 1 + col].Value = strTemp;

                        }
                    }
                    package.Save();
                }
                bRes = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return bRes;

        }

       
        public void Dump_Data(List<string[]> data, string strPath)
        {
            string strDumpFilePath = @strPath;

            FileInfo dumpFile = new FileInfo(strDumpFilePath);

            if (dumpFile.Exists)
            {
                dumpFile.Delete(); // ensures we create a new workbook
                dumpFile = new FileInfo(strDumpFilePath);
            }


            int nRowCount = data.Count;
            int nColCount = data.ElementAt(0).Count();


            // If any file exists in this directory having name 'Sample1.xlsx', then delete it
            using (ExcelPackage package = new ExcelPackage(dumpFile))
            {
                // Add a worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventry");

                // Add the headers
                // Note: Accessing cells by row index and column index

                for (int row = 0; row < nRowCount; row++)
                {
                    for (int col = 0; col < nColCount; col++)
                    {
                        string strTemp = "";

                        if (data.ElementAt(row).ElementAt(col) != null)
                        {
                            strTemp = data.ElementAt(row).ElementAt(col);
                        }

                        worksheet.Cells[1 + row, 1 + col].Value = strTemp;

                    }
                }
                package.Save();
            }

            // save our new workbook and we are done!

            WrapperExcel.ExcuteExcel(strDumpFilePath);
        }

    }

   
}
