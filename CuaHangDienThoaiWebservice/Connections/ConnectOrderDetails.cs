using CuaHangDienThoaiWebservice.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectOrderDetails
    {
        // Thêm chi tiết hóa đơn
        public Response orderdetails(OrderDetails orderDetails, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_add_order_details", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iddh", orderDetails.iddh);
            command.Parameters.AddWithValue("@idsp", orderDetails.idsp);
            command.Parameters.AddWithValue("@sldamua", orderDetails.sldamua);
        // Mở kết nối
        connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Thêm chi tiết hóa đơn mới thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể thêm chi tiết hóa đơn";
            }
            return response;
        }
    }
}
