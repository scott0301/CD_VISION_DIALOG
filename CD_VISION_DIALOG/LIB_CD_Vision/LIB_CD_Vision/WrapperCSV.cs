using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CD_Measure
{
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream): base(stream){}

        public CsvFileWriter(string filename): base(filename){}

        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            
            //foreach (string value in row)
            for( int i = 0; i < row.Count; i++)
            {
                string value = row.ElementAt(i);

                if (value == null) value = string.Empty;

                // Add separator if this isn't the first value
                if (!firstColumn)
                {
                    builder.Append(',');
                }

                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }
                else
                {
                    builder.Append(value);
                }
                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }
    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream): base(stream){}

        public CsvFileReader(string filename): base(filename){}

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }
    public class CFileWriter : StreamWriter
    {
        public CFileWriter(Stream stream): base(stream){}

        public CFileWriter(string filename): base(filename){}

        public void WriteFile(List<string> list){foreach (string value in list){WriteLine(value);}}
    }
    public class CFileReader : StreamReader
    {
        public CFileReader(Stream stream): base(stream){}

        public CFileReader(string filename) : base(filename){}

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadFile(CsvRow row)
        {
            row.LineText = ReadLine();
            return true;
        }
    }

    public class WrapperCVS
    {
        int DATA_COUNT { get { return m_list.Count; } }
        int ROW { get { return DATA_COUNT; } }
        int COL
        {
            get
            {
                int nCount = 0;
                if (DATA_COUNT != 0)
                {
                    string[] arr = GetRow(0);
                    nCount = arr.Length;
                }
                return nCount;
            }
        }

        private List<string[]> m_list = new List<string[]>();

        private bool IsValidIndex(int nIndex)
        {
            bool bValid = true;
            if (nIndex > DATA_COUNT) bValid = false;
            return bValid;
        }

        public void RemoveAll() { m_list.Clear(); }
        public void AddData(string[] data){m_list.Add(data);}
        public string[] GetRow(int row)
        {
            if (this.IsValidIndex(row) == false) return new string[0];

            string[] arrRow = m_list.ElementAt(row).ToArray();
            return arrRow;
        }
        public string GetCol(int row, int col)
        {
            if (this.IsValidIndex(row) == false) return string.Empty;

            string[] arrRow = GetRow(row);
            string strCol = arrRow[col];
            return strCol;
        }
        public List<string[]> GetAll(){return m_list.ToList();}

        public bool ReadCSVFile(string strPath, out string msg)
        {
            bool bSucess = true;
            msg = string.Empty;

            RemoveAll();

            try
            {
                using (CsvFileReader reader = new CsvFileReader(strPath))
                {
                    CsvRow row = new CsvRow();

                    List<string> listRow = new List<string>();
                    while (reader.ReadRow(row))
                    {
                        foreach (string s in row)
                        {
                            listRow.Add(s);
                        }

                        AddData(listRow.ToArray());
                        listRow.Clear();
                    }
                }
            }
            catch (IOException ex)
            {
                msg = ex.ToString();
                bSucess = false;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                bSucess = false;
            }
            return bSucess;
        }
        public void SaveCSVFile(string strPath, List<string[]> list)
        {
            m_list = list.ToList();

            using (CsvFileWriter writer = new CsvFileWriter(strPath))
            {
                for (int i = 0; i < ROW; i++)
                {
                    CsvRow row = new CsvRow();

                    string[] temp = list.ElementAt(i);

                    for (int j = 0; j < temp.Length; j++)
                    {
                        row.Add(temp[j]);
                    }

                    if (temp.Length < COL)
                    {
                        for (int j = temp.Length; j < COL; j++)
                        {
                            row.Add("");
                        }
                    }
                    writer.WriteRow(row);
                }
            }
        }

        public void SaveFile(string strPath, List<string> list)
        {
            RemoveAll();

            for (int i = 0; i < list.Count; i++)
            {
                string[] single = new string[1];

                single[0] = list.ElementAt(i);

                AddData(single);
            }

            using (CsvFileWriter writer = new CsvFileWriter(strPath))
            {
                for (int i = 0; i < ROW; i++)
                {
                    CsvRow row = new CsvRow();

                    string[] temp = GetRow(i);

                    for (int j = 0; j < COL; j++)
                    {
                        row.Add(temp[j]);
                    }
                    writer.WriteRow(row);
                }
            }
        }




    }

  
}
