using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

    public class sy_model
    {
        public string stud_id { get; set; }
        public string stud_grno { get; set; }
        public string stud_PermanentAdd { get; set; }
        public string stud_PermanentPhone { get; set; }
        public string stud_NativePhone { get; set; }
        public string stud_Email { get; set; }
        public string stud_Gender { get; set; }
        public string stud_MartialStatus { get; set; }
        public string If_PHYSICALALLY_RESERVED { get; set; }
        public string stud_Father_Occupation { get; set; }
        public string stud_Father_TelNo { get; set; }
        public string stud_Father_BusinessServiceAdd { get; set; }
        public string stud_Mother_Occupation { get; set; }
        public string stud_Mother_TelNo { get; set; }
        public string stud_Mother_BusinessServiceAdd { get; set; }
        public string stud_NoOfFamilyMembers { get; set; }
        public string stud_Earning { get; set; }
        public string stud_NonEarning { get; set; }
        public string stud_YearlyIncome { get; set; }
        public string propose_scholarship { get; set; }
        public string member_of_ncc { get; set; }
        public string extra_activity { get; set; }
        public string ayid { get; set; }
        public string group_id { get; set; }
        public string is_new_Stud { get; set; }
        public string stud_BloodGroup { get; set; }

        public DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }
    }

