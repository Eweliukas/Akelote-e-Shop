using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Akelote_e_Shop.Areas.Admin.Services.DI;
using Akelote_e_Shop.Models;

namespace Akelote_e_Shop.Areas.Admin.Services {
    public class ExcelImportExportService : ImportExportService {

        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void Export<T>(List<T> list, string filename) {
            var grid = new System.Web.UI.WebControls.GridView {DataSource = list};
            grid.DataBind();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename="+ filename + ".xls");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grid.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }

        public void Import(string filename) {
            throw new NotImplementedException();
        }
    }
}