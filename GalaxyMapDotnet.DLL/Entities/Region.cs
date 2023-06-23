using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyMapDotnet.DLL.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CounturPoints { get; set; }
        public string Colour { get; set; }
    }

    //  Insert into[GalaxyMap].[dbo].[Region]
    //  ([Name]
    //    ,[CounturPoints]
    //    ,[Colour]) VALUES(
    // 'Klingon Empire',
    //'|50,0|100,10|170,10|250,-10|250,-70|170,-90|80,-90|30,-40|',
    //'#ff0000'
    // )


//    UPDATE[GalaxyMap].[dbo].[Region]
//    SET[CounturPoints] = '|50,0|100,10|170,10|450,-10|450,-70|170,-90|80,-90|30,-40|'
//WHERE Id = 1;
}