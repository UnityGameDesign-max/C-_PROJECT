using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DatabaseDetails;
using Newtonsoft.Json;

namespace c_application.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{
    SqlConnection connection = new SqlConnection($"server={ConfigDetails.server};database={ConfigDetails.databaseName};trusted_connection=true");
    
    [HttpGet(Name = "GETBrand")]
    [Route("getbrands")]
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
    [Route("insertbrands")]
    public string insertBrandAPI([FromBody]string brandName){
        try{
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Brands (BrandName) VALUES ('"+brandName+"')", connection);
            
            int numsOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return numsOfRowsAffected == 1 ? "Brand INSERTED!!" : "Query did not work";
        }catch(Exception error){
            return error.Message;
        }
    }

    [HttpPut(Name="UPDATEBrand")]
    [Route("updatebrand")]
    public string updateBrandAPI([FromBody]string brandName, int brandId){
        try{
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Brands SET BrandName= '"+brandName+"' WHERE BrandId= '"+brandId+"'", connection);
            int numsOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return numsOfRowsAffected == 1 ? "Brand UPDATED!!" : "Brand NOT UPDATED";
        }catch(Exception error){
            return error.Message;
        }
    }

    [HttpDelete(Name="DELETEBrand")]
     [Route("deletebrand")]
    public string deleteBrandAPI(int brandId){
        try{
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE Brands WHERE BrandId='"+brandId+"'", connection);
            int numsOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return numsOfRowsAffected == 1 ? "Brand DELETED!!" : "Query did not work";
        }catch(Exception error){
            return error.Message;
        }
    }
}
