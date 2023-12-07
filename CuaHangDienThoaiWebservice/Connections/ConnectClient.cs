using CuaHangDienThoaiWebservice.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CuaHangDienThoaiWebservice.Connections
{
    public class ConnectClient
    {
        // Hiển thị sản phẩm trong giỏ hàng
        public Response client(SqlConnection connection, int idkh)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_data_client", connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@idkh", idkh);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Client> arrayClient = new List<Client>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Client c = new Client();
                    c.idkh = Convert.ToInt32(dataTable.Rows[i]["idkh"]);
                    c.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    c.matkhau = Convert.ToString(dataTable.Rows[i]["matkhau"]);
                    c.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    c.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    c.ngaysinh = Convert.ToDateTime(dataTable.Rows[i]["ngaysinh"]);
                    c.gioitinh = Convert.ToInt32(dataTable.Rows[i]["gioitinh"]);
                    c.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    // Gán dữ liệu vào mảng
                    arrayClient.Add(c);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayClient.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Khách hàng cần lấy thông tin";
                response.arrayClient = arrayClient;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được khách hàng nào !";
                response.arrayClient = null;
            }
            return response;
        }

        // Lấy tất cả khách hàng
        public Response clientAll(SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand sql = new SqlCommand("sp_client_all", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(sql);

            // Sau khi lấy xong dữ liệu ta sẽ tạo ra 1 bảng dữ liệu mới
            DataTable dataTable = new DataTable();
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            // Khởi tạo mảng hứng dữ liệu
            List<Client> arrayClient = new List<Client>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Client client = new Client();
                    client.idkh = Convert.ToInt32(dataTable.Rows[i]["idkh"]);
                    client.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    client.matkhau = Convert.ToString(dataTable.Rows[i]["matkhau"]);
                    client.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    client.ngaysinh = Convert.ToDateTime(dataTable.Rows[i]["ngaysinh"]);
                    client.gioitinh = Convert.ToInt32(dataTable.Rows[i]["gioitinh"]);
                    client.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    client.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    // Gán dữ liệu vào mảng
                    arrayClient.Add(client);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayClient.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Danh sách tất cả khách hàng";
                response.arrayClient = arrayClient;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được khách hàng nào !";
                response.arrayClient = null;
            }
            return response;
        }

        // Xóa người dùng
        public Response deleteClient(SqlConnection connection, int idkh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_delete_client", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idkh", idkh);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Xóa người dùng thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Không thể xóa người dùng";
            }
            return response;
        }

        public Response login(Client clinet, SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("sp_login_client", connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@sdt", clinet.sdt);
            adapter.SelectCommand.Parameters.AddWithValue("@matkhau", clinet.matkhau);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Response response = new Response();
            List<Client> arrayClient = new List<Client>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Client client = new Client();
                    client.idkh = Convert.ToInt32(dt.Rows[i]["idkh"]);
                    client.sdt = Convert.ToString(dt.Rows[i]["sdt"]);
                    client.matkhau = Convert.ToString(dt.Rows[i]["matkhau"]);
                    client.hoten = Convert.ToString(dt.Rows[i]["hoten"]);
                    client.ngaysinh = Convert.ToDateTime(dt.Rows[i]["ngaysinh"]);
                    client.gioitinh = Convert.ToInt32(dt.Rows[i]["gioitinh"]);
                    client.diachi = Convert.ToString(dt.Rows[i]["diachi"]);
                    client.email = Convert.ToString(dt.Rows[i]["email"]);
                    // Gán dữ liệu vào mảng
                    arrayClient.Add(client);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayClient.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Đăng nhập thành công !";
                response.arrayClient = arrayClient;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Đăng nhập thất bại !";
                response.arrayClient = null;
            }
            return response;
        }
        public Response register(Client clinet, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register_client", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sdt", clinet.sdt);
            cmd.Parameters.AddWithValue("@matkhau", clinet.matkhau);
            cmd.Parameters.AddWithValue("@hoten", clinet.hoten);
            cmd.Parameters.AddWithValue("@ngaysinh", clinet.ngaysinh);
            cmd.Parameters.AddWithValue("@gioitinh", clinet.gioitinh);
            cmd.Parameters.AddWithValue("@diachi", clinet.diachi);
            cmd.Parameters.AddWithValue("@email", clinet.email);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Đăng ký thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Đăng ký thất bại";
            }
            return response;
        }
        public Response getClientId(SqlConnection connection, int idkh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_getClient_id", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idkh", idkh);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            // Mở kết nối
            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();
            List<Client> arrayClient = new List<Client>();
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Client client = new Client();
                    client.idkh = Convert.ToInt32(dataTable.Rows[i]["idkh"]);
                    client.sdt = Convert.ToString(dataTable.Rows[i]["sdt"]);
                    client.matkhau = Convert.ToString(dataTable.Rows[i]["matkhau"]);
                    client.hoten = Convert.ToString(dataTable.Rows[i]["hoten"]);
                    client.ngaysinh = Convert.ToDateTime(dataTable.Rows[i]["ngaysinh"]);
                    client.gioitinh = Convert.ToInt32(dataTable.Rows[i]["gioitinh"]);
                    client.diachi = Convert.ToString(dataTable.Rows[i]["diachi"]);
                    client.email = Convert.ToString(dataTable.Rows[i]["email"]);
                    // Gán dữ liệu vào mảng
                    arrayClient.Add(client);
                }
            }
            // Kiểm tra nếu mảng có dữ liệu
            if (arrayClient.Count > 0)
            {
                // Thông báo thành công
                response.StatusCode = 200;
                response.StatusMessage = "Lấy được khách hàng thành công";
                response.arrayClient = arrayClient;
            }
            else
            {
                // Thông báo thất bại
                response.StatusCode = 100;
                response.StatusMessage = "Không tìm được khách hàng nào !";
                response.arrayClient = null;
            }
            return response;
        }
        public Response updateClient(Client client, SqlConnection connection, int idkh)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_update_client", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idkh", idkh);
            command.Parameters.AddWithValue("@sdt", client.sdt);
            command.Parameters.AddWithValue("@matkhau", client.matkhau);
            command.Parameters.AddWithValue("@hoten", client.hoten);
            command.Parameters.AddWithValue("@ngaysinh", client.ngaysinh);
            command.Parameters.AddWithValue("@gioitinh", client.gioitinh);
            command.Parameters.AddWithValue("@diachi", client.diachi);
            command.Parameters.AddWithValue("@email", client.email);
            // Mở kết nối
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Cập nhật thành công";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Cập nhật thất bại";
            }
            return response;
        }
    }
}
