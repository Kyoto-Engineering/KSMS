﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KyotoSalesManagementSystem.DBGateway
{
    
  public   class ConnectionGateway
  {
      protected SqlConnection connection;

      public ConnectionGateway()
      {
          string connectionString = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=NewProductList;User=sa;Password=SystemAdministrator;Persist Security Info=true"; 
          connection=new SqlConnection(connectionString);
      }
  }
}
