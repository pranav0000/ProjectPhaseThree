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
    public class TblAdminsController : Controller
    {
        private readonly Project_3DBContext _context;

        public TblAdminsController(Project_3DBContext context)
        {
            _context = context;
        }



        // GET: TblAdmins
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblAdmin.ToListAsync());
        }

        // GET: TblAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAdmin = await _context.TblAdmin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (tblAdmin == null)
            {
                return NotFound();
            }

            return View(tblAdmin);
        }




        // GET: TblAdmins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,AdminUname,AdminPwd")] TblAdmin tblAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblAdmin);
        }




        // GET: TblAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAdmin = await _context.TblAdmin.FindAsync(id);
            if (tblAdmin == null)
            {
                return NotFound();
            }
            return View(tblAdmin);
        }

        // POST: TblAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,AdminUname,AdminPwd")] TblAdmin tblAdmin)
        {
            if (id != tblAdmin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblAdminExists(tblAdmin.AdminId))
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
            return View(tblAdmin);
        }





        // GET: TblAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblAdmin = await _context.TblAdmin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (tblAdmin == null)
            {
                return NotFound();
            }

            return View(tblAdmin);
        }

        // POST: TblAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblAdmin = await _context.TblAdmin.FindAsync(id);
            _context.TblAdmin.Remove(tblAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblAdminExists(int id)
        {
            return _context.TblAdmin.Any(e => e.AdminId == id);
        }



        //Admin Login


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
        public IActionResult login(TblAdmin admin)
        {
            cn = ConnectToSqlServer();
            query = "select * from tbl_admin where admin_uname = @uname and admin_pwd =@password";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@uname", admin.AdminUname);
            cmd.Parameters.AddWithValue("@password", admin.AdminPwd);
            SqlDataReader dr = cmd.ExecuteReader();
            TblAdmin ad = new TblAdmin();
            if (dr.Read())
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                return RedirectToAction("Home");
            }
            else
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                ViewBag.msg = "Invalid Credentials";
                return View();
            }
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
