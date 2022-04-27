using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DatabaseDetails;
using QueriesSQLSERVER;
using Newtonsoft.Json;

namespace c_application.Controllers;

[ApiController]
[Route("[controller]")]
public class StoreController : ControllerBase
{
    SqlConnection connection = new SqlConnection($"server={ConfigDetails.server};database={ConfigDetails.databaseName};trusted_connection=true");
    
    [HttpGet(Name = "GETBrand")]
    public string GetBrandAPI()
    {
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Brands", connection);
        DataTable dataTable = new DataTable();

        adapter.Fill(dataTable);
        try{
            return JsonConvert.SerializeObject(dataTable);
        }catch(Exception error){
            return $"The error is {error.Message}";
        }
    }
    [HttpPost(Name="POSTBrand")]
    public string insertBrandAPI([FromBody]string brandName){
        try{
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Brands (BrandName) VALUES ('"+brandName+"')", connection);
            connection.Close();
            return "Data inserted";
        }catch(Exception error){
            return error.Message;
        }
    }

    [HttpDelete(Name="DELETEBrand")]
    public string deleteBrandAPI([FromBody]int brandId){
        try{
            connection.Open();
            SqlCommand updateCommand = new SqlCommand("UPDATE")
        }
    }
}
