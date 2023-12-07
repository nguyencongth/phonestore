using CuaHangDienThoaiWebservice.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectProduct
    {
        public Response productAll(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_product_all", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Product> arrayproducts = new List<Product>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product pro = new Product();
                    pro.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    pro.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    pro.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    pro.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    pro.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    pro.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    pro.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    // Gán dữ liệu vào mảng
                    arrayproducts.Add(pro);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayproducts.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Danh sách tất cả sản phẩm";
                response.arrayProduct = arrayproducts;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayProduct = null;
            }
            return response;
        }

        // Lấy ra 15 sản phẩm mới nhất
        public Response productnew(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_product_new", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Product> arrayproducts = new List<Product>();
            if (dataTable.Rows.Count > 0) 
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product pro = new Product();
                    pro.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    pro.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    pro.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    pro.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    pro.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    pro.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    pro.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    // Gán dữ liệu vào mảng
                    arrayproducts.Add(pro);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayproducts.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Top 15 sản phẩm mới nhất";
                response.arrayProduct = arrayproducts;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayProduct = null;
            }
            return response;
        }

        // Lấy ra 5 sản phẩm laptop mới nhất
        public Response laptopnew(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_laptop_new", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Product> arrayproducts = new List<Product>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product pro = new Product();
                    pro.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    pro.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    pro.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    pro.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    pro.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    pro.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    pro.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    // Gán dữ liệu vào mảng
                    arrayproducts.Add(pro);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayproducts.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Top 5 sản phẩm laptop mới nhất";
                response.arrayProduct = arrayproducts;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayProduct = null;
            }
            return response;
        }

        // Lấy ra toàn bộ sản phẩm là điện thoại
        public Response smartphone(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_smartphone", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Product> arrayproducts = new List<Product>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product pro = new Product();
                    pro.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    pro.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    pro.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    pro.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    pro.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    pro.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    pro.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    // Gán dữ liệu vào mảng
                    arrayproducts.Add(pro);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayproducts.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Toàn bộ sản phẩm điện thoại";
                response.arrayProduct = arrayproducts;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayProduct = null;
            }
            return response;
        }

        // Lấy ra toàn bộ sản phẩm là laptop
        public Response laptop(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_laptop", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Product> arrayproducts = new List<Product>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product pro = new Product();
                    pro.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    pro.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    pro.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    pro.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    pro.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    pro.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    pro.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    // Gán dữ liệu vào mảng
                    arrayproducts.Add(pro);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayproducts.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Toàn bộ sản phẩm laptop";
                response.arrayProduct = arrayproducts;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayProduct = null;
            }
            return response;
        }

        public Response getProductId(SqlConnection connection, int idsp)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_spchitiet", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idsp", idsp);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            // Mở kết nối
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            List<Product> arrayproducts = new List<Product>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Product pro = new Product();
                    pro.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    pro.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    pro.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    pro.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    pro.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    pro.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    pro.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    // Gán dữ liệu vào mảng
                    arrayproducts.Add(pro);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayproducts.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Toàn bộ sản phẩm laptop";
                response.arrayProduct = arrayproducts;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayProduct = null;
            }
            return response;
        }

        // Thêm sản phẩm
        public Response addproduct(Product product, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_add_product", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idloai", product.idloai);
            command.Parameters.AddWithValue("@anhsp", product.anhsp);
            command.Parameters.AddWithValue("@tensp", product.tensp);
            command.Parameters.AddWithValue("@giasp", product.giasp);
            command.Parameters.AddWithValue("@thongtinsp", product.thongtinsp);
            command.Parameters.AddWithValue("@slsanpham", product.slsanpham);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Thêm sản phẩm mới thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể thêm sản phẩm";
            }
            return response;
        }

        // Sửa sản phẩm
        public Response updateproduct(Product product, SqlConnection connection, int idsp)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_update_product", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idsp", idsp);
            command.Parameters.AddWithValue("@idloai", product.idloai);
            command.Parameters.AddWithValue("@anhsp", product.anhsp);
            command.Parameters.AddWithValue("@tensp", product.tensp);
            command.Parameters.AddWithValue("@giasp", product.giasp);
            command.Parameters.AddWithValue("@thongtinsp", product.thongtinsp);
            command.Parameters.AddWithValue("@slsanpham", product.slsanpham);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Cập nhật sản phẩm thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể cập nhật sản phẩm";
            }
            return response;
        }

        // Xóa sản phẩm
        public Response deleteproduct(SqlConnection connection, int idsp)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_delete_product", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idsp", idsp);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Xóa sản phẩm thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể xóa sản phẩm";
            }
            return response;
        }
    }
}
