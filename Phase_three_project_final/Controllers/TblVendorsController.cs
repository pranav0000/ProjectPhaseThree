﻿using System;
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
    public class TblVendorsController : Controller
    {
        private readonly Project_3DBContext _context;

        public TblVendorsController(Project_3DBContext context)
        {
            _context = context;
        }

        // GET: TblVendors
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblVendors.ToListAsync());
        }

        // GET: TblVendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVendors = await _context.TblVendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            if (tblVendors == null)
            {
                return NotFound();
            }

            return View(tblVendors);
        }

        // GET: TblVendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblVendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,VendorUname,VendorPwd,VendorName,VendorLoc,VendorPhno")] TblVendors tblVendors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblVendors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblVendors);
        }

        // GET: TblVendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVendors = await _context.TblVendors.FindAsync(id);
            if (tblVendors == null)
            {
                return NotFound();
            }
            return View(tblVendors);
        }

        // POST: TblVendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorId,VendorUname,VendorPwd,VendorName,VendorLoc,VendorPhno")] TblVendors tblVendors)
        {
            if (id != tblVendors.VendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblVendors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblVendorsExists(tblVendors.VendorId))
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
            return View(tblVendors);
        }

        // GET: TblVendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblVendors = await _context.TblVendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            if (tblVendors == null)
            {
                return NotFound();
            }

            return View(tblVendors);
        }

        // POST: TblVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblVendors = await _context.TblVendors.FindAsync(id);
            _context.TblVendors.Remove(tblVendors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblVendorsExists(int id)
        {
            return _context.TblVendors.Any(e => e.VendorId == id);
        }




        // Vendor Login


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
        public IActionResult login(TblVendors vndr)
        {
            cn = ConnectToSqlServer();
            query = "select * from tbl_vendors where vendor_uname = @uname and vendor_pwd =@password";
            cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@uname", vndr.VendorUname);
            cmd.Parameters.AddWithValue("@password", vndr.VendorPwd);
            SqlDataReader dr = cmd.ExecuteReader();
            TblAdmin ad = new TblAdmin();
            if (dr.Read())
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                return RedirectToAction("index", "tbllaptops");
            }
            else
            {
                cmd.Dispose(); dr.DisposeAsync();
                cn.Close();
                ViewBag.msg = "Invalid Credentials";
                return View();
            }
        }
    }
}
