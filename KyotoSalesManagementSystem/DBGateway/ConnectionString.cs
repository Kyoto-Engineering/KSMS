using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyotoSalesManagementSystem.DBGateway
{
  public   class ConnectionString
    {
      public string DBConn = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=NewProductList;User=sa;Password=SystemAdministrator;Persist Security Info=true";
        // public string DBConn = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=NewProductList;User=sa;Password=SystemAdministrator;Persist Security Info=True;";
    }
}
