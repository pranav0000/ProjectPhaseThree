using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Phase_three_project_final.Models;

namespace Phase_three_project_final.Controllers
{
    public class TblCustomersController : Controller
    {
        private readonly Project_3DBContext _context;

        static List<TblLaptops> laptop_cart_list = new List<TblLaptops>();
        

        public TblCustomersController(Project_3DBContext context)
        {
            _context = context;
        }


        // GET: TblCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblCustomers.ToListAsync());
        }



        // GET: TblLaptops
        public IActionResult Laptop_List()
        {
            return View( _context.TblLaptops);
        }

        // GET: TblCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCustomers = await _context.TblCustomers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (tblCustomers == null)
            {
                return NotFound();
            }

            return View(tblCustomers);
        }

        // GET: TblCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,CustomerUname,CustomerPwd,CustomerEmail")] TblCustomers tblCustomers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCustomers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCustomers);
        }

        // GET: TblCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCustomers = await _context.TblCustomers.FindAsync(id);
            if (tblCustomers == null)
            {
                return NotFound();
            }
            return View(tblCustomers);
        }

        // POST: TblCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,CustomerUname,CustomerPwd,CustomerEmail")] TblCustomers tblCustomers)
        {
            if (id != tblCustomers.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCustomers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCustomersExists(tblCustomers.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblCustomers);
        }

        // GET: TblCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCustomers = await _context.TblCustomers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (tblCustomers == null)
            {
                return NotFound();
            }

            return View(tblCustomers);
        }

        // POST: TblCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCustomers = await _context.TblCustomers.FindAsync(id);
            _context.TblCustomers.Remove(tblCustomers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCustomersExists(int id)
        {
            return _context.TblCustomers.Any(e => e.CustomerId == id);
        }



        // Login customer


        string cs = string.Empty; string query = string.Empty;
        SqlConnection cn = null; SqlCommand cmd = null;

        public SqlConnection ConnectToSqlServer()
        {
            cs = this.cs = "server=(local);integrated security=true;database=Project_3DB";
            cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(TblCustomers ctmr)
        {
            cn = ConnectToSqlServer();
            query = "select * from tbl_customers where customer_uname = @uname and customer_pwd =@password";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@uname", ctmr.CustomerUname);
            cmd.Parameters.AddWithValue("@password", ctmr.CustomerPwd);
            SqlDataReader dr = cmd.ExecuteReader();
            TblAdmin ad = new TblAdmin();
            if (dr.Read())
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                return RedirectToAction("Laptop_List","tblcustomers");
            }
            else
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                ViewBag.msg = "Invalid Credentials";
                return View();
            }
        }

        //Adding to cart feature
        public ActionResult order_cart(int id)
        {
            int total_amount = 0;
            TblLaptops new_lappy = _context.TblLaptops.Single(x => x.LaptopId == id);
            int index = new_lappy.LaptopId;
            laptop_cart_list.Add(new_lappy);
            
            // Total sum of laptops
            for (int j =0; j<laptop_cart_list.Count; j++)
            {
                total_amount += laptop_cart_list[j].LaptopPrice;
            }
            

            ViewBag.list = laptop_cart_list;
            ViewBag.price = total_amount;
            return View();
        }

         public IActionResult laptop_details(int id)
        {
            var laptop = _context.TblLaptops.Single(x => x.LaptopId == id);
            return View(laptop);
        }

        
        //Order Confirmation View
        public IActionResult order_confirm()
        {
            return View();
         }
    }
}
