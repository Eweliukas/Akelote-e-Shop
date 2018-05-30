using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akelote_e_Shop.Areas.Admin.Services.DI {
    public interface ImportExportService {

        void Export<T>(List<T> list, string filename);

        void Import(string filename);

    }
}
