using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_CSV.Models;
using System.IO;
using System.Configuration;
using System.Windows;
using System.Threading.Tasks;

namespace Project_CSV.Controllers
{
    public class PathL : Controller
    {
        public string MapPath { get; set; }
       
        public PathL() {
            this.MapPath = String.Format($"{HttpRuntime.AppDomainAppPath}App_Data\\uploads\\TestReader.csv").Replace(@"\\",@"\");           
        }
     
    }
          
    public class Matrix
    {
        const double FixedRateE = 0.095;      
        private static double EURIBOR {get; } = -0.0024;       
        private static double InterestRate { get; set; }
        private static double Principal { get; set; }
        public  static double CalculateInterestRate(IBankingModel bm)
        {         
                switch (bm.IsVariable)
                {
                    case true:
                        InterestRate = (EURIBOR + FixedRateE) * bm.LoanAmount;
                        break;
                    case false:
                        InterestRate = bm.InterestPercent * bm.LoanAmount;
                        break;
                }
        
            return InterestRate;
        }

        public static double CalculatePrincipal(IBankingModel bm)
        {          
           Principal = bm.LoanAmount - InterestRate;         
           return Principal;
        } 

    }

    class IO:Controller
    {
        public static string[] reader = Array.Empty<string>();
        public static string[] item = Array.Empty<string>();
        public static List<BankingModel> lst;
        public static PathL _path;

        public static List<BankingModel> Read()
        {
            _path = new PathL();          
            lst = new List<BankingModel>();
            reader = System.IO.File.ReadAllLines(_path.MapPath);

            for (var i = 0; i < reader.Length; i++)
            {
                item = reader[i].ToString().Split(',');

                long PIN;
                bool sPIN = long.TryParse(item[0], out PIN);
                int LoanAmount;
                bool loanAmount = Int32.TryParse(item[3],out LoanAmount);
                float Interestpercent;
                bool interestPercent = float.TryParse(item[4],out Interestpercent);

                    try
                    {                    
                        lst.Add(new BankingModel
                        {                      
                            PIN = PIN,
                            Surname = item[1],
                            Name = item[2],
                            LoanAmount = LoanAmount,
                            InterestPercent = Interestpercent,
                            IsVariable = bool.Parse(item[5]),
                            InterestRate = double.Parse(item[6]),
                            Principal = double.Parse(item[7])
                            
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error " + ex);
                    }
                              
            }
            return lst;
        }

    }

    public class MainController : Controller
    {
        PathL _path = new PathL();

        [HttpGet]
        public ActionResult Graph()
        {
            var list = new List<BankingModel>(); ;
            if (System.IO.File.Exists($"{_path.MapPath}"))
            {
                list = IO.Read();
            }
            return Json(list.ToArray(),JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Insert(BankingModel bm)
        {
            using (StreamWriter a = new StreamWriter(_path.MapPath, true))
            {

                a.WriteLine($"{bm.PIN},{bm.Name},{bm.Surname},{bm.LoanAmount}," +
                    $"{bm.InterestPercent},{bm.IsVariable},{Matrix.CalculateInterestRate(bm).ToString("F")},{Matrix.CalculatePrincipal(bm).ToString("F")}");

            }
            return RedirectToAction("Main");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("Main");
        }

        [HttpGet]
        public ActionResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes($"{_path.MapPath}");
            string fileName = "TestReader.csv";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);         
        }

        public ActionResult Main()
        {
            ViewData.Clear();
            if (System.IO.File.Exists($"{_path.MapPath}"))
            {
                ViewBag.List = IO.Read();
            }
            
            return View("Main");
        }

    }
}