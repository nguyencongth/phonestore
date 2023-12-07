using CuaHangDienThoaiWebservice.Models;
using System.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectCart
    {
        // Hiển thị sản phẩm trong giỏ hàng
        public Response cart(SqlConnection connection, int idkh)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_cart", connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@idkh", idkh);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Cart> arrayCart = new List<Cart>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Cart c = new Cart();
                    c.idgh = Convert.ToInt32(dataTable.Rows[i]["idgh"]);
                    c.idkh = Convert.ToInt32(dataTable.Rows[i]["idkh"]);
                    c.idsp = Convert.ToInt32(dataTable.Rows[i]["idsp"]);
                    c.idloai = Convert.ToInt32(dataTable.Rows[i]["idloai"]);
                    c.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    c.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    c.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    c.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    c.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    c.slchon = Convert.ToInt32(dataTable.Rows[i]["slchon"]);
                    // Gán dữ liệu vào mảng
                    arrayCart.Add(c);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayCart.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Tất cả sản phẩm có trong giỏ hàng";
                response.arrayCart = arrayCart;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được sản phẩm nào !";
                response.arrayCart = null;
            }
            return response;
        }

        // Tăng số lượng sản phẩm chọn
        public Response increasecart(Cart cart, SqlConnection connection, int idgh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_increase_cart", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgh", idgh);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Tăng số lượng chọn thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Thất Bại";
            }
            return response;
        }

        // Giảm số lượng sản phẩm chọn
        public Response reducecart(Cart cart, SqlConnection connection, int idgh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_reduce_cart", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgh", idgh);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Giảm số lượng chọn thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Thất Bại";
            }
            return response;
        }

        // Xóa sản phẩm có trong giỏ hàng
        public Response deletecart(SqlConnection connection, int idgh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_delete_cart", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgh", idgh);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Chúc mừng, bạn đã xóa sản phẩm thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Thất Bại";
            }
            return response;
        }

        // Thêm Giỏ Hàng
        public Response addCart(Cart cart, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("post_cart", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idkh", cart.idkh);
            command.Parameters.AddWithValue("@idsp", cart.idsp);
            command.Parameters.AddWithValue("@slchon", cart.slchon);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Thêm sản phẩm vào giỏ hàng thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể thêm sản phẩm  vào giỏ hàng";
            }
            return response;
        }
    }
}
