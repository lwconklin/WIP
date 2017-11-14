using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QT.DataAccess;

namespace TestParser {
    class Program {

        static void Main(string[] args) {

            try {

                StringBuilder sb = new StringBuilder();
                sb.Append("select i1srvend,i1y55vndnm, i1sdattn, i1y55adr1, i1y55adr2, i1y55cty, i1adds, i1addz");
                sb.Append(" from ");
                sb.Append("F5508091 ");
                sb.Append(" where abac21 not like '%ZZZ%' and i1srvend = 225508");
                DataSet ds = null; 
                ds = DataProvider.GetDataSet(sb.ToString(), "SQLDB", CommandType.Text);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\ParseAddress.csv", false)) {
                    file.WriteLine("Vendor#,BeforeName,AfterName,BefoerAddr1,AfterAddr1,BeforeAddr2,AfterAddr2,BeforeCity,AfterCity,State,Zip");
                    foreach (DataRow row in ds.Tables[0].Rows) {
                        file.WriteLine(
                            row["i1srvend"].ToString() + "," +
                            AddressParser.ParseAddress.ReplaceComma(row["i1y55vndnm"].ToString()) + "," +
                            AddressParser.ParseAddress.FormatProperCase(AddressParser.ParseAddress.ReplaceComma(row["i1y55vndnm"].ToString())) //+ "," +
                            //AddressParser.ParseAddress.ReplaceComma(row["i1y55adr1"].ToString()) + "," +
                            //AddressParser.ParseAddress.Parse(AddressParser.ParseAddress.ReplaceComma(row["i1y55adr1"].ToString())).Addr + "," +
                            //AddressParser.ParseAddress.ReplaceComma(row["i1y55adr2"].ToString()) + "," +
                            //AddressParser.ParseAddress.Parse(AddressParser.ParseAddress.ReplaceComma(row["i1y55adr2"].ToString())).Addr + "," +
                            //AddressParser.ParseAddress.ReplaceComma(row["i1y55cty"].ToString()) + "," +
                            //AddressParser.ParseAddress.FormatProperCase(AddressParser.ParseAddress.ReplaceComma(row["i1y55cty"].ToString()) )+ "," +
                            //row["i1adds"].ToString() + "," +
                            //row["i1addz"].ToString()
                            );
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

    } // end of class
} // end of namespace
//4705 SOUTH 129TH EAST AVENUE TULSA, OK 74134
//4705 south 129th east avenue tulsa, ok 74134
//4705 South 129Th East Avenue Tulsa, Ok 74134
//4705 South 129Th East Avenue Tulsa, Ok 74134.
//4705 South 129Th East Avenue Tulsa Ok 74134.
//PO BOX 3475 TULSA, OK 74101 
//po box 3475 tulsa, ok 74101 
//PO Box 3475 Tulsa, OK 74101 
//131 S Center St # 3094
//PO BOX 910
//P. O. Box 562 
//2 Main Street Suite 562
//City names
//Winston-Salem, NC
//Wilkes-Barre, Pa
//Fuquay-Varina, NC
//Sedro-Woolley, WA
//Coeur d'Alene, ID
//Dover-Foxcroft, ME
//O'Fallon, IL
//PO Box                 
//P O Box                 
//P. O. Box                 
//P.O.Box                 
//Post Box                 
//Post Office Box                 
//Post Office                 
//P.O.B                 
//P.O.B.                 
//POB                 
//Post Office Bin                 
//Box                 
//Bin                 
//Post                 
//Postal Code                 
//100   P O Box Des Moines                 
// P O Box DesMoines1000                 
// P O Box Des Moines 1000                 
// Post Office Box                 
// Post Office Box                   
//Post Box 

//AddressParser.ParseAddress.Parse(row["i1y55adr1"].ToString());
//AddressParser.ParseAddress.Parse("4705 SOUTH 129TH EAST AVENUE");
//AddressParser.ParseAddress.Parse("PO BOX 910");
//AddressParser.ParseAddress.Parse("P. O. Box 910");
//AddressParser.ParseAddress.Parse("P.O. Box 910");
//AddressParser.ParseAddress.Parse("2 Main Street Suite 562");