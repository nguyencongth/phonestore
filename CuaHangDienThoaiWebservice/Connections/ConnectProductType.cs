using CuaHangDienThoaiWebservice.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectProductType
    {
        // Lấy tất cả loại sản phẩm
        public Response productTpyeAll(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_productType_all", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<ProductType> arrayProductType = new List<ProductType>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ProductType type = new ProductType();
                    type.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    type.tenloai = Convert.ToString(dataTable.Rows[i]["tenloai"]);
                    // Gán dữ liệu vào mảng
                    arrayProductType.Add(type);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayProductType.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Danh sách tất cả loại sản phẩm";
                response.arrayProductType = arrayProductType;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được loại sản phẩm nào !";
                response.arrayProductType = null;
            }
            return response;
        }
        public Response getProductTypeId(SqlConnection connection, int idloai)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("type_typechitiet", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idloai", idloai);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            // Mở kết nối
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            List<ProductType> arrayProductType = new List<ProductType>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ProductType type = new ProductType();
                    type.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    type.tenloai = Convert.ToString(dataTable.Rows[i]["tenloai"]);
                    // Gán dữ liệu vào mảng
                    arrayProductType.Add(type);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayProductType.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Danh sách tất cả loại sản phẩm";
                response.arrayProductType = arrayProductType;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được loại sản phẩm nào !";
                response.arrayProductType = null;
            }
            return response;
        }

        public Response addType(ProductType productType, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_type", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@tenloai", productType.tenloai);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Thêm Loai mới thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể thêm ";
            }
            return response;
        }

        public Response updateType(ProductType productType, SqlConnection connection, int idloai)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("type_update", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idloai", idloai);
            command.Parameters.AddWithValue("@tenloai", productType.tenloai);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Cập nhật  thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể cập nhật ";
            }

            return response;
        }

        public Response deleteproductType(SqlConnection connection, int idLoai)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("type_delete", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idsp", idLoai);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Xóa thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể xóa ";
            }
            return response;
        }
    }
}
