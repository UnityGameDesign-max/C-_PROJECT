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

        try{
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
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
    [Route("updatebrand/{brandId}")]
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
    [Route("deletebrand/{brandInt}")]
    public string deleteBrandAPI(int brandId){
        try{
            connection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Brands WHERE BrandId='"+brandId+"'", connection);
            int numsOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return numsOfRowsAffected == 1 ? "Brand DELETED!!" : "Brand NOT DELETED!!";
        }catch(Exception error){
            return error.Message;
        }
    }


    [HttpGet(Name="GetBrandOfName")]
    [Route("getbrandOfName/{brandName}")]
    public string CountBrandOfName(string brandName){
        
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT COUNT(BrandName) AS COUNT FROM Brands WHERE BrandName ='"+brandName+"'", connection);
        DataTable dataTable = new DataTable();
        try{
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            return JsonConvert.SerializeObject(dataTable);
        }catch(Exception error){
            return $"The error is {error.Message}";
        }
    }


    [HttpGet(Name="GetBrandNameToText")]
    [Route("getbrandOfNameToText/{brandName}")]
    public void BrandNameToTextFile(string brandName){
        
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Brands", connection);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        // StreamWriter file = new(@"C:\Users\bbdnet2642\Documents\c#application\brands.txt");
        string path=@"C:\Users\bbdnet2642\Documents\c#application\brands.txt";
       
        try{
            foreach (DataRow row in dataTable.Rows)
            {
                using(StreamWriter stream = new StreamWriter(path))
                {
                    stream.WriteLine(row["brandName"]);
                }
            }
        }catch(Exception error){
            Console.WriteLine($"The error is {error.Message}");
        }
    }
}
