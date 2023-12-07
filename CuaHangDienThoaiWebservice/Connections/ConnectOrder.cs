using CuaHangDienThoaiWebservice.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectOrder
    {
        // Thêm hóa đơn
        public Response order(Order order, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_add_order", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idkh", order.idkh);
            command.Parameters.AddWithValue("@idql", order.idql);
            command.Parameters.AddWithValue("@hoten", order.hoten);
            command.Parameters.AddWithValue("@diachi", order.diachi);
            command.Parameters.AddWithValue("@sdt", order.sdt);
            command.Parameters.AddWithValue("@email", order.email);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            // Lấy ra iddh mới nhất
            SqlCommand sql = new SqlCommand("sp_iddh", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();
            int iddh = 0;
            if (dataTable.Rows.Count > 0)
            {   
                iddh = Convert.ToInt32(dataTable.Rows[0]["iddh"]);
                response.iddh = iddh;
            }
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Thêm hóa đơn mới thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể thêm hóa đơn";
            }

            return response;
        }

        // lấy iddh mới nhất
        public Response orderiddh(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_iddh", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            int iddh = 0;
            if (dataTable.Rows.Count > 0)
            {
                iddh = Convert.ToInt32(dataTable.Rows[0]["iddh"]);
                response.iddh = iddh;
            }
            return response;
        }

        // Lấy tất cả sản phẩm
        public Response orderAll(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_order_all", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Order> arrayOrder = new List<Order>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Order order = new Order();
                    order.iddh = Convert.ToInt32(dataTable.Rows[i]["iddh"]);
                    order.idkh = Convert.ToInt32(dataTable.Rows[i]["idkh"]);
                    order.idql = Convert.ToInt32(dataTable.Rows[i]["idql"]);
                    order.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    order.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    order.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    order.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    order.ngaylap = Convert.ToDateTime(dataTable.Rows[i]["ngaylap"]);
                    // Gán dữ liệu vào mảng
                    arrayOrder.Add(order);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayOrder.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Danh sách tất cả đơn hàng";
                response.arrayOrder = arrayOrder;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được đơn hàng nào !";
                response.arrayOrder = null;
            }
            return response;
        }


        public Response getOderByIdkh(SqlConnection connection, int idkh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_hoadon", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idkh", idkh);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            // Mở kết nối
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            List<OrderDetailClient> arrayOrder = new List<OrderDetailClient>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    OrderDetailClient orderClient = new OrderDetailClient();
                    orderClient.iddh = Convert.ToInt32(dataTable.Rows[i]["iddh"]);
                    orderClient.idkh = Convert.ToInt32(dataTable.Rows[i]["idkh"]);
                    orderClient.idql = Convert.ToInt32(dataTable.Rows[i]["idql"]);
                    orderClient.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    orderClient.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    orderClient.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    orderClient.sldamua = Convert.ToInt32(dataTable.Rows[i]["sldamua"]);
                    orderClient.anhsp = Convert.ToString(dataTable.Rows[i]["anhsp"]);
                    orderClient.tensp = Convert.ToString(dataTable.Rows[i]["tensp"]);
                    orderClient.giasp = Convert.ToDouble(dataTable.Rows[i]["giasp"]);
                    orderClient.thongtinsp = Convert.ToString(dataTable.Rows[i]["thongtinsp"]);
                    orderClient.slsanpham = Convert.ToInt32(dataTable.Rows[i]["slsanpham"]);
                    orderClient.ngaylap = Convert.ToDateTime(dataTable.Rows[i]["ngaylap"]);
                    // Gán dữ liệu vào mảng
                    arrayOrder.Add(orderClient);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayOrder.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Lấy được đơn hàng khách hàng thành công";
                response.arrayOderClient = arrayOrder;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được đơn hàng khách hàng nào !";
                response.arrayOderClient = null;
            }
            return response;
        }

    }
}
