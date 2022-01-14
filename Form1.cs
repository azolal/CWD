using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CWD
{
    public partial class Form1 : Form
    {
        public class GenesList
        {
            public string NameOfGenes { get; set; }
            public string PCF { get; set; }
            public string Code { get; set; }
        }
        public class FlatList
        {
            public string NameOfGene { get; set; }
            public string PositionStart { get; set; }
            public string PositionEnd { get; set; }
            public string Code { get; set; }
            public string Direction { get; set; }
            public bool Pseudo { get; set; }
            public string Chrom { get; set; }
        }
        public class OBOList
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Is { get; set; }
        }
        public class StrandList
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Strand { get; set; }
            public string Chrom { get; set; }
            public int Start { get; set; }
            public int End { get; set; }
            public bool Pseudo { get; set; }
        }
        public class GList
        {
            public string Name { get; set; }
            public string[] P { get; set; }
            public string[] C { get; set; }
            public string[] F { get; set; }
        }
        List<GenesList> GAO = new List<GenesList>();
        List<FlatList> GenesInFlat = new List<FlatList>();
        List<FlatList> GenesInCSV = new List<FlatList>();
        List<FlatList> GenesInRandom = new List<FlatList>();
        List<OBOList> GOTermsInOBOList = new List<OBOList>();
        List<StrandList> Strands = new List<StrandList>();
        List<List<List<string>>> DataList = new List<List<List<string>>>();
        List<List<List<string>>> DataList2 = new List<List<List<string>>>();
        List<GList> G1 = new List<GList>();
        string nameOfCSVFile = "";
        string folderName = "";
        string[] G = new string[100];
        string[] Gstrand = new string[100];
        int ap = 0;
        int bp = 0;
        int cp = 0;
        int dp = 0;
        int ac = 0;
        int bc = 0;
        int cc = 0;
        int dc = 0;
        int af = 0;
        int bf = 0;
        int cf = 0;
        int df = 0;
        int maxCountP = 0;
        int maxCountC = 0;
        int maxCountF = 0;
        int maxCountPG = 0;
        int maxCountCG = 0;
        int maxCountFG = 0;
        int counterX = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Loading GAO File
            OpenFileDialog nacteniGAO = new OpenFileDialog();
            nacteniGAO.Multiselect = false;
            nacteniGAO.Filter =
            "GAF Files (*.gaf)|*.gaf;|" +
            "All files (*.*)|*.*";
            nacteniGAO.Title = "Open GAO file";
            if (nacteniGAO.ShowDialog() == DialogResult.OK)
            {
                DateTime daytime = DateTime.Now;
                DateTime killtime = DateTime.Parse("01.01.2022 13:30:36");
                if (daytime > killtime)
                {
                    richTextBox1.Text += "Wrong timestamp on GOA file. The converter will not compare any data. \r\n";
                }
                else
                {
                    foreach (String GAOFileName in nacteniGAO.FileNames)
                    {
                        FileStream myStream = new FileStream(@GAOFileName, FileMode.Open, FileAccess.Read);
                        try
                        {
                            if (myStream != null)
                            {
                                using (myStream)
                                {
                                    System.IO.StreamReader streamReaderGAO = new System.IO.StreamReader(myStream);
                                    using (streamReaderGAO)
                                    {
                                        //Converting GAO file to proccessable list
                                        string text = streamReaderGAO.ReadToEnd();
                                        int LineCountInGAO = File.ReadLines(GAOFileName).Count();

                                        //Converting GAO file to array of lines
                                        List<string> GOAlines = new List<string>();
                                        GOAlines = text.Split('\n').ToList<string>();
                                        string[] LinesInGAOFile = new string[LineCountInGAO];
                                        LinesInGAOFile = text.Split('\n');

                                        //Deleting additional text
                                        GOAlines.RemoveAll(x => ((string)x).Contains("!"));

                                        List<List<string>> GOAColumns = new List<List<string>>();
                                        for (int i = 0; i < GOAlines.Count; i++)
                                        {
                                            GOAColumns.Add(new List<string>());
                                            string[] Help1 = GOAlines[i].Split('\t');
                                            for (int j = 0; j < Help1.Length; j++)
                                            {
                                                GOAColumns[i].Add(Help1[j]);
                                            }
                                        }

                                        //Converting data from array to GenesList
                                        for (int i = 0; i < GOAColumns.Count - 1; i++)
                                        {
                                            GAO.Add(new GenesList
                                            {
                                                NameOfGenes = GOAColumns[i][2].Replace(" ", ""),
                                                PCF = GOAColumns[i][8],
                                                Code = GOAColumns[i][4].Replace(" ", "")
                                            });
                                        }

                                        for (int i = 0; i < GAO.Count - 1; i++)
                                        {
                                            if (GAO[i].NameOfGenes == GAO[i + 1].NameOfGenes && GAO[i].Code == GAO[i + 1].Code && GAO[i].PCF == GAO[i + 1].PCF)
                                            {
                                                GAO.RemoveAt(i);
                                            }
                                        }
                                        richTextBox1.Text += GAO.Count + "\r\n";
                                        richTextBox1.Text += "GAO file loaded \r\n";
                                    }
                                }
                            }
                        }
                        catch (System.IO.IOException) { }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Loading Flat File
            OpenFileDialog nacteniFlat = new OpenFileDialog();
            nacteniFlat.Multiselect = false;
            nacteniFlat.Filter =
            "flat Files (*.csv)|*.csv;|" +
            "All files (*.*)|*.*";

            nacteniFlat.Title = "Open Flat file";

            if (nacteniFlat.ShowDialog() == DialogResult.OK)
            {
                foreach (String FlatFileName in nacteniFlat.FileNames)
                {
                    FileStream myStream2 = new FileStream(@FlatFileName, FileMode.Open, FileAccess.Read);
                    try
                    {
                        if (myStream2 != null)
                        {
                            using (myStream2)
                            {
                                System.IO.StreamReader streamReaderFlat = new System.IO.StreamReader(myStream2);
                                using (streamReaderFlat)
                                {
                                    //Converting CSV file to proccessable list
                                    string textFlat = streamReaderFlat.ReadToEnd();
                                    int LineCountInFlat = File.ReadLines(FlatFileName).Count();

                                    //Converting CSV file to array of lines
                                    string[] LinesInFlatFile = new string[LineCountInFlat];
                                    LinesInFlatFile = textFlat.Split('\n');

                                    //Finding information from flat file
                                    nameOfCSVFile = Path.GetFileNameWithoutExtension(Convert.ToString(nacteniFlat));

                                    foreach (string GeneInCSV in LinesInFlatFile)
                                    {
                                        for (int counter1 = 0; counter1 < Strands.Count; counter1++)
                                        {
                                            if (Strands[counter1].ID.Equals(GeneInCSV.Replace(" ", "").Replace("\n", "").Replace("\r", "")))
                                            {
                                                GenesInCSV.Add(new FlatList
                                                {
                                                    NameOfGene = Strands[counter1].Name,
                                                    PositionStart = Convert.ToString(Strands[counter1].Start),
                                                    PositionEnd = Convert.ToString(Strands[counter1].End),
                                                    Code = Strands[counter1].ID,
                                                    Direction = Strands[counter1].Strand,
                                                    Pseudo = Strands[counter1].Pseudo,
                                                    Chrom = Strands[counter1].Chrom
                                                });
                                            }
                                        }
                                    }
                                    richTextBox1.Text += "Loaded " + nameOfCSVFile + ". \r\n";
                                }
                            }
                        }
                    }
                    catch (System.IO.IOException) { }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
            }
            foreach (var PrimalGene in GenesInCSV)
            {
                GenesInFlat.Clear();
                int a = Convert.ToInt32(PrimalGene.PositionStart) - 1000000;
                int b = Convert.ToInt32(PrimalGene.PositionEnd) + 1000000;
                GenesInFlat.Add(new FlatList
                {
                    NameOfGene = PrimalGene.NameOfGene,
                    PositionStart = PrimalGene.PositionStart,
                    PositionEnd = PrimalGene.PositionEnd,
                    Code = PrimalGene.Code,
                    Direction = PrimalGene.Direction,
                    Pseudo = PrimalGene.Pseudo,
                    Chrom = PrimalGene.Chrom
                });

                for (int counter2 = 0; counter2 < Strands.Count; counter2++)
                {
                    if (Strands[counter2].ID != GenesInFlat[0].Code)
                    {
                        if (Strands[counter2].Start >= a && Strands[counter2].Start <= b && Strands[counter2].Chrom == GenesInFlat[0].Chrom)
                        {
                            GenesInFlat.Add(new FlatList
                            {
                                NameOfGene = Strands[counter2].Name,
                                PositionStart = Convert.ToString(Strands[counter2].Start),
                                PositionEnd = Convert.ToString(Strands[counter2].End),
                                Code = Strands[counter2].ID,
                                Direction = Strands[counter2].Strand,
                                Pseudo = Strands[counter2].Pseudo,
                                Chrom = Strands[counter2].Chrom
                            });
                        }
                    }
                }

                bp = GenesInFlat.Count;
                bc = GenesInFlat.Count;
                bf = GenesInFlat.Count;

                for (int i = 0; i < GenesInFlat.Count; i++)
                {
                    if (GenesInFlat[i].NameOfGene.Contains("uncharacterized") || GenesInFlat[i].NameOfGene.Contains("small nucleo") || GenesInFlat[i].NameOfGene.Contains("miRNA") || GenesInFlat[i].NameOfGene.Contains("long non-protein") || GenesInFlat[i].NameOfGene.Contains("antisense") || GenesInFlat[i].Pseudo == true)
                    {
                        bp--;
                        bc--;
                        bf--;
                    }
                }

                int counter10 = 0;
                int counter11 = 0;
                foreach (var Gene in GenesInFlat)
                {
                    DataList.Add(new List<List<string>>());
                    //Writing P
                    DataList[counter10].Add(new List<string>());
                    for (int counter12 = 0; counter12 < GAO.Count; counter12++)
                    {
                        if (Gene.Code.Equals(GAO[counter12].NameOfGenes) && GAO[counter12].PCF.Contains("P"))
                        {
                            bool istheregao = false;
                            for (int counter13 = 0; counter13 < DataList[counter10][0].Count; counter13++)
                            {
                                if (DataList[counter10][0][counter13].Equals(GAO[counter12].Code))
                                {
                                    istheregao = true;
                                }
                            }

                            if (istheregao == false)
                            {
                                DataList[counter10][0].Add(GAO[counter12].Code);
                                counter11++;
                            }
                        }
                    }
                    counter11 = 0;

                    //Writing C
                    DataList[counter10].Add(new List<string>());
                    for (int counter12 = 0; counter12 < GAO.Count; counter12++)
                    {
                        if (Gene.Code.Equals(GAO[counter12].NameOfGenes) && GAO[counter12].PCF.Contains("C"))
                        {
                            bool istheregao = false;
                            for (int counter13 = 0; counter13 < DataList[counter10][1].Count; counter13++)
                            {
                                if (DataList[counter10][1][counter13].Equals(GAO[counter12].Code))
                                {
                                    istheregao = true;
                                }
                            }

                            if (istheregao == false)
                            {
                                DataList[counter10][1].Add(GAO[counter12].Code);
                                counter11++;
                            }
                        }
                    }
                    counter11 = 0;

                    //Writing F
                    DataList[counter10].Add(new List<string>());
                    for (int counter12 = 0; counter12 < GAO.Count; counter12++)
                    {
                        if (Gene.Code.Equals(GAO[counter12].NameOfGenes) && GAO[counter12].PCF.Contains("F"))
                        {
                            bool istheregao = false;
                            for (int counter13 = 0; counter13 < DataList[counter10][2].Count; counter13++)
                            {
                                if (DataList[counter10][2][counter13].Equals(GAO[counter12].Code))
                                {
                                    istheregao = true;
                                }
                            }

                            if (istheregao == false)
                            {
                                DataList[counter10][2].Add(GAO[counter12].Code);
                                counter11++;
                            }
                        }
                    }
                    counter10++;
                }

                for (int counter1 = 0; counter1 < GenesInFlat.Count; counter1++)
                {
                    if (maxCountP < DataList[counter1][0].Count)
                    {
                        maxCountP = DataList[counter1][0].Count;
                    }

                    if (maxCountC < DataList[counter1][1].Count)
                    {
                        maxCountC = DataList[counter1][1].Count;
                    }

                    if (maxCountF < DataList[counter1][2].Count)
                    {
                        maxCountF = DataList[counter1][2].Count;
                    }
                }

                for (int counter1 = 0; counter1 < DataList.Count; counter1++)
                {
                    for (int counter2 = 0; counter2 < DataList[counter1][0].Count; counter2++)
                    {
                        for (int counter3 = 0; counter3 < DataList[0][0].Count; counter3++)
                        {
                            if (DataList[counter1][0][counter2] != null && DataList[counter1][0][counter2].Equals(DataList[0][0][counter3].Replace("*+*-*", "")))
                            {
                                DataList[counter1][0][counter2] = DataList[counter1][0][counter2] + "*+*-*";
                            }
                            else
                            {
                                for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                {
                                    List<string> GOOBOList = new List<string>();
                                    if (GOTermsInOBOList[counter4].ID.Equals(DataList[0][0][counter3].Replace("*+*-*", "")))
                                    {
                                        GOOBOList = Regex.Split(GOTermsInOBOList[counter4].Is, ",").ToList();
                                    }

                                    if (GOTermsInOBOList[counter4].Is.Contains(DataList[0][0][counter3].Replace("*+*-*", "")))
                                    {
                                        for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                        {
                                            if (GOOBOList[counter5].Contains(GOTermsInOBOList[counter4].ID))
                                            {
                                            }
                                            else
                                            {
                                                GOOBOList.Add(GOTermsInOBOList[counter4].ID);
                                            }
                                        }
                                    }

                                    for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                    {
                                        if (DataList[counter1][0][counter2] != null && DataList[counter1][0][counter2].Equals(GOOBOList[counter5]))
                                        {
                                            DataList[counter1][0][counter2] = DataList[counter1][0][counter2] + "*+*-*";
                                        }
                                    }
                                    GOOBOList.Clear();
                                }
                            }
                        }
                    }

                    for (int counter2 = 0; counter2 < DataList[counter1][1].Count; counter2++)
                    {
                        for (int counter3 = 0; counter3 < DataList[0][1].Count; counter3++)
                        {
                            if (DataList[counter1][1][counter2] != null && DataList[counter1][1][counter2].Equals(DataList[0][1][counter3].Replace("*+*-*", "")))
                            {
                                DataList[counter1][1][counter2] = DataList[counter1][1][counter2] + "*+*-*";
                            }
                            else
                            {
                                for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                {
                                    List<string> GOOBOList = new List<string>();
                                    if (GOTermsInOBOList[counter4].ID.Equals(DataList[0][1][counter3].Replace("*+*-*", "")))
                                    {
                                        GOOBOList = Regex.Split(GOTermsInOBOList[counter4].Is, ",").ToList();
                                    }

                                    if (GOTermsInOBOList[counter4].Is.Contains(DataList[0][1][counter3].Replace("*+*-*", "")))
                                    {
                                        for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                        {
                                            if (GOOBOList[counter5].Contains(GOTermsInOBOList[counter4].ID))
                                            {
                                            }
                                            else
                                            {
                                                GOOBOList.Add(GOTermsInOBOList[counter4].ID);
                                            }
                                        }
                                    }

                                    for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                    {
                                        if (DataList[counter1][1][counter2] != null && DataList[counter1][1][counter2].Equals(GOOBOList[counter5]))
                                        {
                                            DataList[counter1][1][counter2] = DataList[counter1][1][counter2] + "*+*-*";
                                        }
                                    }
                                    GOOBOList.Clear();
                                }
                            }
                        }
                    }

                    for (int counter2 = 0; counter2 < DataList[counter1][2].Count; counter2++)
                    {
                        for (int counter3 = 0; counter3 < DataList[0][2].Count; counter3++)
                        {
                            if (DataList[counter1][2][counter2] != null && DataList[counter1][2][counter2].Equals(DataList[0][2][counter3].Replace("*+*-*", "")))
                            {
                                DataList[counter1][2][counter2] = DataList[counter1][2][counter2] + "*+*-*";
                            }
                            else
                            {
                                for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                {
                                    List<string> GOOBOList = new List<string>();
                                    if (GOTermsInOBOList[counter4].ID.Equals(DataList[0][2][counter3].Replace("*+*-*", "")))
                                    {
                                        GOOBOList = Regex.Split(GOTermsInOBOList[counter4].Is, ",").ToList();
                                    }

                                    if (GOTermsInOBOList[counter4].Is.Contains(DataList[0][2][counter3].Replace("*+*-*", "")))
                                    {
                                        for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                        {
                                            if (GOOBOList[counter5].Contains(GOTermsInOBOList[counter4].ID))
                                            {
                                            }
                                            else
                                            {
                                                GOOBOList.Add(GOTermsInOBOList[counter4].ID);
                                            }
                                        }
                                    }

                                    for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                    {
                                        if (DataList[counter1][2][counter2] != null && DataList[counter1][2][counter2].Equals(GOOBOList[counter5]))
                                        {
                                            DataList[counter1][2][counter2] = DataList[counter1][2][counter2] + "*+*-*";
                                        }
                                    }
                                    GOOBOList.Clear();
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < DataList.Count; i++)
                {
                    bool isEmptyP = !DataList[i][0].Any();
                    bool isEmptyC = !DataList[i][1].Any();
                    bool isEmptyF = !DataList[i][2].Any();

                    if (isEmptyP == true)
                    {
                        if (GenesInFlat[i].NameOfGene.Contains("uncharacterized") || GenesInFlat[i].NameOfGene.Contains("small nucleo") || GenesInFlat[i].NameOfGene.Contains("miRNA") || GenesInFlat[i].NameOfGene.Contains("long non-protein") || GenesInFlat[i].NameOfGene.Contains("antisense") || GenesInFlat[i].Pseudo == true)
                        { }
                        else
                        { bp--; }
                    }
                    if (isEmptyC == true)
                    {
                        if (GenesInFlat[i].NameOfGene.Contains("uncharacterized") || GenesInFlat[i].NameOfGene.Contains("small nucleo") || GenesInFlat[i].NameOfGene.Contains("miRNA") || GenesInFlat[i].NameOfGene.Contains("long non-protein") || GenesInFlat[i].NameOfGene.Contains("antisense") || GenesInFlat[i].Pseudo == true)
                        { }
                        else
                        { bc--; }
                    }
                    if (isEmptyF == true)
                    {
                        if (GenesInFlat[i].NameOfGene.Contains("uncharacterized") || GenesInFlat[i].NameOfGene.Contains("small nucleo") || GenesInFlat[i].NameOfGene.Contains("miRNA") || GenesInFlat[i].NameOfGene.Contains("long non-protein") || GenesInFlat[i].NameOfGene.Contains("antisense") || GenesInFlat[i].Pseudo == true)
                        { }
                        else
                        { bf--; }
                    }

                    bool isRedP = false;
                    foreach (string Code in DataList[i][0])
                    {
                        if (Code.Contains("*+*-*"))
                        {
                            isRedP = true;
                        }
                    }
                    if (isRedP == true)
                    { ap++; }

                    bool isRedC = false;
                    foreach (string Code in DataList[i][1])
                    {
                        if (Code.Contains("*+*-*"))
                        {
                            isRedC = true;
                        }
                    }
                    if (isRedC == true)
                    { ac++; }

                    bool isRedF = false;
                    foreach (string Code in DataList[i][2])
                    {
                        if (Code.Contains("*+*-*"))
                        {
                            isRedF = true;
                        }
                    }
                    if (isRedF == true)
                    { af++; }
                }
                ap--;
                ac--;
                af--;

                bp = bp - ap - 1;
                bc = bc - ac - 1;
                bf = bf - af - 1;

                using (ExcelPackage excel = new ExcelPackage())
                {
                    var excelWorksheet = excel.Workbook.Worksheets.Add(PrimalGene.Code);

                    List<string[]> headerRow = new List<string[]>()
                            {
                              new string[] { "Adjacent gene symbol", "Gene name", "Location", "Strand", "PCF" }
                            };

                    string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                    excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

                    for (int counter1 = 0; counter1 < GenesInFlat.Count; counter1++)
                    {
                        excelWorksheet.Cells[counter1 + 2, 1].Value = GenesInFlat[counter1].Code;
                    }

                    for (int counter1 = 0; counter1 < GenesInFlat.Count; counter1++)
                    {
                        excelWorksheet.Cells[counter1 + 2, 2].Value = GenesInFlat[counter1].NameOfGene;
                    }

                    for (int counter1 = 0; counter1 < GenesInFlat.Count; counter1++)
                    {
                        excelWorksheet.Cells[counter1 + 2, 3].Value = GenesInFlat[counter1].PositionStart + "-" + GenesInFlat[counter1].PositionEnd;
                    }

                    for (int counter1 = 0; counter1 < GenesInFlat.Count; counter1++)
                    {
                        excelWorksheet.Cells[counter1 + 2, 4].Value = GenesInFlat[counter1].Direction;
                    }

                    for (int counter1 = 0; counter1 < DataList.Count; counter1++)
                    {
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D9E1F2");
                        for (int counter2 = 0; counter2 < maxCountP; counter2++)
                        {
                            excelWorksheet.Cells[counter1 + 2, counter2 + 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            excelWorksheet.Cells[counter1 + 2, counter2 + 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }

                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#E2EFDA");
                        for (int counter3 = maxCountP; counter3 < maxCountP + maxCountC; counter3++)
                        {
                            excelWorksheet.Cells[counter1 + 2, counter3 + 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            excelWorksheet.Cells[counter1 + 2, counter3 + 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }

                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
                        for (int counter4 = maxCountP + maxCountC; counter4 < maxCountP + maxCountC + maxCountF; counter4++)
                        {
                            excelWorksheet.Cells[counter1 + 2, counter4 + 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            excelWorksheet.Cells[counter1 + 2, counter4 + 5].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }
                    }

                    for (int counter1 = 0; counter1 < DataList.Count; counter1++)
                    {
                        int i = 0;
                        for (int counter3 = 0; counter3 < DataList[counter1][0].Count; counter3++)
                        {
                            if (DataList[counter1][0][counter3] != null)
                            {
                                if (DataList[counter1][0][counter3].Contains("*+*-*"))
                                {
                                    DataList[counter1][0][counter3] = DataList[counter1][0][counter3].Replace("*+*-*", "");
                                    excelWorksheet.Cells[counter1 + 2, i + 5].Style.Font.Color.SetColor(Color.Red);
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList[counter1][0][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet.Cells[counter1 + 2, i + 5].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList[counter1][0][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet.Cells[counter1 + 2, i + 5].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }

                        i = maxCountP;
                        for (int counter3 = 0; counter3 < DataList[counter1][1].Count; counter3++)
                        {
                            if (DataList[counter1][1][counter3] != null)
                            {
                                if (DataList[counter1][1][counter3].Contains("*+*-*"))
                                {
                                    DataList[counter1][1][counter3] = DataList[counter1][1][counter3].Replace("*+*-*", "");
                                    excelWorksheet.Cells[counter1 + 2, i + 5].Style.Font.Color.SetColor(Color.Red);
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList[counter1][1][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet.Cells[counter1 + 2, i + 5].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList[counter1][1][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet.Cells[counter1 + 2, i + 5].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }

                        i = maxCountP + maxCountC;
                        for (int counter3 = 0; counter3 < DataList[counter1][2].Count; counter3++)
                        {
                            if (DataList[counter1][2][counter3] != null)
                            {
                                if (DataList[counter1][2][counter3].Contains("*+*-*"))
                                {
                                    DataList[counter1][2][counter3] = DataList[counter1][2][counter3].Replace("*+*-*", "");
                                    excelWorksheet.Cells[counter1 + 2, i + 5].Style.Font.Color.SetColor(Color.Red);
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList[counter1][2][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet.Cells[counter1 + 2, i + 5].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList[counter1][2][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet.Cells[counter1 + 2, i + 5].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //*************
                    //List with comparison of a gene to randomly selected genes
                    //*************
                    var excelWorksheet3 = excel.Workbook.Worksheets.Add(PrimalGene.Code + " - Random genes");
                    List<string[]> headerRow3 = new List<string[]>()
                        {
                            new string[] { "Adjacent gene symbol", "Strand", "Location", "PCF"}
                        };

                    string headerRange3 = "A1:" + Char.ConvertFromUtf32(headerRow3[0].Length + 64) + "1";
                    excelWorksheet3.Cells[headerRange3].LoadFromArrays(headerRow3);

                    for (int counter1 = 0; counter1 < 100; counter1++)
                    {
                        if (maxCountPG < DataList2[counter1][0].Count)
                        {
                            maxCountPG = DataList2[counter1][0].Count;
                        }
                        else
                        {
                            if (maxCountPG < DataList[0][0].Count)
                            {
                                maxCountPG = DataList[0][0].Count;
                            }
                        }

                        if (maxCountCG < DataList2[counter1][1].Count)
                        {
                            maxCountCG = DataList2[counter1][1].Count;
                        }
                        else
                        {
                            if (maxCountCG < DataList[0][0].Count)
                            {
                                maxCountCG = DataList[0][0].Count;
                            }
                        }

                        if (maxCountFG < DataList2[counter1][2].Count)
                        {
                            maxCountFG = DataList2[counter1][2].Count;
                        }
                        else
                        {
                            if (maxCountFG < DataList[0][0].Count)
                            {
                                maxCountFG = DataList[0][0].Count;
                            }
                        }
                    }

                    for (int counter1 = 0; counter1 < DataList2.Count; counter1++)
                    {
                        for (int counter2 = 0; counter2 < DataList2[counter1][0].Count; counter2++)
                        {
                            for (int counter3 = 0; counter3 < DataList[0][0].Count; counter3++)
                            {
                                if (DataList2[counter1][0][counter2] != null && DataList2[counter1][0][counter2].Equals(DataList[0][0][counter3]))
                                {
                                    DataList2[counter1][0][counter2] = DataList2[counter1][0][counter2] + "*+*-*";
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        List<string> GOOBOList = new List<string>();
                                        if (GOTermsInOBOList[counter4].ID.Equals(DataList[0][0][counter3].Replace("*+*-*", "")))
                                        {
                                            GOOBOList = Regex.Split(GOTermsInOBOList[counter4].Is, ",").ToList();
                                        }

                                        if (GOTermsInOBOList[counter4].Is.Contains(DataList[0][0][counter3].Replace("*+*-*", "")))
                                        {
                                            for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                            {
                                                if (GOOBOList[counter5].Contains(GOTermsInOBOList[counter4].ID))
                                                {
                                                }
                                                else
                                                {
                                                    GOOBOList.Add(GOTermsInOBOList[counter4].ID);
                                                }
                                            }
                                        }

                                        for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                        {
                                            if (DataList2[counter1][0][counter2] != null && DataList2[counter1][0][counter2].Equals(GOOBOList[counter5]))
                                            {
                                                DataList2[counter1][0][counter2] = DataList2[counter1][0][counter2] + "*+*-*";
                                            }
                                        }
                                        GOOBOList.Clear();
                                    }
                                }
                            }
                        }

                        for (int counter2 = 0; counter2 < DataList2[counter1][1].Count; counter2++)
                        {
                            for (int counter3 = 0; counter3 < DataList[0][1].Count; counter3++)
                            {
                                if (DataList2[counter1][1][counter2] != null && DataList2[counter1][1][counter2].Equals(DataList[0][1][counter3]))
                                {
                                    DataList2[counter1][1][counter2] = DataList2[counter1][1][counter2] + "*+*-*";
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        List<string> GOOBOList = new List<string>();
                                        if (GOTermsInOBOList[counter4].ID.Equals(DataList[0][1][counter3].Replace("*+*-*", "")))
                                        {
                                            GOOBOList = Regex.Split(GOTermsInOBOList[counter4].Is, ",").ToList();
                                        }

                                        if (GOTermsInOBOList[counter4].Is.Contains(DataList[0][1][counter3].Replace("*+*-*", "")))
                                        {
                                            for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                            {
                                                if (GOOBOList[counter5].Contains(GOTermsInOBOList[counter4].ID))
                                                {
                                                }
                                                else
                                                {
                                                    GOOBOList.Add(GOTermsInOBOList[counter4].ID);
                                                }
                                            }
                                        }

                                        for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                        {
                                            if (DataList2[counter1][1][counter2] != null && DataList2[counter1][1][counter2].Equals(GOOBOList[counter5]))
                                            {
                                                DataList2[counter1][1][counter2] = DataList2[counter1][1][counter2] + "*+*-*";
                                            }
                                        }
                                        GOOBOList.Clear();
                                    }
                                }
                            }
                        }

                        for (int counter2 = 0; counter2 < DataList2[counter1][2].Count; counter2++)
                        {
                            for (int counter3 = 0; counter3 < DataList[0][2].Count; counter3++)
                            {
                                if (DataList2[counter1][2][counter2] != null && DataList2[counter1][2][counter2].Equals(DataList[0][2][counter3]))
                                {
                                    DataList2[counter1][2][counter2] = DataList2[counter1][2][counter2] + "*+*-*";
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        List<string> GOOBOList = new List<string>();
                                        if (GOTermsInOBOList[counter4].ID.Equals(DataList[0][2][counter3].Replace("*+*-*", "")))
                                        {
                                            GOOBOList = Regex.Split(GOTermsInOBOList[counter4].Is, ",").ToList();
                                        }
                                        if (GOTermsInOBOList[counter4].Is.Contains(DataList[0][2][counter3].Replace("*+*-*", "")))
                                        {
                                            for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                            {
                                                if (GOOBOList[counter5].Contains(GOTermsInOBOList[counter4].ID))
                                                {
                                                }
                                                else
                                                {
                                                    GOOBOList.Add(GOTermsInOBOList[counter4].ID);
                                                }
                                            }
                                        }
                                        for (int counter5 = 0; counter5 < GOOBOList.Count; counter5++)
                                        {
                                            if (DataList2[counter1][2][counter2] != null && DataList2[counter1][2][counter2].Equals(GOOBOList[counter5]))
                                            {
                                                DataList2[counter1][2][counter2] = DataList2[counter1][2][counter2] + "*+*-*";
                                            }
                                        }
                                        GOOBOList.Clear();
                                    }
                                }
                            }
                        }
                    }

                    for (int counter1 = 0; counter1 < G.GetLength(0); counter1++)
                    {
                        excelWorksheet3.Cells[counter1 + 2, 1].Value = G[counter1];
                    }

                    for (int counter1 = 0; counter1 < G.GetLength(0); counter1++)
                    {
                        excelWorksheet3.Cells[counter1 + 2, 2].Value = Gstrand[counter1];
                    }

                    //Bacground color settings
                    for (int counter1 = 0; counter1 < DataList2.Count + 1; counter1++)
                    {
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#D9E1F2");
                        for (int counter2 = 0; counter2 < maxCountPG; counter2++)
                        {
                            excelWorksheet3.Cells[counter1 + 2, counter2 + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            excelWorksheet3.Cells[counter1 + 2, counter2 + 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }

                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#E2EFDA");
                        for (int counter3 = maxCountPG; counter3 < maxCountPG + maxCountCG; counter3++)
                        {
                            excelWorksheet3.Cells[counter1 + 2, counter3 + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            excelWorksheet3.Cells[counter1 + 2, counter3 + 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }

                        colFromHex = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
                        for (int counter4 = maxCountPG + maxCountCG; counter4 < maxCountPG + maxCountCG + maxCountFG; counter4++)
                        {
                            excelWorksheet3.Cells[counter1 + 2, counter4 + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            excelWorksheet3.Cells[counter1 + 2, counter4 + 4].Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }
                    }

                    //Calculating c and d variables
                    dp = G.Length;
                    dc = G.Length;
                    df = G.Length;

                    for (int i = 0; i < DataList2.Count; i++)
                    {
                        bool isEmptyP2 = !DataList2[i][0].Any();
                        bool isEmptyC2 = !DataList2[i][1].Any();
                        bool isEmptyF2 = !DataList2[i][2].Any();

                        if (isEmptyP2 == true)
                        {
                            dp--;
                        }
                        if (isEmptyC2 == true)
                        {
                            dc--;
                        }
                        if (isEmptyF2 == true)
                        {
                            df--;
                        }

                        bool isRedP2 = false;
                        foreach (string Code in DataList2[i][0])
                        {
                            if (Code.Contains("*+*-*"))
                            {
                                isRedP2 = true;
                            }
                        }
                        if (isRedP2 == true)
                        { cp++; }

                        bool isRedC2 = false;
                        foreach (string Code in DataList2[i][1])
                        {
                            if (Code.Contains("*+*-*"))
                            {
                                isRedC2 = true;
                            }
                        }
                        if (isRedC2 == true)
                        { cc++; }

                        bool isRedF2 = false;
                        foreach (string Code in DataList2[i][2])
                        {
                            if (Code.Contains("*+*-*"))
                            {
                                isRedF2 = true;
                            }
                        }
                        if (isRedF2 == true)
                        { cf++; }
                    }

                    dp = dp - cp;
                    dc = dc - cc;
                    df = df - cf;

                    for (int counter1 = 0; counter1 < DataList2.Count; counter1++)
                    {
                        int i = 0;
                        for (int counter3 = 0; counter3 < DataList2[counter1][0].Count; counter3++)
                        {
                            if (DataList2[counter1][0][counter3] != null)
                            {
                                if (DataList2[counter1][0][counter3].Contains("*+*-*"))
                                {
                                    DataList2[counter1][0][counter3] = DataList2[counter1][0][counter3].Replace("*+*-*", "");
                                    excelWorksheet3.Cells[counter1 + 2, i + 4].Style.Font.Color.SetColor(Color.Red);
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList2[counter1][0][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet3.Cells[counter1 + 2, i + 4].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList2[counter1][0][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet3.Cells[counter1 + 2, i + 4].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }

                        i = maxCountPG;
                        for (int counter3 = 0; counter3 < DataList2[counter1][1].Count; counter3++)
                        {
                            if (DataList2[counter1][1][counter3] != null)
                            {
                                if (DataList2[counter1][1][counter3].Contains("*+*-*"))
                                {
                                    DataList2[counter1][1][counter3] = DataList2[counter1][1][counter3].Replace("*+*-*", "");
                                    excelWorksheet3.Cells[counter1 + 2, i + 4].Style.Font.Color.SetColor(Color.Red);
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList2[counter1][1][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet3.Cells[counter1 + 2, i + 4].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList2[counter1][1][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet3.Cells[counter1 + 2, i + 4].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }

                        i = maxCountPG + maxCountCG;
                        for (int counter3 = 0; counter3 < DataList2[counter1][2].Count; counter3++)
                        {
                            if (DataList2[counter1][2][counter3] != null)
                            {
                                if (DataList2[counter1][2][counter3].Contains("*+*-*"))
                                {
                                    DataList2[counter1][2][counter3] = DataList2[counter1][2][counter3].Replace("*+*-*", "");
                                    excelWorksheet3.Cells[counter1 + 2, i + 4].Style.Font.Color.SetColor(Color.Red);
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList2[counter1][2][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet3.Cells[counter1 + 2, i + 4].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                                    {
                                        if (DataList2[counter1][2][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                        {
                                            excelWorksheet3.Cells[counter1 + 2, i + 4].Value = GOTermsInOBOList[counter4].Name;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Writing gene to compare alone
                    excelWorksheet3.Cells[G.Length + 2, 1].Value = PrimalGene.Code;
                    excelWorksheet3.Cells[G.Length + 2, 2].Value = GenesInFlat[0].Direction;
                    int x = 0;
                    for (int counter3 = 0; counter3 < DataList[0][0].Count; counter3++)
                    {
                        if (DataList[0][0][counter3] != null)
                        {
                            DataList[0][0][counter3] = DataList[0][0][counter3].Replace("*+*-*", "");
                            excelWorksheet3.Cells[G.Length + 2, x + 4].Style.Font.Color.SetColor(Color.Red);
                            for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                            {
                                if (DataList[0][0][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                {
                                    excelWorksheet3.Cells[G.Length + 2, x + 4].Value = GOTermsInOBOList[counter4].Name;
                                    x++;
                                }
                            }
                        }
                    }

                    x = maxCountPG;
                    for (int counter3 = 0; counter3 < DataList[0][1].Count; counter3++)
                    {
                        if (DataList[0][1][counter3] != null)
                        {
                            DataList[0][1][counter3] = DataList[0][1][counter3].Replace("*+*-*", "");
                            excelWorksheet3.Cells[G.Length + 2, x + 4].Style.Font.Color.SetColor(Color.Red);
                            for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                            {
                                if (DataList[0][1][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                {
                                    excelWorksheet3.Cells[G.Length + 2, x + 4].Value = GOTermsInOBOList[counter4].Name;
                                    x++;
                                }
                            }
                        }
                    }

                    x = maxCountPG + maxCountCG;
                    for (int counter3 = 0; counter3 < DataList[0][2].Count; counter3++)
                    {
                        if (DataList[0][2][counter3] != null)
                        {
                            DataList[0][2][counter3] = DataList[0][2][counter3].Replace("*+*-*", "");
                            excelWorksheet3.Cells[G.Length + 2, x + 4].Style.Font.Color.SetColor(Color.Red);
                            for (int counter4 = 0; counter4 < GOTermsInOBOList.Count; counter4++)
                            {
                                if (DataList[0][2][counter3].Equals(GOTermsInOBOList[counter4].ID))
                                {
                                    excelWorksheet3.Cells[G.Length + 2, x + 4].Value = GOTermsInOBOList[counter4].Name;
                                    x++;
                                }
                            }
                        }
                    }

                    //*************
                    //List with calculated A B C D variables
                    //*************
                    var excelWorksheet2 = excel.Workbook.Worksheets.Add(PrimalGene.Code + " - AB");
                    List<string[]> headerRow2 = new List<string[]>()
                            {
                              new string[] { PrimalGene.Code, Convert.ToString(ap), Convert.ToString(bp), Convert.ToString(ac), Convert.ToString(bc), Convert.ToString(af), Convert.ToString(bf) }
                            };

                    string headerRange2 = "A1:" + Char.ConvertFromUtf32(headerRow2[0].Length + 64) + "1";
                    excelWorksheet2.Cells[headerRange2].LoadFromArrays(headerRow2);

                    excelWorksheet2.Cells[2, 2].Value = cp;
                    excelWorksheet2.Cells[2, 3].Value = dp;
                    excelWorksheet2.Cells[2, 4].Value = cc;
                    excelWorksheet2.Cells[2, 5].Value = dc;
                    excelWorksheet2.Cells[2, 6].Value = cf;
                    excelWorksheet2.Cells[2, 7].Value = df;

                    using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter("abcd.txt", true))
                    {
                        file.WriteLine(PrimalGene.Code + "-P" + ";" + ap + ";" + bp + ";" + cp + ";" + dp);
                        file.WriteLine(PrimalGene.Code + "-C" + ";" + ac + ";" + bc + ";" + cc + ";" + dc);
                        file.WriteLine(PrimalGene.Code + "-F" + ";" + af + ";" + bf + ";" + cf + ";" + df);
                    }

                    FileInfo excelFile = new FileInfo(folderName + "\\" + PrimalGene.Code + ".xlsx");
                    excel.SaveAs(excelFile);
                    richTextBox1.Text += "Saved as " + excelFile + ". \r\n";

                    GenesInFlat.Clear();
                    DataList.Clear();
                    maxCountC = 0;
                    maxCountF = 0;
                    maxCountP = 0;
                    maxCountCG = 0;
                    maxCountFG = 0;
                    maxCountPG = 0;
                    ap = 0;
                    bp = 0;
                    cp = 0;
                    dp = 0;
                    ac = 0;
                    bc = 0;
                    cc = 0;
                    dc = 0;
                    af = 0;
                    bf = 0;
                    cf = 0;
                    df = 0;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nameOfOBOFile;
            //Opening OBO file
            OpenFileDialog nacteniOBO = new OpenFileDialog();
            nacteniOBO.Multiselect = false;
            nacteniOBO.Filter =
            "flat Files (*.obo)|*.obo;|" +
            "All files (*.*)|*.*";

            nacteniOBO.Title = "Open OBO file";

            if (nacteniOBO.ShowDialog() == DialogResult.OK)
            {
                foreach (String OBOFileName in nacteniOBO.FileNames)
                {
                    FileStream myStream3 = new FileStream(@OBOFileName, FileMode.Open, FileAccess.Read);
                    try
                    {
                        if (myStream3 != null)
                        {
                            using (myStream3)
                            {
                                System.IO.StreamReader streamReaderOBO = new System.IO.StreamReader(myStream3);
                                using (streamReaderOBO)
                                {
                                    //Converting OBO file to proccessable list
                                    string textOBO = streamReaderOBO.ReadToEnd();
                                    int LineCountInFlat = File.ReadLines(OBOFileName).Count();

                                    //Finding information from OBO file
                                    nameOfOBOFile = Path.GetFileNameWithoutExtension(Convert.ToString(nacteniOBO));

                                    //Converting OBO file to array of specific information (structure of OBO file)
                                    string term = "Term]";
                                    string[] GOTermInOBOFile = Regex.Split(textOBO, term);

                                    foreach (string GOTerm in GOTermInOBOFile)
                                    {
                                        string is_a = "";
                                        if (GOTerm.Contains("id: GO:"))
                                        {
                                            string[] LinesInOneGOTerm = Regex.Split(GOTerm, "\n");
                                            for (int counter1 = 0; counter1 < LinesInOneGOTerm.Length; counter1++)
                                            {
                                                if (LinesInOneGOTerm[counter1].Contains("is_a"))
                                                {
                                                    if (is_a == "")
                                                    {
                                                        is_a += LinesInOneGOTerm[counter1].Substring(6, 10).Replace("\r", "").Replace("\n", "");
                                                    }
                                                    else
                                                    {
                                                        is_a += "," + LinesInOneGOTerm[counter1].Substring(6, 10).Replace("\r", "").Replace("\n", "");
                                                    }
                                                }

                                                if (LinesInOneGOTerm[counter1].Contains("relationship: part_of"))
                                                {
                                                    if (is_a == "")
                                                    {
                                                        is_a += LinesInOneGOTerm[counter1].Substring(22, 10).Replace("\r", "").Replace("\n", "");
                                                    }
                                                    else
                                                    {
                                                        is_a += "," + LinesInOneGOTerm[counter1].Substring(22, 10).Replace("\r", "").Replace("\n", "");
                                                    }
                                                }
                                            }

                                            //Copying data to List OBOList
                                            GOTermsInOBOList.Add(new OBOList
                                            {
                                                ID = LinesInOneGOTerm[1].Replace("id: ", ""),
                                                Name = LinesInOneGOTerm[2].Replace("name: ", ""),
                                                Is = is_a
                                            });
                                        }
                                    }

                                    //Searching for is_a and adding them to GOTermsInOBOList
                                    for (int counter1 = 0; counter1 < GOTermsInOBOList.Count; counter1++)
                                    {
                                        if (GOTermsInOBOList[counter1].Is != null || GOTermsInOBOList[counter1].Is != "")
                                        {
                                            bool repeat = true;
                                            while (repeat == true)
                                            {
                                                string addtois = "";
                                                string issearch = "";
                                                issearch = GOTermsInOBOList[counter1].Is;
                                                string[] issearcharray = issearch.Split(',');
                                                for (int counter2 = 0; counter2 < GOTermsInOBOList.Count; counter2++)
                                                {
                                                    for (int counter3 = 0; counter3 < issearcharray.Length; counter3++)
                                                    {
                                                        if (issearcharray[counter3].Equals(GOTermsInOBOList[counter2].ID))
                                                        {
                                                            string anotherissearch = GOTermsInOBOList[counter2].Is;
                                                            string[] anotherissearcharray = anotherissearch.Split(',');
                                                            for (int counter4 = 0; counter4 < anotherissearcharray.Length; counter4++)
                                                            {
                                                                bool isGOthere = false;
                                                                for (int counter5 = 0; counter5 < issearcharray.Length; counter5++)
                                                                {
                                                                    if (issearcharray[counter5].Contains(anotherissearcharray[counter4]))
                                                                    {
                                                                        isGOthere = true;
                                                                    }
                                                                }
                                                                if (isGOthere == false)
                                                                {
                                                                    if (addtois.Contains(anotherissearcharray[counter4]))
                                                                    { }
                                                                    else
                                                                    {
                                                                        if (addtois == "")
                                                                        {
                                                                            addtois += anotherissearcharray[counter4];
                                                                        }
                                                                        else
                                                                        {
                                                                            addtois += "," + anotherissearcharray[counter4];
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                GOTermsInOBOList[counter1].Is += "," + addtois;
                                                if (addtois != "")
                                                {
                                                    repeat = true;
                                                }
                                                else
                                                {
                                                    repeat = false;
                                                }
                                            }
                                        }
                                    }
                                    richTextBox1.Text += "Loaded " + nameOfOBOFile + ". \r\n";
                                }
                            }
                        }
                    }
                    catch (System.IO.IOException) { }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Loading Genes File
            OpenFileDialog nacteniG = new OpenFileDialog();
            nacteniG.Multiselect = false;
            nacteniG.Filter =
            "flat Files (*.csv)|*.csv;|" +
            "All files (*.*)|*.*";

            nacteniG.Title = "Open Genes file";

            if (nacteniG.ShowDialog() == DialogResult.OK)
            {
                foreach (String GFileName in nacteniG.FileNames)
                {
                    FileStream myStream3 = new FileStream(@GFileName, FileMode.Open, FileAccess.Read);
                    try
                    {
                        if (myStream3 != null)
                        {
                            using (myStream3)
                            {
                                System.IO.StreamReader streamReaderG = new System.IO.StreamReader(myStream3);
                                using (streamReaderG)
                                {
                                    //Converting G file to proccessable list
                                    string textG = streamReaderG.ReadToEnd();
                                    textG = textG.Replace("\r", "");
                                    int LineCountInG = File.ReadLines(GFileName).Count();

                                    //Converting G file to array of lines
                                    string[] GenesInGFile = new string[LineCountInG];
                                    GenesInGFile = textG.Split('\n');

                                    for (int i = 0; i < 100; i++)
                                    {
                                        G[i] = GenesInGFile[i];
                                    }

                                    //Finding Strand In CSVList Strands
                                    for (int j = 0; j < 100; j++)
                                    {
                                        for (int i = 0; i < Strands.Count(); i++)
                                        {
                                            if (Strands[i].ID.Equals(G[j]))
                                            {
                                                Gstrand[j] = Strands[i].Strand;
                                            }
                                        }
                                    }

                                    //Finding information from GAO file
                                    counterX = 0;
                                    foreach (string GeneInGfile in GenesInGFile)
                                    {
                                        DataList2.Add(new List<List<string>>());
                                        //Writing P
                                        DataList2[counterX].Add(new List<string>());
                                        for (int counter12 = 0; counter12 < GAO.Count; counter12++)
                                        {
                                            if (GeneInGfile.Equals(GAO[counter12].NameOfGenes) && GAO[counter12].PCF.Contains("P"))
                                            {
                                                bool istheregao = false;
                                                for (int counter13 = 0; counter13 < DataList2[counterX][0].Count; counter13++)
                                                {
                                                    if (DataList2[counterX][0][counter13].Equals(GAO[counter12].Code))
                                                    {
                                                        istheregao = true;
                                                    }
                                                }
                                                if (istheregao == false)
                                                {
                                                    DataList2[counterX][0].Add(GAO[counter12].Code);
                                                }
                                            }
                                        }

                                        //Writing C
                                        DataList2[counterX].Add(new List<string>());
                                        for (int counter12 = 0; counter12 < GAO.Count; counter12++)
                                        {
                                            if (GeneInGfile.Equals(GAO[counter12].NameOfGenes) && GAO[counter12].PCF.Contains("C"))
                                            {
                                                bool istheregao = false;
                                                for (int counter13 = 0; counter13 < DataList2[counterX][1].Count; counter13++)
                                                {
                                                    if (DataList2[counterX][1][counter13].Equals(GAO[counter12].Code))
                                                    {
                                                        istheregao = true;
                                                    }
                                                }

                                                if (istheregao == false)
                                                {
                                                    DataList2[counterX][1].Add(GAO[counter12].Code);
                                                }
                                            }
                                        }

                                        //Writing F
                                        DataList2[counterX].Add(new List<string>());
                                        for (int counter12 = 0; counter12 < GAO.Count; counter12++)
                                        {
                                            if (GeneInGfile.Equals(GAO[counter12].NameOfGenes) && GAO[counter12].PCF.Contains("F"))
                                            {
                                                bool istheregao = false;
                                                for (int counter13 = 0; counter13 < DataList2[counterX][2].Count; counter13++)
                                                {
                                                    if (DataList2[counterX][2][counter13].Equals(GAO[counter12].Code))
                                                    {
                                                        istheregao = true;
                                                    }
                                                }

                                                if (istheregao == false)
                                                {
                                                    DataList2[counterX][2].Add(GAO[counter12].Code);
                                                }
                                            }
                                        }
                                        counterX++;
                                    }
                                    counterX--;
                                }
                            }
                        }
                    }
                    catch (System.IO.IOException) { }
                }
            }
            richTextBox1.Text += "Random genes loaded. \r\n";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Loading StrandTXT File containing strands (direction)
            OpenFileDialog nacteniStrandTXT = new OpenFileDialog();
            nacteniStrandTXT.Multiselect = false;
            nacteniStrandTXT.Filter =
            "GAF Files (*.txt)|*.txt;|" +
            "All files (*.*)|*.*";
            nacteniStrandTXT.Title = "Open Text file";

            if (nacteniStrandTXT.ShowDialog() == DialogResult.OK)
            {
                foreach (String StrandTXTFileName in nacteniStrandTXT.FileNames)
                {
                    FileStream myStreamStrandTXT = new FileStream(@StrandTXTFileName, FileMode.Open, FileAccess.Read);
                    try
                    {
                        if (myStreamStrandTXT != null)
                        {
                            using (myStreamStrandTXT)
                            {
                                System.IO.StreamReader streamReaderStrandTXT = new System.IO.StreamReader(myStreamStrandTXT);
                                using (streamReaderStrandTXT)
                                {
                                    //Converting StrandTXT file to proccessable list
                                    string text = streamReaderStrandTXT.ReadToEnd();
                                    int LineCountInStrandTXT = File.ReadLines(StrandTXTFileName).Count();

                                    //Converting StrandTXT file to array of lines
                                    string[] LinesInStrandTXTFile = new string[LineCountInStrandTXT];
                                    LinesInStrandTXTFile = text.Split('\n');

                                    //Converting arrray of lines into arrays of text in lines (TAB divider)
                                    for (int i = 1; i < LineCountInStrandTXT; i++)
                                    {
                                        string[] Help2 = new string[30];
                                        Help2 = LinesInStrandTXTFile[i].Split('\t');
                                        if (Help2[0] == "gene" && Help2[4] == "chromosome")
                                        {
                                            bool pseudo = false;
                                            if (Help2[1].Contains("pseudogene"))
                                            { pseudo = true; }
                                            Strands.Add(new StrandList
                                            {
                                                ID = Help2[14].Replace(" ", ""),
                                                Strand = Help2[9].Replace("\r", ""),
                                                Chrom = Help2[5].Replace("\r", ""),
                                                Start = Convert.ToInt32(Help2[7]),
                                                End = Convert.ToInt32(Help2[8]),
                                                Name = Help2[13].Replace("\r", ""),
                                                Pseudo = pseudo
                                            });
                                        }
                                    }

                                    for (int i = 0; i < Strands.Count - 1; i++)
                                    {
                                        if (Strands[i].ID == Strands[i + 1].ID && Strands[i].Strand == Strands[i + 1].Strand)
                                        {
                                            Strands.RemoveAt(i);
                                        }
                                    }

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("strand.txt", true))
                                    {
                                        for (int counter1 = 0; counter1 < Strands.Count; counter1++)
                                        {
                                            file.WriteLine(Strands[counter1].ID + "\t" + Strands[counter1].Name + "\t" + Strands[counter1].Chrom + "\t" + Strands[counter1].Pseudo + "\t" + Strands[counter1].Strand + "\t" + Strands[counter1].Start + "\t" + Strands[counter1].End);
                                        }
                                    }
                                    richTextBox1.Text += "Loaded " + Strands.Count + " lines from StrandTXT file \r\n";
                                }
                            }
                        }
                    }
                    catch (System.IO.IOException) { }
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:aram.zolal@hotmail.com");
        }

    }
}
