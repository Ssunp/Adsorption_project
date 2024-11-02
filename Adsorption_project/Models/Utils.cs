using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adsorption_project.Models
{
    public class Utils
    {
        // Utility functions
        public static void PrintArr(double[] arr)
        {
            Debug.Print("Double Array: [" + string.Join(", ", arr) + "]");
        }
        public static void PrintArr2(string note, double[] arr)
        {
            Debug.Print(note + " : [" + string.Join(", ", arr) + "]");
        }

    }
}
